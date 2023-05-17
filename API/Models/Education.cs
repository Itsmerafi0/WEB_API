namespace API.Models
{
    public class Education
    {
        public Guid Guid { get; set; }
        public string Major {get; set;}
        public string Degree {get; set;}
        public float Gpa { get; set;}    
        public DateTime CreateDate { get; set;}
        public DateTime ModifiedDate { get; set;}
        public Guid UniversityGuid { get; set;}
    }
}
