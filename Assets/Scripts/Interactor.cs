using UnityEngine;
using UnityEngine.InputSystem;
public class Interactor : MonoBehaviour
{
    [SerializeField] private InputActionReference interactKey;

    Interactable _current;

    void OnEnable()
    {
        interactKey.action.Enable();
    }

    void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponentInParent<Interactable>();
        if (interactable != null)
            _current = interactable;
    }

    void OnTriggerExit(Collider other)
    {
        var interactable = other.GetComponentInParent<Interactable>();
        if (interactable != null && interactable == _current)
            _current = null;
    }

    void Update()
    {
        if (!interactKey.action.WasPressedThisFrame()) return;

        if (_current && _current.isActiveAndEnabled)
            _current.Interact();
    }
}