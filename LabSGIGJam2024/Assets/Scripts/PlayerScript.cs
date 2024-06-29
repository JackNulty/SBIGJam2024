using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    //Input variables 
    private InputAction moveInput;
    private InputAction fireInput;
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 moveDirection = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        moveInput = GetComponent<PlayerInput>().actions.FindAction("Move");
        fireInput = GetComponent<PlayerInput>().actions.FindAction("Fire");

        // Subscribe to the move input event
        moveInput.performed += OnMovePerformed;
        moveInput.canceled += OnMoveCanceled;

    }

    private void OnDisable()
    {
        // Unsubscribe from the move input event
        moveInput.performed -= OnMovePerformed;
        moveInput.canceled -= OnMoveCanceled;
    }
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        // Read the move input
        moveDirection = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        // Reset the move input when it's canceled
        moveDirection = Vector2.zero;
    }

}
