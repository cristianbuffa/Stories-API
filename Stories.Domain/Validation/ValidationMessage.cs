using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Stories.Domain.Validation
{
    /// <summary>Contains a validation message</summary>
    public class ValidationMessage
    {
        #region Constants

        private const string DuplicateDefaultTextPattern = "{0} is duplicate";

        private const string InvalidDefaultTextPattern = "{0} is invalid";

        private const string MismatchDefaultTextPattern = "{0} is mismatched";

        private const string RangeDefaultTextPattern = "{0} is out of range";

        private const string RequiredDefaultTextPattern = "{0} is required";

        private const string RequiredCompositeText = "{0} are all required";

        private const string RequiredNonCompositeText = "{0} are required";

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes static members of the <see cref="ValidationMessage"/> class.</summary>
        static ValidationMessage()
        {
            NoValue = new NoValueClass();
        }

        /// <summary>Initializes a new instance of the <see cref="ValidationMessage"/> class.</summary>
        /// <param name="category">The category.</param>
        /// <param name="name">The name.</param>
        /// <param name="message">The error message text.</param>
        /// <param name="value">The value of the named element.</param>
        public ValidationMessage(ValidationCategory category, string name, string message, object value)
        {
            Category = category;
            Name = name;
            Message = message;
            Value = value;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the no value singleton instance
        /// </summary>
        public static object NoValue { get; private set; }

        /// <summary>Gets a value indicating the parameter name.</summary>
        public ValidationCategory Category { get; private set; }

        /// <summary>Gets a value with the error message text.</summary>
        public string Message { get; private set; }

        /// <summary>Gets a value indicating the parameter name.</summary>
        public string Name { get; private set; }

        /// <summary>Gets the actual value of the named element.</summary>
        public object Value { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>Creates a duplicate validation message</summary>
        /// <param name="name">The name</param>
        /// <param name="message">The message to display (pass null to use a default message)</param>
        /// <param name="value">The value of the named element or <see cref="NoValue"/>.</param>
        /// <returns>A validation message</returns>
        public static ValidationMessage Duplicate(string name, string message, object value)
        {
            return new ValidationMessage(ValidationCategory.Duplicate, name, message ?? string.Format(DuplicateDefaultTextPattern, name), value);
        }

        /// <summary>Creates a general validation message</summary>
        /// <param name="name">The name</param>
        /// <param name="message">The message to display</param>
        /// <param name="value">The value of the named element or <see cref="NoValue"/>.</param>
        /// <returns>A validation message</returns>
        public static ValidationMessage General(string name, string message, object value)
        {
            return new ValidationMessage(ValidationCategory.General, name, message ?? "No message provided", value);
        }

        /// <summary>Creates an invalid validation message</summary>
        /// <param name="name">The name</param>
        /// <param name="message">The message to display (pass null to use a default message)</param>
        /// <param name="value">The value of the named element or <see cref="NoValue"/>.</param>
        /// <returns>A validation message</returns>
        public static ValidationMessage Invalid(string name, string message, object value)
        {
            return new ValidationMessage(ValidationCategory.Invalid, name, message ?? string.Format(InvalidDefaultTextPattern, name), value);
        }

        /// <summary>Creates a mismatch validation message</summary>
        /// <param name="name">The name</param>
        /// <param name="message">The message to display (pass null to use a default message)</param>
        /// <param name="value">The value of the named element or <see cref="NoValue"/>.</param>
        /// <returns>A validation message</returns>
        public static ValidationMessage Mismatch(string name, string message, object value)
        {
            return new ValidationMessage(ValidationCategory.Mismatch, name, message ?? string.Format(MismatchDefaultTextPattern, name), value);
        }

        /// <summary>Creates a range validation message</summary>
        /// <param name="name">The name</param>
        /// <param name="message">The message to display (pass null to use a default message)</param>
        /// <param name="value">The value of the named element or <see cref="NoValue"/>.</param>
        /// <returns>A validation message</returns>
        public static ValidationMessage Range(string name, string message, object value)
        {
            return new ValidationMessage(ValidationCategory.Range, name, message ?? string.Format(RangeDefaultTextPattern, name), value);
        }

        /// <summary>Creates a required validation message with default message text</summary>
        /// <param name="name">The name</param>
        /// <returns>A validation message</returns>
        public static ValidationMessage Required(string name)
        {
            return Required(name, null);
        }

        /// <summary>Creates a required validation message</summary>
        /// <param name="name">The name</param>
        /// <param name="message">The message to display (pass null to use a default message)</param>
        /// <returns>A validation message</returns>
        public static ValidationMessage Required(string name, string message)
        {
            return new ValidationMessage(ValidationCategory.Required, name, message ?? string.Format(RequiredDefaultTextPattern, name), null);
        }

        /// <summary>Creates a required validation message based on multiple items</summary>
        /// <param name="names">The names</param>
        /// <param name="asComposite">Whether or not the fields are required as a set or separately (AND vs OR)</param>
        /// <returns>A validation message</returns>
        public static ValidationMessage Required(List<string> names, bool asComposite)
        {
            return new ValidationMessage(ValidationCategory.Required, string.Join(", ", names), string.Format(asComposite ? RequiredCompositeText : RequiredNonCompositeText, string.Join(", ", names)), null);
        }

        #endregion

        /// <summary>
        /// Class to indicate that there is no value for the validation message
        /// </summary>
        private class NoValueClass
        {
            public override string ToString()
            {
                return "No value defined";
            }
        }
    }
}
