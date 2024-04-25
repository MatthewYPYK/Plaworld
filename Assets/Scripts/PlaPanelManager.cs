using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaManager : Singleton<PlaManager>
{
    [SerializeField]
    private GameObject plaPanel;

    private List<PlaBtn> plaBtnsList = new List<PlaBtn>();
    public int PlaBtnsCount => plaBtnsList.Count;

    void Start()
    {
        foreach (Transform child in plaPanel.transform)
        {
            // Debug.Log(child.name);
            PlaBtn plaBtn = child.GetComponent<PlaBtn>();
            if (plaBtn != null)
                plaBtnsList.Add(plaBtn);
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TogglePlaPanel();
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

    void TogglePlaPanel()
    {
        plaPanel.gameObject.SetActive(!plaPanel.gameObject.activeSelf);
    }

    public PlaBtn getStoneBtn()
    {
        for (int i = 0; i < plaBtnsList.Count; i++)
        {
            if (plaBtnsList[i].name == "Stone")
            {
                return plaBtnsList[i];
            }
        }
        return null;
    }
}
