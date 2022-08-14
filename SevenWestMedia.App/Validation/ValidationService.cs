using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SevenWestMedia.Common.Models;

namespace SevenWestMedia.App.Validation
{
    public class ValidationService<T> : IValidationService<T>
    {
        public bool IsModelValid(T model, out IList<ValidationResult> validationResults)
        {
            ValidationContext validationContext = new ValidationContext(model,null, null);
            validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(model, validationContext, validationResults, true);
        }
    }
}