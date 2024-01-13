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
        inputActions = new();
        inputActions.Enable();
    }

    public Vector2 GetMovementVector()
    {
        return inputActions.Player1.Movement.ReadValue<Vector2>();
    }
}
