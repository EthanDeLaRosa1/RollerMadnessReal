using UnityEngine;
using System.Collections;
public class Chaser : MonoBehaviour {
    public float speed = 5.0f;
    public float minDist = 1f;
    public Transform target;
    void Start ()
    {
        if (target == null) { // if no target specified, assume the player
            if (GameObject.FindWithTag ("Player")!=null)
                target = GameObject.FindWithTag ("Player").GetComponent<Transform>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject); // destroys enemy if it hits another one
        }
        if (collision.gameObject.tag == "DeathZone")
        {
            Destroy(gameObject);
        }
        // if (collision.gameObject.tag == "Player")
        // {
        //     GameManager.gm.dec_health();
        // }
    }
    void Update ()
    {
        if (GameManager.gm.gameState != GameManager.gameStates.Playing) return;

        if (target == null) return;
        transform.LookAt(target); // face the target
        //get the distance between the chaser and the target
        float distance = Vector3.Distance(transform.position,target.position);
        //so long as the chaser is farther away than the minimum distance, move towards it at rate speed.
        if(distance > minDist) transform.position += transform.forward * speed * Time.deltaTime;
    }
}
