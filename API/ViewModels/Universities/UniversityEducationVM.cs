using API.ViewModels.Educations;

namespace API.ViewModels.Universities
{
    public class UniversityEducationVM
    {
        public Guid? Guid { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public IEnumerable<EducationVM> Educations { get; set; }
    }
}
