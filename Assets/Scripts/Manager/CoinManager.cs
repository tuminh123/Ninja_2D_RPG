using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : Singleton<CoinManager>
{
    [SerializeField] private float coinTest = 1000;
    private float coins;
    public float Coins=>coins;
    private const string COIN_KEY = "Coins";

    private void Start()
    {
        coins = SaveGame.Load(COIN_KEY, coinTest);
        coins = coinTest; // For testing purposes, remove this line in production
    }
    public void AddCoins(float amount)
    {
        coins += amount;
        SaveGame.Save(COIN_KEY, coins);
    }
    public void RemoveCoins(float amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            SaveGame.Save(COIN_KEY, coins);
        }
    }
}
