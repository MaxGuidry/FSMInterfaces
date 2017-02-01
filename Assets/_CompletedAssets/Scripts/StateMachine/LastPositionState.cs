using UnityEngine;
using System.Collections;
using System;

public class LastPositionState : IEnemyState
{
    private readonly StatePatternEnemy enemy;
    public LastPositionState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            ToChaseState();
    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }

    public void ToLastPositionState()
    {
        throw new NotImplementedException();
    }

    public void ToPatrolState()
    {
        throw new NotImplementedException();
    }

    public void UpdateState()
    {
        Look();
        GoTo();
    }
    private void Look()
    {
        RaycastHit hit;
        if (Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
            ToChaseState();

        }
    }
    private void GoTo()
    {
        enemy.meshRendererFlag.material.color = Color.blue;
        enemy.navMeshAgent.destination = enemy.lastKnownPosition.position;
        //Debug.Log(enemy.lastKnownPosition.position);
        enemy.navMeshAgent.Resume();
        if((enemy.transform.position - enemy.lastKnownPosition.position).magnitude>=0f&& (enemy.transform.position - enemy.lastKnownPosition.position).magnitude<=2f)
        {
           
            ToAlertState();
        }
    }

   
}
