using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SevenWestMedia.App.Validation
{
    public interface IValidationService<T>
    {
        bool IsModelValid(T model, out IList<ValidationResult> validationResults);
    }
}