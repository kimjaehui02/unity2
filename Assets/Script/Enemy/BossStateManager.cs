using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class BossStateManager : MonoBehaviour
{
    public BossData bossData;
    [SerializeField]
    protected int BossID=0;
    protected int BossMaxHp = 0;
    protected int BossCurrentHp = 0;
    protected int BossShield = 0;
   

    protected int BossMaxSouls = 0;
    protected int BossCurrentSouls = 0;
    protected int BossRemainSouls = 0;

    protected string BossKrName = null;
    protected string BossEngName = null;
    [SerializeField]
    protected bool isBossStunned = false;
    protected bool isStageClear = false;

    protected void Awake()
    {
        InitBossState();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void InitBossState()
    {
        BossMaxHp = bossData.bossMaxHp[BossID];
        BossCurrentHp = BossMaxHp;
        BossMaxSouls = bossData.bossMaxSouls[BossID];
        BossRemainSouls = bossData.bossRemainSouls[BossID];
        BossCurrentSouls = BossMaxSouls;
        BossKrName = bossData.bossKrName[BossID];
        BossEngName = bossData.bossEngName[BossID]; ;
        BossShield = 0;
       
    }
    public int GetBossMaxHp()
    { 
        return BossMaxHp;
    }
    public int GetBossCurrentHp()
    {
        return BossCurrentHp;
    }
    public void SetBossCurrentHp(int CurrentHp)
    {
        BossCurrentHp= CurrentHp;
    }
    public int GetBossShield()
    {
        return BossShield;
    }
    public void SetBossShield(int Shield)
    {
        BossShield = Shield;
    }
    public int GetBossMaxSouls()
    {
        return BossMaxSouls;
    }
    public int GetBossCurrentSouls()
    {
        return BossCurrentSouls;
    }
    public void SetBossCurrentSouls(int Souls)
    {
         BossCurrentSouls= Souls;
    }
    public string GetBossKrName()
    {
        return BossKrName;
    }
    public string GetBossEngName()
    {
        return BossEngName;
    }
    public bool GetIsBossStunned()
    {
        return isBossStunned;
    }
    
    public void SetIsBossStunned(bool StunnedState)
    {
        isBossStunned = StunnedState;
    }
    public bool GetisStageClear()
    {
        return isStageClear;
    }

    public void SetisStageClear(bool StageClear)
    {
        isStageClear = StageClear;
    }
    virtual public void BossDamaged(int PlayerAtkDamage)
    {

        if (BossShield>0)
        {
            int ReamainDamage = PlayerAtkDamage - BossShield;
            if (ReamainDamage < 0) ReamainDamage = 0;
            BossShield -= PlayerAtkDamage;
            BossCurrentHp -= ReamainDamage;
        }
        else
        {
            BossCurrentHp -= PlayerAtkDamage;
        }
    }
    public void RecoveryStunned()
    {
        if(isBossStunned) isBossStunned = false;
        BossCurrentHp = BossMaxHp;
    }
    [ContextMenu("Save")]
    void SavePlayerDataToJson()
    {
       
        string jsonData = JsonUtility.ToJson(bossData, true);
        string path = Path.Combine(Application.dataPath, "bossData.json");
        File.WriteAllText(path, jsonData);

    }
    [ContextMenu("Load")]
    void LoadPlayerDataFromJson()
    {
        string path = Path.Combine(Application.dataPath, "bossData.json");
        string jsonData = File.ReadAllText(path);
        bossData = JsonUtility.FromJson<BossData>(jsonData);
    }
    public void BossDamageCalculator()
    {
        
    }

}
[System.Serializable]
public class BossData
{
    public int[] bossMaxHp;
    public int[] bossMaxSouls;
    public int[] bossRemainSouls;
    public string [] bossKrName;
    public string[] bossEngName;
    
}
