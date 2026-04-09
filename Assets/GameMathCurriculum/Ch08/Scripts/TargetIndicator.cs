using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : MonoBehaviour
{
    public Transform[] targets;          // 타겟들
    public GameObject indicatorPrefab;   // UI 프리팹 (Pivot 0,0)
    public Canvas canvas;                // 캔버스

    private List<GameObject> indicators = new List<GameObject>();
    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        foreach (var target in targets)
        {
            GameObject indicator = Instantiate(indicatorPrefab, canvas.transform);

            Renderer r = target.GetComponent<Renderer>();
            if (r != null)
            {
                Image img = indicator.GetComponent<Image>();
                img.color = r.material.color;
            }

            indicators.Add(indicator);
        }
    }

    void LateUpdate()
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        for (int i = 0; i < targets.Length; i++)
        {
            Transform target = targets[i];
            GameObject indicator = indicators[i];
            RectTransform rect = indicator.GetComponent<RectTransform>();

            // 월드 좌표 → 스크린 좌표
            Vector3 screenPos = cam.WorldToScreenPoint(target.position);

            // 뒤쪽이면 반전
            if (screenPos.z < 0)
            {
                screenPos.x = Screen.width - screenPos.x;
                screenPos.y = Screen.height - screenPos.y;
                screenPos.z = Mathf.Abs(screenPos.z);
            }

            // 화면 안이면 숨김
            bool isOnScreen = screenPos.x >= 0 && screenPos.x <= Screen.width &&
                              screenPos.y >= 0 && screenPos.y <= Screen.height;
            indicator.SetActive(!isOnScreen);
            if (!indicator.activeSelf) continue;

            float halfW = rect.rect.width / 2f;
            float halfH = rect.rect.height / 2f;

            float clampedX = Mathf.Clamp(screenPos.x, halfW, Screen.width - halfW);
            float clampedY = Mathf.Clamp(screenPos.y, halfH, Screen.height - halfH);

            // Canvas 모드에 따라 카메라 설정
            Camera uiCam = canvas.renderMode == RenderMode.ScreenSpaceCamera ? cam : null;

            // 스크린 좌표 → 캔버스 LocalPosition
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
                new Vector2(clampedX, clampedY),
                uiCam,
                out localPoint
            );

            rect.localPosition = localPoint;
        }
    }
}