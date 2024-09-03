using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace CompanyManagement.Infrastructure.Persistence.Convertors;

public static class FluentApiExtensions
{
    public static PropertyBuilder<T> HasValueJsonConverter<T>(this PropertyBuilder<T> builder) =>
        builder
            .HasConversion(new ValueJsonConverter<T>(), new ValueJsonComparer<T>());
}

public class ValueJsonConverter<T> : ValueConverter<T, string>
{
    public ValueJsonConverter(ConverterMappingHints? mappingHints = null)
        : base(
        v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
        v => JsonSerializer.Deserialize<T>(v, JsonSerializerOptions.Default)!,
        mappingHints)
    {
    }
}

public class ValueJsonComparer<T> : ValueComparer<T>
{
    public ValueJsonComparer()
        : base(
            (left, right) => JsonSerializer.Serialize(left, JsonSerializerOptions.Default) == JsonSerializer.Serialize(right, JsonSerializerOptions.Default),
            v => v == null ? 0 : JsonSerializer.Serialize(v, JsonSerializerOptions.Default).GetHashCode(),
            v => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(v, JsonSerializerOptions.Default), JsonSerializerOptions.Default)!)

    {
    }
}
