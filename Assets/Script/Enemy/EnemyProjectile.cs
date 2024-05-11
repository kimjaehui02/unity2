using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float lifespan = 5.0f; // 오브젝트의 수명 (초)

    private float timer = 0.0f;
    private bool playerDetected = false;

    private void Update()
    {
        timer += Time.deltaTime;

        // 일정 시간이 지나면 오브젝트를 파괴
        if (timer >= lifespan)
        {
            Destroy(gameObject);
            return;
        }
    }

    // 콜라이더 트리거 진입시 호출됨
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어를 감지하고 아직 감지되지 않았을 경우
        if (other.CompareTag("Player") && !playerDetected)
        {
            playerDetected = true;
            Debug.Log("플레이어를 감지했습니다!");
            Destroy(gameObject);
            other.GetComponent<Players>().EnemyAttack();
        }
    }
}
