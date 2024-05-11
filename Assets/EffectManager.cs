using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    void Start()
    {
        // 0.5초 후에 현재 게임 오브젝트를 삭제
        Destroy(gameObject, 0.5f);
    }
}
