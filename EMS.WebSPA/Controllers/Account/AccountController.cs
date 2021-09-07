using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using EMS.Domain.AggregatesModel.UserProfileAggregate;
using EMS.WebSPA.Services;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace EMS.WebSPA.Controllers.Account{
    /// <inheritdoc />
    /// <summary>
    ///     This sample controller implements a typical login/logout/provision workflow for local and external accounts.
    ///     The login service encapsulates the interactions with the user data store. This data store is in-memory only and
    ///     cannot be used for production!
    ///     The interaction service provides a way for the UI to communicate with identityserver for validation and context
    ///     retrieval
    /// </summary>
    [SecurityHeaders]
    public class AccountController : Controller{
        private readonly AccountService _account;
        private readonly IEventService _events;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly ILogger<AccountController> _logger;
        private readonly ILoginService<UserProfile> _loginService;
        private readonly IMediator _mediator;
        private readonly UserManager<UserProfile> _userManager;
        public AccountController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IHttpContextAccessor httpContextAccessor,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events, IMediator mediator, ILoginService<UserProfile> loginService,
            UserManager<UserProfile> userManager, ILogger<AccountController> logger){
            // if the TestUserStore is not in DI, then we'll just use the global users collection
            _interaction = interaction;
            _events = events;
            _mediator = mediator;
            _loginService = loginService;
            _userManager = userManager;
            _logger = logger;
            _account = new AccountService(interaction, httpContextAccessor, schemeProvider, clientStore);
        }
        /// <summary>
        ///     Show login page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl){
            // build a model so we know what to show on the login page
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null){
                // if IdP is passed, then bypass showing the login screen
                // return _account.ExternalLogin(context.IdP, returnUrl);
            }
            
            var vm = await _account.BuildLoginViewModelAsync(returnUrl);
            ViewData["ReturnUrl"] = returnUrl;
            return View(vm);
        }
        /// <summary>
        ///     Handle postback from username/password login
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string button){
            //if (button != "login")
            //{
            //    // the user clicked the "cancel" button
            //    var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
            //    if (context != null)
            //    {
            //        // if the user cancels, send a result back into IdentityServer as if they 
            //        // denied the consent (even if this client does not require consent).
            //        // this will send back an access denied OIDC error response to the client.
            //        await _interaction.GrantConsentAsync(context, ConsentResponse.Denied);

            //        // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
            //        return Redirect(model.ReturnUrl);
            //    }
            //    else
            //    {
            //        // since we don't have a valid context, then we just go back to the home page
            //        return Redirect("~/");
            //    }
            //}
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
            if (ModelState.IsValid){
                var user = await _loginService.FindByUsername(model.Username);
                if (user != null){
                    if (!await _userManager.IsLockedOutAsync(user)){
                        if (await _loginService.ValidateCredentials(user, model.Password)){
                            var props = new AuthenticationProperties
                            {
                                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(5),
                                AllowRefresh = true,
                                RedirectUri = model.ReturnUrl
                            };
                            if (model.RememberLogin)
                                props = new AuthenticationProperties
                                {
                                    IsPersistent = true,
                                    ExpiresUtc = DateTimeOffset.UtcNow.AddYears(10)
                                };
                            ;
                            await _loginService.SignInAsync(user, props);

                            // make sure the returnUrl is still valid, and if yes - redirect back to authorize endpoint
                            if (_interaction.IsValidReturnUrl(model.ReturnUrl) || Url.IsLocalUrl(model.ReturnUrl))
                                return Redirect(model.ReturnUrl);
                            return Redirect("~/");
                        }

                        ModelState.AddModelError("", "نام کاربری یا پسورد شما اشتباه است");
                    }
                    else{ ModelState.AddModelError("", "کاربری شما غیر فعال است"); }
                }
                ModelState.AddModelError("", "نام کاربری یا پسورد شما اشتباه است");
            }

            // something went wrong, show form with error
            var vm = await _account.BuildLoginViewModelAsync(model);
            ViewData["ReturnUrl"] = model.ReturnUrl;
            return View(vm);
        }
        /// <summary>
        ///     initiate roundtrip to external authentication provider
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ExternalLogin(string provider, string returnUrl){
            returnUrl = Url.Action("ExternalLoginCallback", new {returnUrl});

            // windows authentication needs special handling
            // since they don't support the redirect uri, 
            // so this URL is re-triggered when we call challenge
            if (AccountOptions.WindowsAuthenticationSchemeName == provider){
                // see if windows auth has already been requested and succeeded
                var result = await HttpContext.AuthenticateAsync(AccountOptions.WindowsAuthenticationSchemeName);
                if (result?.Principal is WindowsPrincipal wp){
                    var props = new AuthenticationProperties();
                    props.Items.Add("scheme", AccountOptions.WindowsAuthenticationSchemeName);
                    var id = new ClaimsIdentity(provider);
                    id.AddClaim(new Claim(JwtClaimTypes.Subject, wp.Identity.Name));
                    id.AddClaim(new Claim(JwtClaimTypes.Name, wp.Identity.Name));

                    // add the groups as claims -- be careful if the number of groups is too large
                    if (AccountOptions.IncludeWindowsGroups){
                        var wi = wp.Identity as WindowsIdentity;
                        var groups = wi?.Groups?.Translate(typeof(NTAccount));
                        var roles = (groups ?? throw new InvalidOperationException()).Select(x =>
                            new Claim(JwtClaimTypes.Role, x.Value));
                        id.AddClaims(roles);
                    }

                    await HttpContext.SignInAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme,
                        new ClaimsPrincipal(id), props);
                    return Redirect(returnUrl);
                }

                // challenge/trigger windows auth
                return new ChallengeResult(AccountOptions.WindowsAuthenticationSchemeName);
            }

            {
                // start challenge and roundtrip the return URL
                var props = new AuthenticationProperties
                {
                    RedirectUri = returnUrl,
                    Items = {{"scheme", provider}}
                };
                return new ChallengeResult(provider, props);
            }
        }
        /// <summary>
        /// Post processing of external authentication
        /// </summary>
        //[HttpGet]
        //public async Task<IActionResult> ExternalLoginCallback(string returnUrl)
        //{
        //    // read external identity from the temporary cookie
        //    var result = await HttpContext.AuthenticateAsync(IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme);
        //    if (result?.Succeeded != true)
        //    {
        //        throw new Exception("External authentication error");
        //    }

        //    // retrieve claims of the external user
        //    var externalUser = result.Principal;
        //    var claims = externalUser.Claims.ToList();

        //    // try to determine the unique id of the external user (issued by the provider)
        //    // the most common claim type for that are the sub claim and the NameIdentifier
        //    // depending on the external provider, some other claim type might be used
        //    var userIdClaim = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject) ?? claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

        //    if (userIdClaim == null)
        //    {
        //        throw new Exception("Unknown userid");
        //    }

        //    // remove the user id claim from the claims collection and move to the userId property
        //    // also set the name of the external authentication provider
        //    claims.Remove(userIdClaim);
        //    var provider = result.Properties.Items["scheme"];
        //    var userId = userIdClaim.Value;

        //    // this is where custom logic would most likely be needed to match your users from the
        //    // external provider's authentication result, and provision the user as you see fit.
        //    // 
        //    // check if the external user is already provisioned
        //   var user = _users.FindByExternalProvider(provider, userId) ?? _users.AutoProvisionUser(provider, userId, claims);

        //    var additionalClaims = new List<Claim>();

        //    // if the external system sent a session id claim, copy it over
        //    // so we can use it for single sign-out
        //    var sid = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.SessionId);
        //    if (sid != null)
        //    {
        //        additionalClaims.Add(new Claim(JwtClaimTypes.SessionId, sid.Value));
        //    }

        //    // if the external provider issued an id_token, we'll keep it for signout
        //    AuthenticationProperties props = null;
        //    var idToken = result.Properties.GetTokenValue("id_token");
        //    if (idToken != null)
        //    {
        //        props = new AuthenticationProperties();
        //        props.StoreTokens(new[] { new AuthenticationToken { Name = "id_token", Value = idToken } });
        //    }

        //    // issue authentication cookie for user
        //    await _events.RaiseAsync(new UserLoginSuccessEvent(provider, userId, user.SubjectId, user.Username));
        //    await HttpContext.SignInAsync(user.SubjectId, user.Username, provider, props, additionalClaims.ToArray());

        //    // delete temporary cookie used during external authentication
        //    await HttpContext.SignOutAsync(IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme);

        //    // validate return URL and redirect back to authorization endpoint or a local page
        //    if (_interaction.IsValidReturnUrl(returnUrl) || Url.IsLocalUrl(returnUrl))
        //    {
        //        return Redirect(returnUrl);
        //    }

        //    return Redirect("~/");
        //}
        /// <summary>
        ///     Show logout page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId){
            if (User.Identity.IsAuthenticated == false) return await Logout(new LogoutViewModel {LogoutId = logoutId});

            //Test for Xamarin. 
            var context = await _interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false) return await Logout(new LogoutViewModel {LogoutId = logoutId});

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.
            var vm = new LogoutViewModel
            {
                LogoutId = logoutId
            };
            return View(vm);
        }
        /// <summary>
        ///     Handle logout page postback
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutViewModel model){
            var idp = User?.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
            if (idp != null && idp != IdentityServerConstants.LocalIdentityProvider){
                if (model.LogoutId == null) model.LogoutId = await _interaction.CreateLogoutContextAsync();
                var url = "/Account/Logout?logoutId=" + model.LogoutId;
                try{
                    // hack: try/catch to handle social providers that throw
                    await HttpContext.SignOutAsync(idp, new AuthenticationProperties
                    {
                        RedirectUri = url
                    });
                }
                catch (Exception ex){ _logger.LogCritical(ex.Message); }
            }

            // delete authentication cookie
            await HttpContext.SignOutAsync();
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            // set this so UI rendering sees an anonymous user
            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await _interaction.GetLogoutContextAsync(model.LogoutId);
            return Redirect(logout?.PostLogoutRedirectUri);
        }

        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null){
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //[Route("createClient")]
        //[HttpPost]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //[AllowAnonymous]
        //public async Task<IActionResult> CreateClient([FromBody] RegisterClientCommand registerAgentCommand,
        //    [FromHeader(Name = "x-requestid")] string requestId)
        //{
        //    var commandResult = false;
        //    if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
        //    {
        //        var createAgentRequest = new IdentifiedCommand<RegisterClientCommand, bool>(registerAgentCommand, guid);
        //        commandResult = await _mediator.Send(createAgentRequest);
        //    }

        //    return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();
        //}
        //
        // POST: /Account/Register
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        //{
        //    ViewData["ReturnUrl"] = returnUrl;
        //    if (ModelState.IsValid)
        //    {
        //        var user = new ApplicationUser
        //        {
        //            UserName = model.Email,
        //            Email = model.Email,
        //            LastName = model.User.LastName,
        //            Name = model.User.Name,
        //            PhoneNumber = model.User.PhoneNumber,
        //        };
        //        var result = await _userManager.CreateAsync(user, model.Password);
        //        if (result.Errors.Count() > 0)
        //        {
        //            AddErrors(result);
        //            // If we got this far, something failed, redisplay form
        //            return View(model);
        //        }
        //        else if (result.Succeeded)
        //        {
        //            return await Login(new LoginViewModel
        //            {
        //                Email = model.Email,
        //                Password = model.Password,
        //                ReturnUrl = returnUrl
        //            });
        //        }
        //    }

        //    if (returnUrl != null)
        //    {
        //        if (HttpContext.User.Identity.IsAuthenticated)
        //            return Redirect(returnUrl);
        //        else
        //            if (ModelState.IsValid)
        //            return RedirectToAction("login", "account", new { returnUrl = returnUrl });
        //        else
        //            return View(model);
        //    }

        //    return RedirectToAction("index", "home");
        //}
    }
}