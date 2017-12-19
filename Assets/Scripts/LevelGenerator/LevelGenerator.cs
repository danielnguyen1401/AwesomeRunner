using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private int levelLength;

    [SerializeField] private int startPlatformLength = 5, endPlatformLength = 5;

    [SerializeField] private int distanceBetweenPlatforms;

    [SerializeField] private Transform platformPrefab, platformParent;

    [SerializeField] private Transform monster, monsterParent;

    [SerializeField] private Transform healthCollectable, healthCollectableParent;

    [SerializeField] private float platformPositionMinY = 0f, platformPositionMaxY = 10f;

    [SerializeField] private int platformLengthMin = 1, platformLengthMax = 4;

    [SerializeField] private float chanceForMonsterExistence = 0.25f, chanceForCollectbaleExistence = 0.1f;

    [SerializeField] private float healthCollectableMinY = 1f, healthCollectableMaxY = 3f;

    float platformLastPositionX;


    private enum PlatformType
    {
        None,
        Flat
    }

    private class PlatformPositionInfo
    {
        public PlatformType platfromType;
        public float positionY;
        public bool hasMonster;
        public bool hasHealthCollectable;

        public PlatformPositionInfo(PlatformType type, float posY, bool has_monster, bool has_collectable)
        {
            platfromType = type;
            positionY = posY;
            hasMonster = has_monster;
            hasHealthCollectable = has_collectable;
        }
    } // class PlatformPositionInfo

    void Start()
    {
        GenerateLevel(true);
    }

    void FillOutPositionInfo(PlatformPositionInfo[] platformInfo)
    {
        int currentPlatformInfoIndex = 0;

        for (int i = 0; i < startPlatformLength; i++)
        {
            platformInfo[currentPlatformInfoIndex].platfromType = PlatformType.Flat;
            platformInfo[currentPlatformInfoIndex].positionY = 0f;

            currentPlatformInfoIndex++;
        }

        while (currentPlatformInfoIndex < levelLength - endPlatformLength)
        {
            if (platformInfo[currentPlatformInfoIndex - 1].platfromType != PlatformType.None)
            {
                currentPlatformInfoIndex++;
                continue;
            }

            float platformPositionY = Random.Range(platformPositionMinY, platformPositionMaxY);

            int platformLength = Random.Range(platformLengthMin, platformLengthMax);


            for (int i = 0; i < platformLength; i++)
            {
                bool has_Monster = (Random.Range(0f, 1f) < chanceForMonsterExistence);
                bool has_healthCollectable = (Random.Range(0f, 1f) < chanceForCollectbaleExistence);

                platformInfo[currentPlatformInfoIndex].platfromType = PlatformType.Flat;
                platformInfo[currentPlatformInfoIndex].positionY = platformPositionY;
                platformInfo[currentPlatformInfoIndex].hasMonster = has_Monster;
                platformInfo[currentPlatformInfoIndex].hasHealthCollectable = has_healthCollectable;

                currentPlatformInfoIndex++;

                if (currentPlatformInfoIndex > (levelLength - endPlatformLength))
                {
                    currentPlatformInfoIndex = levelLength - endPlatformLength;
                    break;
                }
            }

            for (int i = 0; i < endPlatformLength; i++)
            {
                platformInfo[currentPlatformInfoIndex].platfromType = PlatformType.Flat;
                platformInfo[currentPlatformInfoIndex].positionY = 0f;

                currentPlatformInfoIndex++;
            }
        } // while loop
    }

    void CreatePlatformsFromPositionInfo(PlatformPositionInfo[] platformPositionInfo, bool gameStarted)
    {
        for (int i = 0; i < platformPositionInfo.Length; i++)
        {
            PlatformPositionInfo positionInfo = platformPositionInfo[i];

            if (positionInfo.platfromType == PlatformType.None)
            {
                continue;
            }

            Vector3 platformPosition;

            // here we are going to check if the game is started or not
            if (gameStarted)
                platformPosition = new Vector3(distanceBetweenPlatforms * i, positionInfo.positionY, 0);
            else
                platformPosition = new Vector3(distanceBetweenPlatforms + platformLastPositionX, positionInfo.positionY, 0);

            // save the platform position x for later use
            platformLastPositionX = platformPosition.x;

            Transform createBlock = (Transform) Instantiate(platformPrefab, platformPosition, Quaternion.identity);
            createBlock.parent = platformParent;

            if (positionInfo.hasMonster)
            {
            }

            if (positionInfo.hasHealthCollectable)
            {
            }
        } // for loop
    }

    public void GenerateLevel(bool gameStarted)
    {
        PlatformPositionInfo[] platformInfo = new PlatformPositionInfo[levelLength];
        for (int i = 0; i < platformInfo.Length; i++)
        {
            platformInfo[i] = new PlatformPositionInfo(PlatformType.None, -1f, false, false);
        }

        FillOutPositionInfo(platformInfo);
        CreatePlatformsFromPositionInfo(platformInfo, gameStarted);
    }
}