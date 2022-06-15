using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GridClass : MonoBehaviour
{
    [SerializeField] private Cell _prefabCell;
    [SerializeField] private Transform _prefabPoint;
    [SerializeField] private float offset;
    private Transform _child;

    [ContextMenu("Generate Grid")]
    public void GridAll()
    {
        var cellSize = _prefabCell.GetComponent<Transform>().localScale;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            GridGenerate(this.transform.GetChild(i));
        }
    }
    public void GridGenerate(Transform _child)
    {   //Получение размеров сетки и клетки для вычислений
        var cellSize = _prefabCell.GetComponent<Transform>().localScale;
        var parentSize = new Vector3(_child.transform.localScale.x, _child.transform.localScale.y, _child.transform.localScale.z);
        var parentPosition = new Vector3(_child.transform.localPosition.x, _child.transform.position.y, _child.transform.localPosition.z);
        for (int x = 0; x < parentSize.x; x++)
        {
            for (int y = 0; y < parentSize.z; y++)
            {
                //cell
                var position_cell = new Vector3(x * (cellSize.x + offset) + (parentPosition.x - parentSize.x / 2 + cellSize.x / 2), parentPosition.y + (float)0.55, y * (cellSize.z + offset) + (parentPosition.z - parentSize.z / 2 + cellSize.z / 2));
                var cell = Instantiate(_prefabCell, position_cell, Quaternion.identity);
                cell.GetComponent<Transform>().SetParent(_child);
                cell.name = $"Cell: X:{x}, Y:{y}";
                //cell.setCoordinate(x, y);

                //point
                var pointPosition = new Vector3(position_cell.x, position_cell.y + 1, position_cell.z);
                var point = Instantiate(_prefabPoint, pointPosition, Quaternion.identity, cell.transform);
                point.name = $"Point :X:{x}, Y:{y}";
            }
        }
    }

    [ContextMenu("Delete Grid")]
    public void GridDelete()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            _child = this.transform.GetChild(i);
            for (int j = _child.transform.childCount; j > 0; --j)
                DestroyImmediate(_child.transform.GetChild(0).gameObject);
        }
    }
}
