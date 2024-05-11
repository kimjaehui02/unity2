using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpearCase : MonoBehaviour
{
    public Transform playerTransform;
    private PlayerStateManager playerStateManager;
    [SerializeField]
    private List<GameObject> SpearCaseObjList = new List<GameObject>();
    [SerializeField]
    private List<Soul> SpearCaseSoulList = new List<Soul>();
    [SerializeField]
    private GameObject SpearPrefeb;
    [SerializeField]
    float SpearRecoveryTime;
    bool isSpearRecharging=false;
    // Start is called before the first frame update
    void Start()
    {
        playerStateManager = GetComponentInParent<PlayerStateManager>();
        //InitSpearCase();
    }

    // Update is called once per frame
    void Update()
    {
        SpearRecharge();
    }
    void SpearRecharge()
    {
        if(playerStateManager.GetPlayerCurrentSpearAmount()<playerStateManager.GetPlayerMaxSpearAmount()&&!isSpearRecharging)
        {
            StartCoroutine(CoSpearRecharge());
        }
    }
    IEnumerator CoSpearRecharge()
    {
        isSpearRecharging = true;
        yield return new WaitForSeconds(SpearRecoveryTime);
        playerStateManager.SetPlayerCurrentSpearAmount(playerStateManager.GetPlayerCurrentSpearAmount() + 1);
        isSpearRecharging = false;
    }
    void ResetSpearProperty()
    {
        SpearCaseSoulList.Clear();
        for (int i=0;i<SpearCaseObjList.Count;i++)
        {
            SpearCaseSoulList.Add(SpearCaseObjList[i].GetComponent<Soul>());
        }
        if(SpearCaseSoulList.Count>0)
            SpearCaseSoulList[0].Init(SpearCaseObjList[0].GetComponentInChildren<GameObject>(),new Vector3(0, 0, 0), 7, playerTransform
            , 0.04f, 0.5f,0.75f);
        for(int i=1;i< SpearCaseSoulList.Count;i++)
        {
            SpearCaseSoulList[i].Init(SpearCaseObjList[i].GetComponentInChildren<GameObject>(),new Vector3(0, 0, 0), 7, SpearCaseObjList[i-1].GetComponent<Transform>()
            , 0.04f, 0.5f,0.75f);
        }
        for(int i=0;i< SpearCaseSoulList.Count;i++)
        {
            SpearCaseSoulList[i].SettingComplete();
        }
    }
    void DeStroyAllOnSpearCaseObj()
    {
        for(int i=0; i< SpearCaseObjList.Count;i++)
        {
            Destroy(SpearCaseObjList[i]);
            SpearCaseObjList.RemoveAt(i);
        }
    }
    void CreateSpearObj()
    {
        GameObject Spear = Instantiate(SpearPrefeb) as GameObject;
        Spear.transform.SetParent(this.transform, false);
        SpearCaseObjList.Add(Spear);
    }
    void InitSpearCase()
    {
        DeStroyAllOnSpearCaseObj();
        for(int i=0; i<playerStateManager.GetPlayerCurrentSpearAmount();i++)
        {
            CreateSpearObj();
        }
        ResetSpearProperty();
    }
}
