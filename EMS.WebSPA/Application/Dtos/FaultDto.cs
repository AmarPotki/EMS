
﻿using System;
namespace EMS.WebSPA.Application.Dtos{
    public class FaultDto{
        public int Id { get; set; }
        public string Title { get; set; }
        public string FaultType { get; set; }
        public string Location { get; set; }
        public string ItemType { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset AssingTime { get; set; }
        public string AssignUser { get; set; }
        public string FaultStatus { get; set; }
    }
}