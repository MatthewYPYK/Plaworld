using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaBtn : MonoBehaviour
{
    [SerializeField]
    private GameObject plaPrefab;

    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private string towerName;

    [SerializeField]
    private int price;

    public GameObject buttonText;
    TextMeshProUGUI textMesh_buttonText;

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

    void Start()
    {
        textMesh_buttonText = buttonText.GetComponent<TextMeshProUGUI>();
        textMesh_buttonText.text = towerName + ":" + price.ToString();
    }

}
