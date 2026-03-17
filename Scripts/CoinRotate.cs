using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    public float rotateSpeed = 100f;
    public enum whichwayToRotate {x, y, z};
    public whichwayToRotate rotateDirection = whichwayToRotate.y;

    // Update is called once per frame
    void Update()
    {
        switch (rotateDirection)
        {
            case whichwayToRotate.x:
                transform.Rotate(Vector3.right * Time.deltaTime * rotateSpeed);
                break;
            case whichwayToRotate.y:
                transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
                break;
            case whichwayToRotate.z:
                transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
                break;
        }
        //transform.Rotate(Vector3.right * Time.deltaTime * rotateSpeed);
    }
}
