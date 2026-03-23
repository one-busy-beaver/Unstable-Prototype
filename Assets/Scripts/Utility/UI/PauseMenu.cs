using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] ExitSpawnID mainMenuID;
    public GameObject pauseMenuUI;
    private bool isPaused = false;

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
        SceneLoader.Instance.LoadScene(SceneID.Main_Menu.ToString(), mainMenuID);
    }
}