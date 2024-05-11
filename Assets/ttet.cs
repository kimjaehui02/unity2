using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ttet : MonoBehaviour
{
    // 최대 폭
    public float maxRangeWidth = 5.0f;

    // 공격 범위 증가 속도
    public float increaseSpeed = 0.5f;

    // 매 프레임마다 호출되는 함수
    void Update()
    {
        // 공격 범위를 지속적으로 키움
        IncreaseAttackRange();
    }

    // 공격 범위를 오른쪽으로 키우는 함수
    void IncreaseAttackRange()
    {
        // 현재 스케일 값 가져오기
        Vector3 currentScale = transform.localScale;

        // 새로운 폭 계산 (오른쪽으로만 키우기 위해 x값만 변경)
        float newWidth = currentScale.x + increaseSpeed * Time.deltaTime;

        // 최대 폭 제한
        newWidth = Mathf.Min(newWidth, maxRangeWidth);

        // 현재 위치
        Vector3 currentPosition = transform.position;

        // 새로운 스케일 값 설정 (왼쪽에서 변경된 부분을 반영하여 중심축을 변경)
        Vector3 newScale = new Vector3(newWidth, currentScale.y, currentScale.z);

        // 스케일 변경에 따른 위치 조정
        float positionChange = (newWidth - currentScale.x) / 2.0f;

        // 스케일과 위치를 동시에 변경
        transform.localScale = newScale;
        transform.position = new Vector3(currentPosition.x + positionChange, currentPosition.y, currentPosition.z);
    }
}
