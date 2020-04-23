using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _3.Reflection__Serialize__Deserialize_
{
    static class StaticExt
    {
        public static string ToSerializedString<T>(this T obj)
        {
            Type type = obj.GetType();
            var properties = string.Join(Environment.NewLine, type.GetProperties()
                .Select(x => $"{ x.Name }={x.GetValue(obj)}"));
            var fields = string.Join(Environment.NewLine, type.GetFields()
                .Select(x => $"{ x.Name }={x.GetValue(obj)}"));
            
            return properties + (properties.Length > 0 ? "\n" : "") + fields;
        }
        public static T AsDeserializedString<T>(this string str) where T : new()
        {
            T obj = new T();
            Type type = obj.GetType();
            foreach (string item in str.Split('\n'))
            {
                var parts = item.Split('=');
                var sName = parts[0] ?? default;
                var sVal = parts[1] ?? default;
                if (sName == default || sVal == default)
                    continue;

                PropertyInfo prop = type.GetProperties().Where(x => x.Name == sName).FirstOrDefault();
                prop?.SetValue(obj, ParseTo(prop.PropertyType, sVal));

                FieldInfo field = type.GetFields().Where(x => x.Name == sName).FirstOrDefault();
                field?.SetValue(obj, ParseTo(field.FieldType, sVal));
            }

            return obj;
            
            object ParseTo(Type t, string s)
            {
                if (t.Equals(typeof(int)))
                    return int.Parse(s);
                if (t.Equals(typeof(decimal)))
                    return decimal.Parse(s);
                if (t.Equals(typeof(DateTime)))
                    return DateTime.Parse(s);

                return s;
            }
        }
            
        public static void Dump<T>(this T obj)
        {
            Console.WriteLine(obj);
        }
    }
}