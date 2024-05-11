using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDataBase : MonoBehaviour
{
    //주의 사항 
    #region 싱글톤 패턴(삭제)
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

    #region UI데이터베이스 변수
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

    #region UI DB변수 프로퍼티 묶음
    //p(변수명) 형식, p는 property의 약자 , 이 형식의 장접은 아래 pAudioVolume처럼 값 세팅의 제한을 줄수있음, 단점은 UIDataBase클래스가 Mvvm 패턴중
    //viewmodel 부분이기 때문에 실제 게임의 모델에서 값을 추출할때 이미 버그났을 확률이 높음. 즉 예방이 불가능 그냥 이런 형식이 있다 정도로만 확인
    #region UISettingData 프로퍼티
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
            else Debug.Log("볼륨 설정이 0rhk 100사이가 아닙니다");
        }
    }
    #endregion
    #region PlayerData 프로퍼티
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
    #region BossData 프로퍼티
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
    #region KeyInput 프로퍼티
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
    #region Signal프로퍼티

    #endregion
    #endregion

    #region 기본함수

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
