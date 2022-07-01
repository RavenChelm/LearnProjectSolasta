
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Material defaultColor; // обычный цвет
    [SerializeField] private Material hoverColor;   // цвет при наведение мыши
    [SerializeField] private Material backColor;    // подсветка при выборе движения
    [SerializeField] public MeshRenderer _meshRenderer; //Нужно ли? 
    private PlayerController _playerController;
    public List<Cell> neighbours;
    public int step = -1;
    public bool mark = false;
    public bool sideCell = false;
    private int h = 0;
    private void Start()
    {
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>(); //Возможно изменить, если будет провисать оптимизация, слишком много объектов делают find, но хорошо, что один раз.
        ChangeColor(-1);
    }

    private void Update()
    {
        if (neighbours.Count > 8)
        {
            Debug.Log(this.name);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cell")
        { neighbours.Add(other.gameObject.GetComponent<Cell>()); }
    }
    private void OnMouseEnter()
    {
        ChangeColor(0);//Подсвечиваются при наведение мышки
    }
    private void OnMouseExit()
    {
        if (_meshRenderer.material != backColor)
        {
            ChangeColor(-1);
        }

    }
    private void OnMouseDown()
    {
        _playerController.SetTargetCell(this);
    }


    private void ChangeColor(int color)
    {
        switch (color)
        {
            case 0: //при наведение
                _meshRenderer.enabled = true;
                _meshRenderer.material = hoverColor;
                break;
            case 1: //подсветка доступных для перемещения
                _meshRenderer.enabled = true;
                _meshRenderer.material = backColor;
                break;
            case -1: //изначально выключенны
                _meshRenderer.enabled = false;
                break;
        }
    }

    public void StepBackLight(int lastStep, Cell parent, bool side)
    {
        step = lastStep; //получаем последний шаг
        if (mark == false)//отмечаем ячейку как посещённую
            mark = true;
        else
            return;
        if (step < _playerController.AvalibleDistance) // дальность хода не превышенна 
        {
            for (int i = 0; i < neighbours.Count; i++) //цикл по соседям
            {
                //дистанция до соседней клетки. Нужна, чтобы отсеивать клетки по диагонали?
                float distanceToNeigbours = Vector3.Distance(this.transform.position, neighbours[i].transform.position);
                //дистанция до родительской клетки. Нужно для определение главной линии
                float distanceToParent = Vector3.Distance(neighbours[i].transform.position, parent.transform.position);

                //Если соседни клетки находятся по бокам на одном уровне или|| они побокам на уровень выше и && клетка не посещена
                if (((distanceToNeigbours <= (float)1 + 0.05 && (distanceToNeigbours >= (float)1 - 0.05) && neighbours[i].h == this.h) || (
                    (distanceToNeigbours == (float)1.414214) && neighbours[i].h == this.h + 1)))
                {
                    if (side == false)
                    {
                        //Если эта клетка находиться на главной оси или|| она родительская 
                        if ((neighbours[i].transform.position.z == parent.transform.position.z || this == parent))
                        {
                            neighbours[i].ChangeColor(1);
                            neighbours[i].StepBackLight((int)distanceToParent + 1, parent, false);
                        }
                        else if ((neighbours[i].transform.position.x == parent.transform.position.x))
                        {
                            neighbours[i].ChangeColor(1);
                            neighbours[i].StepBackLight((int)distanceToParent + 1, parent, false);
                        }
                        else
                        {
                            neighbours[i].ChangeColor(1);
                            neighbours[i].StepBackLight((int)distanceToParent + 2, parent: this, true);
                        }
                    }
                    else
                    {
                        if (neighbours[i].transform.position.z == parent.transform.position.z)
                        {
                            neighbours[i].ChangeColor(1);
                            neighbours[i].StepBackLight(step + 1, parent, true);
                        }
                        else if (neighbours[i].transform.position.x == parent.transform.position.x)
                        {
                            neighbours[i].ChangeColor(1);
                            neighbours[i].StepBackLight(step + 1, parent, true);
                        }
                    }
                }
            }
        }
    }
    public void LeavingStepBackLight(int lastStep)
    {
        mark = false;
        step = 0;
        if (lastStep < _playerController.AvalibleDistance - 1)
        {
            for (int i = 0; i < neighbours.Count; i++)
            {
                neighbours[i].ChangeColor(-1);
                neighbours[i].LeavingStepBackLight(lastStep + 1);
            }

        }
    }
}
