using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oculus : BossStateManager
{
    // ������ ���¸� ������ �� �ִ� ������
    public enum BossState
    {
        Idle,
        Attack,
        Move,
        // �ʿ��� �ٸ� ���µ� �߰�
    }

    public BossState currentState;

    public List<GameObject> gameObjectsOfAttacks;

    public Animator animatorOfOculus;

    public Transform transformOfPlayer;

    public float floatOfTime;


    public float floatOfDelay;


    #region ����
    public float attractionForce = 10f; // ������ ��

    private bool boolOfPull; // ���� ��
    #endregion

    public bool isBossStunned2 = false;

    void Start()
    {

        // 0�ʺ��� �����ؼ� floatOfDelay �� �������� RepeatDelayedAttackMethod �޼��带 ȣ���մϴ�.
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





        yield return new WaitForSeconds(floatOfTime); // ������Ʈ ���� ���� (��)
        StartCoroutine(SpawnObjectstart());

    }



    IEnumerator SpawnObjects()
    {
        for (int i = 0; i < 4; i++)
        {
            Instantiate(gameObjectsOfAttacks[0], new Vector3(-7.5f + (5f * i), 0, 0), Quaternion.identity);

            yield return new WaitForSeconds(1.5f); // ������Ʈ ���� ���� (��)
        }
    }

    IEnumerator SpawnObjects2()
    {
        for (int i = 0; i < 4; i++)
        {
            Instantiate(gameObjectsOfAttacks[1], transformOfPlayer.position, Quaternion.identity);
            Invoke("BossToggle", 3f);
            Invoke("BossToggle", 3.6f);
            yield return new WaitForSeconds(1.5f); // ������Ʈ ���� ���� (��)
            //BossToggle();
        }
    }
    
    IEnumerator SpawnObjects3()
    {
        // gameObjectsOfAttacks[1]�� �ν��Ͻ�ȭ�� �������� ��Ÿ���ϴ�.
        GameObject instantiatedObject = Instantiate(gameObjectsOfAttacks[2], transform.position, Quaternion.identity);

        // �ڽ��� �ڽ����� �߰�
        instantiatedObject.transform.parent = transform;


        animatorOfOculus.SetBool("Pull", true);
        boolOfPull = true;

        yield return new WaitForSeconds(6f); // ������Ʈ ���� ���� (��)

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
