using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private bool oneTimeOnly;
    [SerializeField] private UnityEvent onActivated;

    public bool IsActivated { get; private set; }

    public bool CanInteract => !(oneTimeOnly && IsActivated);

    public void Interact()
    {
        if (!CanInteract) return;

        IsActivated = true;
        onActivated.Invoke();
    }
}