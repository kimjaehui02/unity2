using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIPlayerInfoSetter : MonoBehaviour
{
    [SerializeField]
    private UIDataBase UIDatabase;
    public Slider PlayerHpBar;
    public Slider PlayerStaminaBar;
    public Slider PlayerShieldBar;

    public GameObject PlayerHpBarLineHolder;
    [SerializeField]
    private float UnitHp = 50f;

    [SerializeField]
    private GameObject SpearImg;
    [SerializeField]
    private Transform SpearCase;
    [SerializeField]
    List<GameObject> SpearList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        SpearImgInit();
    }

    // Update is called once per frame
    void Update()
    {
        InitPlayerHpBar();
        InitPlayerStaminaBar();
        SpearImgManage();
        InitPlayerShieldBar();
        InitPlayerHpLineScale();
    }

    private void InitPlayerHpBar()
    {
        if ((float)UIDatabase.pPlayerMaxHp == 0) return;//NAN 버그 수정용
        PlayerHpBar.value = (float)((float)UIDatabase.pPlayerCurrentHp / (float)UIDatabase.pPlayerMaxHp);
    }
    private void InitPlayerStaminaBar()
    {
        if ((float)UIDatabase.pPlayerMaxStamina==0) return;
        PlayerStaminaBar.value = (float)((float)UIDatabase.pPlayerCurrentStamina / (float)UIDatabase.pPlayerMaxStamina);
    }
    private void SpearImgInit()
    {
        for (int i = 0; i < UIDatabase.pPlayerCurrentSpearAmount; i++)
        {
            SpearImgCreate();
        }
    }
    private void InitPlayerShieldBar()
    {
        if (UIDatabase.pPlayerCurrentHp + UIDatabase.pPlayerShield > UIDatabase.pPlayerMaxHp)
        {
            if ((float)(UIDatabase.pPlayerCurrentHp + UIDatabase.pPlayerShield) == 0) return;
            PlayerShieldBar.value = ((float)UIDatabase.pPlayerCurrentHp + (float)UIDatabase.pPlayerShield) / (float)(UIDatabase.pPlayerCurrentHp + UIDatabase.pPlayerShield);
        }
        else
        {
            if ((float)(UIDatabase.pPlayerMaxHp) == 0) return;

            PlayerShieldBar.value = ((float)UIDatabase.pPlayerCurrentHp + (float)UIDatabase.pPlayerShield) / (float)(UIDatabase.pPlayerMaxHp);
        }

    }
    private void InitPlayerHpLineScale()
    {
        return;
        PlayerHpBarLineHolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(false);
        if (UIDatabase.pPlayerCurrentHp + UIDatabase.pPlayerShield > UIDatabase.pPlayerMaxHp)
        {
            if (((float)(UIDatabase.pPlayerCurrentHp + UIDatabase.pPlayerShield) / UnitHp) == 0) return;
            float scaleX = ((float)(UIDatabase.pPlayerMaxHp) / UnitHp) / ((float)(UIDatabase.pPlayerCurrentHp + UIDatabase.pPlayerShield) / UnitHp);
            foreach (Transform child in PlayerHpBarLineHolder.transform)
            {
                child.gameObject.transform.localScale = new Vector3(scaleX, 1, 1);
            }
        }
        else
        {
            if (((float)(UIDatabase.pPlayerMaxHp) / UnitHp) == 0) return;

            float scaleX = ((float)(UIDatabase.pPlayerMaxHp) / UnitHp) / ((float)(UIDatabase.pPlayerMaxHp) / UnitHp);
            foreach (Transform child in PlayerHpBarLineHolder.transform)
            {
                child.gameObject.transform.localScale = new Vector3(scaleX, 1, 1);
            }
        }
        PlayerHpBarLineHolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(true);


    }

    private void SpearImgCreate()
    {
        GameObject NewSpear = Instantiate(SpearImg);
        NewSpear.transform.SetParent(SpearCase);
        SpearList.Add(NewSpear);
    }
    private void SpearImgDelete()
    {
        if (SpearList.Count <= 0)
            return;
        Destroy(SpearList[SpearList.Count - 1]);
        SpearList.RemoveAt(index: SpearList.Count - 1);
    }
    private void SpearImgManage()
    {
        if (SpearList.Count > UIDatabase.pPlayerCurrentSpearAmount)
            SpearImgDelete();
        else if (SpearList.Count < UIDatabase.pPlayerCurrentSpearAmount)
            SpearImgCreate();
    }
}
