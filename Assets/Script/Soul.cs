using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    public GameObject GameObject;

    public Vector3 followPos;
    public int followDelay;
    public Transform parent;
    public Queue<Vector3> parentPos;

    public float moveDistance = 1.0f; // 이동 거리
    public float moveSpeed = 1.0f;   // 이동 속도

    private Vector3 initialLocalPosition;
    public float t = 0.0f;
    private bool movingUp = true;

    private bool IsSettingValue = false;
    private void Awake()
    {
        parentPos = new Queue<Vector3>();
    }


    // Update is called once per frame
    void Update()
    {
       
            Watch();
            Follow();
            UpDown();
        
    }

    void Watch()
    {
        if(!parentPos.Contains(parent.position))
        {
            parentPos.Enqueue(parent.position);
        }

        //Debug.Log(parentPos.Count);
        if((parentPos.Count > followDelay))
        {
            followPos = parentPos.Dequeue();
        }

    }

    void Follow()
    {
        transform.position = followPos;
    }

    void UpDown()
    {


        // t에 랜덤값을 더하여 불규칙성 추가
        t += (Time.deltaTime) * moveSpeed;

        if (movingUp)
        {
            GameObject.transform.localPosition = Vector3.Lerp(initialLocalPosition, initialLocalPosition + Vector3.up * moveDistance, t);
            if (t >= 1.0f)
            {
                movingUp = false;
                t = 0.0f;
            }
        }
        else
        {
            GameObject.transform.localPosition = Vector3.Lerp(initialLocalPosition + Vector3.up * moveDistance, initialLocalPosition, t);
            if (t >= 1.0f)
            {
                movingUp = true;
                t = 0.0f;
            }
        }
    }
    public void Init(GameObject gameObject, Vector3 followPos, int followDelay, Transform parent, float moveDistance = 1.0f, float moveSpeed = 1.0f, float T=0.75f)
    {
        this.GameObject = gameObject;
        this.followPos = followPos;
        this.followDelay = followDelay;
        this.parent = parent;
        this.moveDistance = moveDistance;
        this.moveSpeed = moveSpeed;
        this.t = T;
    }
    public void SettingComplete()
    {
        IsSettingValue = true;
    }
}
