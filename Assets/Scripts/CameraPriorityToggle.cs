using UnityEngine;
using Unity.Cinemachine;
public class CameraPriorityToggle : MonoBehaviour
{
    [SerializeField] public int activePriority;
    [SerializeField] public int inactivePriority;
    
    CinemachineCamera _camera;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = GetComponent<CinemachineCamera>();
        _camera.Priority = inactivePriority;
    }
    
    public void SetCameraActive(bool active)
    {
        _camera.Priority = active ? activePriority : inactivePriority;
    }
}
