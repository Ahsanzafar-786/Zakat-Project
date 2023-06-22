using Focus.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Noble.Api.Models
{
    public class LoginVm
    {
        public string Id { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? RoleId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public Guid UserId { get; set; }
        public string LastName { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        
        public bool IsProceed { get; set; }
        public bool IsActive { get; set; }
        public bool IsStatus { get; set; }
        public string Remarks { get; set; }


    }
}
