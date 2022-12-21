using System.Reflection;
using System.Text;
using WhatsNewAttributes;

namespace LookupWhatsNew
{
    internal class Program
    {
        private static StringBuilder outputText = new StringBuilder(1024);
        private static DateTime backDateTo = new DateTime(2009, 2, 1);
        private static string assemblyName = "VectorClass.dll";

        static void Main(string[] args)
        {
            if (args.Length > 0) assemblyName = args[0];

            Assembly assembly = Assembly.LoadFrom(assemblyName);
            WriteToOutputText($"assembly: {assembly.FullName}");

            Attribute? attr = assembly.GetCustomAttribute(typeof(SupportsWhatsNewAttribute));
            if (attr == null)
            {
                WriteToOutputText($"{assemblyName} does not support WhatsNew Attributes.");
            }
            else
            {
                WriteToOutputText($"What's new since {backDateTo:D}");
                WriteToOutputText($"{assemblyName} defined types:");
                foreach (Type type in assembly.DefinedTypes)
                {
                    WriteTypeInfo(type);
                }
            }

            Console.Write(outputText.ToString());
        }

        private static void WriteTypeInfo(Type type)
        {
            IEnumerable<LastModifiedAttribute> typeInfoAttrs =
                //from a in type.GetTypeInfo().GetCustomAttributes<LastModifiedAttribute>()
                from a in type.GetCustomAttributes<LastModifiedAttribute>()
                where a.DateModified >= backDateTo
                select a;
            WriteToOutputText("=========================================================================================");
            WriteToOutputText($"class {type.Name}, changes:");
            if (typeInfoAttrs.Any())
                foreach (var a in typeInfoAttrs)
                {
                    WriteAttributeInfo(a, 0);
                }
            else
                WriteToOutputText("no changes.");

            var memberInfoAttrs =
                from m in type.GetMembers(BindingFlags.NonPublic | BindingFlags.Public)
                //from m in type.GetConstructors()
                let mAttrs = from a in m.GetCustomAttributes<LastModifiedAttribute>()
                             where a.DateModified >= backDateTo
                             select a
                where mAttrs.Any() == true
                select (m, mAttrs);
            foreach (var m in memberInfoAttrs)
            {
                WriteToOutputText($"\tmember {m.m}, changes:");
                foreach (var a in m.mAttrs)
                {
                    WriteAttributeInfo(a, 1);
                }
            }
        }

        private static void WriteAttributeInfo(LastModifiedAttribute attribute, int paddings)
        {
            StringBuilder text = new StringBuilder();
            WritePaddings(paddings, text);
            text.Append($"modified: {attribute.DateModified:D}: {attribute.Changes}");

            if (attribute.Issues != null)
            {
                text.AppendLine();
                WritePaddings(paddings, text);
                text.Append($"Outstanding issues: {attribute.Issues}");
            }

            WriteToOutputText(text.ToString());

            static void WritePaddings(int paddings, StringBuilder text)
            {
                for (int i = 0; i < paddings; i++)
                {
                    text.Append('\t');
                }
            }
        }

        private static void WriteToOutputText(string text) => outputText.AppendLine().Append(text);
    }
}