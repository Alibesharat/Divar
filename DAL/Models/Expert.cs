namespace divar.DAL.Models
{
    public class Expert
    {
        public int ExpertId {get;set;}
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Reservation> Reservations{get;set;}
    }
}