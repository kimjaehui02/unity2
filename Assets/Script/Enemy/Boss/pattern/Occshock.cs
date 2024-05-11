using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Occshock : MonoBehaviour
{
    //public Oculus Oculus;

    public float floatOfStartShockTime; // �ʱ� ������Ʈ�� ������ �ð� (��: 5��)
    public float floatOfShockTime; // �ʱ� ������Ʈ�� ������ �ð� (��: 5��)

    public float floatOfDelay;


    public GameObject gameObjectOfEffect;
    public GameObject gameObjectOfEffect2;

    public Collider2D myCollider; // �ڽ��� 2D �ݶ��̴�

    public bool boolOfStay;

    public float timer;
    public float timer2;
    public float interval;

    private void Start()
    {
        Invoke("Gone", 6f);
    }

    void Update()
    {
        ShockTime();
        StayShockTime();
    }


    /// <summary>
    /// �ð��� ������ �ݶ��̴��� Ű�� ó���� �մϴ�
    /// </summary>
    private void ShockTime()
    {

        if (boolOfStay == true)
        {
            return;
        }

        // floatOfShockTime�� 0 ������ �� �ݶ��̴��� Ȱ��ȭ�ϰ� �Լ� ȣ��
        if (floatOfShockTime <= floatOfStartShockTime)
        {
            // floatOfShockTime ���� ���ҽ�Ų��.
            floatOfShockTime += Time.deltaTime;
        }


        // floatOfShockTime�� 0 ������ �� �ݶ��̴��� Ȱ��ȭ�ϰ� �Լ� ȣ��
        if (floatOfShockTime >= floatOfStartShockTime)
        {
            // �ݶ��̴� Ȱ��ȭ
            myCollider.enabled = true;
            if (gameObjectOfEffect2 != null)
            {
                gameObjectOfEffect2.SetActive(true);
            }
            Invoke("Gone", floatOfDelay);

        }
        gameObjectOfEffect.transform.localScale = Vector3.one * (floatOfShockTime / floatOfStartShockTime);
    }

    private void StayShockTime()
    {
        if (boolOfStay == false)
        {
            return;
        }
        // �� �����Ӹ��� ����Ǵ� �ڵ�

        // ��� �ð��� ����
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;

        // ���� ���ݸ��� �Լ� ȣ��
        if (timer >= interval)
        {
            myCollider.enabled = false;
            timer = 0.0f;   // Ÿ�̸� �ʱ�ȭ
        }
        else
        {
            myCollider.enabled = true;
        }
        transform.localScale = Vector3.one * (timer2/3);
    }

    private void Gone()
    {

        Destroy(gameObject);
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        Debug.Log(1);

    }
}