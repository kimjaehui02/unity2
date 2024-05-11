using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuberixBodyDmg : MonoBehaviour
{
    public CuberixAttack BoosAttack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag=="Player")
        {
            BoosAttack.AttackDamage();
        }
        if (collision.transform.tag == "NormalAtk")
        {
            if(collision.transform.GetComponentInParent<CuberixBodyMovement>()!=null) 
                collision.transform.GetComponentInParent<CuberixBodyMovement>().StopMoving();
            if (collision.transform.GetComponentInParent<BossStateManager>() != null)
                collision.transform.GetComponentInParent<BossStateManager>().BossDamaged(10);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            BoosAttack.AttackDamage();
        }
        if(collision.transform.tag =="NormalAtk")
        {
            if (collision.transform.GetComponentInParent<CuberixBodyMovement>() != null)
                collision.transform.GetComponentInParent<CuberixBodyMovement>().StopMoving();
            if (collision.transform.GetComponentInParent<BossStateManager>() != null)
                collision.transform.GetComponentInParent<BossStateManager>().BossDamaged(10);
        }
    }
}
