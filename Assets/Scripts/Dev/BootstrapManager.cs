using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapManager : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The scene to load if no playtest scene is found.")]
    [SerializeField] private string defaultScene = "MainMenu"; 

    void Start()
    {
        string sceneToLoad = defaultScene;

        #if UNITY_EDITOR
        // Retrieve the name of the scene you were looking at
        string lastScene = UnityEditor.EditorPrefs.GetString("LastOpenedScene", "");
        
        // Only use it if it's not empty and not the bootstrap scene itself
        if (!string.IsNullOrEmpty(lastScene) && lastScene != SceneManager.GetActiveScene().name)
        {
            sceneToLoad = lastScene;
        }
        #endif

        Debug.Log($"<color=green>Bootstrap:</color> Loading target scene: <b>{sceneToLoad}</b>");
        SceneManager.LoadScene(sceneToLoad);
    }
}