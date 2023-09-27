using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkWalls : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private GameObject frontWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject backWall;
    [SerializeField] private GameObject leftWall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Configuration(int config)
    {
        frontWall.SetActive(IsKthBitSet(config, 0));
        rightWall.SetActive(IsKthBitSet(config, 1));
        backWall.SetActive(IsKthBitSet(config, 2));
        leftWall.SetActive(IsKthBitSet(config, 3));

    }
    public bool IsKthBitSet(int configuration,int k)
    {
        if ((configuration & (1 << k)) > 0)
            return false;
        else
            return true;
    }
}
