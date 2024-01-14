using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;
    public PlayerInputActions inputActions;

    private void Awake()
    {
        Singleton();
        Initialize();
    }

    // Ensure there's only one instance of GameInput's GameObject
    private void Singleton()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Initialize()
    {
        // Enable Input Actions
        inputActions = new();
        inputActions.Enable();

        // GameInput's GameObject is kept between scene loads
        DontDestroyOnLoad(this);
    }

    // Get PlayerInputActions' movement Vector2
    public Vector2 GetPlayer1MovementVector()
    {
        return inputActions.Player1.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetPlayer2MovementVector()
    {
        return inputActions.Player2.Movement.ReadValue<Vector2>();
    }
}
