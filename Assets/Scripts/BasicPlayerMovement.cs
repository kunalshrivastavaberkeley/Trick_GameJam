using UnityEngine;
using UnityEngine.InputSystem;

public class BasicPlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    [SerializeField] private InputActionReference moveAction;

    void OnEnable()  { moveAction.action.Enable(); }
    void OnDisable() { moveAction.action.Disable(); }

    void Update()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0f, input.y);
        transform.Translate(move * speed * Time.deltaTime);
    }
}