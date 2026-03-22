using UnityEngine;

public class MainMenu : MonoBehaviour
{

    [Header("UI Panels")]
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject controlsPanel;
    [SerializeField] SceneID sceneToLoad;
    [SerializeField] SceneID pointToSpawn;

    public void PlayGame()
    {
        SceneLoader.Instance.LoadScene(sceneToLoad, pointToSpawn);
    }

    public void ControlPanel()
    {
        mainMenuPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void CloseControlPanel()
    {
        controlsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Exited");
    }
}
