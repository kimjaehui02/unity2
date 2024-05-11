using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;

public class CuberixAttackPattern : MonoBehaviour
{
    public GameObject Boss;
    public GameObject BossGimmik;

    [SerializeField]
    private List<CuberixAttack> AttackPatternList = new List<CuberixAttack>();
    
    [SerializeField]
    private int CurrentPatternListIndex = 0;
    private int BossPhase;
    private int BossGimmikPhase;


    void Start()
    {
        BossPatternSetting();
        CuberixAttackPatternPlay();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void BossPatternSetting()
    {
        
    }
    public void CuberixAttackPatternPlay()
    {
        UpdateBossPhase();

        switch (BossGimmikPhase)
        {
            case 1:
                if (BossPhase == 1|| BossPhase == 2||BossPhase == 3)
                {
                    AttackPatternList[0].enabled = true;
                    AttackPatternList[0].PlayAttack();
                    AttackPatternList[1].StopAttack();
                    AttackPatternList[2].StopAttack();
                }
                break;
            case 2:
                if (BossPhase == 2 || BossPhase == 3)
                {
                    AttackPatternList[0].enabled = true;
                    AttackPatternList[1].enabled = true;
                    AttackPatternList[1].PlayAttack();
                    AttackPatternList[2].StopAttack();

                }
                break;
            case 3:
                if(BossPhase == 3)
                {
                    AttackPatternList[0].enabled = true;
                    AttackPatternList[1].enabled = true;
                    AttackPatternList[2].enabled = true;

                    AttackPatternList[2].PlayAttack();
                }
                break;

            case 0:
                
                AttackPatternList[0].StopAttack();
                AttackPatternList[1].StopAttack();
                AttackPatternList[2].StopAttack();

                break;
        }
    }
    private void UpdateBossPhase()
    {
        BossPhase= BossGimmik.GetComponent<CuberixGimmik>().GetBossPhase();
        BossGimmikPhase = BossGimmik.GetComponent<CuberixGimmik>().GetGimmikPhase();

    }
}

