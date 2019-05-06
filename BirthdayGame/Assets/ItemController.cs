using UnityEngine;

public class ItemController : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (gameObject.tag)
            {
                case "Gem":
                    EnemyFollow.PlayerToEnemy_Damage += (int)EnemySpawning.OpossumStuff[1];
                    gameObject.GetComponent<Animator>().Play("GemDestroyed");
                    break;
                case "Berry":
                    collision.gameObject.GetComponent<Player_Monster>().PlayerHealth += GameObject.Find("LevelManager").GetComponent<LevelManagement>().GetBossSpawner.FrogBoss.GetComponent<EnemyFollow>().Enemy_Health;
                    gameObject.GetComponent<Animator>().Play("CherryDestroyed");
                    break;
            }           
        }else if(collision.gameObject.tag == "Walls")
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Destroy(gameObject, 7f);
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }
}
