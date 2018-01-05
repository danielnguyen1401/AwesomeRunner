using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDamageShoot : MonoBehaviour
{
    [SerializeField] private Transform playerBullet;

    [SerializeField] float distanceBeforeNewPlatform = 120f;
    [SerializeField] private Button shootButton;

    private LevelGenerator levelGenerator;

//    private LevelGeneratorPooling levelGeneratorPooling;
    [HideInInspector] public bool canShoot = false;

    void Awake()
    {
        levelGenerator = GameObject.Find(Tags.LEVEL_GENERATOR_OBJ).GetComponent<LevelGenerator>();
//        levelGeneratorPooling = GameObject.Find(Tags.LEVEL_GENERATOR_OBJ).GetComponent<LevelGeneratorPooling>();

        shootButton.onClick.AddListener(() => Shoot());
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
//        Fire();
    }

    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (canShoot)
            {
                Vector3 bulletPos = transform.position;
                bulletPos.x += 1f;
                bulletPos.y += 1.5f;
                Transform newBullet = (Transform) Instantiate(playerBullet, bulletPos, Quaternion.identity);
                newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1500f);
                newBullet.parent = transform;
            }
        }
    }

    public void Shoot()
    {
        if (canShoot)
        {
            Vector3 bulletPos = transform.position;
            bulletPos.x += 1f;
            bulletPos.y += 1.5f;
            Transform newBullet = (Transform) Instantiate(playerBullet, bulletPos, Quaternion.identity);
            newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1500f);
            newBullet.parent = transform;
        }
    }

    void OnTriggerEnter(Collider target)
    {
        if (target.tag == Tags.MONSTER_BULLET_TAG)
        {
            // inform Game controller that player has died
            GameplayController.instance.TakeDamage(1);
        }
        if (target.tag == Tags.BOUND_TAG)
        {
            Destroy(gameObject);
        }
        if (target.tag == Tags.HEALTH_TAG)
        {
            // inform Game controller that player collect health
            GameplayController.instance.IncremenHealth(1);
            target.gameObject.SetActive(false);
        }
        if (target.tag == Tags.MORE_PLATFORM_TAG)
        {
            Vector3 temp = transform.position;
            temp.x += distanceBeforeNewPlatform;
            target.transform.position = temp;

            levelGenerator.GenerateLevel(false);
//            levelGeneratorPooling.PoolingPlatforms();
        }
    }

    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == Tags.MONSTER_TAG)
        {
            GameplayController.instance.TakeDamage(1);
//            Destroy(gameObject);
        }
    }
}