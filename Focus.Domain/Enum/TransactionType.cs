
using System.ComponentModel.DataAnnotations;


namespace Focus.Domain.Enum
{
    public enum TransactionType
    {
       
        [Display(Name = "Cash")]
        Cash = 1,
        [Display(Name = "Credit")]
        Credit = 2,
        
    }
}
