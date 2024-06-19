using System.IO;
using UnityEngine;
namespace CapybaraRancher.Save
{
    public class FileEditor : MonoBehaviour
    {
        public static void DeleteFile(string path)
        {
            FileInfo fileInfo = new(path);
            if(fileInfo.Exists)
            {
                fileInfo.Delete();
            }
        }
    }
}
