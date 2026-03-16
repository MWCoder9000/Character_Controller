using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
public class EnemyPatrol : MonoBehaviour
{
    public Transform[] PatrolPoints;
    
    public NavMeshAgent MeshAgent;
    private int PatrolPointChosen = 0;
    public bool TargetFound = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MeshAgent = GetComponent<NavMeshAgent>();
        NextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (!MeshAgent.isOnNavMesh)
        {
            MeshAgent = GetComponent<NavMeshAgent>();
            return;
        }
        if (!MeshAgent.pathPending && !TargetFound && MeshAgent.remainingDistance <= MeshAgent.stoppingDistance)
        {
            if (Random.Range(0, 100) > 80)
            {
                NextPoint();
            }
        }
    }

    void NextPoint()
    {
        if (PatrolPoints.Length == 0) //No Patrols
        {
            return;
        }
        if (!MeshAgent.isStopped)
        {
            PatrolPointChosen = Random.Range(0, PatrolPoints.Length);
        }
        MeshAgent.destination = PatrolPoints[PatrolPointChosen].position;
    }

    
    public void SomethingHeard()
    {
        Debug.Log("Reached");
        MeshAgent.isStopped = true;
    }

    public void TargetSeen(bool FoundOrNot)
    {
        TargetFound = FoundOrNot;
    }

    void TargetLost()
    {

        MeshAgent.isStopped = false;
    }
}
