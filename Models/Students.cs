namespace SchoolMVCApp.Models
{
    public class Students
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB {  get; set; }
        public float GPA { get; set; }
        public ICollection<Classes> Classes { get; set; }
        public Students() { }
    }
}
