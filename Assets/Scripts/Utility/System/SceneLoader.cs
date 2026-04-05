using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    private ExitSpawnID targetSpawnID;
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

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded; // Connect the method to the Unity Engine
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded; // Disconnect it when the object is destroyed

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "_Bootstrap") return;

        // Find every SpawnPoint component in the new scene
        SpawnPoint[] allSpawns = Object.FindObjectsByType<SpawnPoint>(FindObjectsInactive.Exclude);
        SpawnPoint selectedSpawn = null;

        // Priority 1: Look for the specific ID we requested
        if (true)
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

        // Priority 2: "Respawn" Tag
        if (selectedSpawn == null)
        {
            foreach (SpawnPoint sp in allSpawns)
            {
                if (sp.CompareTag("Respawn"))
                {
                    selectedSpawn = sp;
                    break;
                }
            }
        }

        // Priority 3 (FALLBACK): If ID not found or empty, take the first one available
        if (selectedSpawn == null && allSpawns.Length > 0)
        {
            selectedSpawn = allSpawns[0];
        }

        // Spawn the Player
        if (selectedSpawn != null)
        {
            GameObject newPlayer = Instantiate(playerPrefab, selectedSpawn.transform.position, selectedSpawn.transform.rotation);
            
            CameraFollow follow = FindAnyObjectByType<CameraFollow>();
            if (follow != null)
            {
                follow.SetTarget(newPlayer);
            }
        }
        else
        {
            Debug.Log($"SceneLoader: No SpawnPoints found in {scene.name}!");
        }

        targetSpawnID = null; // Reset for next time
    }

    public void LoadScene(string sceneToLoad, ExitSpawnID pointToSpawn)
    {
        targetSpawnID = pointToSpawn;
        // Async does not pause the current scene
        SceneManager.LoadSceneAsync(sceneToLoad.ToString());
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
