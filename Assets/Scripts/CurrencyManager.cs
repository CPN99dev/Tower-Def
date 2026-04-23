using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    public int money = 100;

    void Awake() => instance = this;

    public bool CanAfford(int cost) => money >= cost;

    public void SpendMoney(int amount) => money -= amount;

    public void AddMoney(int amount) => money += amount;
}