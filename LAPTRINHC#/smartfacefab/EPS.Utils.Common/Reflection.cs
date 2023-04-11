using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Utils.Common
{
    public static class Reflection
    {
        public static object GetProperty(this object obj, string propertyName)
        {
            string[] props = propertyName.Split('.');
            object value = obj;
            foreach (var p in props)
            {
                if (value == null)
                    return null;
                if (String.IsNullOrEmpty(p))
                    continue;
                var prop = value.GetType().GetProperty(p);
                if (prop == null)
                    throw new ApplicationException(string.Format("Property \"{0}\" is not found in type \"{1}\"", propertyName, obj.GetType().FullName));
                value = prop.GetValue(value, null);
            }
            return value;
        }

        public static void SetProperty(this object obj, string propertyName, object value)
        {
            var property = obj.GetType().GetProperty(propertyName);

            if (property == null)
            {
                throw new ApplicationException(string.Format("Property \"{0}\" is not found in type \"{1}\"", propertyName, obj.GetType().FullName));
            }

            property.SetValue(obj, value);
        }

        public static bool IsDerivedFromGenericInterface(this object obj, Type genericType)
        {
            return obj.GetType().GetInterfaces().Any(x =>
                x.IsGenericType &&
                x.GetGenericTypeDefinition() == genericType);
        }
    }
}
