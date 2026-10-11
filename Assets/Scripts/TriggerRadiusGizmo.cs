using UnityEngine;

public class TriggerZoneSphereGizmo : MonoBehaviour
{
    [SerializeField] Color gizmoColor;

    void OnDrawGizmos()
    {
        var sphere = GetComponent<SphereCollider>();
        
        Vector3 scale = transform.lossyScale;
        float maxScale = Mathf.Max(Mathf.Abs(scale.x), Mathf.Abs(scale.y), Mathf.Abs(scale.z));
        float radius = sphere.radius * maxScale;

        Vector3 worldCenter = transform.TransformPoint(sphere.center);
        Gizmos.matrix = Matrix4x4.TRS(worldCenter, transform.rotation, Vector3.one);

        Color fill = gizmoColor;

        Color outline = gizmoColor;
        outline.a = 1f;

        Gizmos.color = fill;
        Gizmos.DrawSphere(Vector3.zero, radius);

        Gizmos.color = outline;
        Gizmos.DrawWireSphere(Vector3.zero, radius);
    }
}