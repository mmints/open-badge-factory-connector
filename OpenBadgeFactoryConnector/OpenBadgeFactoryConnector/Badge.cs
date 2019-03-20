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
    /// <exception cref="Exception"></exception>
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

    // Hacky code, needs refraction
    
/// <summary>
/// Saves the base64 encoded image as png in given path
/// </summary>
/// <param name="path">Path to save directory</param>
/// <exception cref="ArgumentNullException">Check if path is null</exception>
/// <exception cref="Exception">Check if image is null or empty</exception>
    public void writeImgToFile(string path)
    {
        if (path == null) throw new ArgumentNullException(nameof(path));
        if (image != null || image != "")
        {
            string replace = this.image.Replace("data:image/png;base64,", "");
        
            var data = Convert.FromBase64String(replace);
            var imageFile = new FileStream(path + this.name+".png", FileMode.Create);
            imageFile.Write(data, 0, data.Length);
            imageFile.Flush();   
        }
        else
        {
            throw new Exception("Image is null or empty! Make sure to deserialize badge from JSON.");
        }
    }
}