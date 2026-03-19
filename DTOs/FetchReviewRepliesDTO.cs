using QualityWebSystem.Models;

namespace QualityWebSystem.DTOs
{
    public class FetchReviewRepliesDTO
    {
        public int ReplyId { get; set; }
        public int ReviewId { get; set; }
        public string ReplyMessage { get; set; }
        public string AdminName { get; set; }
        public DateTime ReplyDate { get; set; }
    }
}
