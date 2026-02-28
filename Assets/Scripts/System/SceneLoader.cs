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

        // Safety: Don't spawn players in the Bootstrap scene itself
        if (scene.name == "_Bootstrap") return;

        // 2. If targetSpawnID is empty (like when first hitting Play), 
        // look for a default spawn point instead
        if (string.IsNullOrEmpty(targetSpawnID))
        {
            // Change "DefaultSpawn" to whatever you name your starting point in the levels
            targetSpawnID = "DefaultSpawn"; 
        }

        GameObject spawnPoint = GameObject.Find(targetSpawnID);

        if (spawnPoint != null)
        {
            GameObject newPlayer = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
            
            if (Camera.main != null && Camera.main.TryGetComponent<CameraFollow>(out var follow))
            {
                follow.SetTarget(newPlayer);
            }
        }
        else
        {
            Debug.LogWarning($"SceneLoader: Could not find spawn point '{targetSpawnID}' in {scene.name}");
        }
        
        // 3. Clear the ID so it doesn't accidentally reuse an old one next time
        targetSpawnID = null;
    }

    public void LoadScene(string sceneName, string spawnPoint)
    {
        targetSpawnID = spawnPoint;
        // Async does not pause the current scene
        SceneManager.LoadSceneAsync(sceneName);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
