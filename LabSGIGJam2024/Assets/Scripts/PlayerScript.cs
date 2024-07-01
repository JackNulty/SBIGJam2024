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
    public GameObject soundCircle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsMouse();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(moveDirection.x * (moveSpeed * 1.5f), moveDirection.y * (moveSpeed * 1.5f));
            soundCircle.transform.localScale = new Vector2(14, 14);
        }
        else if (Input.GetKey(KeyCode.C))
        {
            rb.velocity = new Vector2(moveDirection.x * (moveSpeed * 0.5f), moveDirection.y * (moveSpeed * 0.5f));
            soundCircle.transform.localScale = new Vector2(5, 5);
        }
        else
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
            soundCircle.transform.localScale = new Vector2(9, 9);
        }
        if (Input.GetKey(KeyCode.W) == false && Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.S) == false && Input.GetKey(KeyCode.D) == false)
        {
            soundCircle.transform.localScale = new Vector2(3, 3);
        }
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

    private void RotateTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - playerPos.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3f);
    }

}
