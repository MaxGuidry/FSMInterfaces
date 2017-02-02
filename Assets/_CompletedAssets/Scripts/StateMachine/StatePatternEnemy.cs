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
    
    public Vector3 lastKnownPosition;
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
    public Vector3 currentPosition;
    [HideInInspector]
    public int layermask;
    private void Awake()
    {
        layermask = gameObject.layer;
        chaseState = new ChaseState(this);
        alertState = new AlertState(this);
        patrolState = new PatrolState(this);
        lastPositionState = new LastPositionState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    // Use this for initialization
    void Start () {
        currentState = patrolState;
        lastKnownPosition = eyes.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentPosition = transform.position;
        currentState.UpdateState();
        
	}
    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    [ContextMenu("Test it")]
    public void TestStuff()
    {
        int num = 0 << 1;
        num = ~num;
        
    }
}
