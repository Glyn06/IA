using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerBT : MonoBehaviour
{

    private Seeker seeker;
    private float goldDeposit = 0.0f;
    private Mine goldMine;
    private float timer = 0f;

    public GoldDeposit home;
    public float maxGoldCapacity;

    Selector main;

    // Use this for initialization
    void Start()
    {
        seeker = GetComponent<Seeker>();

        n_LookForMines lookForMines = new n_LookForMines(seeker, this);
        n_Move move = new n_Move(seeker);
        n_CheckMaxCapacity checkMaxCapacity = new n_CheckMaxCapacity(this);
        n_Mine mine = new n_Mine(this);
        n_LookForHome lookForHome = new n_LookForHome(seeker, this);
        n_Deposit deposit = new n_Deposit(this);

        Sequence GoToMineSequence = new Sequence(new List<NodeBT> { lookForMines, move, checkMaxCapacity, mine });
        Sequence GoHomeSequence = new Sequence(new List<NodeBT> { lookForHome, move, deposit });

        main = new Selector(new List<NodeBT> { GoToMineSequence, GoHomeSequence });
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (main.Evaluate() == NodeState.Fail)
        {
            Debug.Log(gameObject.name + ": Im iddle!");
        }
    }

    public float GetGoldDeposit()
    {
        return goldDeposit;
    }
    public Mine GetGoldMine()
    {
        return goldMine;
    }
    public void SetGoldMine(Mine _mine)
    {
        goldMine = _mine;
    }

    public void Mine()
    {
        if (goldMine.gold > 0)
        {
            goldMine.gold--;
            goldDeposit++;
        }
    }

    public void Deposit()
    {

        if (goldDeposit > 0)
        {
            goldDeposit--;
            home.acumulatedGold++;
        }
    }

    public Mine LookForGoldMine()
    {
        Mine mine = FindObjectOfType<Mine>();
        return mine;
    }

    public float GetTimer()
    {
        return timer;
    }
    public void SetTimer(float _timer)
    {
        timer = _timer;
    }
}
