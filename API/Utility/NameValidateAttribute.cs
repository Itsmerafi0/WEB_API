using API.Contracts;
using System.ComponentModel.DataAnnotations;

namespace API.Utility
{
    public class NameValidateAttribute : ValidationAttribute
    {
        private readonly string _propertyName;

        public NameValidateAttribute(string propertyName)
        {
            _propertyName = propertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value == null) return new ValidationResult($"{_propertyName} is required");
            var roomRepository = validationContext.GetService(typeof(IRoomRepository)) 
                                                as IRoomRepository;

            var checkName = roomRepository.CheckName(value.ToString());
            if (checkName) return new ValidationResult($"{_propertyName} '{value}' already exits.");
                return ValidationResult.Success;
        }
    }
}
