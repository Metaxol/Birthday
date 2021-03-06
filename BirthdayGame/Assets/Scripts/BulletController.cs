﻿using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

    [HideInInspector] public float xSpeed = 0;
    [HideInInspector] public float ySpeed = 0;

    public float DestroyTime;

    private bool BulletCanMov = true;

    public static float BulletRange = 0.2f;

    public AudioClip impact;
    AudioSource audioSource;

    private void Awake()
    {
        gameObject.GetComponent<Animator>().enabled = false;      
    }

    public void Destroy_Directly(float DestroyIn)
    {
        audioSource.PlayOneShot(impact);
        gameObject.GetComponent<Animator>().enabled = true;
        BulletCanMov = false;
        Destroy(gameObject, DestroyIn);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine("DestroyBullet");
    }

    public void changeRot(float[] newRotation)
    {
        Quaternion changeRot;
        changeRot = transform.rotation;
        changeRot.eulerAngles = new Vector3(newRotation[0], newRotation[1], newRotation[2]);
        transform.rotation = changeRot;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Walls":
                //audioSource.PlayOneShot(impact);
                Destroy_Directly(0.3f);
                break;
        }
    }

    private void Update()
    {
        Vector2 BulletPos = transform.position;

        if (BulletCanMov)
        {
            BulletPos.x += xSpeed;
            BulletPos.y += ySpeed;
        }

        transform.position = BulletPos;
    }

    private IEnumerator DestroyBullet()
    {      
        yield return new WaitForSeconds(DestroyTime);
        Destroy_Directly(BulletRange);
    }
}
