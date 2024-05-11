using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuberixStateManager : BossStateManager
{
    override public void BossDamaged(int PlayerAtkDamage)
    {
        if (BossShield > 0)
        {
            int ReamainDamage = PlayerAtkDamage - BossShield;
            if (ReamainDamage < 0) ReamainDamage = 0;
            BossShield -= PlayerAtkDamage;
            BossCurrentHp -= ReamainDamage;
        }
        else if(this.GetComponentInChildren<CuberixGimmik>()!=null
            && this.GetComponentInChildren<CuberixGimmik>().GetIsGimmik()
            && !this.GetComponentInChildren<CuberixGimmik>().GetIsShield()
            )
        {
            this.GetComponentInChildren<CuberixGimmik>().GimmikTranslateBossBody();
            this.GetComponentInChildren<CuberixGimmik>().GimmikBossDamaged();

        }
        else 
        { 
            BossCurrentHp -= PlayerAtkDamage;
        }
    }
}
