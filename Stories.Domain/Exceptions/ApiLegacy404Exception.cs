using System.Text.Json;
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

        public ApiLegacy404Exception(
            ICollection<ValidationMessage> messages)
            : base (message: string.Join(", ", messages.Select(m => m.Message)))
        {
            Messages = messages.ToArray();

            Data["messages"] = JsonSerializer.Serialize(Messages);
        }

        public ValidationMessage[] Messages { get; }
    }
}