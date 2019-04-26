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
            print("Hello");
        }
    }

    private void OnTriggerStay2D(Collider2D collision) //Find out why this isn't working. (Collision detection)
    {
        if (collision.gameObject.tag == "Walls")
        {
            print("Hello");
        }
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }
}
