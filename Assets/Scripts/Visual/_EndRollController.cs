using UnityEngine;
using TMPro; // If using TextMeshPro
using System.Collections;

public class EndRollController : MonoBehaviour
{
    [SerializeField] private GameObject thanksPanel;
    [SerializeField] private CanvasGroup fadeOverlay;
    [SerializeField] private float fadeDuration = 1f;

    void Start()
    {
        // Only run if we actually just finished the game
        // We check a system-level flag or just put this script in a specific scene
        StartCoroutine(RunEndSequence());
    }

    private IEnumerator RunEndSequence()
    {
        // 1. Fade in the 'Thanks for Playing' UI
        thanksPanel.SetActive(true);
        yield return StartCoroutine(Fade(1)); // Fade to opaque

        // 2. Wait for player input or a timer
        yield return new WaitForSeconds(3f);

        // 3. Fade out and maybe show credits
        yield return StartCoroutine(Fade(0)); 
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeOverlay.alpha;
        float time = 0;
        while (time < fadeDuration)
        {
            fadeOverlay.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
        fadeOverlay.alpha = targetAlpha;
    }
}