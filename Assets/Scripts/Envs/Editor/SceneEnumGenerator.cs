using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

public class SceneEnumGenerator : EditorWindow
{
    // Change this path if you want the enum saved somewhere else
    private const string FilePath = "Assets/Scripts/Envs/SceneID.cs"; 

    [MenuItem("Tools/Generate Scene Enum")]
    public static void GenerateEnum()
    {
        // Get all enabled scenes from the Build Settings
        var scenes = EditorBuildSettings.scenes.Where(s => s.enabled).ToArray();

        if (scenes.Length == 0)
        {
            Debug.LogWarning("No scenes found in Build Settings! Add your scenes to File > Build Settings first.");
            return;
        }

        using (StreamWriter writer = new StreamWriter(FilePath))
        {
            writer.WriteLine("// AUTO-GENERATED FILE. DO NOT MODIFY MANUALLY.");
            writer.WriteLine("public enum SceneID");
            writer.WriteLine("{");

            foreach (var scene in scenes)
            {
                string sceneName = Path.GetFileNameWithoutExtension(scene.path);
                
                // Sanitize the string to ensure it's a valid C# enum name
                string safeName = Regex.Replace(sceneName, @"[^a-zA-Z0-9_]", "_");
                if (Regex.IsMatch(safeName, @"^[0-9]")) 
                {
                    safeName = "_" + safeName; // Enums cannot start with numbers
                }

                writer.WriteLine($"    {safeName},");
            }

            writer.WriteLine("}");
        }

        // Tell Unity to recompile so the new Enum becomes immediately available
        AssetDatabase.Refresh();
        Debug.Log($"SceneID enum successfully generated at {FilePath}!");
    }
}