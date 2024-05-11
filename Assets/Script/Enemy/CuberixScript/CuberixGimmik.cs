using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CuberixGimmik : MonoBehaviour
{
    public GameObject Boss;
    public GameObject Player;
    public GameObject GimmikAngleSettingZeroPoint;
    public GameObject GimmikAngleSettingPoint;

    public bool ChanegeGimmikPos=false;
    public bool StopGimmik = false;
    private bool isShield = false;
    [SerializeField]
    private int ShieldAmount = 30;

   [SerializeField]
    private bool IsGimmik = false;
    private float GimmikAngle = 0f;
    private float PlayerAngle = 0f;
  
   
    [SerializeField]
    private int BossGimmikPos = 0; // 0은 위, 1은 오른쪽, 2는 아래, 3은 왼쪽 
    [SerializeField]
    private int PlayerGimmikPos = 0;
    private int CurrentGimimikBody = 2; //0은 머리, 1은 구멍 두개인 몸통, 2는 구멍 세개인 몸통

    [SerializeField]
    private GameObject CuberixHead;
    [SerializeField]
    private List<GameObject> CuberixBody = new List<GameObject>();
    [SerializeField]
    private List<CuberixBodyRotateAnimation> CuberixBodySprite = new List<CuberixBodyRotateAnimation>(); //0은 머리, 1은 구멍 두개인 몸통, 2는 구멍 세개인 몸통
    [SerializeField]
    private List<GameObject> CuberixBodyCollison = new List<GameObject>();
    [SerializeField]
    private List<GameObject> CuberixAtkCollison = new List<GameObject>();
    [SerializeField]
    private float GimmikMoveDelayTime = 1f;
    [SerializeField]
    private float BossStunnedTime = 1f;
    
    [SerializeField]
    private int BossPhase = 1;
    [SerializeField]
    private int GimmikPhase = 1;
    private Vector3 FirstLocation;
    private Vector3 SecondLocation;
    private Vector3 ThirdLocation;
    private CuberixGimmikLight GimmikLight = null;

    // Start is called before the first frame update
    void Start()
    {
        InitAllGimmik();

    }

    // Update is called once per frame
    void Update()
    {
        CheckBossShield();
        ChangeBossPhase();
        CheckGimmikPhase();
        Gimmik();
       
    }
    void InitAllGimmik()
    {
        FirstLocation = CuberixBody[0].transform.position;
        SecondLocation = CuberixBody[1].transform.position;
        ThirdLocation = CuberixHead.transform.position;
        GimmikLight = this.GetComponentInChildren<CuberixGimmikLight>();

        ShieldRecharge();

        InitGimmikAngle();
        SetRandomGimmikPos();
        BossPhase = 1;
        GimmikPhase = 1;

    }
    void InitGimmikAngle()
    {
        GimmikAngle = GetAngle(GimmikAngleSettingZeroPoint.transform.position, GimmikAngleSettingPoint.transform.position);
    }
    void SetPlayerAngle()
    {
        PlayerAngle = GetAngle(GimmikAngleSettingZeroPoint.transform.position, Player.transform.position);
    }
    private float GetAngle(Vector3 vStart, Vector3 vEnd)
    {
        Vector3 v = vEnd - vStart;

        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
    private void SetRandomGimmikPos()
    {
        if(CurrentGimimikBody==0)
        {
            BossGimmikPos = 5;
            return;
        }
        int saveGimmik = Random.Range(0, 4);
        while (BossGimmikPos== saveGimmik)
        {
            saveGimmik = Random.Range(0, 4);    
        }
        BossGimmikPos = saveGimmik;
        SetGimmikBodySprite();
    }
    private void SetGimmikBodySprite()
    {
        CuberixBodySprite[CurrentGimimikBody].RotateDir(BossGimmikPos*4);
    }
    private void Gimmik()
    {
        if (StopGimmik)
        {
            IsGimmik = false;
            return;
        }

        SetPlayerGimmikPos();
        if (ChanegeGimmikPos)
        {
            SetRandomGimmikPos();
        }
        if (PlayerGimmikPos == BossGimmikPos)
        {
            IsGimmik = true;
        }
        else
        {
            IsGimmik = false;
        }

    }
    private void SetPlayerGimmikPos()
    {
        SetPlayerAngle();
        if (PlayerAngle<GimmikAngle&&PlayerAngle>-GimmikAngle)
        {
            PlayerGimmikPos = 1;
        }else if(PlayerAngle>=GimmikAngle&&PlayerAngle<=180f-GimmikAngle)
        {
            PlayerGimmikPos = 0;
        }
        else if (PlayerAngle <= -GimmikAngle && PlayerAngle >= -180f +GimmikAngle)
        {
            PlayerGimmikPos = 2;
        }
        else
        {
            PlayerGimmikPos = 3;
        }
    }
    public int GetGimmikPos()
    {
        return BossGimmikPos;
    }
    public bool GetIsGimmik()
    {
        return IsGimmik;
    }
    public bool GetStopGimmik()
    {
        return StopGimmik;
    }
    public bool GetIsShield()
    {
        return isShield;
    }
    public int GetBossPhase()
    {
        return BossPhase;
    }
    public int GetGimmikPhase()
    {
        return GimmikPhase;
    }
    public void SetStopGimmik(bool stopgimmik)
    {
        StopGimmik = stopgimmik;
    }
    public void GimmikTranslateBossBody()
    {
        if (!IsGimmik) return;
        if (GimmikPhase == 1)
        {
            StopGimmik = true;
            if(this.GetComponentInChildren<CuberixGimmikLight>() != null) GimmikLight.HideLight();
            Vector3 TagetLocation = CuberixBody[0].transform.position + TargetPos();
            CuberixBody[0].GetComponent<CuberixBodyMovement>().MovingBody(TagetLocation, GimmikMoveDelayTime);
           // CuberixBodyCollison[0].SetActive(false);


            CuberixBody[1].GetComponent<CuberixBodyMovement>().MovingBody(FirstLocation, GimmikMoveDelayTime);
            CuberixBodyCollison[1].SetActive(true);

            CuberixHead.GetComponent<CuberixBodyMovement>().MovingBody(SecondLocation,GimmikMoveDelayTime);

            GimmikPhase = 2;
            CurrentGimimikBody = 1;
            SetRandomGimmikPos();
            ShieldRecharge();

            StopGimmik = false;
            Boss.GetComponent<CuberixAttackPattern>().CuberixAttackPatternPlay();

        }
        else
        if(GimmikPhase==2)
        {
            StopGimmik = true;
            if (this.GetComponentInChildren<CuberixGimmikLight>() != null) GimmikLight.HideLight();
            Vector3 TagetLocation = CuberixBody[1].transform.position + TargetPos();
            CuberixBody[1].GetComponent<CuberixBodyMovement>().MovingBody(TagetLocation, GimmikMoveDelayTime);
            
            

            CuberixHead.GetComponent<CuberixBodyMovement>().MovingBody(FirstLocation, GimmikMoveDelayTime);
            CuberixBodyCollison[2].SetActive(true);

            GimmikPhase = 3;
            CurrentGimimikBody = 0;
            SetRandomGimmikPos();
            
            ShieldRecharge();

            StopGimmik = false;
            Boss.GetComponent<CuberixAttackPattern>().CuberixAttackPatternPlay();

        }
        else if(GimmikPhase==3)
        {
            StopGimmik = true;
            if (this.GetComponentInChildren<CuberixGimmikLight>() != null) GimmikLight.HideLight();

            SetRandomGimmikPos();
            
            StartCoroutine(BossStunnedPhase());
           
            StopGimmik = false;
        }

    }
    public void GimmikBossDamaged()
    {
        Boss.GetComponent<BossStateManager>().BossDamaged(100);
        ShieldRecharge();
    }
    private void ChangeBossPhase()
    {
        if(Boss.GetComponent<BossStateManager>().GetBossCurrentSouls() == 5)
        {
            BossPhase = 1;
        }
        else if((Boss.GetComponent<BossStateManager>().GetBossCurrentSouls() == 4 || Boss.GetComponent<BossStateManager>().GetBossCurrentSouls() == 3))
        {
            BossPhase = 2;
        }
        else if ((Boss.GetComponent<BossStateManager>().GetBossCurrentSouls() == 2 || Boss.GetComponent<BossStateManager>().GetBossCurrentSouls() == 1))
        {
            BossPhase = 3;
        }
    }
    public void RelocateCuberix()
    {
        CuberixBody[0].GetComponent<CuberixBodyMovement>().MovingBody(FirstLocation, GimmikMoveDelayTime);
        CuberixBodySprite[2].RotateDir(8);
        CuberixBodyCollison[0].SetActive(true);
        CuberixAtkCollison[0].SetActive(false);
        

        CuberixBody[1].GetComponent<CuberixBodyMovement>().MovingBody(SecondLocation, GimmikMoveDelayTime);
        CuberixBodySprite[1].RotateDir(8);
        CuberixBodyCollison[1].SetActive(false);
        CuberixAtkCollison[1].SetActive(false);

        CuberixHead.GetComponent<CuberixBodyMovement>().MovingBody(ThirdLocation, GimmikMoveDelayTime);
        CuberixBodySprite[0].RotateDir(8);
        CuberixBodyCollison[2].SetActive(false);

        
    }
    private Vector3 TargetPos()
    {
        switch (BossGimmikPos)
        {
            case 0:
                return new Vector3(0,-2.5f,0);
            case 1:
                return new Vector3(-3f,0,0);
            case 2:
                return new Vector3(0,2.5f,0);
            case 3:
                return new Vector3(3f,0,0);
        }
        return new Vector3(0,0,0);
    }
    private void CheckBossShield()
    {
        if(Boss.GetComponent<BossStateManager>().GetBossShield()>0)
        {
            isShield = true;
            return;
        }
        else
        {
            isShield = false;
        }
    }
    private void CheckGimmikPhase()
    {
        if (Boss.GetComponent<BossStateManager>().GetIsBossStunned()&& GimmikPhase!=0)
        {
            StartCoroutine(BossStunnedPhase());
        }
        if (Boss.GetComponent<BossStateManager>().GetBossCurrentHp() <= 0&&GimmikPhase==3&&!StopGimmik&&!isShield)
        {
            IsGimmik = true;
            GimmikTranslateBossBody();
            Boss.GetComponent<CuberixAttackPattern>().CuberixAttackPatternPlay();

        }
        else if((Boss.GetComponent<BossStateManager>().GetBossCurrentHp() <= 100 ) &&
            Boss.GetComponent<BossStateManager>().GetBossShield() <= 0 && GimmikPhase == 2 && !StopGimmik && !isShield)
        {
            IsGimmik = true;
            GimmikTranslateBossBody();
            Boss.GetComponent<CuberixAttackPattern>().CuberixAttackPatternPlay();

        }
        else if ((Boss.GetComponent<BossStateManager>().GetBossCurrentHp()<= 200)&&
            Boss.GetComponent<BossStateManager>().GetBossShield()<= 0 && GimmikPhase == 1 && !StopGimmik && !isShield)
        {
            IsGimmik = true;
            GimmikTranslateBossBody();
            Boss.GetComponent<CuberixAttackPattern>().CuberixAttackPatternPlay();
        }
    }
    private void ShieldRecharge()
    {
        if (isShield) return;

        Boss.GetComponent<BossStateManager>().SetBossShield(ShieldAmount);
        
    }
    public IEnumerator BossStunnedPhase()
    {
        GimmikPhase = 0;
        Boss.GetComponent<CuberixAttackPattern>().CuberixAttackPatternPlay();
        yield return new WaitForSeconds(BossStunnedTime);

        Boss.GetComponent<BossStateManager>().RecoveryStunned();
        yield return new WaitWhile(() => CuberixBody[0].GetComponent<CuberixBodyMovement>().GetIsMoving()||
        CuberixBody[1].GetComponent<CuberixBodyMovement>().GetIsMoving()||CuberixHead.GetComponent<CuberixBodyMovement>().GetIsMoving());
        RelocateCuberix();
        yield return new WaitWhile(() => CuberixBody[0].GetComponent<CuberixBodyMovement>().GetIsMoving() ||
        CuberixBody[1].GetComponent<CuberixBodyMovement>().GetIsMoving() || CuberixHead.GetComponent<CuberixBodyMovement>().GetIsMoving());
        ShieldRecharge();
        GimmikPhase = 1;
        CurrentGimimikBody = 2;
        SetRandomGimmikPos();
        Boss.GetComponent<CuberixAttackPattern>().CuberixAttackPatternPlay();

        yield return null;
    }
}
