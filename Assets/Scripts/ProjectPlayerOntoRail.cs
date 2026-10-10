using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(TrackPlayerInZone))]
public class ProjectPlayerPositionOntoRail : MonoBehaviour
{
    [SerializeField] SplineContainer playerRail;
    [SerializeField] SplineContainer cameraRail;
    [SerializeField] Transform cameraRailTarget;

    TrackPlayerInZone _tracker;

    public float T { get; private set; }
    public Vector3 PlayerRailPoint { get; private set; }
    public Vector3 CameraRailPoint { get; private set; }
    
    public Transform CameraRailTarget => cameraRailTarget;

    void Awake()
    {
        _tracker = GetComponent<TrackPlayerInZone>();
        
    }

    void Update()
    {
        if (!_tracker.PlayerInside) return;

        SplineUtility.GetNearestPoint(playerRail.Spline, (float3)_tracker.LocalPosition,
            out float3 nearest, out float t);

        T = t;
        PlayerRailPoint = playerRail.transform.TransformPoint(nearest);
        CameraRailPoint = cameraRail.EvaluatePosition(T); // world space

        cameraRailTarget.position = CameraRailPoint;
    }

    void OnDrawGizmos()
    {
        if (_tracker == null || !_tracker.PlayerInside) return;

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(PlayerRailPoint, 0.2f);

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(CameraRailPoint, 0.2f);

        Gizmos.DrawLine(PlayerRailPoint, CameraRailPoint);
    }
}