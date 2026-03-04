using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Interactable _currentInteractable;

    private void Update()
    {
        // 1. Check if InputManager exists
        if (InputManager.Instance == null || InputManager.Instance.Controls == null) return;

        // 2. Use the "Interact" trigger from your Input Action Asset
        // Replace '.Interact' with whatever you named your button in the PlayerControls asset
        bool interactPressed = InputManager.Instance.Controls.Player.Interact.triggered;

        if (_currentInteractable != null && interactPressed)
        {
            _currentInteractable.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Use TryGetComponent for better performance and cleaner code
        if (other.TryGetComponent(out Interactable interactable))
        {
            _currentInteractable = interactable;
            
            if (UIManager.Instance != null)
            {
                UIManager.Instance.ShowPrompt(interactable.promptText);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Interactable interactable) && interactable == _currentInteractable)
        {
            _currentInteractable = null;
            
            if (UIManager.Instance != null)
            {
                UIManager.Instance.HidePrompt();
            }
        }
    }
}