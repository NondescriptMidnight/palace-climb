using UnityEngine;
using System.Collections;

public class ObjectBehavior : MonoBehaviour

{
    public AudioClip popSound;
    public AudioClip itemName;
    public GameObject popEffect;

    void OnTriggerEnter2D(Collider2D coll)
    {
        PlayerController player = coll.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            AudioSource audio = GetComponent<AudioSource>();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            audio.PlayOneShot(itemName);
            AudioSource.PlayClipAtPoint(popSound, transform.position, 1f);
            GameObject poppingEffect = Instantiate(popEffect, transform.position, Quaternion.identity) as GameObject;
            Destroy(poppingEffect, 0.5f);
            Destroy(gameObject, itemName.length);
        }
    }
}
