using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool IsActive;
   public void Spawn()
   {
        transform.position = LevelManager.Instance.GreenPortal.transform.position;

        StartCoroutine(Scale(new Vector3(0.1f,0.1f),new Vector3(1,1), false));
   }

    public IEnumerator Scale(Vector3 from, Vector3 to, bool remove)
    {
        float progress = 0;

        while (progress <=1)
        {
            transform.localScale = Vector3.Lerp(from,to,progress);

            progress += Time.deltaTime;

            yield return null;
        }

        transform.localScale = to;
        IsActive = true;
        if (remove)
        {
            Release();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coral")
        {
            StartCoroutine(Scale(new Vector3(1,1),new Vector3(0.1f,0.1f), true));
        }
    }

    private void Release()
    {
        IsActive = false;
        //GridPosition = LevelManager.Instance.GreenSpawn;
        GameManager.Instance.Pool.ReleaseObject(gameObject);
        GameManager.Instance.RemoveEnemy(this);
    }
}
