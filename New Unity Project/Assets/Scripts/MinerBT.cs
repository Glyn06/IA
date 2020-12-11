using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerBT : MonoBehaviour {

    private Seeker pathfinder;
    private float goldDeposit = 0.0f;
    private Mine goldMine;
    private float timer = 0f;

    public GoldDeposit deposit;
    public float maxGoldCapacity;

    //==================================//
    public MinerBehaviourTree behaviourTree;

    public Conditional hasGold;
    public Conditional isInMine;
    public Conditional isInHome;

    Action goHome;
    Action goMining;
    Action mine;
    Action depositGold;

    public float GetGoldDeposit() {
        return goldDeposit;
    }
    public GameObject GetGoldMine()
    {
        return goldMine.gameObject;
    }


    // Use this for initialization
    void Start () {
        pathfinder = GetComponent<Seeker>();

        behaviourTree.children.Add(hasGold);
        behaviourTree.children.Add(isInMine);
        behaviourTree.children.Add(isInHome);
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
    }
}
