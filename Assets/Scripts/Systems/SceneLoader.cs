using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    private string targetSpawnID;
    [SerializeField] private GameObject playerPrefab;
    

    private void Awake()
    {
        // Only allow one SceneLoader to exist
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        // This connects your method to the Unity Engine
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // This disconnects it when the object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the spawn point by name
        GameObject spawnPoint = GameObject.Find(targetSpawnID);

        if (spawnPoint != null)
        {
            // Create the player at the spawn point's location
            GameObject newPlayer = Instantiate(
                playerPrefab, 
                spawnPoint.transform.position, 
                Quaternion.identity);
            
            // Link the camera to this specific new player
            Camera.main.GetComponent<CameraFollow>().SetTarget(newPlayer);
        }
    }

    public void LoadScene(string sceneName, string spawnPoint)
    {
        targetSpawnID = spawnPoint;
        // Async does not pause the current scene
        SceneManager.LoadSceneAsync(sceneName);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
