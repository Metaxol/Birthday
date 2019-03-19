using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

    [SerializeField] private GameObject Player_Bullets;
    public float BulletSpeed;

    private float Timestamp;
    public float cooldown = 2f;

    private PlayerMovement GetPlayerMovement;

    [HideInInspector] public bool canShoot = true;

    private void Shoot(float[] Rotation, Vector3 Position, float ySpeed, float xSpeed) //Add Vector3 spawnRotation and other stuff. (later)
    {
        if (Time.time > Timestamp)
        {
            GameObject bullet = Instantiate(Player_Bullets, transform.position + Position, transform.rotation);
            Timestamp = Time.time + cooldown;
            bullet.GetComponent<BulletController>().changeRot(Rotation);
            bullet.GetComponent<BulletController>().ySpeed = ySpeed;
            bullet.GetComponent<BulletController>().xSpeed = xSpeed;
            StartCoroutine(Delay_StopMov(1f));
        }
    }

    private void Awake()
    {
        GetPlayerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if (canShoot)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Shoot(new float[] { 0, 0, 0 }, new Vector3(0, 0.5f), 0.1f, 0f);
            }
            else if (Input.GetKeyDown(KeyCode.D) && (GetPlayerMovement.gameObject.transform.position.x > GetPlayerMovement.LastPosition.x))
            {
                Shoot(new float[] { 0, 0, -90 }, new Vector3(0.5f, 0), 0f, 0.1f);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Shoot(new float[] { 0, 0, -180 }, new Vector3(0, -0.5f), -0.1f, 0f);
            }
            else if (Input.GetKeyDown(KeyCode.A) && (GetPlayerMovement.gameObject.transform.position.x < GetPlayerMovement.LastPosition.x))
            {
                Shoot(new float[] { 0, 0, -270 }, new Vector3(-0.5f, 0), 0f, -0.1f);
            }
        }
    }

    public IEnumerator Delay_StopMov(float Delay)
    {
        GetPlayerMovement.PlayerAnimator.SetBool("isShooting", true);
        GetPlayerMovement.PlayerCanMove = false;
        GetPlayerMovement.PlayerRigidbody2D.velocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(Delay);
        GetPlayerMovement.PlayerAnimator.SetBool("isShooting", false);
        GetPlayerMovement.PlayerCanMove = true;
    }
}
