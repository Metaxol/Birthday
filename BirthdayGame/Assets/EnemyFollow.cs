using UnityEngine;

public class EnemyFollow : MonoBehaviour {

    public float EnemySpeed;
    private Transform Player;

    private Vector3 LastPosition;

    private void Awake()
    {
        LastPosition = transform.position;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, Player.position, EnemySpeed * Time.deltaTime);

        if (transform.position.x < LastPosition.x)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            LastPosition.x = transform.position.x;
        }

        else if (transform.position.x > LastPosition.x)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            LastPosition.x = transform.position.x;
        }
    }
}
