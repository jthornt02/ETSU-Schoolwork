

namespace WebAPI
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    //Plant attributes
    public partial class Plant
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }

        [JsonProperty("plant_id")]
        public long PlantId { get; set; }

        [JsonProperty("specimen_id")]
        public long SpecimenId { get; set; }

        [JsonProperty("common")]
        public string Common { get; set; }

        [JsonProperty("genus")]
        public string Genus { get; set; }

        [JsonProperty("species")]
        public string Species { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }
    }

    //Makes the address a constant so address can't be changed
    public enum Address { The3400VineStreetCincinnatiOh45220, The8815EastKemperRoadCincinnatiOh45242 };

    //Deserializes the json data
    public partial class Plant
    {
        public static List<Plant> FromJson(string json) => JsonConvert.DeserializeObject<List<Plant>>(json, WebAPI.Converter.Settings);
    }

    //Serializes the json data
    public static class Serialize
    {
        public static string ToJson(this List<Plant> self) => JsonConvert.SerializeObject(self, WebAPI.Converter.Settings);
    }

    //Converts the json data into their attributes
    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                AddressConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    //Splits the addresses in the json data
    internal class AddressConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Address) || t == typeof(Address?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            return Address.The3400VineStreetCincinnatiOh45220;
        }

        //Determines which address to use
        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Address)untypedValue;
            switch (value)
            {
                case Address.The3400VineStreetCincinnatiOh45220:
                    serializer.Serialize(writer, "3400 Vine Street Cincinnati OH 45220");
                    return;
                case Address.The8815EastKemperRoadCincinnatiOh45242:
                    serializer.Serialize(writer, "8815 East Kemper Road Cincinnati OH 45242");
                    return;
            }
            throw new Exception("Cannot marshal type Address");
        }

        
        public static readonly AddressConverter Singleton = new AddressConverter();
    }
}

