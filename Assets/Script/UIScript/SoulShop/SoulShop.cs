using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulShop : MonoBehaviour
{
    public List<Item> items;
    [SerializeField]
    private Transform slotBundle;
    [SerializeField]
    private ShopSlot[] slots;
    [SerializeField]
    private List<int> shopItemIdList = new List<int>();

    private void Start()
    {
        InitSlots();
    }
    private void InitSlots()
    {
        //List<Dictionary<string, object>> itemListData=  CSVReader.Read("ItemList"); ;

        //for (int i = 0; i < slots.Length; i++)
        //{
        //    Item ShopItem = new Item();
        //    ShopItem.itemName = itemListData[shopItemIdList[i]]["Name"].ToString();
        //    ShopItem.itemImage = null;
        //    ShopItem.itemPrice = Convert.ToInt32(itemListData[shopItemIdList[i]]["Price"]);
        //    ShopItem.itemExplanation = itemListData[shopItemIdList[i]] ["Explanation"].ToString();

        //    slots[i].pItem = ShopItem;
        //}
    }
    public void PurchaseItem()
    {

    }
}
