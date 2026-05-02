using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    bool isPaused = false;
    [HideInInspector] [SerializeField] string mainMenuSceneName;

#if UNITY_EDITOR
    [Tooltip("Drag your Main Menu scene asset here")]
    [SerializeField] SceneAsset mainMenuScene;

    private void OnValidate()
    {
        if (mainMenuScene != null)
        {
            mainMenuSceneName = mainMenuScene.name;
        }
    }
#endif

    void Update()
    {
        bool pausePresses = InputManager.Instance.Controls.Player.Pause.triggered;
        if (pausePresses)
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resumes game time
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Freezes game time
        isPaused = true;
    }

    public void LoadMainMenu()
    {
        Resume();
        SceneLoader.Instance.LoadScene(mainMenuSceneName, null);
    }
}