using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CashManager : MonoBehaviour
{
    public static CashManager instance;

    [Header("Settings")]
    private int coins;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);


       LoadData();
        UpdateCoinText();
    }
    public void UseCoin(int amount)
    {
        AddCoins(-amount);
    }
    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinText();


        SaveData();
    }

    [NaughtyAttributes.Button]
    private void Add500Coin()
    {
        AddCoins(100);
    }

    public void UpdateCoinText()
    {
        GameObject[] coinContainers = GameObject.FindGameObjectsWithTag("CoinText");

        foreach(GameObject coinContainer in coinContainers)
        {
            coinContainer.GetComponent<TextMeshProUGUI>().text = coins.ToString();
        }
    }

    public int GetCoin()
    {
        return coins;
    }

    public void LoadData()
    {
        coins= PlayerPrefs.GetInt("Coins");
    }
    public void SaveData()
    {
        PlayerPrefs.SetInt("Coins", coins);
    }
}
