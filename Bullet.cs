using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {

    [Header("Assignments")]
    [SerializeField]
    private int damage = 25;
    [SerializeField]
    private float bulletForce;
    [SerializeField]
    GameObject impactEffect;
    [SerializeField]
    float offsetMin = -.5f;
    [SerializeField]
    float offsetMax = .5f;
    [SerializeField]
    int maxEffects = 3;

    Transform effectHolder;

    private Rigidbody2D rb;
    private Vector3 forceVector;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        effectHolder = GameObject.FindGameObjectWithTag("EffectHolder").transform;
    }

    void OnEnable()
    {
        forceVector = new Vector3(0, bulletForce, 0);
        rb.AddForce(forceVector);
    }

    void OnDisable()
    {
        rb.velocity = Vector3.zero;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Vector3 effectPoint;

        if (other.contacts.Length > 0)
        {
            effectPoint = other.contacts[0].point;
        }
        else
        {
            effectPoint = transform.position;
            print(effectPoint.z);
        }

        
        for (int i = Random.Range(1, maxEffects) - 1; i >= 0; i--)
        {
            Vector3 effectOffset = new Vector3(Random.Range(offsetMin, offsetMax), Random.Range(offsetMin, offsetMax), -1);
            GameObject spawnedEffect = Instantiate(impactEffect, effectPoint + effectOffset, Quaternion.identity);
            spawnedEffect.transform.parent = effectHolder;
        }
        
        if (other.gameObject.tag == "Enemy")
        {
            print("Doing damage!");
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
        gameObject.SetActive(false);
    }
}
