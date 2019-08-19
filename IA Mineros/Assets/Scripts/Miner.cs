using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour {

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
    private Rigidbody2D rb;
    private float goldDeposit = 0.0f;
    private Mine goldMine;
    public GoldDeposit deposit;
    public float speed;
    public float maxGoldCapacity;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        fsm = new FSM((int)States._count, (int)Flags._count);
        goldMine = FindObjectOfType<Mine>();
        if (goldMine)
        {
            fsm.SetState((int)States.goToMining);
        }
        else
        {
            fsm.SetState((int)States.idle);
        }

        fsm.SetRelation((int)States.goToMining, (int)Flags.inMine, (int)States.minning);
        fsm.SetRelation((int)States.minning, (int)Flags.OnFullDeposit, (int)States.goToHome);
        fsm.SetRelation((int)States.minning, (int)Flags.OnEmptyMine, (int)States.goToHome);
        fsm.SetRelation((int)States.goToHome, (int)Flags.inHome, (int)States.deposit);
        fsm.SetRelation((int)States.deposit, (int)Flags.OnEmptyDeposit, (int)States.goToMining);
	}
	
	void FixedUpdate () {

        switch (fsm.GetState())
        {
            case (int)States.idle:
                Debug.Log("Estoy al pedo!");
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

    private void GoToMine()
    {
        Vector2 direction = new Vector2((goldMine.gameObject.transform.position - transform.position).x, (goldMine.gameObject.transform.position - transform.position).y);
        direction.Normalize();
        rb.velocity = new Vector2((direction.x * speed * Time.deltaTime),(direction.y * speed * Time.deltaTime));
        if (Vector2.Distance(goldMine.gameObject.transform.position, transform.position) < 0.1f)
        {
            fsm.SetState((int)States.minning);
            rb.velocity = Vector2.zero;
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
            fsm.SetState((int)States.goToHome);
        }


    }

    private void GoToHome()
    {
        Vector2 direction = new Vector2((deposit.transform.position - transform.position).x, (deposit.transform.position - transform.position).y);
        direction.Normalize();
        rb.velocity = new Vector2((direction.x * speed * Time.deltaTime), (direction.y * speed * Time.deltaTime));
        if (Vector2.Distance(deposit.transform.position, transform.position) < 0.1f)
        {
            fsm.SetState((int)States.deposit);
            rb.velocity = Vector2.zero;
        }
    }

    private void Deposit()
    {
        deposit.acumulatedGold++;
        goldDeposit--;
        if (goldDeposit == 0)
        {
            fsm.SetState((int)States.goToMining);
        }
        if (goldMine == null)
        {
            goldMine = FindObjectOfType<Mine>();
            if (goldMine)
            {
                fsm.SetState((int)States.goToMining);
            }
            else
            {
                fsm.SetState((int)States.idle);
            }
        }
    }

}
