using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float XDistance=15f, YDistance=8f, ZDistance=8f;
    public float marginDistance = 1f;
    void Update()
    {
        float d = Vector3.Distance(transform.position, target.position);
        if (d > marginDistance) {
            Vector3 v1 = new Vector3(XDistance, 0, ZDistance);
            Vector3 wantedPosition = target.position - v1;
            wantedPosition.y = YDistance;
            transform.position = wantedPosition; //new Vector3(currentX, wantedPosition.y, currentZ);
        }
        transform.LookAt(target);
    }
}