using UnityEngine;

public class EnemyFollow : MonoBehaviour { //It's more of an "EnemyController".

    public float EnemySpeed;
    private Transform Player;

    private Vector3 LastPosition;

    private LevelManagement GetLevelManagement;

    public int Health;

    private void Awake()
    {
        GetLevelManagement = FindObjectOfType<LevelManagement>();
        LastPosition = transform.position;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player_Bullet")
        {
            collision.gameObject.GetComponent<BulletController>().Destroy_Directly(0.3f);
            Health -= 1;
            if(Health == 0)
            {
                GetLevelManagement.Enemies_Killed += 1;
                this.gameObject.GetComponent<Animator>().Play("Enemy_Destroyed");
                Destroy(gameObject, 0.5f);
            }
        }
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
