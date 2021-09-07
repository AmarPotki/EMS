using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.WebSPA.Application.Dtos
{
    public class FixUnitDto
    {
        public string ItemType { get; private set; }
        public int ItemTypeId { get; private set; }
        public string Owner { get; private set; }
        public string OwnerName { get;  set; }
        public string Location { get; private set; }
        public int LocationId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int Id { get; set; }
        public List<MemberDto> MemberDtos { get; set; }
        //private readonly List<Member> _members;
    }
}
