using System;
using System.Collections;
using UnityEngine;

public class WaveBase : MonoBehaviour
{
    protected Func<string,Enemy> SpawnEnemy = GameManager.Instance.SpawnEnemy;

    // Start the wave
    // public virtual void StartWave()
    // {
    //     StartCoroutine(SpawnSequence());
    // }
    public virtual IEnumerator SpawnSequence()
    {
        yield return new WaitForSeconds(1f);
    }

}
