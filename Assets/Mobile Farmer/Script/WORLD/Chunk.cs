using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject unlockedElements;
    [SerializeField] private GameObject lockedElements;
    [SerializeField] private TextMeshPro priceText;
    private ChunkWalls chunkWalls;

    [Header("Elemetns")]
    [SerializeField] private int initialPrice;
    [SerializeField] private int currentPrice;
    private bool unlocked;

    [Header("Action")]
    public static Action onUnlocked;
    public static Action onPriceChanged;

    private void Awake()
    {
        chunkWalls = GetComponent<ChunkWalls>();
    }

    private void Start()
    {

    }

    public void Initialize(int loadedPrice)
    {
        currentPrice = loadedPrice;
        priceText.text = currentPrice.ToString();

        if (currentPrice <= 0)
        {
            Unlock(false);
        }
    }

    public void TryUnlock()
    {
        if (CashManager.instance.GetCoin() <= 0)
            return;

        currentPrice--;
        CashManager.instance.UseCoin(1);

        onPriceChanged?.Invoke();

        priceText.text = currentPrice.ToString();
        if (currentPrice <= 0)
            Unlock();

    }

    public void UpdateWalls(int configuration)
    {
        chunkWalls.Configuration(configuration);
    }
    public void DisplayLockedElements()
    {
        lockedElements.SetActive(true);
    }

    public void Unlock(bool triggerAction= true)
    {
        unlockedElements.SetActive(true);
        lockedElements.SetActive(false);

        unlocked = true;
        if(triggerAction)
            onUnlocked?.Invoke();

    }
    public bool IsUnlocked()
    {
        return unlocked;
    }

    public int GetInitialPrices()
    {
        return initialPrice;
    }
    public int GetCurrentPrices()
    {
        return currentPrice;
    }
}
