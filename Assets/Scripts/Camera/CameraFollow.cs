using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float cameraLerpTime = 0.05f;
    [SerializeField] float offsetX = 13f;
    [SerializeField] float offsetZ = -15f;
    [SerializeField] float constantY = 5f;
    private Transform playerTarget;

    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
    }

    void Update()
    {
        if (playerTarget)
        {
            Vector3 targetPosition = new Vector3(playerTarget.position.x + offsetX, constantY,playerTarget.position.z + offsetZ);
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraLerpTime);
        }
    }
}