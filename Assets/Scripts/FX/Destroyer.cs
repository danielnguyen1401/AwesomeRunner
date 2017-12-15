using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private float timer = 1.8f;

    void Start()
    {
        Destroy(gameObject, timer);
    }

    void Update()
    {
    }
}