using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : MonoBehaviour
{
    public Transform[] targets;          // Å¸°Ùµé
    public GameObject indicatorPrefab;   // UI ÇÁ¸®ÆÕ 
    public Canvas canvas;                // Äµ¹ö½º

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

            // ¿ùµå ÁÂÇ¥ ¡æ ½ºÅ©¸° ÁÂÇ¥
            Vector3 screenPos = cam.WorldToScreenPoint(target.position);

            
            

            // È­¸é ¾ÈÀÌ¸é ¼û±è
            bool isOnScreen = screenPos.x >= 0 && screenPos.x <= Screen.width &&
                              screenPos.y >= 0 && screenPos.y <= Screen.height;
            indicator.SetActive(!isOnScreen);
            if (!indicator.activeSelf) continue;

            Vector3 local = cam.transform.InverseTransformPoint(targets[i].position);
            Vector2 dir = new Vector2(local.x, local.y);
            Vector2 center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
            float scale = Screen.width;
            Vector2 pos = center + dir*scale;

            pos.x = Mathf.Clamp(pos.x, 0f, Screen.width);
            pos.y = Mathf.Clamp(pos.y, 0f, Screen.height);

            // Canvas ¸ðµå¿¡ µû¶ó Ä«¸Þ¶ó ¼³Á¤
            Camera uiCam = canvas.renderMode == RenderMode.ScreenSpaceCamera ? cam : null;

            // ½ºÅ©¸° ÁÂÇ¥ ¡æ Äµ¹ö½º LocalPosition
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
                new Vector2(pos.x, pos.y),
                uiCam,
                out localPoint
            );

            rect.localPosition = localPoint; 
        }
    }
}