using UnityEngine;
public class SpawnEnemy : MonoBehaviour 
{
    public GameObject target;
    void Start() { InvokeRepeating("Spawn", 2f, 5f); }
    void Spawn() {
        int n = Random.Range(0, 3);
        Vector3 p = Vector3.zero;
        switch (n) {
            case 0: p = new Vector3(13, 0, 13); break;
            case 1: p = new Vector3(-13, 0, 13); break;
            case 2: p = new Vector3(13, 0, -13); break;
            case 3: p = new Vector3(-13, 0, -13); break;
        }
        Instantiate(target, p, Quaternion.identity);
    } 
}