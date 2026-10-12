using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;


[RequireComponent(typeof(CharacterController))]
public class BasicPlayerMovement : MonoBehaviour
{
    [SerializeField] DialogueRunner dialogueRunner;

    [Header("Movement")]
    public float speed = 5f;
    public float acceleration = 20f;   
    public float deceleration = 12f;   

    [Header("Gravity")]
    public float gravity = -20f;
    public float groundedStickForce = -2f; 

    [SerializeField] private InputActionReference moveAction;

    private CharacterController controller;
    private Vector3 horizontalVelocity;
    private float verticalVelocity;

    void Awake()     { controller = GetComponent<CharacterController>(); }
    void OnEnable()  { moveAction.action.Enable(); }
    void OnDisable() { moveAction.action.Disable(); }

    void Update()
    {
        if (dialogueRunner.IsDialogueRunning) return;

        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 inputDir = new Vector3(input.x, 0f, input.y);
        inputDir = Vector3.ClampMagnitude(inputDir, 1f);

        Vector3 targetVelocity = transform.TransformDirection(inputDir) * speed;

        float rate = inputDir.sqrMagnitude > 0.01f ? acceleration : deceleration;
        horizontalVelocity = Vector3.MoveTowards(horizontalVelocity, targetVelocity, rate * Time.deltaTime);

        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = groundedStickForce;
        else
            verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity = horizontalVelocity + Vector3.up * verticalVelocity;
        controller.Move(velocity * Time.deltaTime);
    }
}