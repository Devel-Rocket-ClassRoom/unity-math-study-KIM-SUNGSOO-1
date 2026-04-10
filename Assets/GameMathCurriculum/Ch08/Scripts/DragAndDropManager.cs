using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DragAndDropManager : MonoBehaviour
{
    public Camera camera;
    public LayerMask ground;
    public LayerMask dragObject;
    public LayerMask dropZone;
    private bool isDraging = false;
    private DragObject draggingObject;


    private void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if(Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, dragObject))
            {
                Debug.Log("Drag Start");
                isDraging = true;
                draggingObject = hitInfo.collider.GetComponent<DragObject>();
                draggingObject.DragStart();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isDraging)
            {
                if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, dropZone))
                {
                    draggingObject.DragEnd();
                    
                }
                else
                {
                    draggingObject.Return();
                }

                    
                isDraging = false;
                draggingObject = null;
            }
            

            
        }
        else if(isDraging)
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, ground))
            {
                
                draggingObject.transform.position = hitInfo.point;

            }
        }
        
    }
}
