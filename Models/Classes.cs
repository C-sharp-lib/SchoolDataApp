namespace SchoolMVCApp.Models
{
    public class Classes
    {
        public int ClassId { get; set; }
        public string Title { get; set; }
        public string Prerequisites { get; set; }
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public ICollection<StudentClasses> StudentClasses { get; set; }
        
    }
}
