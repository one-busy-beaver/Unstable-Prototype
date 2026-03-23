using UnityEngine;

public class MainMenu : MonoBehaviour
{

    [Header("UI Panels")]
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject controlsPanel;
    [SerializeField] SceneID sceneToLoad;
    [SerializeField] ExitSpawnID targetSpawnID;

    public void PlayGame()
    {
        SceneLoader.Instance.LoadScene(sceneToLoad.ToString(), targetSpawnID);
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

    public void ResetBinding()
    {
        // Tell the InputManager to wipe everything
        InputManager.Instance.ResetAllBindings();
        
        // Find all rebind buttons in the panel and tell them to refresh their labels
        RebindButton[] allButtons = controlsPanel.GetComponentsInChildren<RebindButton>();
        foreach (var btn in allButtons)
        {
            btn.RefreshUI();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Exited");
    }
}
