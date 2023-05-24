using System;
using System.Collections.Generic;

namespace Noble.Api.Models
{
    public class LoginModel
    {
        public string Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid? EmployeeId { get; set; }
       
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public string ImagePath { get; set; }
        public string PhoneNo { get; set; }
        public object Token { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool EmailConfirmed { get; set; }
      
        public string LoginUserName { get; set; }
       
    }
}
