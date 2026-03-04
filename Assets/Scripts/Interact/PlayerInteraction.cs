using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Interactable _currentInteractable;

    private void Update()
    {
        if (_currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            _currentInteractable.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            _currentInteractable = interactable;
            // Tell UI to show "Press E to [PromptText]"
            UIManager.Instance.ShowPrompt(interactable.promptText);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Interactable>() == _currentInteractable)
        {
            _currentInteractable = null;
            UIManager.Instance.HidePrompt();
        }
    }
}