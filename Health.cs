using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField]
    private int health = 100;
    [SerializeField]
    private int moneyKillReward = 25;
    [SerializeField]
    GameObject deathEffect;

    bool alive = true;

    GameManager gamemanager;
    PlayerStats playerStats;
    ShopManager shopManager;

    void Awake()
    {
        gamemanager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        playerStats = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerStats>();
        shopManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ShopManager>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0 && alive)
        {
            alive = false;
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }

            shopManager.ChangeMoney(moneyKillReward);
            gamemanager.kills += 1;

            Destroy(gameObject);
        }
    }

}
