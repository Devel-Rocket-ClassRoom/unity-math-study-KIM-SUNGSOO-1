using UnityEngine;

public class DragandDrop : MonoBehaviour
{
    public Transform dropZone; //드랍할 곳
    public float dropThreshold; //드랍 판정 범위(1~1.5f정도)
    private Vector3 originalPosition; //드롭 실패 시 기존으로 돌아갈 큐브의 최초 위치
    private bool isDragging = false; //드래그 상태 판정 변수
    private Vector3 offset;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        originalPosition = transform.position;
    }

    void Update()
    {
        if (isDragging)
        {
            DragObject();
        }

        if (Input.GetMouseButtonDown(0))
        {
            TryStartDrag();
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            StopDrag();
        }
    }

    void TryStartDrag()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform)
            {
                isDragging = true;
                offset = transform.position - hit.point;
            }
        }
    }

    void DragObject()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Terrain 위에 이동
        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Terrain")))
        {
            Vector3 targetPos = hit.point + offset;
            transform.position = new Vector3(targetPos.x, hit.point.y + transform.localScale.y / 2, targetPos.z);
        }
    }

    void StopDrag()
    {
        isDragging = false;

        // DropZone과 거리 계산
        float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                                          new Vector3(dropZone.position.x, 0, dropZone.position.z));

        if (distance <= dropThreshold)
        {
            // 성공적으로 드롭, 위치 고정
            transform.position = new Vector3(dropZone.position.x, dropZone.position.y + transform.localScale.y / 2, dropZone.position.z);
        }
        else
        {
            // 실패 시 Terrain 위로 부드럽게 원위치
            StopAllCoroutines();
            StartCoroutine(MoveBackToOriginal());
        }
    }

    System.Collections.IEnumerator MoveBackToOriginal()
    {
        float t = 0f;
        Vector3 startPos = transform.position;

        while (t < 1f)
        {
            t += Time.deltaTime;
            Vector3 nextPos = Vector3.Lerp(startPos, originalPosition, t);

            // Terrain 높이 맞추기
            Ray ray = new Ray(new Vector3(nextPos.x, 100f, nextPos.z), Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit, 200f, LayerMask.GetMask("Terrain")))
            {
                nextPos.y = hit.point.y + transform.localScale.y / 2;
            }

            transform.position = nextPos;
            yield return null;
        }
    }

}