﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour {

	public State currentState;
	public EnemyStats enemyStats;
	public Transform eyes;
	public State remainState;

    [HideInInspector] public NavMeshAgent navMeshAgent;
	[HideInInspector] public Transform chaseTarget;
	[HideInInspector] public float stateTimeElapsed;
    [HideInInspector] public Vector3 nextWanderTarget;

	private bool aiActive {get { return navMeshAgent.enabled; }}

	void Awake () 
	{
		navMeshAgent = GetComponent<NavMeshAgent> ();
	}

	public void TransitionToState(State nextState)
	{
		if (nextState == remainState) return;

	    stateTimeElapsed = 0;
	    currentState.ExitState(this);
        currentState = nextState;
        currentState.EnterState(this);
	}

	public bool CheckIfCountDownElapsed(float duration)
	{
		stateTimeElapsed += Time.deltaTime;
		return stateTimeElapsed >= duration;
	}

    public void GenerateNewWanderTarget()
    {
        Vector2 randVect2 = Random.insideUnitCircle * enemyStats.wanderRadius;
        nextWanderTarget = transform.position + new Vector3(randVect2.x, 0f, randVect2.y);
    }

    void Update()
	{
		if (!aiActive) return;

		currentState.UpdateState(this);
	}

	void OnDrawGizmos()
	{
		if (currentState != null && eyes != null)
		{
			Gizmos.color = currentState.sceneGizmoColor;
			Gizmos.DrawWireSphere(eyes.position, enemyStats.lookSphereCastRadius);

		    Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, enemyStats.scanForPlayerRadius);
		}
	}

}