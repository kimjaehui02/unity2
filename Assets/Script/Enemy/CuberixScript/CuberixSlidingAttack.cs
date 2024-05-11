using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuberixSlidingAttack : CuberixAttack
{
    public GameObject Boss;
    public GameObject Player;
    [SerializeField]
    private int SlidingAtkDmg = 0;
    [SerializeField]
    private float AttackDelayTime = 0f;

    [SerializeField]
    private float SlidingDelayTime = 1f;
    [SerializeField]
    private float SlidingDistance = 2f;
    [SerializeField]
    private float SlideAttackAniTime = 1f;
    [SerializeField]
    private bool c_break = false;
    [SerializeField]
    private float MaxLength;
    [SerializeField]
    private Vector2 Minpos;
    [SerializeField]
    private Vector2 Maxpos;
    public GameObject AtkCollider;
    private Vector3 TargetPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    override public void AttackDamage()
    {
        this.GetComponent<BossAttack>().NormalBossAttack(SlidingAtkDmg);
    }

    override public void PlayAttack()
    {
        c_break = false;
        Debug.Log("aTTACK");
        StartCoroutine(StartJumpAttack());
    }
    override public void StopAttack()
    {
        c_break = true;
    }
   
    void GetTargetPos()
    {
        // ¿ÜºÐÁ¡
        float TargetPosX = (SlidingDistance * Player.transform.position.x - this.gameObject.transform.position.x) / (SlidingDistance - 1);
        float TargetPosY = (SlidingDistance * Player.transform.position.y - this.gameObject.transform.position.y) / (SlidingDistance - 1);
        TargetPos = new Vector3(Mathf.Clamp(TargetPosX, Minpos.x, Maxpos.x),
            Mathf.Clamp(TargetPosY, Minpos.y, Maxpos.y), 0);
        if(TargetPos.magnitude> MaxLength)
        {
            TargetPos = new Vector3(Mathf.Clamp(Player.transform.position.x, Minpos.x, Maxpos.x),
            Mathf.Clamp(Player.transform.position.y, Minpos.y, Maxpos.y), 0);
        }
       // TargetPos = Vector3.ClampMagnitude(TargetPos, MaxLength);

        
        
    }
    void Slide()
    {
        GetTargetPos();
        this.GetComponent<CuberixBodyMovement>().MovingBody(TargetPos , SlidingDelayTime);
    }
 
    IEnumerator StartJumpAttack()
    {
        Debug.Log("StartJumpRoutine");

        while (true)
        {
            if (c_break)
            {
                break;
            }
            yield return new WaitForSeconds(AttackDelayTime);
            Slide();
            yield return new WaitWhile(()=>this.GetComponent<CuberixBodyMovement>().GetIsMoving());

        }
        Debug.Log("StopJumpRoutine");
        this.AtkCollider.SetActive(false);
        this.GetComponent<CuberixSlidingAttack>().enabled = false;

        yield return null;
    }
}
