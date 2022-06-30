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
            //this.targetCell.StepBackLight(0, nowCell, nowCell);
        }
        else
        {
            targetCell = null;

        }
        //Debug.Log(m_Agent.hasPath);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cell")
        {
            nowCell = other.gameObject.GetComponent<Cell>();
            nowCell.mark = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Cell")
        {
            lasCell = other.gameObject.GetComponent<Cell>();
            lasCell.mark = false;
            lasCell.step = -1;
        }
    }
}
