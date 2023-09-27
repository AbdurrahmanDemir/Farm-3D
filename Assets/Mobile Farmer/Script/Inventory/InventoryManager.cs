using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(InventoryDiscplay))]
public class InventoryManager : MonoBehaviour
{
    private Inventory inventory;
    private InventoryDiscplay inventoryDiscplay;
    private string dataPath;

    private void Start()
    {
        dataPath = Application.persistentDataPath + "/inventoryData.txt";
        //inventory = new Inventory(); //envanter oluþturuluyor

        LoadInventory();
        ConfigureInventoryDisplay();
        CropTile.onCropHarvested += CropHarvestedCallBack;
    }
    private void OnDestroy()
    {
        CropTile.onCropHarvested -= CropHarvestedCallBack;
    }
    private void ConfigureInventoryDisplay()
    {

        inventoryDiscplay = GetComponent<InventoryDiscplay>();
        inventoryDiscplay.Configure(inventory);

    }
    private void CropHarvestedCallBack(CropType cropType)
    {
        inventory.CropHarvestedCallBack(cropType);

        inventoryDiscplay.UpdateDisplay(inventory);

        SaveInventory();
    }
    [NaughtyAttributes.Button]
    public void ClearInventory()
    {
        inventory.Clear();
        inventoryDiscplay.UpdateDisplay(inventory);

        SaveInventory();
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    private void LoadInventory()
    {
        
        string data = "";

        if (File.Exists(dataPath))
        {
            data= File.ReadAllText(dataPath);
            inventory=JsonUtility.FromJson<Inventory>(data);

            if (inventory == null)
                inventory = new Inventory();
        }
        else
        {
            File.Create(dataPath);
            inventory=new Inventory();
        }
    }
    private void SaveInventory()
    {
        string data = JsonUtility.ToJson(inventory, true);
        File.WriteAllText(dataPath, data);
    }
    
}
