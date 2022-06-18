using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    private CharacterController _Character;
    private Rigidbody _rigidBody;
    private Transform transf;
    private PathFinder _pathFndr;
    public Cell nowCell;
    private Vector3 targetPosition;
    public Cell targetCell;
    private Cell stepCell;
    private Vector3 velosityVector;
    private int velosity;
    public bool inWay; //Добавить метод получения
    public List<Cell> path;
    private void Start()
    {
        _Character = gameObject.GetComponent<CharacterController>();
        transf = gameObject.GetComponent<Transform>();
        nowCell = gameObject.GetComponent<Cell>();
        targetCell = gameObject.GetComponent<Cell>();
        _pathFndr = gameObject.GetComponent<PathFinder>();
        _rigidBody = gameObject.GetComponent<Rigidbody>();
        _rigidBody.WakeUp();
    }
    private void Update()
    {
        //MoveCharacter();    
        //Debug.Log(targetPosition);
    }
    private void MoveCharacter()
    {

        if (path.Count != 0)
        {
            Debug.Log(path.Count);
            var heading = targetPosition - transf.position;
            var distance = heading.magnitude;
            var direction = heading / distance; // This is now the normalized direction.
            _Character.Move(motion: direction * Time.deltaTime * velosity);
            if ((this.transform.position.x <= targetPosition.x + 0.1 && this.transform.position.x >= targetPosition.x - 0.1) &&
                (this.transform.position.y <= targetPosition.y + 0.1 && this.transform.position.y >= targetPosition.y - 0.1) &&
                (this.transform.position.z <= targetPosition.z + 0.1 && this.transform.position.z >= targetPosition.z - 0.1))
            {
                velosity = 0;
                //nowCell = path.First();
                //path.Remove(path.First());
                inWay = false;
            }
            else
            {
                velosity = 5;
                inWay = true;
            }
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

    public void setPath(Cell target)
    {
        // Debug.Log(target);
        // Debug.Log(nowCell);
        // targetCell = target;
        path.Clear();
        path = _pathFndr.FindPath(nowCell, target);
    }
}
