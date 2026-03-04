using UnityEngine;
using System.Collections;

public class UIMessageTrigger : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1.0f;

    void Start()
    {
        // This handles the "Thanks for playing" when returning to menu
        if (GameState.ShowThanksMessage) 
        {
            ShowMessage();
            GameState.ShowThanksMessage = false; 
        }
        else
        {
            // Ensure it's invisible if not triggered
            canvasGroup.alpha = 0;
        }
    }

    // This handles the Double Jump item or any manual trigger
    public void ShowMessage()
    {
        StopAllCoroutines(); 
        StartCoroutine(FadeSequence());
    }

    IEnumerator FadeSequence()
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / fadeDuration;
            yield return null;
        }

        yield return new WaitForSeconds(2.0f);

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / fadeDuration;
            yield return null;
        }
    }
}