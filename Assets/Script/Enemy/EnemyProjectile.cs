using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float lifespan = 5.0f; // ������Ʈ�� ���� (��)

    private float timer = 0.0f;
    private bool playerDetected = false;

    private void Update()
    {
        timer += Time.deltaTime;

        // ���� �ð��� ������ ������Ʈ�� �ı�
        if (timer >= lifespan)
        {
            Destroy(gameObject);
            return;
        }
    }

    // �ݶ��̴� Ʈ���� ���Խ� ȣ���
    private void OnTriggerEnter2D(Collider2D other)
    {
        // �÷��̾ �����ϰ� ���� �������� �ʾ��� ���
        if (other.CompareTag("Player") && !playerDetected)
        {
            playerDetected = true;
            Debug.Log("�÷��̾ �����߽��ϴ�!");
            Destroy(gameObject);
            other.GetComponent<Players>().EnemyAttack();
        }
    }
}
