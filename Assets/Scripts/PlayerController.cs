using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction jumpAction;
    private PlayerControls inputActions;

    private Rigidbody2D rigidbody;
    private BoxCollider2D collider;
    private Animator animator;

    private float objWidth;
    //private float objHeight;
    private Vector2 bounds;

    private bool isFacingRight = true;

    [Header("Speed Variables")]
    public float moveSpeed;
    public float jumpSpeed;

    [HideInInspector]
    public int jumpCounter = 0;
    public bool isGrounded = true;
    private int points = 0;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();

        inputActions = new PlayerControls();

        inputActions.Land.Jump.performed += Jump;
        
        //inputActions.Land.Move.started += Move;
        //jumpAction = playerInput.actions["Jump"];
        //jumpAction.ReadValue<float>();

    }

    private void OnEnable()
    {
        inputActions.Land.Enable();
    }

    private void OnDisable()
    {
        inputActions.Land.Disable();
        inputActions.Land.Jump.performed -= Jump;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        EventManager.Instance.TargetCaptured();
        SpawnerController.spawn = true;
        Destroy(other.gameObject);
    }

    void Start()
    {
        objWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        //option for lambda event subscription
        //inputActions.Land.Attack.started += context => Attack(context);
    }

    void Update()
    {
        Move();
    }

    private void LateUpdate()
    {
        //check if is out of screen
        Vector3 viewPos = transform.position;
        if (transform.position.x + objWidth > bounds.x)
        {
            viewPos.x = bounds.x - objWidth;
        }
        else if(transform.position.x - objWidth < -bounds.x)
        {
            viewPos.x = bounds.x + objWidth;
        }

        transform.position = viewPos;
    }

    public void Move()
    {
        Vector2 inputVector = inputActions.Land.Move.ReadValue<Vector2>();
        //transform.Translate(inputVector.x * moveSpeed * Time.deltaTime, 0, 0);
        //rigidbody.AddForce(inputVector * moveSpeed, ForceMode2D.Force);

        rigidbody.velocity = new Vector2(inputVector.x * moveSpeed, rigidbody.velocity.y);

        //Animations
        if (inputVector.x != 0 && isGrounded)
        {
            animator.SetBool("Run", true);
        }
        else {
            animator.SetBool("Run", false);
        }

        if (!isFacingRight && inputVector.x > 0f) Flip();
        else if (isFacingRight && inputVector.x < 0f) Flip();
    }

    private void Flip() {
        isFacingRight = !isFacingRight;
        Vector2 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Jump(InputAction.CallbackContext context) {
        if (jumpCounter < 2)
        {
            jumpCounter++;
            rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode2D.Impulse);
            if(jumpCounter == 1)
                animator.SetTrigger("Jump");
            else if(jumpCounter == 2) animator.SetTrigger("Double Jump");
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack");
        //context.ReadValue<float>();
        //context.ReadValueAsButton();
    }

    public void AddPoints(int pts)
    {
        points += pts;
    }

    public int ReturnPoints()
    {
        return points;
    }
}
