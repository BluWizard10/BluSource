// Copyright 2024 BluWizard LABS
// 
// This is a Utility Script that creates a shortcut in the Assets Menu that directly creates a Material automatically set to Poiyomi Toon, potentially saving time.
// IF THE SHADER DOES NOT EXIST, IT WILL FADE OUT IN THE MENU AND WILL NOT WORK!
//
// Make sure to install Poiyomi Shaders from https://www.poiyomi.com for this Utility Script to be useful.

using UnityEditor;
using UnityEngine;
using System.IO;

public class PoiToonMaterialCreator
{
    // Menu Item Shortcut
    [MenuItem("Assets/Create/Material (Poiyomi Toon)", false, 301)]
    private static void CreateToonMaterialWithShader()
    {
        // Set to use Poiyomi Toon
        Shader shader = Shader.Find(".poiyomi/Poiyomi Toon");

        if (shader == null)
        {
            Debug.LogError("Shader not found! Make sure Poiyomi Shaders is correctly installed.");
            return;
        }

        // Create Material from the Shader.Find
        Material material = new Material(shader);

        // Get the selected path in the Project window, or default to "Assets" if no folder is selected
        string folderPath = GetSelectedPathOrFallback();

        // Generate a unique asset path for the Material in the selected folder
        string path = AssetDatabase.GenerateUniqueAssetPath($"{folderPath}/New Poiyomi Toon Material.mat");

        // Create the Asset at the specified path
        AssetDatabase.CreateAsset(material, path);
        AssetDatabase.SaveAssets();

        // Ping the created Material in the Project window
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = material;

        Debug.Log("Material created at: " + path);
    }

    // Checker to validate whether the Menu Item should be shown
    [MenuItem("Assets/Create/Material (Poiyomi Toon)", true)]
    private static bool ValidateCreateMaterialWithShader()
    {
        // Check if the Shader exists
        Shader shader = Shader.Find(".poiyomi/Poiyomi Toon");

        // Return true if the Shader exists, false otherwise
        return shader != null;
    }

    // Helper function to get the selected folder, or fallback to "Assets"
    private static string GetSelectedPathOrFallback()
    {
        string path = "Assets"; // Default path
        Object obj = Selection.activeObject;

        if (obj != null)
        {
            // Check if the selected object is a folder
            string selectedPath = AssetDatabase.GetAssetPath(obj);
            if (Directory.Exists(selectedPath))
            {
                path = selectedPath;
            }
            else
            {
                // If the selected object is not a folder, get its parent folder
                path = Path.GetDirectoryName(selectedPath);
            }
        }

        return path;
    }
}
