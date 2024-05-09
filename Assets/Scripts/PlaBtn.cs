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
    private GameObject rangePrefab;

    [SerializeField]
    private string towerName;

    [SerializeField]
    private int towerNumber;

    [SerializeField]
    private int price;

    [SerializeField]
    private bool hasRange;

    [SerializeField]
    private bool isPermanent;
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

    public GameObject RangePrefab
    {
        get
        {
            return rangePrefab;
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
    public int TowerNumber
    {
        get
        {
            return towerNumber;
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
        textMesh_buttonText.text = towerName + "\n" + "[" + towerNumber + "] " + "$" + price.ToString();
    }

}
