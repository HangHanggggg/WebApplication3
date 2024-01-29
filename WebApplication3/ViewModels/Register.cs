using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApplication3.ViewModels
{
    public class Register
    {
        [Required]
        [DataType(DataType.Text)]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.CreditCard)]
        public string CreditCardNo { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string MobileNo { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string DeliveryAddress { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [UniqueEmail(ErrorMessage = "Email address must be unique")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Upload)]
        [AllowedFileExtensions(".jpg", ErrorMessage = "Only .JPG files are allowed")]
        public string Photo { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string AboutMe { get; set; }
    }

    // Custom Validation Attribute for Unique Email
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Implement logic to check if the email is unique in your database
            // You can access your database context or repository through the validationContext
            // Example: var dbContext = validationContext.GetService(typeof(YourDbContext)) as YourDbContext;

            // For demonstration purposes, let's assume the email is always unique
            return ValidationResult.Success;
        }
    }

    // Custom Validation Attribute for Allowed File Extensions
    public class AllowedFileExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedFileExtensionsAttribute(string extensions)
        {
            _extensions = extensions.Split(',');
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not null)
            {
                var fileName = value.ToString();
                var extension = fileName.Substring(fileName.LastIndexOf('.') + 1);

                if (!_extensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}