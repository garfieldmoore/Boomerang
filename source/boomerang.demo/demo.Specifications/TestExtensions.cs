namespace CoffeTime.Specifications
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Xml.Serialization;

    public static class SerialisationExtensions
    {
        public static string SerialiseToString(this object target)
        {
            var xmlSerializer = new XmlSerializer(target.GetType());
            var stringWriter = new StringWriter(new StringBuilder());

            xmlSerializer.Serialize(stringWriter, target);
            stringWriter.Close();

            var serialiseToString = stringWriter.ToString();
            Console.WriteLine(serialiseToString);

            return serialiseToString;
        }

        public static string SerialiseToJsonString(this object target)
        {
            string str;

            var dataContractJsonSerializer = new DataContractJsonSerializer(target.GetType());

            using (var mem = new MemoryStream())
            {
                dataContractJsonSerializer.WriteObject(mem, target);
                mem.Flush();
                mem.Position = 0;
                str = Encoding.Default.GetString(mem.ToArray());
                Console.WriteLine(str);
            }

            return str;
        }

    }


}