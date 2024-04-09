using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaManager : MonoBehaviour
{

    public List<PlaBtn> plaBtnsList = new List<PlaBtn>();
    private GameManager gameManager;

    void Start()
    {
        foreach (Transform child in transform)
        {
            // Debug.Log(child.name);
            PlaBtn plaBtn = child.GetComponent<PlaBtn>();
            if (plaBtn != null)
                plaBtnsList.Add(plaBtn);
        }

        GameObject gameManagerObject = GameObject.Find("GameManager");
        if (gameManagerObject != null)
            gameManager = gameManagerObject.GetComponent<GameManager>();
        else 
            Debug.LogError("Cannot find 'GameManager' script");
    }

    void Update()
    {
        for (int i = 0; i < plaBtnsList.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                if (i < plaBtnsList.Count)
                    gameManager?.PickPla(plaBtnsList[i]);
                else
                    Debug.LogError("Index out of range. No PlaBtn corresponds to the key pressed.");
                break;
            }
        }
    }
}
