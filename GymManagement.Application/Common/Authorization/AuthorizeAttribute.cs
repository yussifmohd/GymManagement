
namespace GymManagement.Application.Common.Authorization
{
    // Specifies the application elements on which it is valid to apply an attribute.
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AuthorizeAttribute : Attribute
    {
        public string? Permissions { get; set; }
        public string? Roles { get; set; }
    }
}
