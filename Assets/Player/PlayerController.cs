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
    public Cell lasCell;
    public Cell targetCell;
    public bool inWay = false;
    public int AvalibleDistance;
    private void Start()
    {
        transf = gameObject.GetComponent<Transform>();
        nowCell = gameObject.GetComponent<Cell>();
        targetCell = gameObject.GetComponent<Cell>();
        lasCell = gameObject.GetComponent<Cell>();
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
        if (m_Agent.velocity == Vector3.zero && inWay == true)
        {
            this.nowCell.LeavingStepBackLight(0);
            this.nowCell.StepBackLight(0, nowCell, false);
            inWay = false;
        }
        else
        {
            inWay = true;
        }
    }

    public void SetTargetCell(Cell target)
    {
        if (inWay == false)
            targetCell = target;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cell")
        {
            nowCell = other.gameObject.GetComponent<Cell>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Cell")
        {
            lasCell = other.gameObject.GetComponent<Cell>();
        }
    }
}
