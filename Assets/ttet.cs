using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ttet : MonoBehaviour
{
    // �ִ� ��
    public float maxRangeWidth = 5.0f;

    // ���� ���� ���� �ӵ�
    public float increaseSpeed = 0.5f;

    // �� �����Ӹ��� ȣ��Ǵ� �Լ�
    void Update()
    {
        // ���� ������ ���������� Ű��
        IncreaseAttackRange();
    }

    // ���� ������ ���������� Ű��� �Լ�
    void IncreaseAttackRange()
    {
        // ���� ������ �� ��������
        Vector3 currentScale = transform.localScale;

        // ���ο� �� ��� (���������θ� Ű��� ���� x���� ����)
        float newWidth = currentScale.x + increaseSpeed * Time.deltaTime;

        // �ִ� �� ����
        newWidth = Mathf.Min(newWidth, maxRangeWidth);

        // ���� ��ġ
        Vector3 currentPosition = transform.position;

        // ���ο� ������ �� ���� (���ʿ��� ����� �κ��� �ݿ��Ͽ� �߽����� ����)
        Vector3 newScale = new Vector3(newWidth, currentScale.y, currentScale.z);

        // ������ ���濡 ���� ��ġ ����
        float positionChange = (newWidth - currentScale.x) / 2.0f;

        // �����ϰ� ��ġ�� ���ÿ� ����
        transform.localScale = newScale;
        transform.position = new Vector3(currentPosition.x + positionChange, currentPosition.y, currentPosition.z);
    }
}
