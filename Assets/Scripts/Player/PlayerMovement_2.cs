using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_2 : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float jumpForceMagnitude = 10f;
    [Space]
    [Header("Ground check")]
    [SerializeField] private LayerMask groundLayer = 6;

    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private PlayerAnimations playerAnimations;
    private Vector2 movementVector;
    private float originalGravityScale;
    private bool isFlying;

    private void OnEnable()
    {
        GameInput.Instance.inputActions.Player2.Jump.performed += Jump_performed;
    }

    private void OnDisable()
    {
        GameInput.Instance.inputActions.Player2.Jump.performed -= Jump_performed;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        playerAnimations = GetComponent<PlayerAnimations>();
        originalGravityScale = rb.gravityScale;
    }

    private void Start()
    {
        // Start flying
        Jump();
    }

    private void Update()
    {
        movementVector = GameInput.Instance.GetPlayer2MovementVector();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    // Using Unity's new Input system from GameInput's monobehaviour instance.
    private void HandleMovement()
    {

        if (movementVector != Vector2.zero)
        {
            Vector2 velocity = Vector2.zero;
            velocity += movementVector * Time.fixedDeltaTime;

            if (isFlying)
                rb.velocity += velocity * movementSpeed;

            else
            {
                float xMovement = velocity.x * movementSpeed;
                xMovement = Mathf.Clamp(xMovement, -maxSpeed, maxSpeed);
                rb.velocity += new Vector2(xMovement, rb.velocity.y * Time.fixedDeltaTime);
            }

            playerAnimations.SetMoveBoolTransition(true);

            // -1 as X local scale "flips" the player to the left
            if (movementVector.x < 0f)
            {
                transform.localScale = new(-1f, 1f);
            }
            else
            {
                transform.localScale = new(1f, 1f);
            }

            playerAnimations.SetMoveAnimation(movementVector);
        }
        else
        {
            /*if (!isFlying && IsGrounded())
                rb.velocity = new Vector2(0f, rb.velocity.y);*/

            playerAnimations.SetMoveBoolTransition(false);
        }


    }

    // On Jump button pressed
    private void Jump_performed(InputAction.CallbackContext _)
    {
        Jump();
    }

    private void Jump()
    {
        if (IsGrounded())
            rb.AddForce(Vector2.up * jumpForceMagnitude);

        else if (rb.gravityScale == originalGravityScale)
        {
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
        }

        else
        {
            rb.gravityScale = originalGravityScale;
        }

        isFlying = rb.gravityScale == 0f;
    }

    // Check if the player is on the ground
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, 0.2f, groundLayer);
    }
}
