using UnityEngine;
using System.Collections;

public class Tank : TankBase
{
    float fitness = 0;
    protected override void OnReset()
    {
        fitness = 1;
    }

    protected override void OnThink(float dt) 
	{
        // Direction to closest mine (normalized!)
        Vector3 dirToTank = GetDirToTank(nearestTank);

        // Current tank view direction (it's always normalized)
        Vector3 dir = this.transform.forward;

        // Sets current tank view direction and direction to the mine as inputs to the Neural Network
        inputs[0] = dirToTank.x;
        inputs[1] = dirToTank.z;
        inputs[2] = dir.x;
        inputs[3] = dir.z;

        // Think!!! 
        float[] output = brain.Synapsis(inputs);

        SetForces(output[0], output[1], output[2], dt);
	}
    
    public override void AddFitness()
    {
        fitness += 10;
        genome.fitness = fitness;
    }

    public override void RemoveFitness()
    {
        fitness /= 2;
        genome.fitness = fitness;
    }

}
