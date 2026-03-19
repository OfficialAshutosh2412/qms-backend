namespace QualityWebSystem.DTOs
{
    public class DepartmentSummaryDTO
    {
        public string Department { get; set; }
        public int TotalReviews { get; set; }
        public int PositiveReviews { get; set; }
        public int NeutralReviews { get; set; }
        public int NegativeReviews { get; set; }

    }
}
