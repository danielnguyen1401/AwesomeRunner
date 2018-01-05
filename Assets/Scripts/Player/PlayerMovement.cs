using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpPower = 10f;
    [SerializeField] float secondJumpPower = 10f;

    [SerializeField] Transform groundCheckPosition;
    [SerializeField] float radius = 0.5f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private GameObject smokePosition;
    [SerializeField] private Button jumpButton;

    private Rigidbody myBody;
    private bool playerJumped;
    private bool isGrounded;
    private bool canJumpDouble;
    private bool gameStarted;
    private PlayerAnimation playerAnimation;
    private BGScroller bgScroller;
    private PlayerHealthDamageShoot playerHealthDamageShoot;

    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerHealthDamageShoot = GetComponent<PlayerHealthDamageShoot>();
        smokePosition.SetActive(false);
        bgScroller = GameObject.FindGameObjectWithTag(Tags.BACKGROUND_TAG).GetComponent<BGScroller>();

        jumpButton.onClick.AddListener(() => Jump());
    }

    void Start()
    {
        StartCoroutine(StartGame());
    }

    void Update() // FixedUpdate() is called every 4 frames
    {
        if (gameStarted)
        {
            PlayerMove();
            PlayerGrounded();
            PlayerJump();
        }
    }

    void PlayerMove()
    {
        myBody.velocity = new Vector3(movementSpeed, myBody.velocity.y, 0f);
    }

    public void PlayerDied()
    {
        movementSpeed = 0;
        playerJumped = false;
        playerAnimation.PlayDie();
    }

    void PlayerGrounded()
    {
        isGrounded = Physics.OverlapSphere(groundCheckPosition.position, radius, groundLayer).Length > 0;
    }

    void Jump()
    {
        if (!isGrounded && canJumpDouble)
        {
            playerAnimation.DidJump();
            canJumpDouble = false;
            myBody.AddForce(new Vector3(0, secondJumpPower, 0));
        }
        else if (isGrounded)
        {
            playerAnimation.DidJump();
            myBody.AddForce(new Vector3(0, jumpPower, 0));
            playerJumped = true;
            canJumpDouble = true;
        }
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && canJumpDouble)
        {
            playerAnimation.DidJump();
            canJumpDouble = false;
            myBody.AddForce(new Vector3(0, secondJumpPower, 0));
        }
        else if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
            playerAnimation.DidJump();
            myBody.AddForce(new Vector3(0, jumpPower, 0));
            playerJumped = true;
            canJumpDouble = true;
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2f);
        gameStarted = true;
        playerHealthDamageShoot.canShoot = true;
        GameplayController.instance.canCountScore = true;
        bgScroller.canScroll = true;
        smokePosition.SetActive(true);
        playerAnimation.PlayRun();
    }

    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == Tags.PLATFORM_TAG)
        {
            if (playerJumped)
            {
                playerJumped = false;
                playerAnimation.DidLand();
            }
        }
    }
}