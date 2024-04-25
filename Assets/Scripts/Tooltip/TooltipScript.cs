using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipScript : MonoBehaviour
{
    [SerializeField]
    private Camera uiCamera;
    private TextMeshProUGUI tooltipText;
    private RectTransform backgroundRectTransform;

    private void Awake()
    {
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        tooltipText = transform.Find("TooltipText").GetComponent<TextMeshProUGUI>();

        // ShowTooltip("hello there");
    }

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        transform.localPosition = new Vector2(localPoint.x - 2, localPoint.y - 2);
    }
    public void ShowTooltip(string tooltipString)
    {
        Update();
        gameObject.SetActive(true); 

        tooltipText.text = tooltipString;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2f, tooltipText.preferredHeight + textPaddingSize * 2f);
        backgroundRectTransform.sizeDelta = backgroundSize;

    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
