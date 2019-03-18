using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

/// <summary>
/// Class<c>Badge</c>
/// Badge object.
/// </summary>
[Serializable]
public class Badge
{
    public string name;
    public string id;
    public string image;

    /// <summary>
    /// Deserialize badge form a JSON string. 
    /// </summary>
    /// <param name="jsonString">Input JSON</param>
    /// <returns>A badge object which set fields as note in JSON.</returns>
    public static Badge CreatFromJson(string jsonString)
    {
        Badge badge = new Badge();
     
        var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)); 
        var deserializer = new DataContractJsonSerializer(badge.GetType());
        badge = deserializer.ReadObject(ms) as Badge;
        ms.Close();

        return badge;
    }
}