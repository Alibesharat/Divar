using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace divar.DAL.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }


        public string PostTitle { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public ExpertOption ExpertOption { get; set; }

        public DateTime BookTime { get; set; }

        public string PostToken {get;set;}

        public ReviewStatus ReviewStatus {get;set;}

        // نظر نهایی کارشناس
        public string ExpertReviewResult {get;set;}


        public string TrackingCode {get;set;}


        [ForeignKey(nameof(Expert))]
        public int ExpertId { get; set; }
        public Expert  Expert {get;set;}

    }


    public enum ExpertOption : int
    {
        
        [Display(Name = "پایه")]
        basic = 1,
        [Display(Name = "استاندارد")]
        standard = 2,
        [Display(Name = "ویژه")]
        vip = 3
    }


   public enum ReviewStatus: int
    {
        [Display(Name = "درخواست مشتری")]
        InquiryReceived = 1,    // Customer inquiry is received
        [Display(Name = "تخصیص کارشناس")]
        ExpertAssigned = 2,     // An expert has been assigned to the inquiry
        
        [Display(Name = "در حال کارشناسی")]
        ReviewInProgress = 3,   // The expert is currently reviewing the printer
        [Display(Name = "پایان کارشناسی")]
        ReviewCompleted = 4,     // The expert has completed the review
    }

}