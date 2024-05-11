using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillSprite : MonoBehaviour
{
    public float fillSpeed = 1.0f; // ä������ �ӵ�
    public Transform fillBar;      // HP ���� ä��� �κ�
    public float maxFillWidth = 1.0f; // HP ���� �ִ� �ʺ�

    private float currentFill = 0.0f;

    public List<SpriteRenderer> spriteRenderers;
    public GameObject objectToSpawn; // ��ȯ�� ������Ʈ
    public Transform spawnPoint;    // ��ȯ ��ġ

    private void Start()
    {
        // HP ���� ä��� �κ��� �ʱ� ũ�⸦ �� ���� ������ ����
        fillBar.localScale = new Vector3(0.0f, fillBar.localScale.y, fillBar.localScale.z);
    }

    private void Update()
    {
        UpdateFill();

        // I Ű�� ������ ä��� �۾��� �����
        if (Input.GetKeyDown(KeyCode.I))
        {
            RestartFill();
        }
    }

    private void UpdateFill()
    {
        // HP �ٸ� ���ݾ� ä������
        if (currentFill < 1.0f)
        {
            currentFill += fillSpeed * Time.deltaTime;
            currentFill = Mathf.Clamp01(currentFill);

            // HP ���� ä��� �κ��� ��ġ�� ũ�� ������Ʈ
            float newWidth = currentFill * maxFillWidth;
            float xOffset = (maxFillWidth - newWidth) / 2.0f; // ��� ������ ���� X ������ ���
            fillBar.localPosition = new Vector3(-xOffset, fillBar.localPosition.y, fillBar.localPosition.z);
            fillBar.localScale = new Vector3(newWidth, fillBar.localScale.y, fillBar.localScale.z);
        }
        else
        {
            // �������ٰ� ���� ���� ������Ʈ�� ��ȯ
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

    // ä��� �۾��� ������ϴ� �Լ�
    public void RestartFill()
    {
        currentFill = 0.0f;
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            spriteRenderers[i].enabled = true;
        }
    }
}
