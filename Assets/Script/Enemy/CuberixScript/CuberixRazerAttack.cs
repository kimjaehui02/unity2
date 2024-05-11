using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CuberixRazerAttack : CuberixAttack
{

    public GameObject Player;

    [SerializeField]
    private int RazerAtkDmg = 10;

    [SerializeField]
    private float AttackDelayTime = 0f;
    [SerializeField]
    private float RazerAnimTime = 0.25f;
    [SerializeField]
    private float RazerRemainTime = 0.1f;
    private bool c_break = false;
    public GameObject RazerAngleZeroPoint;
    private float PlayerAngle = 0;
    [SerializeField]
    private int PlayerCurrentPos = 8;
    [SerializeField]
    private List<GameObject> CuberixRazers= new List<GameObject>();
    [SerializeField]
    private List<RazerAnimationSprite> CuberixRazersAnim= new List<RazerAnimationSprite>();
    [SerializeField]
    private int CurrentRazerAtkPos;
    [SerializeField]
    private CuberixBodyRotateAnimation CuberixHeadRotate=null;
    private bool isStop = false;
    // Start is called before the first frame update
    void Start()
    {
        InitRazers();
    }

    // Update is called once per frame
    void Update()
    {
        SetPlayerCurrentPos();
        ChangeSprite();
    }
    void SetPlayerAngle()
    {
        PlayerAngle = GetAngle(RazerAngleZeroPoint.transform.position, Player.transform.position);
    }
    void ChangeSprite()
    {
        if (isStop) return;
        CuberixHeadRotate.RotateDir(PlayerCurrentPos - 1);
    }
    private void SetPlayerCurrentPos()
    {
        SetPlayerAngle();
        if (PlayerAngle <= 101.25 && PlayerAngle > 78.75)
        {
            PlayerCurrentPos = 1;
        }
        else if (PlayerAngle <= 78.75 && PlayerAngle >= 56.25)
        {
            PlayerCurrentPos = 2;
        }
        else if (PlayerAngle <= 56.25 && PlayerAngle >= 33.75)
        {
            PlayerCurrentPos = 3;
        }
        else if (PlayerAngle <= 33.75 && PlayerAngle >= 11.25)
        {
            PlayerCurrentPos = 4;
        }
        else if (PlayerAngle <= 11.25 && PlayerAngle >= -11.25)
        {
            PlayerCurrentPos = 5;
        }
        else if (PlayerAngle <= -11.25 && PlayerAngle >= -33.75)
        {
            PlayerCurrentPos = 6;
        }
        else if (PlayerAngle <= -33.75 && PlayerAngle >= -56.25)
        {
            PlayerCurrentPos = 7;
        }
        else if (PlayerAngle <= -56.25 && PlayerAngle >= -78.75)
        {
            PlayerCurrentPos = 8;
        }
        else if (PlayerAngle <= -78.75 && PlayerAngle >= -101.25)
        {
            PlayerCurrentPos = 9;
        }
        else if (PlayerAngle <= -101.25 && PlayerAngle >= -123.75)
        {
            PlayerCurrentPos = 10;
        }
        else if (PlayerAngle <= -123.75 && PlayerAngle >= -146.25)
        {
            PlayerCurrentPos = 11;
        }
        else if (PlayerAngle <= -146.25 && PlayerAngle >= -168.75)
        {
            PlayerCurrentPos = 12;
        }
        else if (PlayerAngle <= 168.75 && PlayerAngle >= 146.25)
        {
            PlayerCurrentPos = 14;
        }
        else if (PlayerAngle <= 146.25 && PlayerAngle >= 123.25)
        {
            PlayerCurrentPos = 15;
        }
        else if (PlayerAngle <= 123.25 && PlayerAngle >= 101.25)
        {
            PlayerCurrentPos = 16;
        }
        else
        {
            PlayerCurrentPos = 13;
        }

    }
   
    override public void AttackDamage()
    {
        this.GetComponent<BossAttack>().NormalBossAttack(RazerAtkDmg);
    }

    override public void PlayAttack()
    {
        c_break = false;
        Debug.Log("aTK");
        StartCoroutine(StartRazerAttack());
    }
    override public void StopAttack()
    {
        c_break = true;
        InitRazers();
    }
    void InitRazers()
    {
        for(int i=0; i<15;i++)
        {
            //CuberixRazersAnim[i].GetComponent<SpriteRenderer>().enabled = false;
            CuberixRazers[i].GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    private float GetAngle(Vector3 vStart, Vector3 vEnd)
    {
        Vector3 v = vEnd - vStart;

        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
    private void RazerAttack()
    {
        isStop = true;
        CurrentRazerAtkPos = PlayerCurrentPos - 1;
        CuberixRazersAnim[CurrentRazerAtkPos].GetComponent<RazerAnimationSprite>().PlayRazerAnimation();
    }
    private void RazerDetect()
    {
        CuberixRazers[CurrentRazerAtkPos].GetComponent<BoxCollider2D>().enabled = true;
    }
    private void RazerDelete()
    {
        CuberixRazers[CurrentRazerAtkPos].GetComponent<BoxCollider2D>().enabled = false;

    }
    IEnumerator StartRazerAttack()
    {
        while (true)
        {
            if (c_break) break;
            SetPlayerCurrentPos();
            ChangeSprite();
            yield return new WaitForSeconds(AttackDelayTime);
            SetPlayerCurrentPos();
            ChangeSprite();
            RazerAttack();
            yield return new WaitForSeconds(RazerAnimTime);
            RazerDetect();
            yield return new WaitForSeconds(RazerRemainTime);
            RazerDelete();
            isStop = false;

        }
        yield return null;
    }
}
