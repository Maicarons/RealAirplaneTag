﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using RealAirplaneTag;
//
//    var planeType = PlaneType.FromJson(jsonString);

namespace RealAirplaneTag
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class PlaneType
    {
        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("Manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Physical_Class_Engine")]
        public PhysicalClassEngine PhysicalClassEngine { get; set; }

        [JsonProperty("Num_Engines")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long NumEngines { get; set; }

        [JsonProperty("Approach_Speed_knot")]
        public string ApproachSpeedKnot { get; set; }

        [JsonProperty("WTC")]
        public Wtc Wtc { get; set; }
    }

    public enum PhysicalClassEngine { Jet, Piston, Turboprop, Turboshaft };

    public enum Wtc { Heavy, Light, LightMedium, Medium, Super, WtcLight };

    public partial class PlaneType
    {
        public static Dictionary<string, PlaneType> FromJson(string json) => JsonConvert.DeserializeObject<Dictionary<string, PlaneType>>(json, RealAirplaneTag.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Dictionary<string, PlaneType> self) => JsonConvert.SerializeObject(self, RealAirplaneTag.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                PhysicalClassEngineConverter.Singleton,
                WtcConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class PhysicalClassEngineConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(PhysicalClassEngine) || t == typeof(PhysicalClassEngine?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Jet":
                    return PhysicalClassEngine.Jet;
                case "Piston":
                    return PhysicalClassEngine.Piston;
                case "Turboprop":
                    return PhysicalClassEngine.Turboprop;
                case "Turboshaft":
                    return PhysicalClassEngine.Turboshaft;
            }
            throw new Exception("Cannot unmarshal type PhysicalClassEngine");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (PhysicalClassEngine)untypedValue;
            switch (value)
            {
                case PhysicalClassEngine.Jet:
                    serializer.Serialize(writer, "Jet");
                    return;
                case PhysicalClassEngine.Piston:
                    serializer.Serialize(writer, "Piston");
                    return;
                case PhysicalClassEngine.Turboprop:
                    serializer.Serialize(writer, "Turboprop");
                    return;
                case PhysicalClassEngine.Turboshaft:
                    serializer.Serialize(writer, "Turboshaft");
                    return;
            }
            throw new Exception("Cannot marshal type PhysicalClassEngine");
        }

        public static readonly PhysicalClassEngineConverter Singleton = new PhysicalClassEngineConverter();
    }

    internal class WtcConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Wtc) || t == typeof(Wtc?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Heavy":
                    return Wtc.Heavy;
                case "Light":
                    return Wtc.Light;
                case "Light ":
                    return Wtc.WtcLight;
                case "Light/Medium":
                    return Wtc.LightMedium;
                case "Medium":
                    return Wtc.Medium;
                case "Super":
                    return Wtc.Super;
            }
            throw new Exception("Cannot unmarshal type Wtc");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Wtc)untypedValue;
            switch (value)
            {
                case Wtc.Heavy:
                    serializer.Serialize(writer, "Heavy");
                    return;
                case Wtc.Light:
                    serializer.Serialize(writer, "Light");
                    return;
                case Wtc.WtcLight:
                    serializer.Serialize(writer, "Light ");
                    return;
                case Wtc.LightMedium:
                    serializer.Serialize(writer, "Light/Medium");
                    return;
                case Wtc.Medium:
                    serializer.Serialize(writer, "Medium");
                    return;
                case Wtc.Super:
                    serializer.Serialize(writer, "Super");
                    return;
            }
            throw new Exception("Cannot marshal type Wtc");
        }

        public static readonly WtcConverter Singleton = new WtcConverter();
    }
}
