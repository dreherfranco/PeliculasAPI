using Microsoft.AspNetCore.Http;
using PeliculasAPI.Helpers.Validations.HelpersValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PeliculasAPI.Helpers.Validations
{
    public class FileType: ValidationAttribute
    {
        private readonly string[] validTypes;
        public FileType(string[] validTypes)
        {
            this.validTypes = validTypes;
        }

        public FileType(GroupFileType groupFileType)
        {
            if(groupFileType == GroupFileType.Image)
            {
                this.validTypes = new string[] { "image/jpg", "image/jpeg", "image/gif", "image/png" };
            }
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile formFile = value as IFormFile;

            if (formFile == null)
            {
                return ValidationResult.Success;
            }

            if (!this.validTypes.Contains(formFile.ContentType))
            {
                return new ValidationResult($"The type of file must be {string.Join(", ", this.validTypes)}");
            }

            return ValidationResult.Success;
        }
    }
}
