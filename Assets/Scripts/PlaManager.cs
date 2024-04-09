using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaManager : MonoBehaviour
{

    public List<PlaBtn> plaBtnsList = new List<PlaBtn>();

    void Start()
    {
        foreach (Transform child in transform)
        {
            // Debug.Log(child.name);
            PlaBtn plaBtn = child.GetComponent<PlaBtn>();
            if (plaBtn != null)
                plaBtnsList.Add(plaBtn);
        }

    }

    void Update()
    {
        for (int i = 0; i < plaBtnsList.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                if (i < plaBtnsList.Count)
                    GameManager.Instance.PickPla(plaBtnsList[i]);
                break;
            }
        }
    }
}
