using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuberixJumpAttack : CuberixAttack
{
    public GameObject Boss;
    public GameObject Player;
    [SerializeField]
    private int JumpAtkDmg=0;
    [SerializeField]
    private float AttackDelayTime = 0f;
   
    [SerializeField]
    private float jumpDelayTime = 1f;
 
    [SerializeField]
    private float jumpAttackAniTime = 1f;
    [SerializeField]
    private bool c_break=false;
    [SerializeField]
    private GameObject AtkCollider;
    [SerializeField]
    private GameObject BodyCollider;

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
        this.GetComponent<BossAttack>().NormalBossAttack(JumpAtkDmg);
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
        TargetPos = new Vector3(Player.transform.position.x, Player.transform.position.y, 0);
    }
    void Jump()
    {
        GetTargetPos();
        this.AtkCollider.SetActive(false);
        
        this.GetComponent<CuberixBodyMovement>().MovingBody(TargetPos+new Vector3(0,2,-5), jumpDelayTime);
        BodyCollider.SetActive(false);
    }
    void Attack()
    {
        this.GetComponent<CuberixBodyMovement>().MovingBody(TargetPos, jumpAttackAniTime);

    }
    IEnumerator StartJumpAttack()
    {
        Debug.Log("StartJumpRoutine");

        while (true)
        {
            if(c_break)
            {
                break;
            }
            BodyCollider.SetActive(true);
            yield return new WaitForSeconds(AttackDelayTime);
            Jump();
            yield return new WaitWhile(()=>this.GetComponent<CuberixBodyMovement>().GetIsMoving());
            Attack();
            yield return new WaitWhile(() => this.GetComponent<CuberixBodyMovement>().GetIsMoving());
            this.AtkCollider.SetActive(true);

        }
        Debug.Log("StopJumpRoutine");
        this.AtkCollider.SetActive(false);
        this.GetComponent<CuberixJumpAttack>().enabled = false;
        yield return null;
    }

}
