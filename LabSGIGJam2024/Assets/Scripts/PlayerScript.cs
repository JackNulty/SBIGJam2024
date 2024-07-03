using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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
    public GameObject bulletPrefab;
    public GameObject player;
    public GameObject soundCircle;
    public Image lowSound;
    public Image normalSound;
    public Image audibleSound;
    public Image louderSound;
    public Image tooLoudSound;
    public AudioSource hurtSound;
    public static Weapons currentWeapon;
    private int currentWeaponIndex = 0; 
    private int maxWeaponIndex = 3; 
    private float scrollThreshold = 0.2f;
    private float lastScrollTime;
    int currentPlayerHealth = 100;
    static int playerHealth = 100;
    public float swingDuration = 0.7f; // Duration of the sword swing
    public float swingAngle = 90f; // Total angle of the swing
    public float startAngleOffset = -40f;
    public float swordOffsetDistance = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        normalSound.gameObject.SetActive(false);
        audibleSound.gameObject.SetActive(false);
        louderSound.gameObject.SetActive(false);
        tooLoudSound.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsMouse();
        HandleScrollInput();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(moveDirection.x * (moveSpeed * 1.5f), moveDirection.y * (moveSpeed * 1.5f));
            soundCircle.transform.localScale = new Vector2(14, 14);
            louderSound.gameObject.SetActive(true);
            normalSound.gameObject.SetActive(true);
            audibleSound.gameObject.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.C))
        {
            rb.velocity = new Vector2(moveDirection.x * (moveSpeed * 0.5f), moveDirection.y * (moveSpeed * 0.5f));
            soundCircle.transform.localScale = new Vector2(5, 5);
            normalSound.gameObject.SetActive(true);
            audibleSound.gameObject.SetActive(false);
            louderSound.gameObject.SetActive(false);
            tooLoudSound.gameObject.SetActive(false);
        }
        else
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
            soundCircle.transform.localScale = new Vector2(9, 9);
            audibleSound.gameObject.SetActive(true);
            normalSound.gameObject.SetActive(true);
            louderSound.gameObject.SetActive(false);
            tooLoudSound.gameObject.SetActive(false);
        }
        if (Input.GetKey(KeyCode.W) == false && Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.S) == false && Input.GetKey(KeyCode.D) == false)
        {
            soundCircle.transform.localScale = new Vector2(3, 3);

            normalSound.gameObject.SetActive(false);
            audibleSound.gameObject.SetActive(false);
            louderSound.gameObject.SetActive(false);
            tooLoudSound.gameObject.SetActive(false);
        }

        if (Input.GetKey(KeyCode.Alpha1) == true)
        {
            currentWeapon = Weapons.Stick;
        }
        else if (Input.GetKey(KeyCode.Alpha2) == true)
        {
            currentWeapon = Weapons.Nunchuckes;
        }
        else if (Input.GetKey(KeyCode.Alpha3) == true)
        {
            currentWeapon = Weapons.Pistol;
        }

        if(playerHealth != currentPlayerHealth)
        {
            currentPlayerHealth = playerHealth;
            playHurtSound();
        }
    }

    void HandleScrollInput()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scroll) > 0 && Time.time - lastScrollTime > scrollThreshold)
        {
            if (scroll > 0)
            {
                currentWeaponIndex = (currentWeaponIndex + 1) % (maxWeaponIndex + 1);
            }
            else if (scroll < 0)
            {
                currentWeaponIndex = (currentWeaponIndex - 1 + (maxWeaponIndex + 1)) % (maxWeaponIndex + 1);
            }

            Debug.Log("Current Weapon: " + (Weapons)currentWeaponIndex);
            currentWeapon = (Weapons)currentWeaponIndex;
            Debug.Log(currentWeapon.ToString());
            lastScrollTime = Time.time;
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
        if(currentWeapon == Weapons.Nunchuckes)
        {
            Instantiate(Nunchuckes, playerPos.position, Quaternion.identity);

            Nunchuckes weaponScriptNunchuckes = Nunchuckes.GetComponent<Nunchuckes>();
            weaponScriptNunchuckes.player = playerPos;
        }
        else if (currentWeapon == Weapons.Stick)
        {
            //Instantiate(stick, playerPos.position, Quaternion.identity, playerPos);

            //SwingMelee weaponScriptStick = stick.GetComponent<SwingMelee>();
            //weaponScriptStick.player = playerPos;
            StartCoroutine(SwingSword());
        }
        else if(currentWeapon == Weapons.Pistol)
        {
            Instantiate(bulletPrefab, gameObject.transform.position, transform.rotation);
        }
        else
        {
            return;
        }

    }

    IEnumerator SwingSword()
    {
        // Instantiate the sword
        GameObject sword = Instantiate(stick, transform.position, Quaternion.identity, transform);

        // Get the sword's transform
        Transform swordTransform = sword.transform;

        // Calculate direction from player to mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set initial rotation
        Vector3 initialRotation = new Vector3(0, 0, angle - swingAngle / 2);
        swordTransform.localEulerAngles = initialRotation;

        // Swing logic
        float swingTimer = 0f;
        while (swingTimer < swingDuration)
        {
            swingTimer += Time.deltaTime;
            float progress = swingTimer / swingDuration;
            float currentAngle = Mathf.Lerp(0, swingAngle, progress);
            swordTransform.localEulerAngles = initialRotation + new Vector3(0, 0, currentAngle);
            yield return null;
        }

        // Destroy the sword after the swing
        Destroy(sword);

    }

    private void RotateTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - playerPos.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3f);
    }

    void playHurtSound()
    {
        hurtSound.Play();
    }

    public static void damagePlayer(int t_damage)
    {
        playerHealth -= t_damage;

        if(playerHealth <= 0)
        {
            SceneManager.LoadScene("HomeVillage");
            Debug.Log("Player Died D:");
        }
    }
}
