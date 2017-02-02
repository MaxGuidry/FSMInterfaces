using UnityEngine;
using System.Collections;
using System;

public class PatrolState : IEnemyState
{
    private readonly StatePatternEnemy enemy;
    private int nextWayPoint;
    public PatrolState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }
    private void Look()
    {
        
        RaycastHit hit;
        
        if (Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.sightRange,enemy.layermask) && hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
            ToChaseState();

        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            ToAlertState();
            

    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToPatrolState()
    {
        
    }

    public void UpdateState()
    {
        Look();
        Patrol();
    }
    void Patrol()
    {
        enemy.meshRendererFlag.material.color = Color.green;
        enemy.navMeshAgent.destination = enemy.wayPoints[nextWayPoint].position;
        enemy.navMeshAgent.Resume();
        if(enemy.navMeshAgent.remainingDistance<=enemy.navMeshAgent.stoppingDistance &&!enemy.navMeshAgent.pathPending)
        {
            nextWayPoint = (nextWayPoint + 1) % enemy.wayPoints.Length;
        }

    }

    public void ToLastPositionState()
    {
        throw new NotImplementedException();
    }
}
