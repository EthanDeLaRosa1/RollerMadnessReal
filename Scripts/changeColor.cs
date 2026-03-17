using UnityEngine;

public class changeColor : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV(); 
    }
}
