using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIStageClear : MonoBehaviour
{
    [SerializeField]
    private GameObject UIDataBase;
    [SerializeField]
    private GameObject StageClearUI;
    [SerializeField]
    private GameObject HiddenUI;
    [SerializeField]
    bool isStageClear = false;
    [SerializeField]
    TextMeshProUGUI ClearText;
    bool isRefClear = true;
    [SerializeField]
    GameObject ClearWarpZone;
    private bool isCoroutineWorking = false;


    // Start is called before the first frame update
    void Start()
    {
        CheckStageClearRef();

    }

    // Update is called once per frame
    void Update()
    {
        InitIsStageClear();
        StageClear();
    }

    private void StageClear()
    {
        if (isRefClear) return;

        if (isStageClear)
        {
            CallStageClearUI();
        }
        else
        {
            CloseStageClearUI();
        }
    }
    public void CallStageClearUI()
    {
        InitClearMessage();
        if(!isCoroutineWorking)
        {
            StartCoroutine(StageClearCoroutine());
            StageClearUI.SetActive(true);
            HiddenUI.SetActive(false);
        }
        

    }
    public void CloseStageClearUI()
    {
        StageClearUI.SetActive(false);

    }
    private void InitIsStageClear()
    {
        if (isRefClear) return;
        isStageClear = UIDataBase.GetComponent<UIDataBase>().pisStageClear;
    }
    private void CheckStageClearRef()
    {
        if (UIDataBase == null || StageClearUI == null)
        {
            isRefClear = true;
        }
        else
        {
            isRefClear = false;
        }
    }
    private void InitClearMessage()
    {
        if(ClearText!=null)
        {
            ClearText.text = UIDataBase.GetComponent<UIDataBase>().pBossName_Eng.ToString() + " Slained";
        }
    }
    IEnumerator StageClearCoroutine()
    {
        isCoroutineWorking = true;
        ClearWarpZone.GetComponent<WarpZone>().MoveScene();
        yield return new WaitForSeconds(4f);
        StageClearUI.SetActive(false);
        HiddenUI.SetActive(true);

        yield return null;

    }
}
