using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    void Start()
    {
        // 0.5�� �Ŀ� ���� ���� ������Ʈ�� ����
        Destroy(gameObject, 0.5f);
    }
}
