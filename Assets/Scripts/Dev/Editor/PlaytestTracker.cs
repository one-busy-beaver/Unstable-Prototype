using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class PlaytestTracker
{
    static PlaytestTracker()
    {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        // Right as you hit the Play button...
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            string currentScene = EditorSceneManager.GetActiveScene().name;
            
            // If we aren't already in Bootstrap, save this scene name
            if (currentScene != "_Bootstrap")
            {
                EditorPrefs.SetString("LastOpenedScene", currentScene);
            }
        }
    }
}