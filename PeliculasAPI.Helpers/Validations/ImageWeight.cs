using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PeliculasAPI.Helpers.Validations
{
    public class ImageWeight: ValidationAttribute
    {
        private readonly int weightInMegaBytes;

        public ImageWeight(int weightInMegaBytes)
        {
            this.weightInMegaBytes = weightInMegaBytes * 1024 * 1024;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile formFile = value as IFormFile;

            if(formFile == null)
            {
                return ValidationResult.Success;
            }

            if(formFile.Length > WeightInMb(this.weightInMegaBytes))
            {
                return new ValidationResult($"The weight of image must be smaller than {this.weightInMegaBytes}MB");
            }

            return ValidationResult.Success;

        }

        private int WeightInMb(int weight)
        {
            return weight * 1024 * 1024;
        }
    }

}
