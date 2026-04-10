using UnityEngine;

public class DragObject : MonoBehaviour
{
    public bool isReturning;
    public Vector3 originalPosition;
    private Vector3 startPosition;
    public float timeReturn = 2f;

    private float timer;
    private Terrain terrain;
    private void Start()
    {
        terrain = Terrain.activeTerrain;
    }

    
    public void DragStart()
    {
        isReturning = false;
        timer = 0f;
        startPosition = Vector3.zero;
        originalPosition = transform.position;
        
    }
    public void Return()
    {
        Debug.Log("Return");
        timer = 0f;
        isReturning = true;
        startPosition = transform.position;
    }

    public void DragEnd()
    {
        Debug.Log("DragEnd");
        isReturning = false;
        timer = 0f;
        originalPosition = Vector3.zero;
        startPosition = Vector3.zero;

    }
    private void Update()
    {
        if(isReturning)
        {
            timer += Time.deltaTime / timeReturn;
            Vector3 newPos = Vector3.Lerp(startPosition, originalPosition, timer);
            newPos.y = terrain.SampleHeight(newPos);
            transform.position = newPos;

            if(timer > 1f)
            {
                isReturning = false;
                transform.position = originalPosition;
                timer = 0f;
            }
        }
    }

}
