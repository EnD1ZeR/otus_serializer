using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Serializer.Reflection.Extensions
{
    public static class MemberInfoExtensions
    {
        public static void SetValue(this MemberInfo member, object instance, object value)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    ((FieldInfo)member).SetValue(instance, value);
                    break;
                case MemberTypes.Property:
                    ((PropertyInfo)member).SetValue(instance, value);
                    break;
                default:
                    throw new NotImplementedException($"MemberType does not support in method {nameof(MethodBase.GetCurrentMethod)}.");
            }
        }

        public static object GetValue(this MemberInfo member, object instance)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    return ((FieldInfo)member).GetValue(instance);
                case MemberTypes.Property:
                    return ((PropertyInfo)member).GetValue(instance);
                default:
                    throw new NotImplementedException($"MemberType does not support in method {nameof(MethodBase.GetCurrentMethod)}.");
            }
        }

        public static Type GetMemberType(this MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    return ((FieldInfo)member).FieldType;
                case MemberTypes.Property:
                    return ((PropertyInfo)member).PropertyType;
                default:
                    throw new NotImplementedException($"MemberType does not support in method {nameof(MethodBase.GetCurrentMethod)}.");
            }
        }
    }
}
