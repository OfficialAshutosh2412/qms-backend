using System.ComponentModel.DataAnnotations;

namespace QualityWebSystem.Models
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }
        public string DeptName { get; set; }
    }
}
