using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public PlayerAnimationManager PlayerAnimationManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.tag == "Boss")
        {
            StartCoroutine(PlayerAnimationManager.Hited(collision.gameObject));
        }


    }


}
