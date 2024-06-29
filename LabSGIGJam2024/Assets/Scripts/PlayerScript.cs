using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    //Input variables 
    private InputAction moveInput;
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 moveDirection = Vector2.zero;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void OnEnable()
    {
        moveInput = GetComponent<PlayerInput>().actions.FindAction("Move");

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
