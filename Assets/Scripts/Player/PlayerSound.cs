using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private float audioFootVolume = 0.7f;
    [SerializeField] private float soundPitchRandomness = 0.05f;

    [SerializeField] private AudioClip genericFootSound;
    [SerializeField] private AudioClip metalFootSound;
    private AudioSource auSource;

    void Awake()
    {
        auSource = GetComponent<AudioSource>();
    }

//    void Update()
//    {
//    }

    public void FootSound()
    {
        // volume
        auSource.volume = audioFootVolume;

        // pitch
        auSource.pitch = Random.Range(1f - soundPitchRandomness, 1f + soundPitchRandomness);

        if (Random.Range(0, 2) > 0)
            auSource.clip = genericFootSound;
        else
            auSource.clip = metalFootSound;
//        auSource.Play();
    }
}