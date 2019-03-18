using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    [SerializeField] private GameObject Player_Bullets;
    public float BulletSpeed;
    private float Timestamp;

    private void Shoot(float[] Rotation, float ySpeed, float xSpeed) //Add Vector3 spawnRotation and other stuff. (later)
    {
        GameObject bullet = Instantiate(Player_Bullets, transform.position, transform.rotation);
        bullet.GetComponent<BulletController>().changeRot(Rotation);
        bullet.GetComponent<BulletController>().ySpeed = ySpeed;
        bullet.GetComponent<BulletController>().xSpeed = xSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Shoot(new float[] { 0, 0, 0 }, 0.1f, 0f);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Shoot(new float[] { 0, 0, -90 }, 0f, 0.1f);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Shoot(new float[] { 0, 0, -180 }, -0.1f, 0f);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Shoot(new float[] { 0, 0, -270 }, 0f, -0.1f);
        }
    }
}
