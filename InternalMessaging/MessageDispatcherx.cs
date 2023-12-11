using System.Collections;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace InternalMessaging;

public class MessageDispatcher : IMessageDispatcher
{
    readonly Assembly messageHandlerAssembly;
    readonly Type messageHandlerType;
    public MessageDispatcher(IServiceProvider serviceProvider, Type messageHandlerType, Assembly messageHandlerAssembly)
    {
        this.serviceProvider = serviceProvider;
        this.messageHandlerAssembly = messageHandlerAssembly;
        this.messageHandlerType = messageHandlerType;
    }
    readonly IServiceProvider serviceProvider;

    public MessageDispatcher(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public void Dispatch(IEnumerable messages)
    {
        foreach (var handlerType in GetHandlerTypes())
            foreach (var message in messages)
                Instanciate(handlerType).Handle((dynamic)message);
    }

    dynamic Instanciate(Type handlerType)
    => ActivatorUtilities.CreateInstance(serviceProvider, handlerType);


    IEnumerable<Type> GetHandlerTypes()
    => messageHandlerAssembly.GetTypes()
        .SelectMany(t =>
        {
            if (t.GetInterfaces()
            .Any(i => i.IsGenericType && i.GetGenericTypeDefinition().IsAssignableTo(messageHandlerType)))
                return new[] { t };

            return new Type[0];
        });


}