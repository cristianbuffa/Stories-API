using System.Text.Json;
using Stories.Domain.Validation;

namespace Stories.Domain.Exceptions
{
    public class ApiValidationException : Exception
    {
    
        public ApiValidationException(ValidationMessage message)
            : this(new[]
                   {
                       message
                   })
        {
        }

        public ApiValidationException(
            ICollection<ValidationMessage> messages)
            : base(message: string.Join(", ", messages.Select(m => m.Message)))
        {
            Messages = messages.ToArray();

            Data["messages"] = JsonSerializer.Serialize(Messages);
        }

        public ValidationMessage[] Messages { get; }
    }
}