using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuyerInteractor : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private InventoryManager inventoryManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Buyer"))
        {
            SellCrops();
        }
    }
    public void SellCrops()
    {
        Inventory inventory= inventoryManager.GetInventory();
        InventoryItem[] items= inventory.GetInventoryItems();

        int coinEarned = 0;

        for(int i = 0; i < items.Length; i++)
        {
            int itemPrice= DataManager.instance.GetCropPriceFromCropType(items[i].cropType);
            coinEarned += itemPrice * items[i].amount;
        }

        CashManager.instance.AddCoins(coinEarned);

        inventoryManager.ClearInventory();
    }
}
