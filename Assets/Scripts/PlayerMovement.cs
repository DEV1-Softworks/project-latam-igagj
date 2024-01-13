using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForceMagnitude = 10f;
    [Space]
    [Header("Ground check")]
    [SerializeField] private LayerMask groundLayer = 6;

    private Vector2 movementVector;

    private Rigidbody2D rb;
    private Collider2D playerCollider;

    private void OnEnable()
    {
        GameInput.Instance.inputActions.Player1.Jump.performed += Jump_performed;
    }

    private void OnDisable()
    {
        GameInput.Instance.inputActions.Player1.Jump.performed -= Jump_performed;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        movementVector = GameInput.Instance.GetMovementVector();
    }

    private void FixedUpdate()
    {
        HandleHorizontalMovement();
    }

    // Using Unity's new Input system from GameInput's monobehaviour instance.
    private void HandleHorizontalMovement()
    {

        if (movementVector.x != 0f)
        {
            rb.position += movementSpeed * Time.fixedDeltaTime * movementVector;


            // -1 as X local scale "flips" the player to the left
            if (movementVector.x < 0f)
                transform.localScale = new(-1f, 1f);
            else
                transform.localScale = new(1f, 1f);
        }


    }

    // On Jump button pressed
    private void Jump_performed(InputAction.CallbackContext _)
    {
        if (IsGrounded())
            rb.AddForce(Vector2.up * jumpForceMagnitude);
    }

    // Check if the player is on the ground
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
    }
}
