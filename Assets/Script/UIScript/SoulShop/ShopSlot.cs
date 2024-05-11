using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Unity.Burst.Intrinsics.X86;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] 
    private Image image;
    [SerializeField]
    private int slotPrice;
    [SerializeField]
    private TextMeshProUGUI textName;
    [SerializeField]
    bool isSelected = false;

    private Item item;
    public Item pItem
    {
        get { return item; }
        set
        {
            item = value;
            if (item != null)
            {
                image.sprite = item.itemImage;
                image.color = new Color(255f / 255f, 250f / 255f, 132f / 255f, 1);
                textName.text = item.itemName;
            }
            else
            {
                image.color = new Color(255f / 255f, 250f / 255f, 132f / 255f, 0);
                textName.text = "Null";
            }
        }
    }
 
    public int GetSlotPrice()
    {
        return slotPrice;
    }
    public bool GetIsSelected()
    {
        return isSelected;
    }
    public void ItemSelect()
    {
        if (isSelected)
        {
            isSelected = false;
            image.color = new Color(255f/255f, 250f / 255f, 132f / 255f, 1);
        }
        else 
        {
            isSelected = true;
            image.color = new Color(191f/255f, 248f / 255f, 236f / 255f, 1);

        }
    }
    public void SetisSelected(bool isSelect)
    {
        isSelected = isSelect;
        if (isSelected)
        {
            image.color = new Color(191f / 255f, 248f / 255f, 236f / 255f, 1);
        }
        else
        {
            image.color = new Color(255f / 255f, 250f / 255f, 132f / 255f, 1);

        }
    }
}


public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int itemPrice;
    public string itemExplanation;
}
