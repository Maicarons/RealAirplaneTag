using System;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace RealAirplaneTag;



public class JsonHelper
{
    /// <summary>
    /// 将JSON字符串转换为字典。
    /// </summary>
    /// <param name="jsonString">要转换的JSON字符串。</param>
    /// <returns>转换后的字典对象。</returns>
    public static Dictionary<string, Dictionary<string, string>> ConvertJsonToDictionaryMap(string jsonString)
    {
        try
        {
            return JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonString);
        }
        catch (JsonReaderException ex)
        {
            // 处理解析异常，例如JSON格式错误
            Console.WriteLine($"解析JSON时发生错误: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            // 其他异常处理
            Console.WriteLine($"发生未知错误: {ex.Message}");
            return null;
        }
    }
    public static Dictionary<string, string> ConvertJsonToDictionaryFlightNo(string jsonString)
    {
        try
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
        }
        catch (JsonReaderException ex)
        {
            // 处理解析异常，例如JSON格式错误
            Console.WriteLine($"解析JSON时发生错误: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            // 其他异常处理
            Console.WriteLine($"发生未知错误: {ex.Message}");
            return null;
        }
    }
}