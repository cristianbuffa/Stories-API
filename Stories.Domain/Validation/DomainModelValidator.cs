using System.Collections;
using System.ComponentModel.DataAnnotations;


namespace Stories.Domain.Validation
{
    public interface IDomainModelValidator
    {
        void ValidateRecursive<T>(T obj);
    }

    public class DomainModelValidator : IDomainModelValidator
    {
        // Let's hide this method and try to use ValidateRecursive
        protected void Validate(object obj, IDictionary<object, object> validationContextItems = null)
        {
            var context = new ValidationContext(obj, null, validationContextItems);
            Validator.ValidateObject(obj, context, true);
        }

        public void ValidateRecursive<T>(T obj)
        {
            ValidateRecursive(obj, null);
        }

        public void ValidateRecursive<T>(T obj, IDictionary<object, object> validationContextItems)
        {
            ValidateRecursive(obj, new HashSet<object>(), validationContextItems);
        }

        private void ValidateRecursive<T>(T obj, ISet<object> validatedObjects, IDictionary<object, object> validationContextItems = null)
        {
            //short-circuit to avoid infinit loops on cyclical object graphs
            if (validatedObjects.Contains(obj))
            {
                return;
            }

            validatedObjects.Add(obj);
            Validate(obj, validationContextItems);

            var properties = obj.GetType().GetProperties().Where(prop => prop.CanRead
                && !prop.GetCustomAttributes(typeof(SkipRecursiveValidation), false).Any()
                && prop.GetIndexParameters().Length == 0).ToList();

            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(string) || property.PropertyType.IsValueType)
                {
                    continue;
                }

                var value = obj.GetPropertyValue(property.Name);

                if (value == null)
                {
                    continue;
                }

                if (value is IEnumerable asEnumerable)
                {
                    foreach (var enumObj in asEnumerable)
                    {
                        if (enumObj != null)
                        {
                            ValidateRecursive(enumObj, validatedObjects, validationContextItems);
                        }
                    }
                }
                else
                {
                    ValidateRecursive(value, validatedObjects, validationContextItems);
                }
            }
        }
    }
    public static class ObjectExtensions
    {
        public static object GetPropertyValue(this object o, string propertyName)
        {
            object objValue = string.Empty;

            var propertyInfo = o.GetType().GetProperty(propertyName);
            if (propertyInfo != null)
            {
                objValue = propertyInfo.GetValue(o, null);
            }

            return objValue;
        }
    }

    public class SkipRecursiveValidation : Attribute
    {
    }
}
