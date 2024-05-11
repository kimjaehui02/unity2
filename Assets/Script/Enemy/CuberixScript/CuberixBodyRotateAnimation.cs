using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuberixBodyRotateAnimation : MonoBehaviour
{
    //16방향 (12시 0번 -> 시계방향으로 계산, 4번은 3시방향 스프라이트)
    [SerializeField]
    private Sprite[] CuberixSprites = new Sprite[16];
    SpriteRenderer BodySpriteRenderer;
    //16방향 (12시 0번 -> 시계방향으로 계산, 4번은 3시방향 스프라이트)
    [SerializeField]
    int currentBodyDir = 8;
    [SerializeField]
    int targetBodyDir = 8;


    private float currentTime=0f;
    [SerializeField]
    float RotateDelayTime = 0.1f;
    [SerializeField]
    private bool isDirChange = false;

    // Start is called before the first frame update
    void Start()
    {
        BodySpriteRenderer = GetComponent<SpriteRenderer>();
        BodySpriteRenderer.sprite = CuberixSprites[currentBodyDir];

    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrentDirSprite();
    }
    private void UpdateCurrentDirSprite()
    {
        if(isDirChange)
        {
            if (targetBodyDir == currentBodyDir) isDirChange = false;           
            currentTime += Time.deltaTime;
            if (currentTime < RotateDelayTime) return;
            Rotate();
            currentTime = 0f;
        }
    }
    public void RotateDir(int targetDir)
    {
        isDirChange = true;
        targetBodyDir = targetDir;
        currentTime = 0f;

    }
    private void Rotate()
    {
        if(((targetBodyDir-currentBodyDir)+16)%16< ((currentBodyDir+16)-targetBodyDir)%16)
        {
            currentBodyDir++;
            if (currentBodyDir > 15) currentBodyDir = 0;
        }
        else
        {
            currentBodyDir--;
            if (currentBodyDir < 0) currentBodyDir = 15;
        }
        BodySpriteRenderer.sprite = CuberixSprites[currentBodyDir];
    }
}
