using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusAttack : BossAttack
{
    public int floatOfDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.tag == "Player")
        {

            this.GetComponent<BossAttack>().SetPlayer(collision2D.gameObject);
            if (collision2D.GetComponent<Players>().isParry)
            {
                collision2D.GetComponent<Players>().EnemyAttack();
            }
            else
            {
                collision2D.GetComponent<Players>().EnemyAttack();
                NormalBossAttack(floatOfDamage);
            }
        }

    }
}
