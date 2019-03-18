using UnityEngine;

public class BulletController : MonoBehaviour {

    [HideInInspector] public float xSpeed = 0;
    [HideInInspector] public float ySpeed = 0;

    private void Start()
    {
        Destroy(gameObject, 1f);
    }

    public void changeRot(float[] newRotation)
    {
        Quaternion changeRot;
        changeRot = transform.rotation;
        changeRot.eulerAngles = new Vector3(newRotation[0], newRotation[1], newRotation[2]);
        transform.rotation = changeRot;
    }

    private void Update()
    {
        Vector2 BulletPos = transform.position;
        BulletPos.x += xSpeed;
        BulletPos.y += ySpeed;
        transform.position = BulletPos;
    }
}
