using System.ComponentModel.DataAnnotations;

namespace KorsatkoApp.Areas.Admin.ViewModels
{
    public class EnrollmentEditViewModel
    {
        public EnrollmentEditViewModel() { }

        public EnrollmentEditViewModel(string? paymentStatus)
        {
            PaymentStatus = paymentStatus;
        }

        [Display(Name = "حالة الدفع")]
        public string? PaymentStatus { get; set; }
    }
}
