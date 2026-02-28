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

        // 1. Find every SpawnPoint component in the new scene
        SpawnPoint[] allSpawns = Object.FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);
        SpawnPoint selectedSpawn = null;

        // 2. Look for the specific ID we requested
        if (!string.IsNullOrEmpty(targetSpawnID))
        {
            foreach (SpawnPoint sp in allSpawns)
            {
                if (sp.spawnID == targetSpawnID)
                {
                    selectedSpawn = sp;
                    break;
                }
            }
        }

        // 3. FALLBACK: If ID not found or empty, take the first one available
        if (selectedSpawn == null && allSpawns.Length > 0)
        {
            selectedSpawn = allSpawns[0];
        }

        // 4. Spawn the Player
        if (selectedSpawn != null)
        {
            GameObject newPlayer = Instantiate(playerPrefab, selectedSpawn.transform.position, selectedSpawn.transform.rotation);
            
            if (Camera.main != null && Camera.main.TryGetComponent<CameraFollow>(out var follow))
            {
                follow.SetTarget(newPlayer);
            }
        }
        else
        {
            Debug.LogError($"SceneLoader: No SpawnPoints found in {scene.name}!");
        }

        targetSpawnID = null; // Reset for next time
    }

    public void LoadScene(string sceneName, string spawnPoint)
    {
        targetSpawnID = spawnPoint;
        // Async does not pause the current scene
        SceneManager.LoadSceneAsync(sceneName);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
