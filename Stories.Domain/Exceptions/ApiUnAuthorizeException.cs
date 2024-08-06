using System.Text.Json;
using Stories.Domain.Validation;

namespace Stories.Domain.Exceptions
{
    public class ApiUnAuthorizeException : Exception
    {
        public ApiUnAuthorizeException(ValidationMessage message)
            : this(new[]
                   {
                       message
                   })
        {
        }

        public ApiUnAuthorizeException(
            ICollection<ValidationMessage> messages)
            : base(message: string.Join(", ", messages.Select(m => m.Message)))
        {
            Messages = messages.ToArray();

            Data["messages"] = JsonSerializer.Serialize(Messages);
        }

        public ValidationMessage[] Messages { get; }
    }
}