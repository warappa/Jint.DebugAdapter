using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jither.DebugAdapter.Helpers;

/// <summary>
/// StringEnum converter, tailored for DebugAdapter protocol.
/// </summary>
public class StringEnumConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        Type? type = typeToConvert;
        while (type is not null)
        {
            if (type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(StringEnum<>))
            {
                return true;
            }

            type = type.BaseType;
        }

        return false;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            return (JsonConverter)Activator.CreateInstance(
                typeof(Converter<>).MakeGenericType(typeToConvert))!;
        }
        catch (TargetInvocationException ex)
        {
            if (ex.InnerException is not null)
            {
                throw ex.InnerException;
            }

            throw;
        }
    }

    private class Converter<T> : JsonConverter<StringEnum<T>>
        where T : StringEnum<T>, new()
    {
        public override StringEnum<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException("Expected string value");
            }

            var value = reader.GetString();
            if (StringEnum<T>.TryParse(value, out var result))
            {
                return result;
            }

            return StringEnum<T>.Custom(value);
        }

        public override void Write(Utf8JsonWriter writer, StringEnum<T> value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.EnumValue);
        }
    }
}
