using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Occshock : MonoBehaviour
{
    //public Oculus Oculus;

    public float floatOfStartShockTime; // 초기 오브젝트가 존재할 시간 (예: 5초)
    public float floatOfShockTime; // 초기 오브젝트가 존재할 시간 (예: 5초)

    public float floatOfDelay;


    public GameObject gameObjectOfEffect;
    public GameObject gameObjectOfEffect2;

    public Collider2D myCollider; // 자신의 2D 콜라이더

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
    /// 시간이 지나서 콜라이더를 키는 처리를 합니다
    /// </summary>
    private void ShockTime()
    {

        if (boolOfStay == true)
        {
            return;
        }

        // floatOfShockTime이 0 이하일 때 콜라이더를 활성화하고 함수 호출
        if (floatOfShockTime <= floatOfStartShockTime)
        {
            // floatOfShockTime 값을 감소시킨다.
            floatOfShockTime += Time.deltaTime;
        }


        // floatOfShockTime이 0 이하일 때 콜라이더를 활성화하고 함수 호출
        if (floatOfShockTime >= floatOfStartShockTime)
        {
            // 콜라이더 활성화
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
        // 매 프레임마다 실행되는 코드

        // 경과 시간을 누적
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;

        // 일정 간격마다 함수 호출
        if (timer >= interval)
        {
            myCollider.enabled = false;
            timer = 0.0f;   // 타이머 초기화
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