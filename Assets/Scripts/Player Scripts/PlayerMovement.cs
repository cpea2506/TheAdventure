using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController playerController;

    [SerializeField]
    private float movementSpeed = 5f;

    [Header("Gravity Options")]
    [SerializeField][Range(-80f, 0f)] private float gravityScale = -40f;
    [SerializeField][Range(2f, 30f)] private float jumpHeight = 5f;

    [Header("Ground Options")]
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private float groundCheckRadious = 0.2f;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Button Options")]
    [SerializeField] private string jumpButton = "Jump";

    private Vector3 movementDirection;
    private Vector3 verticalVelocity;

    private bool _canDoubleJump;
    private bool _isGrounded;

    public bool CanDoubleJump
    {
        get
        {
            return _canDoubleJump;
        }
        set
        {
            _canDoubleJump = value;
        }
    }

    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }

        set
        {
            _isGrounded = value;
        }
    }

    private PlayerAnimation playerAnimation;

    [SerializeField]
    private GameObject model;
    private Camera mainCamera;

    [SerializeField]
    private float rotateSpeed = 5f;

    private bool isKnockedBack;

    [SerializeField]
    private float knockedBackPower = 3f;

    [SerializeField]
    private float knockedBackDuration = 0.5f;

    private float knockedBackTimer;

    private bool insideThePipe = true;

    private void Awake()
    {
        playerController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        if (insideThePipe)
        {
            verticalVelocity.y = Mathf.Sqrt(-2 * jumpHeight * gravityScale);
            insideThePipe = false;
            AudioManager.instance.PlaySound(SFX.Pipe, 0.1f);
        }

        if (GameplayManager.instance.playerDied)
        {
            return;
        }

        GroundCheck();

        if (isKnockedBack)
        {
            CalculateKnockBackMotion();
        }
        else
        {
            HorizontalMovement();
            VerticalMovement();
        }

        Jump();
        Animate();
    }

    private void GroundCheck()
    {
        IsGrounded = Physics.CheckSphere(groundCheckTransform.position, groundCheckRadious, whatIsGround);
    }

    private void HorizontalMovement()
    {
        float horizontalInput = Input.GetAxis(TagManager.HORIZONTAL_AXIS);
        float verticalInput = Input.GetAxis(TagManager.VERTICAL_AXIS);

        movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection = transform.TransformDirection(movementDirection);

        playerController.Move(movementDirection * movementSpeed * Time.deltaTime);

        Rotate(horizontalInput, verticalInput);
    }

    private void VerticalMovement()
    {
        if (IsGrounded && verticalVelocity.y < 0f)
        {
            verticalVelocity.y = 0f;
        }
        else
        {
            verticalVelocity.y += gravityScale * Time.deltaTime;
        }

        playerController.Move(verticalVelocity * Time.deltaTime);
    }

    private void Rotate(float horizontalInput, float verticalInput)
    {
        if (horizontalInput != 0 || verticalInput != 0)
        {
            transform.rotation = Quaternion.Euler(0f, mainCamera.transform.rotation.eulerAngles.y, 0f);

            model.transform.rotation = Quaternion.Slerp(
                    model.transform.rotation,
                    Quaternion.LookRotation(new Vector3(movementDirection.x, 0f, movementDirection.z)),
                    rotateSpeed * Time.deltaTime);
        }
    }

    private void Jump()
    {
        if (IsGrounded && Input.GetButtonDown(jumpButton))
        {
            CanDoubleJump = true;
            verticalVelocity.y = Mathf.Sqrt(-2 * jumpHeight * gravityScale);

            AudioManager.instance.PlaySound(SFX.Jump);
        }
        else if (Input.GetButtonDown(jumpButton) && CanDoubleJump)
        {
            CanDoubleJump = false;
            verticalVelocity.y = Mathf.Sqrt(-2 * jumpHeight * gravityScale);

            AudioManager.instance.PlaySound(SFX.Jump);
        }
    }

    private void Animate()
    {
        playerAnimation.PlayRun(Mathf.Abs(movementDirection.x) + Mathf.Abs(movementDirection.z));
        playerAnimation.PlayJump(IsGrounded);
    }

    private void CalculateKnockBackMotion()
    {
        knockedBackTimer -= Time.deltaTime;
        isKnockedBack = knockedBackTimer > 0f;
        movementDirection = playerController.transform.forward * -knockedBackPower;
    }

    public void KnockBack()
    {
        isKnockedBack = true;
        knockedBackTimer = knockedBackDuration;
    }
}
