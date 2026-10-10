using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class TrackPlayerInZone : MonoBehaviour
{
    BoxCollider _triggerZone;
    Transform _player;
    
    public UnityEvent<bool> playerInsideChanged;
    
    public bool PlayerInside { get; private set; }
    public Vector3 LocalPosition { get; private set; }

    void Awake() => _triggerZone = GetComponent<BoxCollider>();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.transform;
            PlayerInside = true;
            playerInsideChanged.Invoke(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == _player)
        {
            _player = null;
            PlayerInside = false;
            playerInsideChanged.Invoke(false);
        }
    }

    void Update()
    {
        if (!PlayerInside) return;

        LocalPosition = transform.InverseTransformPoint(_player.position) - _triggerZone.center;
    }
    
    void OnDrawGizmos()
    {
        if (!PlayerInside || _player == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_player.position, 0.3f);
    }
}