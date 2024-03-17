using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   public void Spawn()
   {
        transform.position = LevelManager.Instance.GreenPortal.transform.position;

        StartCoroutine(Scale(new Vector3(0.1f,0.1f),new Vector3(1,1)));
   }

    public IEnumerator Scale(Vector3 from, Vector3 to)
    {
        float progress = 0;

        while (progress <=1)
        {
            transform.localScale = Vector3.Lerp(from,to,progress);

            progress += Time.deltaTime;

            yield return null;
        }

        transform.localScale = to;
    }
}
