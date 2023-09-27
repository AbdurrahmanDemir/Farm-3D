using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crop Data", menuName = "Scriptible/Crop Data", order = 0)]
public class CropData : ScriptableObject
{
    [Header("Settings")]
    public Crop cropPrefabs;
    public CropType cropType;
    public Sprite icon;
    public int price;
}
