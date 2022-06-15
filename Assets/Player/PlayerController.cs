using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    private CharacterController _Character;
    public Transform transf;
    private Vector3 nowPosition;
    public Cell nowCell;
    public Vector3 targetPosition;
    private Cell targetCell;
    private Vector3 velosityVector;
    private int velosity;
    public bool inWay;
    private void Start()
    {
        _Character = gameObject.GetComponent<CharacterController>();
        transf = gameObject.GetComponent<Transform>();
    }
    private void Update()
    {
        MoveCharacter();
        //Debug.Log(targetPosition);
    }
    private void MoveCharacter()
    {
        var heading = targetPosition - transf.position;
        var distance = heading.magnitude;
        var direction = heading / distance; // This is now the normalized direction.
        _Character.Move(motion: direction * Time.deltaTime * velosity);
        if ((this.transform.position.x <= targetPosition.x + 0.1 && this.transform.position.x >= targetPosition.x - 0.1) &&
            (this.transform.position.y <= targetPosition.y + 0.1 && this.transform.position.y >= targetPosition.y - 0.1) &&
            (this.transform.position.z <= targetPosition.z + 0.1 && this.transform.position.z >= targetPosition.z - 0.1))
        {
            velosity = 0;
            nowCell = targetCell;
            targetCell = null;
            inWay = false;
        }
        else
        {
            velosity = 5;
            inWay = true;
        }
    }

    public void setTargetPosition(Vector3 pos, Cell target)
    {
        targetPosition = pos;
        targetCell = target;
    }

    // public Collection<Cell> FindPath (Cell Target) {
    //     var ClosedSet = new Collection<Cell>();
    //     var OpenSet = new Collection<Cell>();
    //     OpenSet.Add(nowCell);
    //     var currentCell = OpenSet.First();
    //     while(OpenSet.Count > 0)
    //     {
    //         if (currentCell == Target)
    //             return OpenSet;
    //         OpenSet.Remove(currentCell);
    //         ClosedSet.Add(currentCell);
    //     }
    // }
}
