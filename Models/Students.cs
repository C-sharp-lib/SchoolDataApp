using System.ComponentModel.DataAnnotations;

namespace SchoolMVCApp.Models
{
    public class Students
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime DOB {  get; set; }
        public double GPA { get; set; }
        public ICollection<StudentClasses> StudentClasses { get; set; }
    }
}
