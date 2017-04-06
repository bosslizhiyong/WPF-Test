using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.Component;

namespace ThinkNet.Component
{
    public class NewtonsoftJsonSerializer : IJsonSerializer
    {
        private readonly JsonSerializerSettings _settings;

        public NewtonsoftJsonSerializer(params Type[] creationWithoutConstructorTypes)
        {
            _settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>
                {
                    new IsoDateTimeConverter(),
                    new CreateObjectWithoutConstructorConverter(creationWithoutConstructorTypes)
                },
                ContractResolver = new CustomContractResolver()
            };
        }

        public string Serialize(object obj)
        {
            //return obj == null ? null : JsonConvert.SerializeObject(obj, _settings);
            return JsonConvert.SerializeObject(obj, new DateTimeConverter(), new EnumTypeConverter());
        }

        public object Deserialize(string value, Type type)
        {
            return JsonConvert.DeserializeObject(value, type, _settings);
        }

        public T Deserialize<T>(string value) where T : class
        {
            //return JsonConvert.DeserializeObject<T>(JObject.Parse(value).ToString(), _settings);
            return JsonConvert.DeserializeObject<T>(value);
        }

        public string SerializeDataTable(DataTable dt)
        {
            String strJson = "[";

            foreach (DataRow row in dt.Rows)
            {
                strJson += "{";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string name = dt.Columns[i].ColumnName;
                    string value = row[dt.Columns[i].ColumnName].ToString();
                    strJson += String.Format("\"{0}\":\"{1}\",", name, value);
                }
                strJson = strJson.Substring(0, strJson.Length - 1);
                strJson += "},";
            }

            strJson = strJson.Substring(0, strJson.Length - 1);
            strJson += "]";
            return strJson;
        }

        public string SerializeEnumerable4EasyuiGrid(IEnumerable objects, int totalCount)
        {
            return string.Format(@"{{""total"":{0},""rows"":{1}}}", totalCount, Serialize(objects));
        }

        public string SerializeDataTable4EasyuiGrid(DataTable dt, int totalCount)
        {
            return string.Format(@"{{""total"":{0},""rows"":{1}}}", totalCount, SerializeDataTable(dt));
        }

        #region 辅助类
        class CreateObjectWithoutConstructorConverter : JsonConverter
        {
            private readonly IEnumerable<Type> _creationWithoutConstructorTypes;

            public CreateObjectWithoutConstructorConverter(IEnumerable<Type> creationWithoutConstructorTypes)
            {
                _creationWithoutConstructorTypes = creationWithoutConstructorTypes;
            }

            public override bool CanWrite
            {
                get { return false; }
            }
            public override bool CanConvert(Type objectType)
            {
                return _creationWithoutConstructorTypes.Any(x => x.IsAssignableFrom(objectType));
            }
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotSupportedException("CreateObjectWithoutConstructorConverter should only be used while deserializing.");
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null)
                {
                    return null;
                }
                var target = FormatterServices.GetUninitializedObject(objectType);
                serializer.Populate(reader, target);
                return target;
            }
        }
        class CustomContractResolver : DefaultContractResolver
        {
            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var jsonProperty = base.CreateProperty(member, memberSerialization);
                if (jsonProperty.Writable) return jsonProperty;
                var property = member as PropertyInfo;
                if (property == null) return jsonProperty;
                var hasPrivateSetter = property.GetSetMethod(true) != null;
                jsonProperty.Writable = hasPrivateSetter;

                return jsonProperty;
            }
        }

        class DateTimeConverter : DateTimeConverterBase
        {
            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                return existingValue;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                writer.WriteValue(Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss"));

            }
        }
        class EnumTypeConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType.BaseType == typeof(Enum) ? true : false;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                return existingValue;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                writer.WriteValue(value.ToString());
            }
        }
        #endregion
    }
}
