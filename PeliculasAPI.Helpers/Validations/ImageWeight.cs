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
            this.weightInMegaBytes = weightInMegaBytes;
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

            var weightInBytes = this.WeightInBytes(this.weightInMegaBytes);
            if(formFile.Length > weightInBytes)
            {
                return new ValidationResult($"The weight of image must be smaller than {this.weightInMegaBytes}bytes");
            }

            return ValidationResult.Success;

        }

        private int WeightInBytes(int weight)
        {
            return (weight * 1024 * 1024) ;
        }
    }

}
