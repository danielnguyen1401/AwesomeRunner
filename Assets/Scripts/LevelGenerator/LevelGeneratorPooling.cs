using UnityEngine;

public class LevelGeneratorPooling : MonoBehaviour
{
    [SerializeField] private int levelStrength = 100;
    [SerializeField] private Transform platform, platformParent;
    [SerializeField] private Transform monster, monsterParent;
    [SerializeField] private Transform health, healthParent;
    [SerializeField] private float chanceForMonsterExistence = 0.5f;
    [SerializeField] private float chanceForHealthExistence = 0.2f;
    [SerializeField] private float healthCollectableMinY = 0.1f, healthCollectableMaxY = 1f;

    [SerializeField] float Min_Position_Y = 1f, Max_Position_Y = 4f;
    [SerializeField] private float distance_Between_Platforms = 15f;
    private Transform[] platformArray;
    private float platformLastPositionX;
    private float platformPositionY;
    private Vector3 platformPosition;

    void Start()
    {
        CreatPlatforms();
    }

    void CreatPlatforms()
    {
        platformArray = new Transform[levelStrength];

        for (int i = 0; i < platformArray.Length; i++)
        {
            Transform newPlatform = (Transform) Instantiate(platform, Vector3.zero, Quaternion.identity);
            platformArray[i] = newPlatform;
        }
        for (int i = 0; i < platformArray.Length; i++)
        {
            platformPositionY = Random.Range(Min_Position_Y, Max_Position_Y);

            if (i < 2)
            {
                platformPositionY = 0f;
            }
            platformPosition = new Vector3(distance_Between_Platforms * i, platformPositionY, 0f);
            platformLastPositionX = platformPosition.x;

            platformArray[i].position = platformPosition;
            platformArray[i].parent = platformParent;

            SpawnHealthAndMonster(platformPosition, i, true);
        }
    }

    public void PoolingPlatforms()
    {
        for (int i = 0; i < platformArray.Length; i++)
        {
            if (!platformArray[i].gameObject.activeInHierarchy)
            {
                platformArray[i].gameObject.SetActive(true);
                platformPositionY = Random.Range(Min_Position_Y, Max_Position_Y);
                platformPosition =
                    new Vector3(distance_Between_Platforms + platformLastPositionX, platformPositionY, 0);

                platformArray[i].position = platformPosition;
                platformLastPositionX = platformPosition.x;

                SpawnHealthAndMonster(platformPosition, i, false);
            }
        }
    }

    void SpawnHealthAndMonster(Vector3 platformPosition, int i, bool gameStarted)
    {
        if (i > 2)
        {
            if (Random.Range(0f, 1f) < chanceForMonsterExistence)
            {
                if (gameStarted)
                {
                    platformPosition = new Vector3(distance_Between_Platforms * i, platformPosition.y + 0.1f, 0);
                }
                else
                {
                    platformPosition = new Vector3(distance_Between_Platforms + platformLastPositionX,
                        platformPosition.y + 0.1f, 0);
                }

                Transform createMonster = (Transform) Instantiate(monster, platformPosition,
                    Quaternion.Euler(0, -90, 0));
                createMonster.parent = monsterParent;
            }

            if (Random.Range(0f, 1f) < chanceForHealthExistence)
            {
                if (gameStarted)
                {
                    platformPosition = new Vector3(distance_Between_Platforms * i,
                        platformPosition.y + Random.Range(healthCollectableMinY, healthCollectableMaxY), 0);
                }
                else
                {
                    platformPosition = new Vector3(distance_Between_Platforms + platformLastPositionX,
                        platformPosition.y + Random.Range(healthCollectableMinY, healthCollectableMaxY), 0);
                }

                Transform createHealthCollectable = (Transform) Instantiate(health,
                    platformPosition, Quaternion.identity);
                createHealthCollectable.parent = healthParent;
            }
        }
    }
}