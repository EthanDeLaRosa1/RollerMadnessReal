using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class MovePlayer : MonoBehaviour
{
    public InputAction moveAction;
    public InputAction jumpAction;
    private Rigidbody rb;
    public float movePower = 350f;
    public bool useTorque = false;
    public float maxAngularVelocity = 25f;
    public float jumpPower = 2f;
    public bool jump = false;
    private const float GroundRayLength = 1f;
    public float maxX = 10f, maxZ = 10f;
    public GameObject coinPrefab;
    public float speed = 5f;
    public GameObject explosionPrefab;
    public GameObject coinExplosionPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DelayAction(2.0f);
        
        moveAction.Enable();
        jumpAction.Enable();
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = maxAngularVelocity;
        InvokeRepeating("SpawnCoin", 1f, 3f); // after 1s, spawn coin every 3s

        GameObject g = GameObject.Find("GameManagerEGO");
        if (g != null)
        {
            GameManager gameManagerScript = g.GetComponent<GameManager>();
        }
    }

    void Update()
    {
        if (GameManager.gm.gameState != GameManager.gameStates.Playing) return;

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 movement = new Vector3(input.x, 0.0f, input.y) * movePower * Time.deltaTime;
        
        if (useTorque)
        {
            rb.AddTorque(new Vector3(movement.z, 0, -movement.x) * movePower * Time.deltaTime);
        }
        else
        {
            rb.AddForce(movement);
        }
        
        bool isGround = Physics.Raycast(transform.position, Vector3.down, GroundRayLength); // detect if the player is on the ground
        
        if (isGround && jumpAction.IsPressed())
        {
            rb.AddForce(Vector3.up * jumpPower * Time.deltaTime, ForceMode.Impulse);
            //jump = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            if (coinExplosionPrefab != null)
            {
                Instantiate(coinExplosionPrefab, collision.transform.position, Quaternion.identity);
            }   
            Destroy(collision.gameObject);
            GameManager.gm.add_score(5);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }
            GameManager.gm.dec_health();
        }
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            GameManager.gm.dec_health();
        }
    }

    void SpawnCoin()
    {
        if (GameManager.gm.gameState != GameManager.gameStates.Playing) return;

        float randomX = UnityEngine.Random.Range(-maxX, maxX);
        float randomZ = UnityEngine.Random.Range(-maxZ, maxZ);
        Vector3 spawnPosition = new Vector3(randomX, 1.0f, randomZ);
        GameObject obj = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        obj.transform.Rotate(0, 0, 0f);
        Destroy(obj.gameObject, 10f);
    }

    private IEnumerator DelayAction(float delayTime)
    {
        while (GameManager.gm.gameState == GameManager.gameStates.Playing) {
            yield return null; // wait for the next frame
        }
        yield return new WaitForSeconds(delayTime);
    }
}
