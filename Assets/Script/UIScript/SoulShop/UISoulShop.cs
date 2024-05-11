using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UISoulShop : MonoBehaviour
{

    [SerializeField]
    private GameObject UIDataBase;
    [SerializeField]
    private GameObject SoulShopUI;
    [SerializeField]
    private GameObject PurchaseButtonUI;

    [SerializeField]
    private List<ShopSlot> soulShopSlots = new List<ShopSlot>();
    [SerializeField]
    bool isShopping = false;
    bool isRefClear = true;
    public TextMeshProUGUI SoulText;
    public PlayerDataManager playerDataManager;
    private int playerSoul = 0;
    bool isPurchase = false;
    [SerializeField]
    int Price = 0;
    [SerializeField]
    int selectedSlotIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        CheckSoulShopRef();
    }

    // Update is called once per frame
    void Update()
    {
        InitIsShopping();
        CheckShopSlot();
        WritePlayerSoulAmount();
        WriteItemPriceWithPlayerSouls();
        PurchaseButton();
        PauseMenu();
    }

    private void PauseMenu()
    {
        if (isRefClear) return;

        if (isShopping)
        {
            CallSoulShop();
        }
        else
        {
            CloseSoulShop();
        }
    }
    public void CallSoulShop()
    {
        SoulShopUI.SetActive(true);
    }
    public void CloseSoulShop()
    {
        SoulShopUI.SetActive(false);

    }
    private void InitIsShopping()
    {
        if (isRefClear) return;
        isShopping = UIDataBase.GetComponent<UIDataBase>().pisShopping;
    }
    private void CheckSoulShopRef()
    {
        if (UIDataBase == null || SoulShopUI == null)
        {
            isRefClear = true;
        }
        else
        {
            isRefClear = false;
        }
    }
    private void WritePlayerSoulAmount()
    {
        if (isRefClear) return;
        if (isPurchase) return;
        playerSoul = playerDataManager.playerData.playerSouls;
        SoulText.text = (playerSoul).ToString();
    }
    private void CheckShopSlot()
    {
        for (int i = 0; i < soulShopSlots.Count; i++)
        {
            if (soulShopSlots[i].GetIsSelected()&&i!=selectedSlotIndex)
            {
                isPurchase = true;
                Price = soulShopSlots[i].GetSlotPrice();
                if(selectedSlotIndex>=0)soulShopSlots[selectedSlotIndex].SetisSelected(false);
                selectedSlotIndex = i;
                return;
            }
        }
        if(selectedSlotIndex != -1&&soulShopSlots[selectedSlotIndex].GetIsSelected())
        {
            isPurchase = true;
            return;
        }
        isPurchase = false;
        selectedSlotIndex = -1;
        return;
    }
    private void WriteItemPriceWithPlayerSouls()
    {
        if (isRefClear) return;
        if (!isPurchase) return;
        if(playerDataManager.playerData.playerSouls - Price>=0)
        {
            playerSoul = playerDataManager.playerData.playerSouls - Price;
            SoulText.text = playerSoul.ToString() + "(-" + Price + ")";
        }
        else
        {
            playerSoul = playerDataManager.playerData.playerSouls;
            SoulText.text = playerSoul.ToString() + "(not enough souls)";
        }
        
    }
    private void PurchaseButton()
    {
        if (isRefClear) return;
        if (isPurchase) PurchaseButtonUI.SetActive(true);
        else PurchaseButtonUI.SetActive(false) ;
    }
    public void PurchaseItem()
    {
        if(playerDataManager.playerData.playerSouls - Price>=0)
        {
            playerDataManager.playerData.playerSouls = playerDataManager.playerData.playerSouls - Price;
            playerDataManager.SavePlayerDataToJson();
            isPurchase = false;
            if (selectedSlotIndex >= 0) soulShopSlots[selectedSlotIndex].SetisSelected(false);
            selectedSlotIndex = -1;
        }
    }
}
