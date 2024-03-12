using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaBtn : MonoBehaviour
{
    [SerializeField]
    private GameObject plaPrefab;

    [SerializeField]
    private Sprite sprite;

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
}
