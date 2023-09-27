using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CropTile : MonoBehaviour
{

    public enum TileFieldState { Empty, Sown, Watered}
    public TileFieldState state;

    [Header("Elements")]
    [SerializeField] private Transform cropParent;
    [SerializeField] private MeshRenderer tileRenderer;
    private Crop crop;
    private CropData cropData;

    [Header("Events")]
    public static Action<CropType> onCropHarvested;
    
    void Start()
    {
        state = TileFieldState.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sow(CropData cropData)
    {
        state = TileFieldState.Sown;
        
        crop = Instantiate(cropData.cropPrefabs, transform.position, Quaternion.identity, cropParent);
        this.cropData = cropData;
    }
    public void Water()
    {
        state = TileFieldState.Watered;
        //tileRenderer.material.color = Color.white * .3f;

        tileRenderer.gameObject.LeanColor(Color.white * .3f, 1);



        crop.ScaleUp();
    }

    public void Harvest()
    {
        state = TileFieldState.Empty;
        crop.ScaleDown();

        tileRenderer.gameObject.LeanColor(Color.white, 1);

        onCropHarvested?.Invoke(cropData.cropType);

    }
    public bool IsEmpty()
    {
        return state == TileFieldState.Empty;
    }

    public bool IsSown()
    {
        return state == TileFieldState.Sown;
    }

}
