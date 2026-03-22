using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    private SceneID exitedSceneID;
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
        SpawnPoint[] allSpawns = Object.FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);
        SpawnPoint selectedSpawn = null;

        // Priority 1: Look for the specific ID we requested
        if (!string.IsNullOrEmpty(exitedSceneID.ToString()))
        {
            foreach (SpawnPoint sp in allSpawns)
            {
                if (sp.exitedSceneID == exitedSceneID)
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
            
            if (Camera.main != null && Camera.main.TryGetComponent<CameraFollow>(out var follow))
            {
                follow.SetTarget(newPlayer);
            }
        }
        else
        {
            Debug.Log($"SceneLoader: No SpawnPoints found in {scene.name}!");
        }

        exitedSceneID = 0; // Reset for next time
    }

    public void LoadScene(SceneID sceneToLoad, SceneID pointToSpawn)
    {
        exitedSceneID = pointToSpawn;
        // Async does not pause the current scene
        SceneManager.LoadSceneAsync(sceneToLoad.ToString());
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
