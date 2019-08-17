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

	void Start () {
        fsm = new FSM((int)States._count, (int)Flags._count);
        fsm.SetState((int)States.goToMining);

        fsm.SetRelation((int)States.goToMining, (int)Flags.inMine, (int)States.minning);
        fsm.SetRelation((int)States.minning, (int)Flags.OnFullDeposit, (int)States.goToHome);
        fsm.SetRelation((int)States.minning, (int)Flags.OnEmptyMine, (int)States.goToHome);
        fsm.SetRelation((int)States.goToHome, (int)Flags.inHome, (int)States.deposit);
        fsm.SetRelation((int)States.deposit, (int)Flags.OnEmptyDeposit, (int)States.goToMining);
	}
	
	void Update () {
		
	}
}
