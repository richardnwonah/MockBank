using System.ComponentModel.DataAnnotations;

namespace CAVBackEndUpdate.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Currency { get; set; }
        public double CumulativeInflow { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
