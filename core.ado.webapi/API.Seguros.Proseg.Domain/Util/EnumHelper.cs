using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace API.Seguros.Proseg.Domain.Util
{
    public static class EnumHelper
    {
        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }

        public static List<Tuple<Enum, string>> ToList(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            List<Tuple<Enum, string>> list = new List<Tuple<Enum, string>>();
            Array enumValues = Enum.GetValues(type);

            foreach (Enum value in enumValues)
            {
                list.Add(new Tuple<Enum, string>(value, GetDescription(value)));
            }

            return list;
        }
    }
}
