using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CuberixBodyMovement : MonoBehaviour
{

    public GameObject BlockCollision;
    private bool UseCollision = false;
    private Vector3 tagetPosition;
    private bool IsMoving = false;
    private float duration = 0f;
    private float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BodyMove();
    }
    void BodyMove()
    {
        time += Time.deltaTime;
        float t = time / duration;
        t = t * t * (3f - 2f * t);
        if (!IsMoving) return;
        this.transform.position= Vector3.Lerp(this.transform.position, tagetPosition, t);
        if((this.transform.position-tagetPosition).magnitude<0.05f)
        {
            IsMoving = false;
        }
    }
    public void MovingBody(Vector3 TagetLocation,float TimeDuration)
    {
        tagetPosition = TagetLocation;
        time = 0f;
        duration = TimeDuration;
        IsMoving = true;
    }
    public bool GetIsMoving()
    {
        return IsMoving;
    }
    public void StopMoving()
    {
        IsMoving = false;
    }
}
