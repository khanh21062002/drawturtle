using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Utils.Common
{
    public static class EnumHelper
    {
        public static string GetDescription(this object obj)
        {
            var des = obj.GetAttribute<DescriptionAttribute>();

            return des == null ? null : des.Description;
        }

        public static TAttribute GetAttribute<TAttribute>(this object obj) where TAttribute : Attribute
        {
            var descriptionAttr = obj.GetType().GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault() as TAttribute;

            return descriptionAttr;
        }

        public static string GetEnumDescription(this Enum enumValue)
        {
            var des = enumValue.GetEnumAttribute<DescriptionAttribute>();

            return des == null ? null : des.Description;
        }

        public static TAttribute GetEnumAttribute<TAttribute>(this Enum enumValue) where TAttribute : Attribute
        {
            MemberInfo memberInfo = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();

            if (memberInfo != null)
            {
                var attribute = (TAttribute)memberInfo.GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault();
                return attribute;
            }
            return null;
        }
    }
}
