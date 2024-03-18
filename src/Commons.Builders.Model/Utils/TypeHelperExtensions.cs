using System;
using System.Linq;

namespace Queo.Commons.Builders.Model.Utils;

public static class TypeHelperExtensions
{
    /// <summary>
    ///		Checks if the the type matches the target type.
    ///		It walks up the inheritance tree and checks each type for a match
    ///		It also checks for matches in the GenerycType definition only (eg typeof(List&LT;&GT;))
    /// </summary>
    /// <param name="currentType">Type which is checked</param>
    /// <param name="targetType">Type it should be/is expected to be</param>
    /// <returns></returns>
    public static bool IsOfType(this Type currentType, Type targetType)
    {
        return MatchInInheritanceTree(currentType, t => t == targetType
                                                     || (t.IsGenericType &&
                                                         t.GetGenericTypeDefinition() == targetType));
    }

    /// <summary>
    ///		Checks if the current type implements a certain interface type
    ///		This also works if it matches the Generic type definition only (eg typeof(ISomething&lt;&gt;))
    /// </summary>
    /// <param name="currentType">Type to check the interfaces on</param>
    /// <param name="interfaceType">Target interface type to search for</param>
    public static bool ImplementsInterface(this Type currentType, Type interfaceType)
    {
        Type[] interfaces = currentType.GetInterfaces();

        return currentType == interfaceType ||
                              interfaces.Contains(interfaceType) ||
                              interfaces.Where(i => i.IsGenericType)
                                        .Select(i => i.GetGenericTypeDefinition())
                                        .Contains(interfaceType);
    }

    /// <summary>
    ///		Returns the generic arguments for a given target interface type
    ///		It checks if the current type implements the target interface
    ///		It especially works for the generic type definition version (typeof(ISomething&lt;,&gt;))
    /// </summary>
    /// <param name="current">Type that implements the target interface</param>
    /// <param name="targetType">Target interface type to check against</param>
    public static Type[] GetGenericArgsOf(this Type current, Type targetType)
    {
        Type? interfaceType = null;
        Type[] interfaces = current.GetInterfaces();

        if (current == targetType)
        {
            interfaceType = current;
        }
        else if (interfaces.Contains(targetType))
        {
            interfaceType = interfaces.First(i => i == targetType);
        }
        else
        {
            interfaceType = interfaces.Where(i => i.IsGenericType)
                                      .First(i => i.GetGenericTypeDefinition() == targetType);
        }

        if (interfaceType is null || !interfaceType.IsGenericType)
        {
            return Array.Empty<Type>();
        }
        else
        {
            return interfaceType.GetGenericArguments();
        }
    }

    /// <summary>
    ///		Gos through the inheritance tree and checks if the base type matches the conditon
    /// 	It will stop when there are no more base types left (When the current type is System.Object)
    /// </summary>
    /// <param name="type">Type that should be checked for matches</param>
    /// <param name="matchCondition">The condition, on which a match is defined</param>
    private static bool MatchInInheritanceTree(Type type, Func<Type, bool> matchCondition)
    {
        Type? declaringType = type;
        while (declaringType is not null)
        {
            if (matchCondition(declaringType))
            {
                return true;
            }
            declaringType = declaringType.BaseType;
        }
        return false;
    }

    /// <summary>
    ///		Determines weather or not the current type should be handled like a value type
    ///		This is neccessary because some types (like strings) who are immutable
    ///		are not considered value types, but still should be handled as such
    ///     TODO: list is incomplete
    /// </summary>
    public static bool HandleAsValueType(this Type currentType)
    {
        return currentType.IsValueType ||
               currentType.IsPrimitive ||
               currentType == typeof(String) ||
               currentType == typeof(Decimal);
    }
}
