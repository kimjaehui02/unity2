using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBossInfoSetter : MonoBehaviour
{
    [SerializeField]
    private UIDataBase UIDatabase;
    public TextMeshProUGUI BossNameKr;
    public TextMeshProUGUI BossNameEng;
    public Slider BossHpBar;
    public Slider BossShieldBar;

    public GameObject BossHpBarLineHolder;
    [SerializeField]
    private float UnitHp=50f;

    [SerializeField]
    private GameObject SoulsImg;
    [SerializeField]
    private GameObject BrokenSoulsImg;
    [SerializeField]
    private Transform SoulsCase;
    [SerializeField]
    List<GameObject> SoulsList = new List<GameObject>();
    private int SoulsImgCount;
    // Start is called before the first frame update
    void Start()
    {
        SoulsImgInit();
    }

    // Update is called once per frame
    void Update()
    {
        InitBossNameTxt();
        InitBossHpBar();
        InitBossShieldBar();
        InitBossHpLineScale();
        SoulsImgManage();
    }
    private void InitBossNameTxt()
    {
        BossNameKr.text = UIDatabase.pBossName_Kr;
        BossNameEng.text = UIDatabase.pBossName_Eng;
    }
    private void InitBossHpBar()
    {
        if(UIDatabase.pBossCurrentHp+UIDatabase.pBossShield>UIDatabase.pBossMaxHp)
        {
            if ((float)(UIDatabase.pBossCurrentHp + UIDatabase.pBossShield) == 0) return;
            BossHpBar.value = ((float)UIDatabase.pBossCurrentHp / (float)(UIDatabase.pBossCurrentHp + UIDatabase.pBossShield));
        }
        else
        {
            if ((float)(UIDatabase.pBossMaxHp) == 0) return;

            BossHpBar.value = ((float)UIDatabase.pBossCurrentHp / (float)(UIDatabase.pBossMaxHp));
        }
    }
    private void InitBossShieldBar()
    {
        if (UIDatabase.pBossCurrentHp + UIDatabase.pBossShield > UIDatabase.pBossMaxHp)
        {
            if ((float)(UIDatabase.pBossCurrentHp + UIDatabase.pBossShield) == 0) return;
            BossShieldBar.value = ((float)UIDatabase.pBossCurrentHp + (float)UIDatabase.pBossShield) / (float)(UIDatabase.pBossCurrentHp + UIDatabase.pBossShield);
        }
        else
        {
            if ((float)(UIDatabase.pBossMaxHp) == 0) return;

            BossShieldBar.value = ((float)UIDatabase.pBossCurrentHp + (float)UIDatabase.pBossShield) / (float)(UIDatabase.pBossMaxHp);
        }
        
    }
    private void InitBossHpLineScale()
    {
        return;

        BossHpBarLineHolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(false);
        if (UIDatabase.pBossCurrentHp + UIDatabase.pBossShield > UIDatabase.pBossMaxHp)
        {
            float scaleX = ((float)(UIDatabase.pBossMaxHp) / UnitHp)/ ((float)(UIDatabase.pBossCurrentHp + UIDatabase.pBossShield)/UnitHp);
            foreach (Transform child in BossHpBarLineHolder.transform)
            {
                child.gameObject.transform.localScale = new Vector3(scaleX, 1, 1);
            }
        }
        else
        {
            float scaleX = ((float)(UIDatabase.pBossMaxHp) / UnitHp) / ((float)(UIDatabase.pBossMaxHp) / UnitHp);
            foreach (Transform child in BossHpBarLineHolder.transform)
            {
                child.gameObject.transform.localScale = new Vector3(scaleX, 1, 1);
            }
        }
        BossHpBarLineHolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(true);


    }

    private void SoulsImgInit()
    {
       for(int i=0; i< UIDatabase.pBossSouls;i++)
        {
            SoulsImgCreate();
        }
    }
    private void SoulsImgCreate()
    {
        GameObject NewSouls = Instantiate(SoulsImg);
        NewSouls.transform.SetParent(SoulsCase);
        SoulsList.Add(NewSouls);
        SoulsImgCount++;
    }
    private void SoulsImgDelete()
    {
        if (SoulsImgCount <= 0)
            return;
        Destroy(SoulsList[SoulsImgCount - 1]);
        SoulsList.RemoveAt(index: SoulsImgCount - 1);
        SoulsImgCount--;
    }
    private void BrokenSoulsImgCreate()
    {
        GameObject NewBrokenSouls = Instantiate(BrokenSoulsImg);
        NewBrokenSouls.transform.SetParent(SoulsCase);
        SoulsList.Add(BrokenSoulsImg);
        
    }
    private void BrokenSoulsImgDelete()
    {
        if (SoulsImgCount <= 0)
            return;
        Destroy(SoulsList[SoulsList.Count - 1]);
        SoulsList.RemoveAt(index: SoulsList.Count - 1);
       
    }
    private void SoulsImgManage()
    {
        if (SoulsImgCount > UIDatabase.pBossSouls)
        {
            SoulsImgDelete();
            BrokenSoulsImgCreate();
        }
        else if (SoulsImgCount < UIDatabase.pBossSouls)
            SoulsImgCreate();
    }
}
