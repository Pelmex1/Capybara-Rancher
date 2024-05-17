using UnityEngine;
using UnityEditor;

public class EnableGPUInstancing : MonoBehaviour
{
    [MenuItem("Tools/Enable GPU Instancing on All Materials")]
    static void EnableGPUInstancingOnAllMaterials()
    {
        string[] materialGUIDs = AssetDatabase.FindAssets("t:Material");

        foreach (string guid in materialGUIDs)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material material = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (material != null)
            {
                if (material.shader != null && material.shader.isSupported)
                {
                    material.enableInstancing = true;
                    EditorUtility.SetDirty(material);
                }
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("GPU Instancing enabled on all materials.");
    }
}
