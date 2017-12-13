using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    private Rigidbody myBody;

    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
    }

    void FixedUpdate() // is called every 4 frames
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        myBody.velocity = new Vector3(movementSpeed, myBody.velocity.y, 0f);
    }
}