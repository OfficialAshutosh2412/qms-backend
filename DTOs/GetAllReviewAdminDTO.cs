namespace QualityWebSystem.DTOs
{
    public class GetAllReviewAdminDTO
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsEdited { get; set; }
        public bool isReplied { get; set; }
        //foreign properties
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string DeptName { get; set; }
    }
}
