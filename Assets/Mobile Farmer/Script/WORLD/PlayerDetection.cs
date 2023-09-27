using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ChunkTrigger"))
        {
            Debug.Log("deneem");
            Chunk chunk = other.GetComponentInParent<Chunk>();
            chunk.TryUnlock();
        }
    }
}
