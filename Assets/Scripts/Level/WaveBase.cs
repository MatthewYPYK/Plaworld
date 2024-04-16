using System;
using System.Collections;
using UnityEngine;

public class WaveBase : MonoBehaviour
{
    protected Func<string,Enemy> SpawnEnemy;
    public virtual void Awake(){
        // Start(){
        SpawnEnemy = GameManager.Instance.SpawnEnemy;
    }
    public virtual IEnumerator SpawnSequence()
    {
        yield return new WaitForSeconds(1f);
    }

}
