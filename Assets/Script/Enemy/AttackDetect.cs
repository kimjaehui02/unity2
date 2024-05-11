using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetect : MonoBehaviour
{
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
                this.GetComponent<CuberixAttack>().AttackDamage();
            }
        }

    }
}
