import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { CreateUserDto, UserDto } from '../usermanager.model';
import { UsermanagerService } from '../usermanager.service';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';

@Component({
  selector: 'app-new',
  templateUrl: './new.component.html',
  styleUrls: ['./new.component.scss'],
  animations   : fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class NewComponent implements OnInit {

  user:CreateUserDto = null;
  userdata:UserDto;
  pageType:string;
  //userForm: FormGroup;
  constructor(
    private userService: UsermanagerService,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.user = new CreateUserDto();
    this.route.params.subscribe(params => {
      let userId = params['id'];
      if(params['id']){
        this.pageType = 'edit'
        this.userService.getUserforEdit(userId).subscribe(user=>{
          this.user = user;
        })
      }else{
        this.pageType = 'new';
      }      
    })
  }

  saveUser():void{
    this.userService.newUser(this.user).subscribe(result=>{
      console.log(result);
      
    })
  }

  //this._location.go('apps/todo/all/' + id);

}
