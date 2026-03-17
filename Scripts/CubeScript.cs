using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeScript : MonoBehaviour
{
    Rigidbody rb;

    public float speed = 5f;
    public GameObject winText;
    GameObject wtxt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        wtxt = GameObject.FindWithTag("WinText");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) // if space pressed
            rb.AddForce(Vector3.up *20f);
        
        rb.linearVelocity = Vector3.forward * speed;
    }

    void OnMouseDown()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
            Destroy(collision.gameObject);
            //SceneManager.LoadScene("Level2");
            winText.SetActive(true);
           // wtxt.SetActive(true);
    }
}
