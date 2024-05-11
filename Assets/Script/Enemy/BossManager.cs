using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BossManager : MonoBehaviour
{
    public int intOfBossHpMax;
    public int intOfBossHp;
    public float floatOfBossStaminaMax;
    public float floatOfBossStamina;


    void Start()
    {
        intOfBossHp = intOfBossHpMax;
    }

    public virtual void TakeDamage(int damage)
    {
        intOfBossHp -= damage;

        if (intOfBossHp <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // 적이 죽었을 때 실행할 코드를 작성
        Destroy(gameObject);
    }
}

