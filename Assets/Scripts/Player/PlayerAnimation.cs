using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] GameObject smokeFx;
    private Animation anim;

    void Awake()
    {
        anim = GetComponent<Animation>();
    }

    void Update()
    {
    }

    public void DidJump()
    {
        Instantiate(smokeFx, transform.position, Quaternion.identity);
        anim.Play(Tags.PLAYER_JUMP);
        anim.PlayQueued(Tags.PLAYER_JUMP_FALL);
    }

    public void DidLand()
    {
        anim.Stop(Tags.PLAYER_JUMP_FALL);
        anim.Stop(Tags.PLAYER_JUMP_LAND);
        anim.Blend(Tags.PLAYER_JUMP_LAND, 0);
        anim.CrossFade(Tags.PLAYER_RUN);
    }

    public void PlayRun()
    {
        anim.Play(Tags.PLAYER_RUN);
    }

    public void PlayDie()
    {
        anim.Play(Tags.PLAYER_DIE);
    }
}