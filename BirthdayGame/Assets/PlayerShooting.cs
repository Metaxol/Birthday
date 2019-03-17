using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    [SerializeField] private GameObject Player_Bullets;
    public float BulletSpeed;

    private void Shooting() //Finish the shooting function.
    {               
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameObject move = Instantiate(Player_Bullets, transform.position, transform.rotation) as GameObject;
            move.GetComponent<Rigidbody2D>().AddForce(transform.forward * BulletSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //GameObject move = Instantiate(Player_Bullets, transform.position, transform.rotation) as GameObject;
            //move.GetComponent<Rigidbody2D>().velocity = transform.forward * BulletSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

        }       
    }

    private void Update()
    {
        Shooting();
    }
}
