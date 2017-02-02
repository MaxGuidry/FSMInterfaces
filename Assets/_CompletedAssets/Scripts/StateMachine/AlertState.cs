using UnityEngine;
using System.Collections;
using System;

public class AlertState : IEnemyState
{
    private readonly StatePatternEnemy enemy;

    private float searchTimer;
    public AlertState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void OnTriggerEnter(Collider other)
    {
       
    }

    public void ToAlertState()
    {
        
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
        searchTimer = 0f;
    }

    public void ToLastPositionState()
    {
        
    }

    public void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
        searchTimer = 0f;
    }

    public void UpdateState()
    {
        Look();
        Search();
    }
    private void Look()
    {
        RaycastHit hit;
        if (Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.sightRange,enemy.layermask) && hit.collider.CompareTag("Player"))
        {
            Debug.Log("this is working");
            enemy.chaseTarget = hit.transform;
            ToChaseState();

        }
    }
    private void Search()
    {
        enemy.meshRendererFlag.material.color = Color.yellow;
        enemy.navMeshAgent.Stop();
        enemy.transform.Rotate(0, enemy.searchingTurnSpeed * Time.deltaTime, 0);
        searchTimer += Time.deltaTime;
        if (searchTimer >= enemy.searchingDuration)
            ToPatrolState();

    }
}
