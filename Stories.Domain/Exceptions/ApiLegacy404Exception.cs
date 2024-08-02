using Newtonsoft.Json;
using Stories.Domain.Validation;

namespace Stories.Domain.Exceptions
{
    public class ApiLegacy404Exception : Exception
    {
        public ApiLegacy404Exception(ValidationMessage message)
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
        public ApiLegacy404Exception(
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