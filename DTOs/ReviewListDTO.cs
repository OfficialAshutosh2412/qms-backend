namespace QualityWebSystem.DTOs
{
    public class ReviewListDTO
    {
        public int ReviewId { get; set; }
        public string CustomerId { get; set; }
        public int DeptId { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsEdited { get; set; }
        public bool isReplied { get; set; }
        public string DeptName { get; set; }
    }
}
