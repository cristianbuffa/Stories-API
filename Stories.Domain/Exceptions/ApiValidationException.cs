using Newtonsoft.Json;
using Stories.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Stories.Domain.Exceptions
{
    /// <summary>
    ///     Standard ApiValidationException
    /// </summary>
    public class ApiValidationException : Exception
    {
        /// <summary>
        /// Creates a new instance of ApiValidationException with a single validation message
        /// </summary>
        /// <param name="message">The validation message (must not be null)</param>
        public ApiValidationException(ValidationMessage message)
            : this(new[]
                   {
                       message
                   })
        {
        }

        /// <summary>
        /// Creates a new instance of ApiValidationException with a several validation messages
        /// </summary>
        /// <param name="messages">The validation messages (must not be null)</param>
        public ApiValidationException(
            ICollection<ValidationMessage> messages)
            : base(message: string.Join(", ", messages.Select(m => m.Message)))
        {
            Messages = messages.ToArray();

            Data["messages"] = JsonConvert.SerializeObject(Messages);
        }

        /// <summary>
        /// Get the messages for the validation exception
        /// </summary>
        public ValidationMessage[] Messages { get; }
    }
}