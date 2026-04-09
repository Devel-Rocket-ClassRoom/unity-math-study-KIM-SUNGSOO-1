using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;
    public float followSpeed = 10f;
    public float rotateSpeed = 10f;
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;


    // Update is called once per frame
    void LateUpdate()
    {

        
        Vector3 desiredPosition = target.position + target.rotation * offset;
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            smoothTime
        );
        transform.LookAt(target);
        
    }
}
