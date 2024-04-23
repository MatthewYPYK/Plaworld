using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class PlaBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject plaPrefab;

    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private string towerName;

    [SerializeField]
    private int price;

    [SerializeField]
    private bool hasRange;

    [SerializeField]
    private bool isPermanent;

    public GameObject buttonText;
    TextMeshProUGUI textMesh_buttonText;

    private bool isHovering = false;
    private bool isPanelActive = false;
    private float hoverTime = 1.0f;
    // [SerializeField]
    // private GameObject infoPanel;

    public GameObject PlaPrefab
    {
        get
        {
            return plaPrefab;
        }
    }

    public Sprite Sprite
    {
        get
        {
            return sprite;
        }
    }
    public string TowerName
    {
        get
        {
            return towerName;
        }

        set
        {
            towerName = value;
        }
    }
    public int Price
    {
        get
        {
            return price;
        }
        set
        {
            price = value;
        }
    }
    public bool HasRange
    {
        get
        {
            return hasRange;
        }
    }
    public bool IsPermanent
    {
        get
        {
            return isPermanent;
        }
    }
    void Start()
    {
        textMesh_buttonText = buttonText.GetComponent<TextMeshProUGUI>();
        textMesh_buttonText.text = towerName + ":" + price.ToString();

    }
    void Update()
    {
        if (isHovering)
        {
            Debug.Log("Hovering");
            hoverTime -= Time.deltaTime;
            if (hoverTime <= 0)
            {
                if (!isPanelActive)
                {
                    isPanelActive = true;
                    UIUpdater.Instance.UpdatePlaInfoFrame(true);
                }
                // infoPanel.SetActive(true);
            }

        }
        else if (isPanelActive)
        {
            isPanelActive = false;
            UIUpdater.Instance.UpdatePlaInfoFrame(false);
            // infoPanel.SetActive(false);
            hoverTime = 1.0f;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }
}
