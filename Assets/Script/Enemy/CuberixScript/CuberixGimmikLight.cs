using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuberixGimmikLight : MonoBehaviour
{
    public GameObject Boss;
    private int BossGimmikLocation;
    public GameObject[] Light= new GameObject[4];
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetLocationLight();
    }
    void SetLocationLight()
    {
        
        BossGimmikLocation = Boss.GetComponent<CuberixGimmik>().GetGimmikPos();
        if (BossGimmikLocation == 5) return;
        for(int i=0;i<4;i++)
        {
            Light[i].SetActive(false);
        }
        Light[BossGimmikLocation].SetActive(true);
    }
    public void HideLight()
    {
        for (int i = 0; i < 4; i++)
        {
            Light[i].SetActive(false);
        }
    }
    public void ShowLight()
    {
        BossGimmikLocation = Boss.GetComponent<CuberixGimmik>().GetGimmikPos();
        if (BossGimmikLocation == 5)
        {
            HideLight();
            return;
        }

        for (int i = 0; i < 4; i++)
        {
            Light[i].SetActive(false);
        }
        Light[BossGimmikLocation].SetActive(true);
    }
}
