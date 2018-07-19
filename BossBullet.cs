using UnityEngine;

public class BossBullet : MonoBehaviour {

    [SerializeField]
    float bulletForce = 2f;

    Rigidbody2D rb;

	// Use this for initialization
	void Awake () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(-transform.up * bulletForce, ForceMode2D.Force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().TakeHit();
        }
    }

}
