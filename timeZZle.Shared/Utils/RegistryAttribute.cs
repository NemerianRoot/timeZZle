using Microsoft.Extensions.DependencyInjection;

namespace timeZZle.Shared.Utils;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class RegistryAttribute(Type interfaceType, ServiceLifetime serviceLifetime) : Attribute
{
    public Type InterfaceType { get; set; } = interfaceType;
    public ServiceLifetime ServiceLifetime { get; set; } = serviceLifetime;
}