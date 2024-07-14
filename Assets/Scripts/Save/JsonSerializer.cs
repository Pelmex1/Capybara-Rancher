using System.IO;
using UnityEngine;
namespace CapybaraRancher.Save
{
  public class JSONSerializer
  {
    public static T Load<T>(string path) where T : class
    {
      if (PathExists($"Save/{path}.json"))
      {
        return JsonUtility.FromJson<T>(File.ReadAllText($"Save/{path}.json"));
      }
      return null;
    }

    public static void Save<T>(string path, T data) where T : class
    {
      File.WriteAllText($"Save/{path}.json", JsonUtility.ToJson(data));
    }
    public static bool PathExists(string path)
    {
      FileStream file = null;
      try
      {
        file = new(path, FileMode.Open);
        file.Close();
        return true;
      }
      catch
      {
        return false;
      }
    }
  }
}
