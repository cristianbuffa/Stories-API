using Stories.Domain.Exceptions;
using Stories.Domain.Validation;


namespace Stories.API.ExceptionHandling
{
    public class ValidationResponse
    {
        
        public ValidationResponse(IEnumerable<Message> messages, string contextId)
        {
            Messages = new List<Message>();
            Messages = messages.ToList();
            ContextId = contextId;
        }

        public string ContextId { get; }


        public List<Message> Messages { get; }

      
        public class Message
        {

            public Message(ValidationMessage message)
            {
                CategoryId = (int)message.Category;
                Category = message.Category.ToString();
                Name = message.Name;
                Text = message.Message;
                Value = message.Value == null
                                 ? null
                                 : message.Value.ToString();
            }

            public int CategoryId { get; }

            public string Category { get; }

            public string Name { get; }

            public string Text { get; }

            public string? Value { get; }

        }


    }
}