using System.ComponentModel.DataAnnotations;

namespace QualityWebSystem.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsEdited { get; set; }
        public bool isReplied { get; set; }
        //foreign properties
        public string CustomerId { get; set; }
        public int DeptId { get; set; }
        public AppUser Customer { get; set; }
        public Department Department { get; set; }
    }
}
