using UnityEngine;

public class PlayerHealthDamageShoot : MonoBehaviour
{
    public float distanceBeforeNewPlatform = 120f;

    private LevelGenerator levelGenerator;

    void Awake()
    {
        levelGenerator = GameObject.Find(Tags.LEVEL_GENERATOR_OBJ).GetComponent<LevelGenerator>();
    }

    void Update()
    {
    }

    void OnTriggerEnter(Collider target)
    {
        if (target.tag == Tags.MORE_PLATFORM_TAG)
        {
            Vector3 temp = transform.position;
            temp.x += distanceBeforeNewPlatform;
            target.transform.position = temp;

            levelGenerator.GenerateLevel(false);
        }
    }
}