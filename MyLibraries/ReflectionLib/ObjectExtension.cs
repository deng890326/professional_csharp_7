using System.Reflection;
using System.Text;

namespace ReflectionLib
{
    public static class ObjectExtension
    {
        public static string GetTypeInfo(this object obj, bool showMembers = true,
            bool showFields = true, bool showProperties = true, bool showMethods = true,
            bool showEvents = false)
        {
            Type targetType = obj.GetType();
            StringBuilder sb = new StringBuilder($"type={targetType?.FullName}");
            if (showMembers)
            {
                object[] targetMembers = targetType?.GetMembers() ?? new MemberInfo[0];
                sb.Append($",\nMembers=[{string.Join(",\n\t", targetMembers)}]");
            }
            if (showFields)
            {
                var targetFields = targetType?.GetRuntimeFields() ?? new FieldInfo[0];
                sb.Append($",\nFields=[{string.Join(",\n\t", targetFields)}]");
            }
            if (showProperties)
            {
                object[] targetProperties = targetType?.GetProperties() ?? new object[0];
                sb.Append($",\nProperties=[{string.Join(",\n\t", targetProperties)}]");
            }
            if (showMethods)
            {
                var targetMethods = targetType?.GetRuntimeMethods() ?? new MemberInfo[0] as IEnumerable<MemberInfo>;
                sb.Append($",\nMethods=[{string.Join(",\n\t", targetMethods)}]");
            }
            if (showEvents)
            {
                var targetEvents = targetType?.GetRuntimeEvents() ?? new MemberInfo[0] as IEnumerable<MemberInfo>;
                sb.Append($",\nEvents=[{string.Join(",\n\t", targetEvents)}]");
            }
            return sb.ToString();
        }
    }
}