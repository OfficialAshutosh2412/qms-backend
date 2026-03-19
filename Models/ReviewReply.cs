using System.ComponentModel.DataAnnotations;

namespace QualityWebSystem.Models
{
    public class ReviewReply
    {
        [Key]
        public int ReplyId { get; set; }
        public string ReplyMessage { get; set; }
        public DateTime ReplyDate { get; set; }
        //foreign properties
        public int ReviewId { get; set; }
        public string AdminId { get; set; }
        public Review Review { get; set; }
        public AppUser Admin { get; set; }
    }
}
