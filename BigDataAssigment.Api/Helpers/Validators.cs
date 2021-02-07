using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BigDataAssigment.Api.Helpers
{
    public static class Validators
    {
        public static void ValidateModel<T>(T model)
        {
            var context = new ValidationContext(model, null, null);
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, context, errors, true);

            if (!isValid)
            {
                throw new AggregateException(errors.Select((e) => new ValidationException(e.ErrorMessage)));
            }
        }
    }
}
