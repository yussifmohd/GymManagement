﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Common.Presistence
{
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
        public ValueJsonComparer() : base(
          (l, r) => JsonSerializer.Serialize(l, JsonSerializerOptions.Default) == JsonSerializer.Serialize(r, JsonSerializerOptions.Default),
          v => v == null ? 0 : JsonSerializer.Serialize(v, JsonSerializerOptions.Default).GetHashCode(),
          v => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(v, JsonSerializerOptions.Default), JsonSerializerOptions.Default)!)
        {
        }
    }
}
