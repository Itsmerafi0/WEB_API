using API.Contracts;
using System.ComponentModel.DataAnnotations;

namespace API.Utility
{
    public class NIKEmailPhoneValidationAttributeneValidationAttribute : ValidationAttribute
    {
        private readonly string _propertyName;

        public NIKEmailPhoneValidationAttributeneValidationAttribute(string propertyName)
        {
            _propertyName = propertyName;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult($"{_propertyName} is required.");
            var employeeRepository = validationContext.GetService(typeof(IEmployeeRepository))
                                        as IEmployeeRepository;

            var checkEmailAndPhone = employeeRepository.CheckEmailAndPhoneAndNIK(value.ToString());
            if (checkEmailAndPhone) return new ValidationResult($"{_propertyName} '{value}' already exists.");
            return ValidationResult.Success;
        
        }
    }
}
