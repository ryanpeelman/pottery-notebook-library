using Newtonsoft.Json;

public class JsonDataWriter
{
    public void WriteToFile<T>(IEnumerable<T> data, string filePath)
    {
        var json = JsonConvert.SerializeObject(data);
        File.WriteAllText(filePath, json);
    }
}
