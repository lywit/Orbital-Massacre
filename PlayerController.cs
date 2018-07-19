using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    [Header("Player Weapons")]
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform[] firePoints;
    [SerializeField]
    private int pooledBullets = 5;
    [SerializeField]
    Transform bulletPool;
    List<GameObject> bullets;

    private bool shooting = false;
    private bool alive = true;

    [Header("Player Movement")]
    private float horizontalInput;
    private Rigidbody2D rb;

    [Header("Object Assignment")]
    [SerializeField]
    Transform effectsHolder;
    [SerializeField]
    Transform boosterPosition1;
    [SerializeField]
    Transform boosterPosition2;
    [SerializeField]
    ParticleSystem boosterParticles1;
    [SerializeField]
    ParticleSystem boosterParticles2;
    [SerializeField]
    GameObject gunEffect;

    PlayerStats playerStats;
    GameManager gamemanager;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        playerStats = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerStats>();
        gamemanager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        bullets = new List<GameObject>();
        for (int i = 0; i < pooledBullets; i++)
        {
            GameObject obj = (GameObject)Instantiate(bullet);
            obj.transform.parent = bulletPool;
            obj.SetActive(false);
            bullets.Add(obj);
        }
        Debug.Log("Pooled " + bullets.Count + "");
	}
	
	// Update is called once per frame
	void Update () {

        var particle1 = boosterParticles1.main;
        var particle1Shape = boosterParticles1.shape;
        var particle1Emission = boosterParticles1.emission;
        particle1Shape.position = new Vector3(boosterPosition1.position.x + 1, 0, 0);
        particle1Shape.rotation = new Vector3(90 + Mathf.Lerp(particle1Shape.rotation.x, Mathf.Clamp(rb.velocity.x * 5, -45, 45), 1), 0, 0);

        var particle2 = boosterParticles2.main;
        var particle2Shape = boosterParticles2.shape;
        var particle2Emission = boosterParticles2.emission;
        particle2Shape.position = new Vector3(boosterPosition2.position.x + 1, 0, 0);
        particle2Shape.rotation = new Vector3(90 + Mathf.Lerp(particle2Shape.rotation.x ,Mathf.Clamp(rb.velocity.x * 5, -45, 45), 1), 0, 0);

        //Check for user input and apply shooting

        if (Input.GetKeyDown(KeyCode.Space) && shooting == false)
        {
            StartCoroutine(Shoot());
        }

        //Check for user input and apply movement
        horizontalInput = Input.GetAxisRaw("Horizontal");

        Vector3 inputVector = new Vector3(horizontalInput * playerStats.speed, 0, 0);

        if (horizontalInput != 0)
        {
            rb.AddForce(inputVector, ForceMode2D.Force);
            
        }

        if (Mathf.Abs(rb.velocity.x) > 3)
        {
            particle1.startSpeed = Mathf.Abs(rb.velocity.x);
            particle2.startSpeed = Mathf.Abs(rb.velocity.x);
        }

        particle1Emission.rateOverTimeMultiplier = Mathf.Lerp(particle1Emission.rateOverTimeMultiplier, (Mathf.Abs(rb.velocity.x) * 6) + Random.Range(4.75f, 10.0f), 2.0f);
        particle2Emission.rateOverTimeMultiplier = Mathf.Lerp(particle2Emission.rateOverTimeMultiplier, (Mathf.Abs(rb.velocity.x) * 6) + Random.Range(4.75f, 10.0f), 2.0f);
        particle1.startSizeMultiplier = Mathf.Lerp(particle1.startSizeMultiplier, (Mathf.Abs(rb.velocity.x) / 35) + .05f, 1.5f);
        particle2.startSizeMultiplier = Mathf.Lerp(particle1.startSizeMultiplier, (Mathf.Abs(rb.velocity.x) / 35) + .05f, 1.5f);
    }

    public void TakeHit()
    {
        if (alive)
        {
            gamemanager.GameOver();
            alive = false;
        }
    }

    //Function for firing the player's weapon
    IEnumerator Shoot()
    {
        shooting = true;
        while (Input.GetKey(KeyCode.Space))
        {
            for (int i = 0; i < firePoints.Length; i++)
            {
                GameObject gunEffect1 = (GameObject)Instantiate(gunEffect, firePoints[0].position, Quaternion.identity);
                GameObject gunEffect2 = (GameObject)Instantiate(gunEffect, firePoints[1].position, Quaternion.identity);
                gunEffect1.transform.parent = effectsHolder;
                gunEffect2.transform.parent = effectsHolder;

                for (int i2 = 0; i2 < bullets.Count; i2++)
                {
                    if (!bullets[i2].activeInHierarchy)
                    {
                        bullets[i2].transform.position = firePoints[i].position;
                        bullets[i2].transform.rotation = Quaternion.identity;
                        bullets[i2].SetActive(true);
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(playerStats.fireDelay);
        }
        shooting = false;
    }
}