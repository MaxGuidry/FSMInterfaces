using UnityEngine;
using System.Collections;

public interface IEnemyState
{

    void UpdateState();
    void OnTriggerEnter(Collider other);
    void ToAlertState();
    void ToPatrolState();
    void ToChaseState();
    void ToLastPositionState();
}
