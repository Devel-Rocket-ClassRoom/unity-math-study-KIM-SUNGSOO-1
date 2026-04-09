using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float speed = 5f;
    private float rotateSpeed = 120f;
    void Start()
    {
        
    }

    
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        transform.position = transform.position + dir *speed * Time.deltaTime;


        if(Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up * -rotateSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }
    }
}
