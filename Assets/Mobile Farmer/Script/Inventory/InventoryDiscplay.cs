using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDiscplay : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform cropContainerParent;
    [SerializeField] private UICrop UICropPrefabs;

    public void Configure(Inventory inventory)
    {
        InventoryItem[] item= inventory.GetInventoryItems();

        for(int i = 0; i < item.Length; i++)
        {
            UICrop cropContainerInstance = Instantiate(UICropPrefabs, cropContainerParent);

            Sprite cropIcon = DataManager.instance.GetCropSpriteFromCropType(item[i].cropType);
            cropContainerInstance.Configure(cropIcon, item[i].amount);
        }
    }

    public void UpdateDisplay(Inventory inventory)
    {
        InventoryItem[] items = inventory.GetInventoryItems();

        for (int i = 0; i < items.Length; i++)
        {
            UICrop containerInstance;

            if (i < cropContainerParent.childCount)
            {
                containerInstance = cropContainerParent.GetChild(i).GetComponent<UICrop>();
                containerInstance.gameObject.SetActive(true);
            }
            else
                containerInstance = Instantiate(UICropPrefabs, cropContainerParent);


            Sprite cropIcon = DataManager.instance.GetCropSpriteFromCropType(items[i].cropType);
            containerInstance.Configure(cropIcon, items[i].amount);
        }

        int remainingContainers = cropContainerParent.childCount - items.Length;

        if (remainingContainers <= 0)
            return;

        for (int i = 0; i < remainingContainers; i++)
            cropContainerParent.GetChild(items.Length + i).gameObject.SetActive(false);

    }
}
