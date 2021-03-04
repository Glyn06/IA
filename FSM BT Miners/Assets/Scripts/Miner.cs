using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{

    public enum States
    {
        idle = 0,
        goToMining = 1,
        minning = 2,
        goToHome = 3,
        deposit = 4,
        _count
    }

    public enum Flags
    {
        OnFullDeposit = 0,
        OnEmptyDeposit = 1,
        inMine = 2,
        inHome = 3,
        OnEmptyMine = 4,
        _count
    }

    private FSM fsm;
    private float goldDeposit = 0.0f;
    private Mine goldMine;
    private Seeker seeker;

    public GoldDeposit deposit;
    public float maxGoldCapacity;

    private float timer = 0f;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        fsm = new FSM((int)States._count, (int)Flags._count);

        fsm.SetState((int)States.idle);

        fsm.SetRelation((int)States.goToMining, (int)Flags.inMine, (int)States.minning);
        fsm.SetRelation((int)States.minning, (int)Flags.OnFullDeposit, (int)States.goToHome);
        fsm.SetRelation((int)States.minning, (int)Flags.OnEmptyMine, (int)States.goToHome);
        fsm.SetRelation((int)States.goToHome, (int)Flags.inHome, (int)States.deposit);
        fsm.SetRelation((int)States.deposit, (int)Flags.OnEmptyDeposit, (int)States.goToMining);
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        switch (fsm.GetState())
        {
            case (int)States.idle:
                LookForMines();
                break;
            case (int)States.goToMining:
                GoToMine();
                break;
            case (int)States.minning:
                Mining();
                break;
            case (int)States.goToHome:
                GoToHome();
                break;
            case (int)States.deposit:
                Deposit();
                break;
            default:
                break;
        }
    }

    public void LookForMines()
    {
        goldMine = FindObjectOfType<Mine>();

        if (goldDeposit > 0)
        {
            seeker.FindPath(gameObject, deposit.gameObject);
            fsm.SetState((int)States.goToHome);
        }

        if (goldMine != null)
        {
            seeker.FindPath(gameObject, goldMine.gameObject);
            fsm.SetState((int)States.goToMining);
        }

        Debug.Log(gameObject.name + " is Iddle");
    }

    private void GoToMine()
    {
        if (goldMine == null)
        {
            Debug.Log(gameObject.name + ": My mine was destroyed!");
            fsm.SetState((int)States.idle);
        }
        else
        {
            if (goldMine != null)
            {
                if (seeker.Move(timer))
                    timer = 0;

                if (goldMine != null && transform.position == seeker.target.transform.position)
                {
                    fsm.SetState((int)States.minning);
                }
            }
        }
    }

    private void Mining()
    {
        if (goldMine.gold > 0)
        {
            goldMine.gold--;
            goldDeposit++;
        }

        if (goldDeposit == maxGoldCapacity || goldMine == null)
        {
            seeker.FindPath(gameObject, deposit.gameObject);
            fsm.SetState((int)States.goToHome);
        }
    }

    private void GoToHome()
    {
        if (seeker.Move(timer))
            timer = 0;

        if (transform.position == seeker.target.transform.position)
        {
            fsm.SetState((int)States.deposit);
        }
    }

    private void Deposit()
    {
        deposit.acumulatedGold++;
        goldDeposit--;
        if (goldDeposit == 0)
        {
            if (goldMine != null)
                seeker.FindPath(gameObject, goldMine.gameObject);

            fsm.SetState((int)States.goToMining);
        }
        if (goldMine == null)
        {
            goldMine = FindObjectOfType<Mine>();
            if (goldMine)
            {
                seeker.FindPath(gameObject, goldMine.gameObject);
                fsm.SetState((int)States.goToMining);
            }
            else
            {
                fsm.SetState((int)States.idle);
            }
        }
    }

}
