﻿using System.Text.Json;
using System.Text.Json.Serialization;
using Answer.King.Domain.Repositories.Models;

namespace Answer.King.Api.Common.JsonConverters;

public class TagIdJsonConverter : JsonConverter<TagId>
{
    public override TagId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            reader.TryGetInt64(out long id);
            return new TagId(id);
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

    public override void Write(Utf8JsonWriter writer, TagId value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.Value);
    }
}
