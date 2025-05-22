// <copyright file="AdditionalPropertiesExtensions.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GraphicsServerApiLocal.Standard.Utilities
{
    public static class AdditionalPropertiesExtensions
    {
        /// <summary>
        /// Converts the specified object to the specified type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to which the object should be converted.</typeparam>
        /// <param name="input">The object to be converted. This can be a JObject or already of type T.</param>
        /// <returns>
        /// An instance of type <typeparamref name="T"/> that represents the converted object.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when the input is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the input is not of type JObject and cannot be converted.</exception>
        public static T ToObject<T>(this object input)
        {
            switch (input)
            {
                case null:
                    throw new ArgumentNullException(nameof(input), "Input cannot be null.");
                case T typedValue:
                    return typedValue;
                case JObject value:
                    return value.ToObject<T>();
                default:
                    throw new ArgumentException(
                        $"Input should be of type JObject to convert. Actual type: {input.GetType().Name}",
                        nameof(input));
            }
        }

        internal static T GetValue<T>(this IDictionary<string, JToken> additionalProperties, string key)
        {
            key.ValidateAdditionalPropertiesKey();
            if (additionalProperties.TryGetValue(key, out var token) && token != null)
            {
                try
                {
                    return token.ToObject<T>();
                }
                catch (JsonSerializationException)
                {
                }
            }

            throw new KeyNotFoundException($"The key '{key}' was not found in the AdditionalProperties.");
        }

        internal static void SetValue(this IDictionary<string, JToken> additionalProperties, string key, object value,
            IEnumerable<string> propertyNames)
        {
            key.ValidateAdditionalPropertiesKey();
            if (propertyNames.IsKeyInPropertyNames(key))
                throw new ArgumentException(
                    $"The key '{key}' cannot be used because it conflicts with an internal property of the object.",
                    nameof(key));
            additionalProperties[key] = JToken.FromObject(value);
        }

        internal static IEnumerable<string> GetPropertyNames(this object obj)
        {
            return obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(propertyInfo =>
                {
                    var jsonPropertyAttribute = propertyInfo.GetCustomAttribute<JsonPropertyAttribute>();
                    return jsonPropertyAttribute != null ? jsonPropertyAttribute.PropertyName : propertyInfo.Name;
                })
                .Where(name => name != null);
        }

        private static void ValidateAdditionalPropertiesKey(this string key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key), "Key cannot be null.");
            }
        }

        
        private static bool IsKeyInPropertyNames(this IEnumerable<string> propertyNames, string key)
        {
            return propertyNames.Contains(key);
        }
    }
}