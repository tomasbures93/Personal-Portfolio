using Portfolio.Application.Abstraction.Mapper;

namespace Portfolio.Application.Common.Mapper;

public class ObjectMapper : IObjectMapper
{
    public TDestination Map<TSource, TDestination>(TSource source)
    {
        if(source == null)
            throw new ArgumentNullException(nameof(source), "Source object cannot be null.");

        var sourceType = typeof(TSource);               // GET TYPE OF SOURCE
        var destinationType = typeof(TDestination);     // GET TYPE OF DESTINATION

        var constructor = destinationType.GetConstructors().First();        // I Get the first constructor
        var constructorParameters = constructor.GetParameters();            // I Get the parameters of the constructor

        var arguments = new object?[constructorParameters.Length];          // Length of the parameters  

        foreach ( var parameter in constructorParameters) { 
            var sourceProperty = sourceType.GetProperty(
                parameter.Name!, 
                System.Reflection.BindingFlags.IgnoreCase | 
                System.Reflection.BindingFlags.Public | 
                System.Reflection.BindingFlags.Instance);
            if (sourceProperty != null) {
                var value = sourceProperty.GetValue(source);
                arguments[parameter.Position] = value;
            } 
            else
            {
                throw new InvalidOperationException($"No matching property found in source for constructor parameter '{parameter.Name}' of type '{destinationType.Name}'.");
            }
        }

        return (TDestination)constructor.Invoke(arguments);
    }
}
