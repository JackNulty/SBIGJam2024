using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    //Input variables 
    private InputAction moveInput;
    private InputAction attackInput;
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 moveDirection = Vector2.zero;
    public Rigidbody2D rb;
    public Transform playerPos;
    public GameObject Nunchuckes;
    public GameObject stick;
    public GameObject player;

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
        attackInput = GetComponent<PlayerInput>().actions.FindAction("Fire");

        // Subscribe to the move input event
        moveInput.performed += OnMovePerformed;
        moveInput.canceled += OnMoveCanceled;

        attackInput.performed += OnAttack;
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

    private void OnAttack(InputAction.CallbackContext context)
    {
        //Instantiate(Nunchukes, playerPos.position, Quaternion.identity);

        //Nunchuckes weaponScriptNunchuckes = Nunchuckes.GetComponent<Nunchuckes>();
        //weaponScriptNunchuckes.player = playerPos;

        Instantiate(stick, playerPos.position, Quaternion.identity, playerPos);

        SwingMelee weaponScriptStick = stick.GetComponent<SwingMelee>();
        weaponScriptStick.player = playerPos;
    }

}
