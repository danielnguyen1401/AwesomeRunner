using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private int levelLength = 200;
    [SerializeField] private int startPlatformLength = 5;
    [SerializeField] private int endPlatformLength = 5;
    [SerializeField] private int distanceBetweenPlatforms = 2;

    [SerializeField] private Transform platformPrefab, platformParent;

    [SerializeField] private Transform monsterPrefab, monsterParent;

    [SerializeField] private Transform healthCollectable, healthCollectableParent;

    [SerializeField] private float platformPositionMinY = 0f, platformPositionMaxY = 10f;

    [SerializeField] private int platformLengthMin = 1, platformLenghtMax = 4;

    [SerializeField] private float chanceForMonsterExistence = 0.25f, chanceForCollectableExistence = 0.1f;

    [SerializeField] private float healthCollectableMinY = 1f, healthCollectableMaxY = 3f;

    private float platformLastPositionX;


    private enum PlatformType
    {
        None,
        Flat,
    }

    private class PlatformPositionInfo
    {
        public PlatformType platformType;
        public float positionY;
        public bool hasMonster;
        public bool hasHealthCollectable;

        public PlatformPositionInfo(PlatformType type, float posY, bool has_Monster, bool hasCollectable)
        {
            platformType = type;
            positionY = posY;
            hasMonster = has_Monster;
            hasHealthCollectable = hasCollectable;
        }
    }

    void Start()
    {
        GenerateLevel();
    }

    void FillOutPositionInfo(PlatformPositionInfo[] platformInfos)
    {
        int currentPlatformInfoIndex = 0;

        for (int i = 0; i < startPlatformLength; i++)
        {
            platformInfos[currentPlatformInfoIndex].platformType = PlatformType.Flat;
            platformInfos[currentPlatformInfoIndex].positionY = 0f;

            currentPlatformInfoIndex++;
        }

        while (currentPlatformInfoIndex < levelLength - endPlatformLength)
        {
            if (platformInfos[currentPlatformInfoIndex - 1].platformType != PlatformType.None)
            {
                currentPlatformInfoIndex++;
                continue;
            }

            float platformPositionY = Random.Range(platformPositionMinY, platformPositionMaxY);

            int platformLength = Random.Range(platformLengthMin, platformLenghtMax);

            for (int i = 0; i < platformLength; i++)
            {
                bool has_Monster = (Random.Range(0f, 1f) < chanceForMonsterExistence);
                bool has_healthCollectable = (Random.Range(0f, 1f) < chanceForCollectableExistence);

                platformInfos[currentPlatformInfoIndex].platformType = PlatformType.Flat;
                platformInfos[currentPlatformInfoIndex].positionY = platformPositionY;
                platformInfos[currentPlatformInfoIndex].hasMonster = has_Monster;
                platformInfos[currentPlatformInfoIndex].hasHealthCollectable = has_healthCollectable;

                currentPlatformInfoIndex++;

                if (currentPlatformInfoIndex > (levelLength - endPlatformLength))
                {
                    currentPlatformInfoIndex = levelLength - endPlatformLength;
                    break;
                }
            }

            for (int i = 0; i < endPlatformLength; i++)
            {
                platformInfos[currentPlatformInfoIndex].platformType = PlatformType.Flat;
                platformInfos[currentPlatformInfoIndex].positionY = 0f;

                currentPlatformInfoIndex++;
            }
        } // end while loop
    }

    void CreatePlatformFromPositionInfo(PlatformPositionInfo[] platformPositionInfos)
    {
        for (int i = 0; i < platformPositionInfos.Length; i++)
        {
            PlatformPositionInfo positionInfo = platformPositionInfos[i];

            if (positionInfo.platformType == PlatformType.None)
            {
                continue; // if none skip below code
            }

            Vector3 platformPosition = new Vector3(distanceBetweenPlatforms * i, positionInfo.positionY, 0);

            // save the position x for later use
            Transform createBlock = (Transform) Instantiate(platformPrefab, platformPosition, Quaternion.identity);

            createBlock.parent = platformParent;

            if (positionInfo.hasMonster)
            {
            }
            if (positionInfo.hasHealthCollectable)
            {
            }
        }
    }

    void GenerateLevel()
    {
        PlatformPositionInfo[] platformInfos = new PlatformPositionInfo[levelLength];

        for (int i = 0; i < platformInfos.Length; i++)
        {
            platformInfos[i] = new PlatformPositionInfo(PlatformType.None, -1f, false, false);
        }

        FillOutPositionInfo(platformInfos);
        CreatePlatformFromPositionInfo(platformInfos);  
    }
}