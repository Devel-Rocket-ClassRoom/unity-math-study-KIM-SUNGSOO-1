using UnityEngine;

public class BezierMover : MonoBehaviour
{
    private Vector3 p0, p1, p2, p3;
    private float duration; //이동시간
    private float time; 

    public void Init(Vector3 start, Vector3 control1, Vector3 control2, Vector3 end, float moveTime)
    {
        p0 = start;
        p1 = control1;
        p2 = control2;
        p3 = end;
        duration = moveTime;
        time = 0f;
    }

    void Update()
    {
        time += Time.deltaTime;
        float t = time / duration;

        transform.position = Bezier(p0, p1, p2, p3, t);

        if (t >= 1f)
        {
            Destroy(gameObject);
        }
    }

    Vector3 Bezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float u = 1 - t;

        return (u * u * u) * p0 +
               (3 * u * u * t) * p1 +
               (3 * u * t * t) * p2 +
               (t * t * t) * p3;
    }
}