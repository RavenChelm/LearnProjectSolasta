
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Material defaultColor; // сейчас не нужно
    [SerializeField] private Material changeColor;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] public GameObject player; //нужно ли SerializeField?
    private PlayerController _playerController;
    private PathFinder _pathFndr;
    //Навигация
    public Cell CameFrom;
    public float PathLengthFromStart;
    public float HeuristicEstimatePathLength;
    public float EstimateFullPathLength
    {
        get
        {
            return this.PathLengthFromStart + this.HeuristicEstimatePathLength;
        }
    }
    //Конец навигации
    public float distanseToPlayer;
    public List<Cell> neighbours;

    // public Cell()
    // {

    // }
    private void Start()
    {

        player = GameObject.FindWithTag("Player");              //Не работает Ссылка на объект, при добоваление через инспектор
        _playerController = player.GetComponent<PlayerController>(); //Возможно изменить, если будет провисать оптимизация, слишком много объектов делают find, но хорошо, что один раз.
        _meshRenderer.enabled = false;
        _pathFndr = GetComponent<PathFinder>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cell")
        { neighbours.Add(other.gameObject.GetComponent<Cell>()); }
    }








    private void OnMouseEnter()
    {
        _meshRenderer.enabled = true;   //Изначально клетки не видны

        _meshRenderer.material = changeColor;   //Подсвечиваются при наведение мышки

    }
    private void OnMouseExit()
    {
        _meshRenderer.enabled = false;
        _meshRenderer.material = defaultColor;
    }
    private void OnMouseDown()
    {
        if (_playerController.inWay == false)
        {
            _playerController.setPath(this);
        }
    }
}