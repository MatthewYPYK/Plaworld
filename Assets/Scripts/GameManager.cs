using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PlaBtn ClickedBtn { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickPla(PlaBtn plaBtn)
    {
        this.ClickedBtn = plaBtn;
        Hover.Instance.Activate(plaBtn.Sprite);
    }

    public void BuyPla()
    {
        ClickedBtn = null;
    }
}
