using UnityEngine;

public class BGScroller : MonoBehaviour
{
    [SerializeField] float offsetSpeed = -0.0006f;

    private Renderer myRenderer;
    [HideInInspector] public bool canScroll;

    void Awake()
    {
        myRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (canScroll)
            myRenderer.material.mainTextureOffset -= new Vector2(offsetSpeed, 0);
    }
}