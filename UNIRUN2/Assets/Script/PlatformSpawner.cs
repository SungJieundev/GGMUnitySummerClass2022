using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    private float spawnTimeMin = 1.25f;
    private float spawnTimeMax = 2.25f;
    private float spawnTime;

    private float yPosMin = -3.5f;
    private float yPosMax = 1.5f;
    private float yPos;

private void Start() 
{
    StartCoroutine(SpawnCoroutine());
}
    IEnumerator SpawnCoroutine()
    {
        while (!GameManager.Instance.IsGameOver)
        {
            spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
            yPos = Random.Range(yPosMin, yPosMax);

            GameObject platform = PoolManager.instance.Pop("Platform");
            platform.transform.position = new Vector3(20, yPos, 0);

            yield return new WaitForSeconds(spawnTime);
        }
    }

}
