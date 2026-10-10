using UnityEngine;

public class TriggerZoneGizmo : MonoBehaviour
{
    [SerializeField] Color gizmoColor;

    void OnDrawGizmos()
    {
        var box = GetComponent<BoxCollider>();
        if (box == null) return;

        Gizmos.matrix = transform.localToWorldMatrix;

        Color fill = gizmoColor;
        fill.a = gizmoColor.a;                   

        Color outline = gizmoColor;       
        outline.a = 1f;                   

        Gizmos.color = fill;
        Gizmos.DrawCube(box.center, box.size);

        Gizmos.color = outline;
        Gizmos.DrawWireCube(box.center, box.size);
    }
}