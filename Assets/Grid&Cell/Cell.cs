
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
    private int step = -1;
    public bool mark = false;
    private int h = 0;
    private void Start()
    {
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>(); //Возможно изменить, если будет провисать оптимизация, слишком много объектов делают find, но хорошо, что один раз.
        ChangeColor(-1);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cell")
        { neighbours.Add(other.gameObject.GetComponent<Cell>()); }
    }
    private void OnMouseEnter()
    {
        ChangeColor(0);//Подсвечиваются при наведение мышки
        // Debug.Log(Vector3.Distance(this.transform.position, neighbours[0].transform.position));
        // Debug.Log(Vector3.Distance(this.transform.position, neighbours[1].transform.position));
        // Debug.Log(Vector3.Distance(this.transform.position, neighbours[2].transform.position));
        // Debug.Log(Vector3.Distance(this.transform.position, neighbours[3].transform.position));
        // Debug.Log(Vector3.Distance(this.transform.position, neighbours[4].transform.position));
        // Debug.Log(Vector3.Distance(this.transform.position, neighbours[5].transform.position));
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
        if (!_playerController.inWay)
            _playerController.targetCell = this;
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

    public void StepBackLight(int lastStep)
    {
        step = lastStep;
        mark = true;
        while (step < _playerController.AvalibleDistance)
        {
            for (int i = 0; i < neighbours.Count; i++)
            {
                float distanceToNeigbours = Vector3.Distance(this.transform.position, neighbours[i].transform.position);
                if (((distanceToNeigbours == (float)1 && neighbours[i].h == this.h) ||
                    (distanceToNeigbours == (float)1.414214) && neighbours[i].h == this.h + 1) &&  //Изменить дистанцую карабканья
                    !neighbours[i].mark)
                {
                    step++;
                    neighbours[i].ChangeColor(1);
                    //neighbours[i].StepBackLight(step);
                }
            }
        }
    }
}

