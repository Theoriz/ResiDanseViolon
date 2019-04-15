using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

class EditorScrips : EditorWindow
{

    [MenuItem("Play/Execute starting scene _%h")]
    public static void RunMainScene()
    {
        string currentSceneName = EditorApplication.currentScene;
        File.WriteAllText(".lastScene", currentSceneName);
        EditorApplication.OpenScene("Assets/BookTemplate/Core/Scenes/Core.unity");
        EditorApplication.isPlaying = true;
    }

    [MenuItem("Play/Reload editing scnee _%g")]
    public static void ReturnToLastScene()
    {
        string lastScene = File.ReadAllText(".lastScene");
        EditorApplication.OpenScene(lastScene);
    }
}
