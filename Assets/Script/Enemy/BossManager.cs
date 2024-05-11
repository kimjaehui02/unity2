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
        // ���� �׾��� �� ������ �ڵ带 �ۼ�
        Destroy(gameObject);
    }
}

