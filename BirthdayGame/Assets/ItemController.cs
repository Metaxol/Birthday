using UnityEngine;

public class ItemController : MonoBehaviour {

    public AudioClip impact;
    AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (gameObject.tag)
            {
                case "Gem":
                    audioSource.PlayOneShot(impact);
                    EnemyFollow.PlayerToEnemy_Damage += (int)EnemySpawning.OpossumStuff[1];
                    gameObject.GetComponent<Animator>().Play("GemDestroyed");
                    break;
                case "Berry":
                    audioSource.PlayOneShot(impact);
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
        audioSource = GetComponent<AudioSource>();
        Destroy(gameObject, 7f);
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }
}
