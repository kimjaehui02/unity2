using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazerAnimationSprite : MonoBehaviour
{
    private Animator animator;

    SpriteRenderer RazerSpriteRenderer;

    private float currentTime = 0f;
    [SerializeField]
    int RazerDir=0;
    int RazerAnimNum = 0;
    void Start()
    {
        animator = this.GetComponent<Animator>();
        RazerSpriteRenderer = this.GetComponent<SpriteRenderer>();

    }
    public void PlayRazerAnimation()
    {
        animator.SetInteger("Dir", RazerDir);
        animator.SetTrigger("RazerAtk");
        //RazerSpriteRenderer.enabled = true;
        //RazerAnimNum = 0;
        //currentTime = 0f;

        //while (RazerAnimNum>15)
        //{
        //    currentTime += Time.deltaTime;
        //    if (RazerSprite[RazerAnimNum] == null) break;
        //    if (currentTime > animTime)
        //    {
        //        RazerSpriteRenderer.sprite = RazerSprite[RazerAnimNum];
        //        RazerAnimNum++;
        //        currentTime = 0f;
        //    }
        //}
        //RazerSpriteRenderer.enabled = false;

    }
}
