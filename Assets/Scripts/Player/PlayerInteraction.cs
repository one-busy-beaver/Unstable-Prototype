using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Interactable _currentInteractable;

    private void Update()
    {
        if (InputManager.Instance == null || InputManager.Instance.Controls == null) return;

        // Use the "Interact" trigger from Player Controls
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
            _currentInteractable.TogglePrompt(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Interactable interactable) && interactable == _currentInteractable)
        {
            _currentInteractable.TogglePrompt(false);
            _currentInteractable = null;
        }
    }
}