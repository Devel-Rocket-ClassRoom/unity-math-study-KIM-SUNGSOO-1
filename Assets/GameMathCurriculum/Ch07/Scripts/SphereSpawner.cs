using UnityEngine;
using UnityEngine.VFX;

public class SphereSpoawner : MonoBehaviour
{
    public GameObject spherePrefab;

    public Transform startPoint;
    public Transform endPoint;

    public int spawnCount = 5;

    public float minDuration = 2f;
    public float maxDuration = 5f;

    public float randomRange = 5f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawn();
        }
    }

    void Spawn()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            
            GameObject obj = Instantiate(spherePrefab, startPoint.position, Quaternion.identity);

            
            BezierMover mover = obj.GetComponent<BezierMover>();

            
            Vector3 p1 = startPoint.position + Random.insideUnitSphere * randomRange;
            Vector3 p2 = endPoint.position + Random.insideUnitSphere * randomRange;

            
            float duration = Random.Range(minDuration, maxDuration);

            
            mover.Init(startPoint.position, p1, p2, endPoint.position, duration);
        }
    }
}
