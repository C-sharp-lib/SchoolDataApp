namespace SchoolMVCApp.Models
{
    public class StudentClasses
    {
        public int StudentId { get; set; }
        public Students Student { get; set; }
        public int ClassId { get; set; }
        public Classes Clas { get; set; }
    }
}
