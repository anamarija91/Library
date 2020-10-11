using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Library.Core.Utils
{
    public static class ReflectionUtility
    {
        /// <summary>
        /// Creates a lambda expression calling a method <paramref name="methodName"/> on a object with a property
        /// <paramref name="propertyName"/>, the method parameter is a constant with value <paramref name="constantValue"/>
        /// with parameter  e.g. (i => i.propertyName.Method(constantValue))
        /// </summary>
        /// <typeparam name="T">Constant value type that matches property type</typeparam>
        /// <typeparam name="TP">Type of the parameter with property <paramref name="propertyName"/></typeparam>
        /// <param name="constantValue">Constant value</param>
        /// <param name="propertyName">Object property name</param>
        /// <param name="methodName">Method name</param>
        /// <returns>Lambda <see cref="Expression"/></returns>
        public static Expression<Func<TP, bool>> GetMethodPredicate<TP, T>(T constantValue, string propertyName, string methodName)
        {
            var method = GetMethod(typeof(T), methodName);
            var parameter = Expression.Parameter(typeof(TP), "type");
            var property = Expression.Property(parameter, propertyName);
            var constant = Expression.Constant(constantValue, typeof(T));
            var equalsMethod = Expression.Call(property, method, constant);

            return Expression.Lambda<Func<TP, bool>>(equalsMethod, parameter);
        }

        /// <summary>
        /// Returns method available to <paramref name="type"/> defined by its name <paramref name="methodName"/>.
        /// </summary>
        /// <param name="type">Type that has <paramref name="methodName"/></param>
        /// <param name="methodName">Method name</param>
        /// <returns>Method <see cref="MethodInfo" for <paramref name="methodName"/>/></returns>
        public static MethodInfo GetMethod(Type type, string methodName) => type?.GetMethod(methodName, new[] { type });
    }
}
