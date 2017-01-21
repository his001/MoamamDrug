using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

public class Utf8StringWriter : StringWriter
{
    public override Encoding Encoding
    {
        get { return Encoding.UTF8; }
    }
}

public static class MyExtender
{
    public static string SerializeObject<T>(this T value)
    {
        if (value == null)
        {
            return null;
        }

        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (StringWriter textWriter = new Utf8StringWriter())
        {
            serializer.Serialize(textWriter, value);
            return textWriter.ToString();
        }
    }
}