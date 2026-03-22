using UnityEngine;

public class SceneExit : TriggeredInteraction
{
    [Header("Transition Settings")]
    [SerializeField] private SceneID sceneToLoad;
    [SerializeField] private SceneID exitedSceneID;

    void Reset()
    {
        autoTrigger = true;
    }

    private void Awake() => GetComponent<BoxCollider2D>().isTrigger = true; // Ensure collider is set to trigger
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (autoTrigger && other.CompareTag("Player"))
        {
            Execute();
        }
    }

    public override void Execute()
    {
        if (SceneLoader.Instance != null)
            SceneLoader.Instance.LoadScene(sceneToLoad, exitedSceneID);
        else
            Debug.LogError("SceneLoader missing!");
    }
}