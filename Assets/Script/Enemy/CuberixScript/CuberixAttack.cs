using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuberixAttack : MonoBehaviour
{
    [SerializeField]
    private float PatternDelaytime = 0f;
    private bool isPatternEnd = false;
    [SerializeField]
    private int attackdamage = 0;
    // Start is called before the first frame update
    virtual public void AttackDamage()
    {
        this.GetComponent<BossAttack>().NormalBossAttack(attackdamage);
    }
    
    virtual public void PlayAttack()
    {
        //Debug.Log("NormalAtk");
    }
    virtual public void StopAttack()
    {
        Debug.Log("NormalAtk_Stop");

    }
 
    public void ResetAttack()
    {
        Debug.Log("NormalAtk_Reset");

    }
    protected void PatternDelay()
    {
        isPatternEnd = true;
    }
}
