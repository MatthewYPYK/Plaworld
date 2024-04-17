using System;
using System.Collections;
using UnityEngine;

public class WaveBase : MonoBehaviour
{
    protected Func<string,Enemy> SpawnEnemy;
    [SerializeField]
    protected int WaveReward = -1;
    public virtual void Awake(){
        // Start(){
        SpawnEnemy = GameManager.Instance.SpawnEnemy;
    }
    public virtual void StartWave()
    {
        if (WaveReward < 0) WaveReward = GameManager.Instance.Wave * 5;
        GameManager.Instance.WaveReward = WaveReward;
        StartCoroutine(SpawnSequence());
    }
    public virtual IEnumerator SpawnSequence()
    {
        yield return new WaitForSeconds(1f);
    }

}
