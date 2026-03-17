using UnityEngine;

public class TimeObjectDestructor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float destroyTime = 3f;
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
