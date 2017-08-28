using System.ComponentModel.DataAnnotations;

namespace MVCData_Group5.Models.Validators
{

    /// <summary>
    /// Specifies that a data field value is required, if the supplied property has the value <c>false</c>.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class RequiredIfFalseAttribute : RequiredAttribute
    {
        readonly string validatorProperty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validatorProperty">Name of the property to check whether this property is required or not</param>
        public RequiredIfFalseAttribute(string validatorProperty)
        {
            if (string.IsNullOrEmpty(validatorProperty))
            {
                throw new System.ArgumentException("Can not be null or empty string.", "validatorProperty");
            }

            this.validatorProperty = validatorProperty;
        }

        public string ValidatorProperty
        {
            get { return validatorProperty; }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(validatorProperty);
            if (property == null)
            {
                throw new ValidationException($"Property {validatorProperty} does not exist.");
            }

            var v = property.GetValue(validationContext.ObjectInstance, null);

            if(v.GetType() != typeof(System.Boolean))
            {
                throw new ValidationException($"Property {validatorProperty} is not of the type Boolean.");
            }

            if ((bool)v == true)
            {
                return null;
            }

            return base.IsValid(value, validationContext);
        }
    }
}