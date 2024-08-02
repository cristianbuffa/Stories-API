using Stories.Domain.Exceptions;
using Stories.Domain.Validation;


namespace Stories.API.ExceptionHandling
{
    /// <summary>Response for representing instances of <see cref="ApiValidationException" />.</summary>
    public class ValidationResponse
    {
        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="ValidationResponse" /> class.</summary>
        /// <param name="messages">The validation messages contained in the response.</param>
        /// <param name="contextId">The context id of the request causing the validation exception.</param>
        public ValidationResponse(IEnumerable<Message> messages, string contextId)
        {
            Messages = new List<Message>();
            Messages = messages.ToList();
            ContextId = contextId;
        }

        #endregion

        #region Properties, Indexers

        /// <summary>Gets or sets Id.</summary>
        public string ContextId { get; }

        /// <summary>Gets or sets validation messages.</summary>
        public List<Message> Messages { get; }

        #endregion

        #region Nested type: Message

        /// <summary>Response for representing a message contained by an instance of <see cref="ValidationResponse" />.</summary>
        public class Message
        {
            #region Constructors

            /// <summary>Initializes a new instance of the <see cref="Message" /> class.</summary>
            /// <param name="message">A ValidationMessage instance</param>
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

            #endregion

            #region Properties, Indexers

            /// <summary>Gets or sets CategoryId.</summary>
            public int CategoryId { get; }

            /// <summary>Gets or sets a string value of the Category</summary>
            public string Category { get; }

            /// <summary>Gets or sets Name.</summary>
            public string Name { get; }

            /// <summary>Gets or sets Text.</summary>
            public string Text { get; }

            /// <summary>
            ///     Gets or sets Value.
            /// </summary>
            public string? Value { get; }

            #endregion
        }

        #endregion
    }
}