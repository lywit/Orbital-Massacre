using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

    [SerializeField]
    float aliveTime = 2f;

	void Start () {
        Invoke("DestroySelf", aliveTime);
	}

    void DestroySelf()
    {
        Destroy(gameObject);
    }

}
