using System.ComponentModel;

namespace Stories.Domain.Validation
{
    /// <summary>Validation categories</summary>
    public enum ValidationCategory
    {
        /// <summary>The default category. Indicates that either no category was suitable or no category was specified.</summary>
        [Description("The default category. Indicates that either no category was suitable or no category was specified.")]
        General = 0,

        /// <summary>The value is required</summary>
        [Description("The value is required")]
        Required = 1,

        /// <summary>The value is invalid</summary>
        [Description("The value is invalid")]
        Invalid = 2,

        /// <summary>The value is a duplicate</summary>
        [Description("The value is duplicate")]
        Duplicate = 3,

        /// <summary>The value has fallen out of some predefined range</summary>
        [Description("The value has fallen out of some predefined range")]
        Range = 4,

        /// <summary>The value does not match some other value</summary>
        [Description("The value does not match some other value or set of values")]
        Mismatch = 5,
    }
}
