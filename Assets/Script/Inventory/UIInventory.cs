using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField]
    private GameObject UIDataBase;
    [SerializeField]
    private GameObject InventoryUI;
    [SerializeField]
    bool isInventory = false;
    bool isRefClear = true;
    // Start is called before the first frame update
    void Start()
    {
        CheckInventoryRef();
    }

    // Update is called once per frame
    void Update()
    {
        InitIsInventory();
        Inventory();
    }

    private void Inventory()
    {
        if (isRefClear) return;

        if (isInventory)
        {
            CallInventory();
        }
        else
        {
            CloseInventory();
        }
    }
    private void CallInventory()
    {
        InventoryUI.SetActive(true);
    }
    private void CloseInventory()
    {
        InventoryUI.SetActive(false);
    }
    private void InitIsInventory()
    {
        if(isRefClear) return;
        isInventory = UIDataBase.GetComponent<UIDataBase>().pisInventory;
    }
    private void CheckInventoryRef()
    {
        if (UIDataBase == null || InventoryUI == null)
        {
            isRefClear = true;
        }
        else
        {
            isRefClear = false;
        }
    }
}
