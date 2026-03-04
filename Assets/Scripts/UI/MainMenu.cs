using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] string sceneToLoad;
    [SerializeField] string pointToSpawn;

    public void PlayGame()
    {
        SceneLoader.Instance.LoadScene(sceneToLoad, pointToSpawn);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Exited");
    }
}
