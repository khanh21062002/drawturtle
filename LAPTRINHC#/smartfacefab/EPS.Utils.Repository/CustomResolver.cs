using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EPS.Utils.Repository
{
    public class CustomResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member, memberSerialization);

            if (prop.PropertyType.IsInterface || (prop.PropertyType.IsClass && prop.PropertyType != typeof(string)))
            {
                prop.ShouldSerialize = obj => false;
            }

            return prop;
        }
    }
}
