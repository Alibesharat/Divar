using System.ComponentModel.DataAnnotations;


namespace divar.DAL.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public ExpertOption ExpertOption { get; set; }

        public DateTime BookTime { get; set; }

        public string PostToken {get;set;}
    }


    public enum ExpertOption : int
    {

        basic = 1,
        standard = 2,
        vip = 3
    }
}