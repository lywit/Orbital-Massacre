using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour {

    [SerializeField]
    float updateDelay = .25f;
    [SerializeField]
    float speed = 1;
    [SerializeField]
    float moveDownDistance = 1f;
    [SerializeField]
    float upDownAnimationMovement = .25f;

    int interpolation;

    enum directionState { Left, Right }
    directionState direction;

    Rigidbody2D rb;
	GameManager gamemanager;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody2D>();
        interpolation = Random.Range(5, 75);

        if (transform.position.x < 0)
            direction = directionState.Right;
        else
            direction = directionState.Left;

        StartCoroutine(AILoop());
		gamemanager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    IEnumerator AILoop()
    {
        while (true)
        {
            if (direction == directionState.Left && rb.velocity.x > -5f)
            {
                rb.velocity = new Vector3(-speed, 0, 0);
            }
            else if (direction == directionState.Right && rb.velocity.x < 5f)
            {
                rb.velocity = new Vector3(speed, 0, 0);
            }
            yield return new WaitForSeconds(updateDelay);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "LeftWall")
        {
            direction = directionState.Right;
            StartCoroutine(MoveDown());
        }
        else if (other.gameObject.tag == "RightWall")
        {
            direction = directionState.Left;
            StartCoroutine(MoveDown());
        }
    }

	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.gameObject.tag == "Base") {
		
			gamemanager.GameOver ();
		
		}

	}

    IEnumerator MoveDown()
    {
        for (int i = 0; i < interpolation; i++)
        {
            rb.position = (new Vector2(rb.position.x, rb.position.y - (moveDownDistance / interpolation)));
            yield return new WaitForSeconds(0f);
        }
    }

}
