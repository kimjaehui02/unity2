using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDataBase : MonoBehaviour
{
    //���� ���� 
    #region �̱��� ����(����)
    //public static UIDataBase instance = null;

    //private void SingleTonPattern()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;

    //    }
    //    else
    //    {
    //        if (instance != this)
    //        {
    //            Destroy(this.gameObject);
    //        }
    //    }
    //    DontDestroyOnLoad(this.gameObject);
    //}
    #endregion

    #region UI�����ͺ��̽� ����
    [Header("UISettingData")]
    [SerializeField]
    private int ScreenSize;
    [SerializeField]
    private int AudioVolume;
    

    [Header("PlayerData")]
    [SerializeField]
    private int PlayerMaxHp;
    [SerializeField]
    private int PlayerCurrentHp;
    [SerializeField]
    private int PlayerShield;
    [SerializeField]
    private int PlayerMaxStamina;
    [SerializeField]
    private int PlayerCurrentStamina;
    [SerializeField]
    private int PlayerMaxSpearAmount;
    [SerializeField]
    private int PlayerCurrentSpearAmount;
    [SerializeField]
    private int PlayerEquipSpearSerialNum;
    [SerializeField]
    private int PlayerObtainSouls;
    [SerializeField]
    private bool isGameOver;

    [Header("BossData")]
    [SerializeField]
    private string BossName_Eng;
    [SerializeField]
    private string BossName_Kr;
    [SerializeField]
    private int BossMaxHp;
    [SerializeField]
    private int BossCurrentHp;
    [SerializeField]
    private int BossShield;
    [SerializeField]
    private int BossSouls;
    [SerializeField]
    private int BossMaxSouls;
    [SerializeField]
    private bool isStageClear;

    [Header("KeyInput")]
    [SerializeField]
    private bool isPaused;
    [SerializeField]
    private bool isInventory;
    [SerializeField]
    private bool isInteraction;
    [SerializeField]
    private bool isSkip;
    [SerializeField]
    private bool isShopping;



    #endregion

    #region UI DB���� ������Ƽ ����
    //p(������) ����, p�� property�� ���� , �� ������ ������ �Ʒ� pAudioVolumeó�� �� ������ ������ �ټ�����, ������ UIDataBaseŬ������ Mvvm ������
    //viewmodel �κ��̱� ������ ���� ������ �𵨿��� ���� �����Ҷ� �̹� ���׳��� Ȯ���� ����. �� ������ �Ұ��� �׳� �̷� ������ �ִ� �����θ� Ȯ��
    #region UISettingData ������Ƽ
    //UISettingData Property
    public int pScreenSize
    {
        get { return ScreenSize; }
        set { ScreenSize = value; }
    }
    public int pAudioVolume
    {
        get { return AudioVolume; }
        set { if (value >= 0 && value <= 100) AudioVolume = value;
            else Debug.Log("���� ������ 0rhk 100���̰� �ƴմϴ�");
        }
    }
    #endregion
    #region PlayerData ������Ƽ
    //PlayerData Property
    public int pPlayerMaxHp
    {
        get { return PlayerMaxHp; }
        set { PlayerMaxHp = value; }
    }
    public int pPlayerCurrentHp
    {
        get { return PlayerCurrentHp; }
        set { PlayerCurrentHp = value; }
    }

    public int pPlayerMaxStamina
    {
        get { return PlayerMaxStamina; }
        set { PlayerMaxStamina = value; }
    }
    public int pPlayerCurrentStamina
    {
        get { return PlayerCurrentStamina; }
        set { PlayerCurrentStamina = value; }
    }
    public int pPlayerShield
    {
        get { return PlayerShield; }
        set { PlayerShield = value; }
    }
    public int pPlayerMaxSpearAmount
    {
        get { return PlayerMaxSpearAmount; }
        set { PlayerMaxSpearAmount = value; }
    }
    public int pPlayerCurrentSpearAmount
    {
        get { return PlayerCurrentSpearAmount; }
        set { PlayerCurrentSpearAmount = value; }
    }
    public int pPlayerEquipSpearSerialNum
    {
        get { return PlayerEquipSpearSerialNum; }
        set { PlayerEquipSpearSerialNum = value; }
    }
    public int pPlayerObtainSouls
    {
        get { return PlayerObtainSouls; }
        set { PlayerObtainSouls = value; }
    }
    public bool pisGameOver
    {
        get { return isGameOver; }
        set { isGameOver = value; }
    }
    #endregion
    #region BossData ������Ƽ
    //BossData Property
    public string pBossName_Eng
    {
        get { return BossName_Eng; }
        set { BossName_Eng = value; }
    }
    public string pBossName_Kr
    {
        get { return BossName_Kr; }
        set { BossName_Kr = value; }
    }
    public int pBossMaxHp
    {
        get { return BossMaxHp; }
        set { BossMaxHp = value; }
    }
    public int pBossCurrentHp
    {
        get { return BossCurrentHp; }
        set { BossCurrentHp = value; }
    }
    public int pBossShield
    {
        get { return BossShield; }
        set { BossShield = value; }
    }
    public int pBossSouls
    {
        get { return BossSouls; }
        set { BossSouls = value; }
    }
    public int pBossMaxSouls
    {
        get { return BossMaxSouls; }
        set { BossMaxSouls = value; }
    }
    public bool pisStageClear
    {
        get { return isStageClear; }
        set { isStageClear = value; }
    }
    #endregion
    #region KeyInput ������Ƽ
    public bool pisPaused
    {
        get { return isPaused; }
        set { isPaused = value; }
    }
    public bool pisInventory
    {
        get { return isInventory; }
        set { isInventory = value; }
    }
    public bool pisInteraction
    {
        get { return isInteraction; }
        set { isInteraction = value; }
    }
    public bool pisSkip
    {
        get { return isSkip; }
        set { isSkip = value; }
    }
    public bool pisShopping
    {
        get { return isShopping; }
        set { isShopping = value; }
    }
    #endregion
    #region Signal������Ƽ

    #endregion
    #endregion

    #region �⺻�Լ�

    private void Awake()
    {
        //SingleTonPattern();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    
}
