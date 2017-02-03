using UnityEngine;
using System.Collections;
using System;

public class ChaseState : IEnemyState
{
    private readonly StatePatternEnemy enemy;
    private Vector3 knownPosition;
    public ChaseState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }
    private void Look()
    {
        RaycastHit hit;
        Vector3 enemyToTarget = ((enemy.chaseTarget.position + enemy.offset) - enemy.eyes.transform.position);
        
        if (Physics.Raycast(enemy.eyes.transform.position, enemyToTarget, out hit, enemy.sightRange)&&hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
            Debug.DrawLine(enemy.eyes.position, enemy.chaseTarget.position, Color.blue);

        }
        else
        {
            ToLastPositionState();
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("stuff");
    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {

    }

    public void ToPatrolState()
    {
       
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
        knownPosition = enemy.chaseTarget.position;
        enemy.navMeshAgent.Resume();
    }

    public void ToLastPositionState()
    {
        enemy.currentState = enemy.lastPositionState;
        enemy.lastKnownPosition = knownPosition;
        
    }

}
