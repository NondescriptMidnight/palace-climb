using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour
{

    public GameObject bombDrop;

    public float firingRate = 0.1f;
    public float nextFire;
    public float speed = 1;

    private bool leftAlready = false;
    private bool rightAlready = true;

    public AudioClip bombSound;

    public static float bombRate = 15f;
    public static float bombStart = 8f;


    // Use this for initialization


    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1);
    }

    void Start()
    {
        Invoke("Fire", Mathf.Clamp(bombStart, 0, bombStart));
    }

    void Fire()
    {
        if (rightAlready)
        {
            GameObject enemyShoot = Instantiate(bombDrop, transform.position, Quaternion.identity) as GameObject;
            enemyShoot.transform.parent = gameObject.transform;
            enemyShoot.GetComponent<Rigidbody2D>().velocity += Vector2.right * speed;
            rightAlready = false;
            leftAlready = true;
            Invoke("Fire", Mathf.Clamp(bombRate, 1.3f, bombRate));
            Destroy(enemyShoot.gameObject, 5f);
        }
        else if (leftAlready)
        {
            GameObject enemyShoot = Instantiate(bombDrop, transform.position, Quaternion.identity) as GameObject;
            enemyShoot.GetComponent<Rigidbody2D>().velocity -= Vector2.right * speed;
            enemyShoot.transform.parent = gameObject.transform;
            rightAlready = true;
            leftAlready = false;
            Invoke("Fire", Mathf.Clamp(bombRate, 1.3f, bombRate));
            Destroy(enemyShoot.gameObject, 5f);

        }
    }
}
