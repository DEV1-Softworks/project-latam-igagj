using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;

    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new();
        inputActions.Enable();
    }

    private void Update()
    {
        Vector2 movementVector = inputActions.Player1.Movement.ReadValue<Vector2>();

        if (movementVector != Vector2.zero)
        {
            transform.position += new Vector3(movementVector.x * Time.deltaTime * movementSpeed, 0f);

            if (movementVector.x < 0f)
                transform.localScale = new(-1f, 1f);
            else
                transform.localScale = new(1f, 1f);
        }
    }
}
