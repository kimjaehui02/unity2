using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject Boss;
    private PlayerStateManager playerStateManager;
    [SerializeField]
    private bool isInAtkRange = false;
    [SerializeField]
    private int PlayerAtkDamage = 10;
    // Start is called before the first frame update
    void Start()
    {
        Init();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {   
        if(collision.transform.tag=="Boss")
        {
            isInAtkRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Boss")
        {
            isInAtkRange = false;
        }
    }

    private void Init()
    {
        playerStateManager = this.GetComponentInParent<PlayerStateManager>();
    }
    public void PlayerAtk()
    {
        if (isInAtkRange)
        {
            if(Boss.GetComponent<BossStateManager>().GetIsBossStunned())
            {
                if(playerStateManager.GetPlayerCurrentSpearAmount()>0)
                {
                    Boss.GetComponent<BossStateManager>().SetBossCurrentSouls(Boss.GetComponent<BossStateManager>().GetBossCurrentSouls() - 1);
                    playerStateManager.SetPlayerObtainsSouls(playerStateManager.GetPlayerObtainsSouls() + 1);
                    playerStateManager.SetPlayerCurrentSpearAmount(playerStateManager.GetPlayerCurrentSpearAmount() - 1);
                }
                if (Boss.GetComponent<BossStateManager>().GetBossCurrentSouls()<=0)
                {
                    Boss.GetComponent<BossStateManager>().SetisStageClear(true) ;
                    playerStateManager.SavePlayerSouls(playerStateManager.GetPlayerObtainsSouls());
                    Boss.SetActive(false);
                }
            }
            
            else if(Boss.GetComponent<BossStateManager>().GetBossCurrentHp()>0)
            {
                Boss.GetComponent<BossStateManager>().BossDamaged(PlayerAtkDamage);
                if (Boss.GetComponent<BossStateManager>().GetBossCurrentHp() <= 0)
                {
                    Boss.GetComponent<BossStateManager>().SetIsBossStunned(true);
                }
            }
        }
    }
}
