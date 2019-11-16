using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int framesOfLife;
    private int frames = 0;
    private TankBase ownerTank;
    private Tank[] allTanks;

    public void SetOwnerTank(TankBase t)
    {
        ownerTank = t;
    }

    private void Start()
    {
        allTanks = PopulationManager.Instance.GetAllTanks();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < PopulationManager.Instance.IterationCount; i++)
        {
            transform.position += transform.forward * Time.unscaledDeltaTime * speed;
            frames++;
            CheckCollision();
            if (frames >= framesOfLife)
            {
                Destroy(gameObject);
            }
        }
    }

    private void CheckCollision()
    {
        for (int i = 0; i < allTanks.Length; i++)
        {
            if ((TankBase)allTanks[i] != ownerTank)
                if ((this.transform.position - allTanks[i].transform.position).sqrMagnitude <= 2.0f)
                {
                    ownerTank.AddFitness();
                    allTanks[i].RemoveFitness();
                    Destroy(this.gameObject);
                }
        }
    }
}
