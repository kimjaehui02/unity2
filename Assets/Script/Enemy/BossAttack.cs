using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{

    [SerializeField]
    private GameObject Player;

    public void NormalBossAttack(int BossAtkDamage)
    {
        int CurrentPlayerHp = Player.GetComponent<PlayerStateManager>().GetPlayerCurrentHp();
        if (CurrentPlayerHp < 0)
        {
            Player.SetActive(false);
            
            return;
        }
        Player.GetComponent<PlayerStateManager>().SetPlayerCurrentHp(CurrentPlayerHp-BossAtkDamage);
    }
   public void SetPlayer(GameObject PlayerObject)
    {
        Player = PlayerObject;
    }
}
