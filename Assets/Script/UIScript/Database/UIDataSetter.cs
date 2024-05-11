using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDataSetter : MonoBehaviour
{
    #region UI Data ��
    private GameObject UIDataBase = null;
    UIDataBase UIDatabase = null;
    [SerializeField]
    private PlayerStateManager PlayerDataModel =null;
    [SerializeField]
    private BossStateManager BossDataModel =null;
    [SerializeField]
    private UIDataBase SettingDataModel =null;
    [SerializeField]
    private Players KeyInputDataModel = null;
    [SerializeField]
    private GameObject SignalDataModel = null;
    #endregion
    #region �⺻ ����

    private bool isUsingDataModel_Player = false;
    private bool isUsingDataModel_Boss= false;
    private bool isUsingDataModel_Setting= false;
    private bool isUsingDataModel_KeyInput= false;
    private bool isSignalDataModel_Setting = false;


    #endregion

    #region �⺻ �Լ�
    // Start is called before the first frame update
    void Start()
    {
        FindUIDatabaseGameObject();
        InitializeDataBase();
        CheckDataModelRefIncluded();
    }

    // Update is called once per frame
    void Update()
    {
        ScreenDataModelSetting();
        BossDataModelSetting();
        PlayerDataModelSetting();
        KeyInputDataModelSetting();
        SignalDataModelSetting();
    }
    #endregion
    private void FindUIDatabaseGameObject()
    {
        if (GameObject.Find("UIDataBase") != null)
        {
            UIDataBase = GameObject.Find("UIDataBase");
            UIDatabase = UIDataBase.GetComponent<UIDataBase>();
            Debug.Log("UIDataSetter��ũ��Ʈ�� UIDataBase �����Ϸ�");

        }
        else
        {
            Debug.Log("UIDataBase��� �̸��� ���ӿ�����Ʈ�� �������� �ʽ��ϴ�");
        }
    }
    public void InitializeDataBase()
    {
        //Setting
        UIDatabase.pAudioVolume = 0;
        UIDatabase.pScreenSize = 0;


        //Boss
        UIDatabase.pBossMaxHp = 100;
        UIDatabase.pBossCurrentHp = 50;
        UIDatabase.pBossShield = 0;
        UIDatabase.pBossName_Eng = "nuLL";
        UIDatabase.pBossName_Kr = "nuLL";
        UIDatabase.pBossSouls = 0;

        //Player
        UIDatabase.pPlayerMaxHp = 0;
        UIDatabase.pPlayerCurrentHp = 0;
        UIDatabase.pPlayerShield = 0;
        UIDatabase.pPlayerObtainSouls = 0;
        UIDatabase.pPlayerMaxStamina = 0;
        UIDatabase.pPlayerCurrentStamina = 0;

        UIDatabase.pPlayerCurrentSpearAmount = 0;
        UIDatabase.pPlayerEquipSpearSerialNum = 0;
        Debug.Log("UI DataBase �ʱ�ȭ �Ϸ�");
    }
    public bool CheckDataModelRefIncluded()
    {
        if(UIDataBase!=null&& PlayerDataModel!=null&& BossDataModel!=null&& SettingDataModel!=null&& KeyInputDataModel!= null &&SignalDataModel != null)
        {
            Debug.Log("��� DataBaseMoadel�ּҰ� ����������  �� �ֽ��ϴ�");
            isUsingDataModel_Player = true;
            isUsingDataModel_Boss = true;
            isUsingDataModel_Setting = false;
            isUsingDataModel_KeyInput = true;
            isSignalDataModel_Setting = true;
            return true;
        }else
        {
            Debug.Log("�Ϻ� Ȥ�� ��� DataBaseMoadel�ּҰ� �����Ǿ����� �ʽ��ϴ�");

            if (UIDataBase == null)
            { 
                Debug.Log("UIDataBase�ּҰ� �����Ǿ����� �ʽ��ϴ�");
            }
            else { }
            if (PlayerDataModel == null)
            {
                Debug.Log("PlayerDataModel�ּҰ� �����Ǿ����� �ʽ��ϴ�");
                isUsingDataModel_Player = false;
            }
            else { isUsingDataModel_Player = true; }
            if (BossDataModel == null)
            {
                Debug.Log("BossDataModel�ּҰ� �����Ǿ����� �ʽ��ϴ�");
                isUsingDataModel_Boss = false;
            }
            else { isUsingDataModel_Boss = true; }
            if (SettingDataModel == null)
            {
                Debug.Log("SettingDataModel�ּҰ� �����Ǿ����� �ʽ��ϴ�");
                isUsingDataModel_Setting = false;
            }
            else { isUsingDataModel_Setting = true; }
            if (KeyInputDataModel == null)
            {
                Debug.Log("KeyInputDataModel�ּҰ� �����Ǿ����� �ʽ��ϴ�");
                isUsingDataModel_KeyInput = false;
            }
            else { isUsingDataModel_KeyInput = true; }
            if (SignalDataModel== null)
            {
                Debug.Log("SignalDataModel�ּҰ� �����Ǿ����� �ʽ��ϴ�");
                isSignalDataModel_Setting = false;
            }
            else { isSignalDataModel_Setting = true; }
        }
        return false;
    }
    public void BossDataModelSetting()
    {

        if (!isUsingDataModel_Boss) return;
        //Boss
        UIDatabase.pBossMaxHp = BossDataModel.GetBossMaxHp();
        UIDatabase.pBossCurrentHp = BossDataModel.GetBossCurrentHp();
        UIDatabase.pBossShield = BossDataModel.GetBossShield();
        UIDatabase.pBossName_Eng = BossDataModel.GetBossEngName();
        UIDatabase.pBossName_Kr = BossDataModel.GetBossKrName();
        UIDatabase.pBossSouls = BossDataModel.GetBossCurrentSouls();
        UIDatabase.pisStageClear = BossDataModel.GetisStageClear();
    }
    public void PlayerDataModelSetting()
    {
        if (!isUsingDataModel_Player) return;
        
        UIDatabase.pPlayerMaxHp = PlayerDataModel.GetPlayerMaxHp();
        UIDatabase.pPlayerCurrentHp = PlayerDataModel.GetPlayerCurrentHp();
        UIDatabase.pPlayerShield = PlayerDataModel.GetPlayerShield();
        UIDatabase.pPlayerMaxStamina = PlayerDataModel.GetPlayerMaxStamina();
        UIDatabase.pPlayerCurrentStamina = PlayerDataModel.GetPlayerCurrentStamina();
        UIDatabase.pPlayerCurrentSpearAmount = PlayerDataModel.GetPlayerCurrentSpearAmount();
        UIDatabase.pPlayerEquipSpearSerialNum = PlayerDataModel.GetPlayerEquipSpearSerialNum();
        UIDatabase.pPlayerObtainSouls = PlayerDataModel.GetPlayerObtainsSouls();
        UIDatabase.pisGameOver = PlayerDataModel.GetIsPlayerGameOver();


    }
    public void ScreenDataModelSetting()
    {
        if (!isUsingDataModel_Setting) return;

        //Setting
        UIDatabase.pAudioVolume = 0;
        UIDatabase.pScreenSize = 2;

    }
    public void KeyInputDataModelSetting()
    {
        if (!isUsingDataModel_KeyInput) return;

        //Setting
        UIDatabase.pisPaused = KeyInputDataModel.GetIsPaused();
        UIDatabase.pisInventory = KeyInputDataModel.GetIsInventory();
        UIDatabase.pisInteraction = false;
        UIDatabase.pisShopping = KeyInputDataModel.GetIsShopping();


    }
    public void SignalDataModelSetting()
    {
        if (!isSignalDataModel_Setting) return;

        //Setting
        


    }
}
