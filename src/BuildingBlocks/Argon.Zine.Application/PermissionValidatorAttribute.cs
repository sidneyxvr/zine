namespace Argon.Zine.Application;

[AttributeUsage(AttributeTargets.All)]
public class PermissionValidatorAttribute : Attribute
{
    public int Permission { get; set; }
}
