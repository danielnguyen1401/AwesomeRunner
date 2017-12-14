using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpPower = 10f;
    [SerializeField] float secondJumpPower = 10f;

    [SerializeField] Transform groundCheckPosition;
    [SerializeField] float radius = 0.5f;
    [SerializeField] LayerMask groundLayer;

    private Rigidbody myBody;
    private bool playerJumped;
    private bool isGrounded;
    private bool canJumpDouble;

    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }

    void Update() // FixedUpdate() is called every 4 frames
    {
        PlayerMove();
        PlayerGrounded();
        PlayerJump();
    }

    void PlayerMove()
    {
        myBody.velocity = new Vector3(movementSpeed, myBody.velocity.y, 0f);
    }

    void PlayerGrounded()
    {
        isGrounded = Physics.OverlapSphere(groundCheckPosition.position, radius, groundLayer).Length > 0;
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && canJumpDouble)
        {
            canJumpDouble = false;
            myBody.AddForce(new Vector3(0, secondJumpPower, 0));
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            myBody.AddForce(new Vector3(0, jumpPower, 0));
            playerJumped = true;
            canJumpDouble = true;
        }
    }
}