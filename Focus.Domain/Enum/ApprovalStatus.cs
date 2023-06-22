using System.ComponentModel.DataAnnotations;

namespace Focus.Domain.Enum
{
  public  enum ApprovalStatus
    {
        [Display(Name = "Pending")]
        Pending  = 1,
        [Display(Name = "Rejected")]
        Rejected  = 2,
        [Display(Name = "Approved")]
        Approved = 3,
        [Display(Name = "Draft")]
        Draft = 4,
       
    }
}
