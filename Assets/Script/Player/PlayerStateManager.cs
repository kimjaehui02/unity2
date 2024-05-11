using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerDataManager playerDataManager;
    private int PlayerMaxHp = 0;
    private int PlayerCurrentHp = 0;
    private int PlayerShield = 0;

    private int PlayerMaxStamina = 0;
    private int PlayerCurrentStamina = 0;

    private int PlayerMaxSpearAmount = 0;
    private int PlayerCurrentSpearAmount = 0;
    private int PlayerEquipSpearSerialNum = 0;

    private int PlayerObtainsSouls = 0;
    [SerializeField]
    private bool IsGameOver = false;

    private void Awake()
    {
        InitPlayerData();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerGameOver();
    }

    private void InitPlayerData()
    {
        PlayerMaxHp = playerDataManager.playerData.playerMaxHp;
        PlayerCurrentHp = PlayerMaxHp;

        PlayerMaxStamina = playerDataManager.playerData.playerMaxStamina;
        PlayerCurrentStamina = PlayerMaxStamina;

        PlayerMaxSpearAmount = playerDataManager.playerData.playerMaxSpearAmount;
        PlayerCurrentSpearAmount = PlayerMaxSpearAmount;
        PlayerEquipSpearSerialNum = 0;

        PlayerObtainsSouls = 0;
    }

    public int GetPlayerMaxHp()
    {
        return PlayerMaxHp;
    }
    public int GetPlayerCurrentHp()
    {
        return PlayerCurrentHp;
    }
    public int GetPlayerShield()
    {
        return PlayerShield;
    }
    public void SetPlayerShield(int Shield)
    {
        PlayerShield = Shield;
    }
    public int GetPlayerMaxStamina()
    {
        return PlayerMaxStamina;
    }

    public int GetPlayerCurrentStamina()
    {
        return PlayerCurrentStamina;
    }
    public void AddPlayerCurrentStamina(int Stamina)
    {
        PlayerCurrentStamina += Stamina;
    }
    public int GetPlayerMaxSpearAmount()
    {
        return PlayerMaxSpearAmount;
    }
    public int GetPlayerCurrentSpearAmount()
    {
        return PlayerCurrentSpearAmount;
    }
    public void SetPlayerCurrentSpearAmount(int Spears)
    {
        PlayerCurrentSpearAmount= Spears;
    }
    public int GetPlayerEquipSpearSerialNum()
    {
        return PlayerEquipSpearSerialNum;
    }
    public int GetPlayerObtainsSouls()
    {
        return PlayerObtainsSouls;
    }
    public void SetPlayerObtainsSouls(int ObtainSouls)
    {
        PlayerObtainsSouls = ObtainSouls;
    }

    public void SetPlayerCurrentHp(int Hp)
    {
        PlayerCurrentHp = Hp;
    }
    private void CheckPlayerGameOver()
    {
        if (PlayerCurrentHp <= 0)
        {
            IsGameOver = true;
            SavePlayerSouls(0);
        }
        else
        {
            IsGameOver = false;
        }
    }
    public bool GetIsPlayerGameOver()
    {
        return IsGameOver;
    }
    public void PlayerRestart()
    {
        IsGameOver = false;
    }
    public void SavePlayerSouls(int DestroySouls)
    {
        playerDataManager.playerData.playerSouls += PlayerObtainsSouls;
        //if (PlayerObtainsSouls- DestroySouls > 0)
        //{
        //    playerDataManager.playerData.playerSouls += PlayerObtainsSouls;
        //}
        PlayerObtainsSouls = 0;
        playerDataManager.SavePlayerDataToJson();
    }
}
