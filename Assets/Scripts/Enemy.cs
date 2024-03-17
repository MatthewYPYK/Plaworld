using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   public void Spawn()
   {
        transform.position = LevelManager.Instance.GreenPortal.transform.position;
   }
}
