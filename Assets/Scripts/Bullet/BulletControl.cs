using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;
    private float startY;

    void Start()
    {
        startY = transform.position.y;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
    }

    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.tag == Tags.MONSTER_TAG || target.tag == Tags.PLAYER_TAG || target.tag == Tags.MONSTER_BULLET_TAG || target.tag == Tags.PLAYER_BULLET_TAG)
        {
            Destroy(gameObject);
        }
    }
}