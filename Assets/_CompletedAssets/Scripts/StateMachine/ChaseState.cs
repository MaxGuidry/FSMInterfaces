using UnityEngine;
using System.Collections;
using System;

public class ChaseState : IEnemyState
{
    private readonly StatePatternEnemy enemy;
    private Transform knownPosition;
    public ChaseState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }
    private void Look()
    {
        RaycastHit hit;
        Vector3 enemyToTarget = ((enemy.chaseTarget.position + enemy.offset) - enemy.eyes.transform.position);
        if (Physics.Raycast(enemy.eyes.transform.position, enemyToTarget, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;

        }
        else
        {
            ToLastPositionState();
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        throw new NotImplementedException();
    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
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
        Chase();

    }
    private void Chase()
    {
        enemy.meshRendererFlag.material.color = Color.red;
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        knownPosition = enemy.chaseTarget.transform;
        enemy.navMeshAgent.Resume();
    }

    public void ToLastPositionState()
    {
        enemy.currentState = enemy.lastPositionState;
        enemy.lastKnownPosition = knownPosition;
    }

}
