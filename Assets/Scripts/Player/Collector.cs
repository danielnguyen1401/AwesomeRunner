using UnityEngine;

public class Collector : MonoBehaviour
{
    void OnTriggerEnter(Collider target)
    {
        if (target.tag == Tags.PLATFORM_TAG || target.tag == Tags.HEALTH_TAG || target.tag == Tags.MONSTER_TAG)
            Destroy(target.gameObject);
    }
}