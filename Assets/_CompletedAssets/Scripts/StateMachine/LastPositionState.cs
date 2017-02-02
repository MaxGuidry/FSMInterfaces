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
       
    }

    public void ToPatrolState()
    {
       
    }

    public void UpdateState()
    {
        Look();
        GoTo();
    }
    private void Look()
    {
        RaycastHit hit;
        int layerMask = 0 << 8;
        if (Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.sightRange,layerMask) && hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
            ToChaseState();

        }
    }
    private void GoTo()
    {
        enemy.meshRendererFlag.material.color = Color.blue;
        enemy.navMeshAgent.Stop();
        enemy.navMeshAgent.destination = enemy.lastKnownPosition;
        enemy.navMeshAgent.Resume();
        //Debug.Log(enemy.lastKnownPosition.position);
        
        if(
            (enemy.transform.position - enemy.lastKnownPosition).magnitude >= 0f 
            && 
            (enemy.transform.position - enemy.lastKnownPosition).magnitude <= 2f)
        {
           
            ToAlertState();
        }
    }

   
}
