using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ball;
    public Transform spawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition; // get pos
            mousePos.z = 10f; // distance from cam 2 object
            Vector3 spawnPos = Camera.main.ScreenToWorldPoint(mousePos);
            Instantiate(ball, spawnPos, Quaternion.identity);
        } 
    }

    void spawnGameObject()
    {
        Instantiate(ball, spawnPoint.position, Quaternion.identity);   
    }
}
