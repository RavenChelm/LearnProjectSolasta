// using UnityEngine;
// using System.Collections.Generic;
// using System.Collections.ObjectModel;
// using System;
// using System.Linq;
// public class PathFinder : MonoBehaviour
// {
//     public List<Cell> FindPath(in Cell start, in Cell goal) //
//     {
//         // Debug.Log(start);
//         // Debug.Log(goal);
//         // Шаг 1.
//         var closedSet = new Collection<Cell>();
//         var openSet = new Collection<Cell>();
//         // Шаг 2.
//         //шагающая точка starCell
//         var startCell = gameObject.GetComponent(typeof(Cell)) as Cell;
//         openSet.Add(startCell);
//         Debug.Log(startCell);
//         while (openSet.Count > 0)
//         {
//             // Шаг 3.
//             var currentCell = openSet.OrderBy(node => node.EstimateFullPathLength).First();
//             // Шаг 4.
//             if (currentCell.transform.position == goal.transform.position)
//             {
//                 return GetPathForNode(currentCell);
//             }
//             // Шаг 5.
//             openSet.Remove(currentCell);
//             closedSet.Add(currentCell);
//             // Шаг 6.
//             foreach (var neighbourNode in GetNeighbours(currentCell, goal))
//             {
//                 // Шаг 7.
//                 if (closedSet.Count(node => node.transform.position == neighbourNode.transform.position) > 0)
//                     continue;
//                 var openNode = openSet.FirstOrDefault(node =>
//                 node.transform.position == neighbourNode.transform.position);
//                 // Шаг 8.
//                 if (openNode == null)
//                     openSet.Add(neighbourNode);
//                 else
//                 if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
//                 {
//                     // Шаг 9.
//                     openNode.CameFrom = currentCell;
//                     openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
//                 }
//             }
//         }
//         // Шаг 10.
//         return null;
//     }

//     private static int GetDistanceBetweenNeighbours()
//     {
//         return 1;
//     }

//     private static float GetHeuristicPathLength(Cell from, Cell to)
//     {
//         return Mathf.Abs(from.transform.position.x - to.transform.position.x) + Math.Abs(from.transform.position.z - to.transform.position.z);
//     }


//     private static List<Cell> GetNeighbours(Cell pathCell, Cell goal)
//     {
//         var result = new List<Cell>();
//         // Соседними точками являются соседние по стороне клетки.
//         foreach (Cell Cell in pathCell.neighbours)
//         {
//             Cell.CameFrom = pathCell;
//             Cell.PathLengthFromStart = pathCell.PathLengthFromStart + GetDistanceBetweenNeighbours();
//             Cell.HeuristicEstimatePathLength = GetHeuristicPathLength(Cell, goal);
//             result.Add(Cell);
//         }
//         return result;
//     }

//     private static List<Cell> GetPathForNode(Cell pathCell)
//     {
//         var result = new List<Cell>();
//         var currentCell = pathCell;
//         while (currentCell != null)
//         {
//             result.Add(currentCell);
//             currentCell = currentCell.CameFrom;
//         }
//         result.Reverse();
//         return result;
//     }
// }