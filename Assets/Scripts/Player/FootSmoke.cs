using UnityEngine;

public class FootSmoke : MonoBehaviour
{
    [SerializeField] GameObject smokePosistion;
    [SerializeField] GameObject footSmokeFx;

    void Start()
    {
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.tag == Tags.PLATFORM_TAG)
        {
            if (smokePosistion.activeInHierarchy)
            {
                Instantiate(footSmokeFx, smokePosistion.transform.position, Quaternion.identity);
            }
        }
    }
}