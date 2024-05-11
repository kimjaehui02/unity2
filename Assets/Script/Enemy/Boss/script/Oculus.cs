using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oculus : BossStateManager
{
    // 보스의 상태를 정의할 수 있는 열거형
    public enum BossState
    {
        Idle,
        Attack,
        Move,
        // 필요한 다른 상태들 추가
    }

    public BossState currentState;

    public List<GameObject> gameObjectsOfAttacks;

    public Animator animatorOfOculus;

    public Transform transformOfPlayer;

    public float floatOfTime;


    public float floatOfDelay;


    #region 당기기
    public float attractionForce = 10f; // 끌어당김 힘

    private bool boolOfPull; // 당기는 힘
    #endregion

    public bool isBossStunned2 = false;

    void Start()
    {

        // 0초부터 시작해서 floatOfDelay 초 간격으로 RepeatDelayedAttackMethod 메서드를 호출합니다.
        StartCoroutine(SpawnObjectstart());
    }

    private void Update()
    {
        if (isBossStunned == true && isBossStunned2 == false)
        {
            isBossStunned2 = true;
            Invoke("RecoveryStunned2", 2f);
        }
    }

    private void RecoveryStunned2()
    {
        RecoveryStunned();
        isBossStunned2 = false;
    }

    void FixedUpdate()
    {
        AttractObject();
    }

    void BoxAttack()
    {

        StartCoroutine(SpawnObjects());
    }

    void CircleAttack()
    {
        StartCoroutine(SpawnObjects2());
    }

    void PullAttack()
    {
        StartCoroutine(SpawnObjects3());
    }


    public void BossToggle()
    {
        if(animatorOfOculus.gameObject.activeSelf)
        {
            animatorOfOculus.gameObject.SetActive(false);
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            animatorOfOculus.gameObject.SetActive(true);
            gameObject.GetComponent<Collider2D>().enabled = true;

        }
    }

    IEnumerator SpawnObjectstart()
    {
        int type = 0;

        type = Random.Range(0, 3);
        switch (type)
        {
            case 0:
                BoxAttack();
                break;
            case 1:
                CircleAttack();
                break;
            case 2:
                PullAttack();
                break;
        }





        yield return new WaitForSeconds(floatOfTime); // 오브젝트 생성 간격 (초)
        StartCoroutine(SpawnObjectstart());

    }



    IEnumerator SpawnObjects()
    {
        for (int i = 0; i < 4; i++)
        {
            Instantiate(gameObjectsOfAttacks[0], new Vector3(-7.5f + (5f * i), 0, 0), Quaternion.identity);

            yield return new WaitForSeconds(1.5f); // 오브젝트 생성 간격 (초)
        }
    }

    IEnumerator SpawnObjects2()
    {
        for (int i = 0; i < 4; i++)
        {
            Instantiate(gameObjectsOfAttacks[1], transformOfPlayer.position, Quaternion.identity);
            Invoke("BossToggle", 3f);
            Invoke("BossToggle", 3.6f);
            yield return new WaitForSeconds(1.5f); // 오브젝트 생성 간격 (초)
            //BossToggle();
        }
    }
    
    IEnumerator SpawnObjects3()
    {
        // gameObjectsOfAttacks[1]은 인스턴스화할 프리팹을 나타냅니다.
        GameObject instantiatedObject = Instantiate(gameObjectsOfAttacks[2], transform.position, Quaternion.identity);

        // 자신의 자식으로 추가
        instantiatedObject.transform.parent = transform;


        animatorOfOculus.SetBool("Pull", true);
        boolOfPull = true;

        yield return new WaitForSeconds(6f); // 오브젝트 생성 간격 (초)

        animatorOfOculus.SetBool("Pull", false);
        boolOfPull = false;
    }




    void AttractObject()
    {
        if(boolOfPull == false)
        {
            return;
        }

        if (transformOfPlayer != null)
        {
            Vector2 direction = transform.position - transformOfPlayer.position;
            transformOfPlayer.GetComponent<Rigidbody2D>().AddForce(direction.normalized * attractionForce);
        }
    }
}
