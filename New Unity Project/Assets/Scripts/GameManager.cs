using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject home, goldMinePrefab;
    public Miner minerPrefab;
    public int goldMineCount = 3;

    private int minerCount = 0;

    public void SpawnMiner()
    {
        Miner newGO = Instantiate(minerPrefab, home.transform.position, Quaternion.identity, null);
        minerCount++;
        newGO.name = "Miner " + minerCount;

        newGO.deposit = home.GetComponent<GoldDeposit>();
    }

    public void SpawnMines()
    {
        for (int i = 0; i < goldMineCount; i++)
        {
            Vector3 spawnPos = new Vector3(home.transform.position.x + Random.Range(-15, 15),
                                           home.transform.position.y + Random.Range(-15, 15),
                                           0);

            GameObject newGO = Instantiate(goldMinePrefab, spawnPos, Quaternion.identity, null);
            newGO.name = "GoldMine " + (i + 1);
        }
    }

    private void Start()
    {
        SpawnMines();
    }
}
