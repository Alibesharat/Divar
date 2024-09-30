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


        [ForeignKey(nameof(Expert))]
        public int ExpertId { get; set; }
        public Expert  Expert {get;set;}

    }


    public enum ExpertOption : int
    {

        basic = 1,
        standard = 2,
        vip = 3
    }


   public enum ReviewStatus: int
{
    InquiryReceived = 1,    // Customer inquiry is received
    ExpertAssigned = 2,     // An expert has been assigned to the inquiry
    ReviewInProgress = 3,   // The expert is currently reviewing the printer
    ReviewCompleted = 4,     // The expert has completed the review
    FeedbackSubmitted = 5,   // The expert has submitted their feedback
    NotificationSent = 6    // The buyer has been notified of the expert's opinion
}

}