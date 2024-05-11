using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillSprite : MonoBehaviour
{
    public float fillSpeed = 1.0f; // 채워지는 속도
    public Transform fillBar;      // HP 바의 채우는 부분
    public float maxFillWidth = 1.0f; // HP 바의 최대 너비

    private float currentFill = 0.0f;

    public List<SpriteRenderer> spriteRenderers;
    public GameObject objectToSpawn; // 소환할 오브젝트
    public Transform spawnPoint;    // 소환 위치

    private void Start()
    {
        // HP 바의 채우는 부분의 초기 크기를 맨 왼쪽 끝으로 설정
        fillBar.localScale = new Vector3(0.0f, fillBar.localScale.y, fillBar.localScale.z);
    }

    private void Update()
    {
        UpdateFill();

        // I 키를 누르면 채우기 작업을 재실행
        if (Input.GetKeyDown(KeyCode.I))
        {
            RestartFill();
        }
    }

    private void UpdateFill()
    {
        // HP 바를 조금씩 채워나감
        if (currentFill < 1.0f)
        {
            currentFill += fillSpeed * Time.deltaTime;
            currentFill = Mathf.Clamp01(currentFill);

            // HP 바의 채우는 부분의 위치와 크기 업데이트
            float newWidth = currentFill * maxFillWidth;
            float xOffset = (maxFillWidth - newWidth) / 2.0f; // 가운데 정렬을 위한 X 오프셋 계산
            fillBar.localPosition = new Vector3(-xOffset, fillBar.localPosition.y, fillBar.localPosition.z);
            fillBar.localScale = new Vector3(newWidth, fillBar.localScale.y, fillBar.localScale.z);
        }
        else
        {
            // 게이지바가 가득 차면 오브젝트를 소환
            if (objectToSpawn != null && spawnPoint != null && spriteRenderers[0].enabled == true)
            {
                Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
            }
            for (int i = 0; i < spriteRenderers.Count; i++)
            {
                spriteRenderers[i].enabled = false;
            }


        }
    }

    // 채우기 작업을 재실행하는 함수
    public void RestartFill()
    {
        currentFill = 0.0f;
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            spriteRenderers[i].enabled = true;
        }
    }
}
