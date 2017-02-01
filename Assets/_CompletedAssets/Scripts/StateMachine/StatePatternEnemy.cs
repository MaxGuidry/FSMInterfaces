using UnityEngine;
using System.Collections;

public class StatePatternEnemy : MonoBehaviour {

    public float searchingTurnSpeed = 120f;
    public float searchingDuration = 4f;
    public float sightRange = 20f;
    public Transform[] wayPoints;
    public Transform eyes;
    public Vector3 offset = new Vector3(0f, .5f, 0f);
    public MeshRenderer meshRendererFlag;
    [HideInInspector]
    public Transform lastKnownPosition;
    [HideInInspector]
    public Transform chaseTarget;
    [HideInInspector]
    public IEnemyState currentState;
    [HideInInspector]
    public ChaseState chaseState;
    [HideInInspector]
    public PatrolState patrolState;
    [HideInInspector]
    public AlertState alertState;
    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    [HideInInspector]
    public LastPositionState lastPositionState;
    private void Awake()
    {
        chaseState = new ChaseState(this);
        alertState = new AlertState(this);
        patrolState = new PatrolState(this);
        lastPositionState = new LastPositionState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    // Use this for initialization
    void Start () {
        currentState = patrolState;
	}
	
	// Update is called once per frame
	void Update () {
        currentState.UpdateState();
        Debug.Log(lastKnownPosition.position);
	}
    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }
}
