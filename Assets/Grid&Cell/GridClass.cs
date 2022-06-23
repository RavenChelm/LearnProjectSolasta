using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GridClass : MonoBehaviour
{
    [SerializeField] private Cell _prefabCell;
    [SerializeField] private float offset;
    private Transform _child;
    [SerializeField] private bool Visibility;

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
                var position_cell = new Vector3(
                    x * (cellSize.x + offset) + (parentPosition.x - parentSize.x / 2 + cellSize.x / 2),
                    parentPosition.y + cellSize.y,
                    y * (cellSize.z + offset) + (parentPosition.z - parentSize.z / 2 + cellSize.z / 2));
                var cell = Instantiate(_prefabCell, position_cell, Quaternion.identity);
                cell.GetComponent<Transform>().SetParent(_child);
                cell.name = $"Cell: X:{x}, Y:{y}";
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
    [ContextMenu("Visibility Grid")]
    public void visibility_Grid()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            var _childFlor = this.transform.GetChild(i);
            for (int j = 0; j < _childFlor.childCount; j++)
            {
                var ChildMesh = _childFlor.GetChild(j).gameObject.GetComponent<MeshRenderer>();
                ChildMesh.enabled = Visibility;
            }
        }
    }

    public void switch_Visibility(Transform _childFlor) { }
}
