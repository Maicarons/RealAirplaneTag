using System;
using System.IO;
using System.Reflection;

namespace RealAirplaneTag;

public class GetRes
{
    public string GetFromJson(string filePath)
    {
        try
        {
            // 使用StreamReader读取文件
            using (StreamReader reader = new StreamReader(filePath))
            {
                return reader.ReadToEnd();
            }
        }
        catch (FileNotFoundException)
        {
            return "";
        }
        catch (Exception ex)
        {
            return "";
        }
        
    }
    public static string GetFromRes(string path)
    {
        string embeddedFilePath = path;
        string fileContent = ReadEmbeddedFile(embeddedFilePath);
        return fileContent;
    }

    static string ReadEmbeddedFile(string embeddedFilePath)
    {
        Type type = MethodBase.GetCurrentMethod().DeclaringType;
        string _namespace = type.Namespace;
        Assembly assembly = Assembly.GetExecutingAssembly();
        //如果跨程序访问或者不确定文件，这里可以判断文件流是否为null
        using (Stream stream = assembly.GetManifestResourceStream(_namespace+embeddedFilePath))
        {
            if (stream == null)
            {
                throw new Exception("Embedded文件未发现: " + embeddedFilePath);
            }

            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}