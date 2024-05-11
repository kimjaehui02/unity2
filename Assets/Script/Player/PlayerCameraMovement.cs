using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour
{
    public Transform player;
    public float SmoothDampTimePlusY = 0.25f;
    public float SmoothDampTimeMinusY = 0.1f;
    public Vector3 CameraOffset;
    public Vector2 MinCameraPos;
    public Vector2 MaxCameraPos;
 
    public bool isBossStage;
    public Transform Boss;
    public float BossCameraMoveSmoothRatioBoss = 1;
    public float BossCameraMoveSmoothRatioPlayer = 1;
    public float BossSmoothDampTimePlusY = 0.25f;
    public float BossSmoothDampTimeMinusY = 0.1f;

    private float Offset = 0.01f;

    IEnumerator SmoothCameraMovementCoroutine()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 Target = player.transform.position;
        while (true)
        {
            Target = player.transform.position;
            Target.x = Mathf.Clamp(player.transform.position.x, MinCameraPos.x, MaxCameraPos.x);
            Target.y = Mathf.Clamp(player.transform.position.y, MinCameraPos.y, MaxCameraPos.y);

            while (Vector3.Distance(this.transform.position, Target + CameraOffset) > Offset)
            {
                Target = player.transform.position;
                Target.x = Mathf.Clamp(player.transform.position.x, MinCameraPos.x, MaxCameraPos.x);
                Target.y = Mathf.Clamp(player.transform.position.y, MinCameraPos.y, MaxCameraPos.y);

                if (this.transform.position.y <= Target.y)
                {
                    this.transform.position = Vector3.SmoothDamp(this.transform.position, Target + CameraOffset, ref velocity, SmoothDampTimePlusY);
                }
                else
                {
                    this.transform.position = Vector3.SmoothDamp(this.transform.position, Target + CameraOffset, ref velocity, SmoothDampTimeMinusY);
                }

                yield return null;
            }
            this.transform.position = Target + CameraOffset;
            yield return null;
        }
    }

    IEnumerator SmoothCameraMovementInBossStageCoroutine()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 CenterPoint = Vector3.zero;
        if (BossCameraMoveSmoothRatioBoss + BossCameraMoveSmoothRatioPlayer == 0)
        {
            Debug.Log("보스 스테이지 카메라 움직임 부드러움 정도의 비율의 합이 0입니다 수정요망");
            BossCameraMoveSmoothRatioBoss = 1;
            BossCameraMoveSmoothRatioPlayer = 1;
        }
        while (true)
        {
            CenterPoint.x = Mathf.Clamp((player.transform.position.x * BossCameraMoveSmoothRatioBoss + Boss.transform.position.x * BossCameraMoveSmoothRatioPlayer) /
                    (BossCameraMoveSmoothRatioBoss + BossCameraMoveSmoothRatioPlayer), MinCameraPos.x, MaxCameraPos.x);
            CenterPoint.y = Mathf.Clamp((player.transform.position.y * BossCameraMoveSmoothRatioBoss + Boss.transform.position.y * BossCameraMoveSmoothRatioPlayer) /
                    (BossCameraMoveSmoothRatioBoss + BossCameraMoveSmoothRatioPlayer), MinCameraPos.y, MaxCameraPos.y);
            CenterPoint.z = CameraOffset.z;
            while (Vector3.Distance(this.transform.position, CenterPoint) > Offset)
            {
                CenterPoint.x = Mathf.Clamp((player.transform.position.x * BossCameraMoveSmoothRatioBoss + Boss.transform.position.x * BossCameraMoveSmoothRatioPlayer) /
                    (BossCameraMoveSmoothRatioBoss + BossCameraMoveSmoothRatioPlayer), MinCameraPos.x, MaxCameraPos.x);
                CenterPoint.y = Mathf.Clamp((player.transform.position.y * BossCameraMoveSmoothRatioBoss + Boss.transform.position.y * BossCameraMoveSmoothRatioPlayer) /
                     (BossCameraMoveSmoothRatioBoss + BossCameraMoveSmoothRatioPlayer), MinCameraPos.y, MaxCameraPos.y);
                CenterPoint.z = CameraOffset.z;
                if (this.transform.position.y <= CenterPoint.y)
                {
                    this.transform.position = Vector3.SmoothDamp(this.transform.position, CenterPoint, ref velocity, BossSmoothDampTimePlusY);
                }
                else
                {
                    this.transform.position = Vector3.SmoothDamp(this.transform.position, CenterPoint, ref velocity, BossSmoothDampTimeMinusY);
                }

                yield return null;
            }
            this.transform.position = CenterPoint;
            yield return null;
        }
    }

    void Start()
    {
        if (isBossStage)
        {
            StartCoroutine(SmoothCameraMovementInBossStageCoroutine());
        }
        else
        {
            StartCoroutine(SmoothCameraMovementCoroutine());
        }
    }

    void Update()
    {
        // ...
    }
}
