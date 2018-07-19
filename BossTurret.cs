using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTurret : MonoBehaviour {

    GameObject target;

    float lastTimeFired;

    [SerializeField]
    float fireDelay = .5f;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    Transform firePoint;
    [SerializeField]
    Transform bulletHolder;

    [SerializeField]
    Vector3 targetOffset;

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
        lastTimeFired = Time.time;
        StartCoroutine(AILoop());
	}

    void FixedUpdate()
    {
        print("");
    }
	
	// Update is called once per frame
	void LookAtTarget () {
        //transform.localRotation = new Quaternion(0, 0,target.transform.position.z - transform.position.z, 0);
        Vector3 targetDir = transform.position - target.transform.position;
        var angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        angle -= 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

    void Fire()
    {
        if (Time.time >= lastTimeFired + fireDelay)
        {
            GameObject spawnedBullet = (GameObject)Instantiate(bullet, firePoint);
            spawnedBullet.transform.parent = bulletHolder;
            lastTimeFired = Time.time;
            Debug.Log("Boss fired!");
        }
    }

    IEnumerator AILoop()
    {
        while (true)
        {
            LookAtTarget();
            Fire();

            yield return new WaitForSeconds(.1f);
        }
    }
}
