using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Material defaultColor;
    [SerializeField] private Material changeColor;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] public GameObject player; //нужно ли SerializeField?
    private PlayerController _playerController;
    private Vector3 position;
    public Transform point;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");              //Не работает Ссылка на объект, при добоваление через инспектор
        _playerController = player.GetComponent<PlayerController>(); //Возможно изменить, если будет провисать оптимизация, слишком много объектов делают find, но хорошо, что один раз.
        point = transform.GetChild(0);              //Получить точку для навигации
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
            _playerController.setTargetPosition(point.position, this);
    }
}