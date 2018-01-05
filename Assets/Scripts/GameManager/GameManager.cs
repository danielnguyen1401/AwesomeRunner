using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector] public bool gameStartedFromMainMenu, gameRestartedPlayerDied;
    [HideInInspector] public float score, level, health;
    [HideInInspector] public bool canPlayMusic = true;

    void Awake()
    {
        MakeSingleton();
    }

    void MakeSingleton()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


}