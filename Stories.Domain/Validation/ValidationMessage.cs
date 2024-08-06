using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Stories.Domain.Validation
{
    public class ValidationMessage
    {


        private const string DuplicateDefaultTextPattern = "{0} is duplicate";

        private const string InvalidDefaultTextPattern = "{0} is invalid";

        private const string MismatchDefaultTextPattern = "{0} is mismatched";

        private const string RangeDefaultTextPattern = "{0} is out of range";

        private const string RequiredDefaultTextPattern = "{0} is required";

        private const string RequiredCompositeText = "{0} are all required";

        private const string RequiredNonCompositeText = "{0} are required";


        static ValidationMessage()
        {
            NoValue = new NoValueClass();
        }


        public ValidationMessage(ValidationCategory category, string name, string message, object value)
        {
            Category = category;
            Name = name;
            Message = message;
            Value = value;
        }


        public static object NoValue { get; private set; }

        public ValidationCategory Category { get; private set; }

        public string Message { get; private set; }

        public string Name { get; private set; }

        public object Value { get; private set; }


        public static ValidationMessage Duplicate(string name, string message, object value)
        {
            return new ValidationMessage(ValidationCategory.Duplicate, name, message ?? string.Format(DuplicateDefaultTextPattern, name), value);
        }
        public static ValidationMessage General(string name, string message, object value)
        {
            return new ValidationMessage(ValidationCategory.General, name, message ?? "No message provided", value);
        }
        public static ValidationMessage Invalid(string name, string message, object value)
        {
            return new ValidationMessage(ValidationCategory.Invalid, name, message ?? string.Format(InvalidDefaultTextPattern, name), value);
        }

        public static ValidationMessage Mismatch(string name, string message, object value)
        {
            return new ValidationMessage(ValidationCategory.Mismatch, name, message ?? string.Format(MismatchDefaultTextPattern, name), value);
        }

        public static ValidationMessage Range(string name, string message, object value)
        {
            return new ValidationMessage(ValidationCategory.Range, name, message ?? string.Format(RangeDefaultTextPattern, name), value);
        }

        public static ValidationMessage Required(string name)
        {
            return Required(name, null);
        }

        public static ValidationMessage Required(string name, string message)
        {
            return new ValidationMessage(ValidationCategory.Required, name, message ?? string.Format(RequiredDefaultTextPattern, name), null);
        }

        public static ValidationMessage Required(List<string> names, bool asComposite)
        {
            return new ValidationMessage(ValidationCategory.Required, string.Join(", ", names), string.Format(asComposite ? RequiredCompositeText : RequiredNonCompositeText, string.Join(", ", names)), null);
        }

        private class NoValueClass
        {
            public override string ToString()
            {
                return "No value defined";
            }
        }
    }
}
