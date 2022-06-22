using System.Collections;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private Transform transf;
    private NavMeshAgent m_Agent;
    public Cell nowCell;
    public Cell targetCell;
    public bool inWay = false;
    private void Start()
    {
        transf = gameObject.GetComponent<Transform>();
        nowCell = gameObject.GetComponent<Cell>();
        targetCell = gameObject.GetComponent<Cell>();
        _rigidBody = gameObject.GetComponent<Rigidbody>();
        m_Agent = gameObject.GetComponent<NavMeshAgent>();
        _rigidBody.WakeUp();
    }
    private void Update()
    {
        MoveCharacter();
    }
    private void MoveCharacter()
    {
        if (targetCell != null && !m_Agent.hasPath)
        {
            m_Agent.destination = targetCell.transform.position;
        }
        else
        {
            targetCell = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cell")
            if (!this.inWay)
            {
                this.nowCell = other.gameObject.GetComponent<Cell>();
            }
    }

}
