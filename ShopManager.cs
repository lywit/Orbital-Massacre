using UnityEngine.UI;
using UnityEngine;

public class ShopManager : MonoBehaviour {

    PlayerStats playerStats;

    [SerializeField]
    int firerateCost = 500;
    [SerializeField]
    int damageCost = 5000;
    [SerializeField]
    int speedCost = 1000;

    [SerializeField]
    Text moneyText;

	// Use this for initialization
	void Start () {
        playerStats = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerStats>();
        
    }

    public void ChangeMoney(int amount)
    {
        playerStats.money += amount;
        moneyText.text = playerStats.money + "$";
    }

    public void upgradeFirerate(float amount)
    {
        if (playerStats.money >= firerateCost)
        {
            playerStats.fireDelay -= amount;
            ChangeMoney(-firerateCost);
        }
            
    }

    public void UpgradeDamage(int amount)
    {
        if (playerStats.money >= damageCost)
        {
            playerStats.damage += amount;
            ChangeMoney(-damageCost);
        }
    }

    public void UpgradeSpeed(float amount)
    {
        if (playerStats.money >= speedCost)
        {
            playerStats.speed += amount;
            ChangeMoney(-speedCost);
        }
    }

}
