using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlocks : MonoBehaviour
{
    public Color Color;
    private Vector3 _mousePos;
    int _indexI, _indexJ;
    Matrix Matrix;
    private void Start()
    {
        Matrix = FindObjectOfType<Matrix>();
    }
    void Update()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//здесь мы записывает координаты позиции мыши не в пикселях а в координатах нашей игры
    }
    void MoveBox() //здесь мы проверяем свободны ли ячейки куда хочется сдвинуть куб , если свободны то в добрый путь
    {
        if (transform.position.x + transform.localScale.x * 0.3 <= _mousePos.x && _mousePos.x <= transform.position.x + transform.localScale.x * 0.5 && Matrix.CellArray[_indexI, _indexJ + 1].GetComponent<CellSettings>().Cell_Is_Full == false)
        {
            gameObject.transform.position = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
            gameObject.GetComponent<enabled>().enabled = false;
        }
        else if (transform.position.x - transform.localScale.x * 0.3 >= _mousePos.x && _mousePos.x >= transform.position.x - transform.localScale.x * 0.5 && Matrix.CellArray[_indexI, _indexJ - 1].GetComponent<CellSettings>().Cell_Is_Full == false)
        {
            gameObject.transform.position = new Vector3(transform.position.x - 10, transform.position.y, transform.position.z);
            gameObject.GetComponent<enabled>().enabled = false;
        }
        if (transform.position.z + transform.localScale.z * 0.3 <= _mousePos.z && _mousePos.z <= transform.position.z + transform.localScale.z * 0.5 && Matrix.CellArray[_indexI + 1, _indexJ].GetComponent<CellSettings>().Cell_Is_Full == false)
        {
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10);
            gameObject.GetComponent<enabled>().enabled = false;
        }
        else if (transform.position.z - transform.localScale.z * 0.3 >= _mousePos.z && _mousePos.z >= transform.position.z - transform.localScale.z * 0.5 && Matrix.CellArray[_indexI - 1, _indexJ].GetComponent<CellSettings>().Cell_Is_Full == false)
        {
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 10);
            gameObject.GetComponent<enabled>().enabled = false;
        }

    }
    void SearchForTheNearestCell()
    {
        for (int i = 4; i >= 0; i--)
        {
            for (int j = 0; j < 5; j++) //тут мы находим ближайшую ячейку к кубу и запоминаем её индексы в матрице 
            {
                float _distancebox = Vector3.Distance(Matrix.CellArray[i, j].transform.position, transform.position);
                if (_distancebox < 0.8f)
                {
                    _indexI = i;
                    _indexJ = j;
                }
            }
        }
    }
    private void OnMouseDown()
    {
        SearchForTheNearestCell();
        GetComponent<enabled>().enabled = true;
    }
    private void OnMouseUp()
    {
        MoveBox();
        GetComponent<enabled>().enabled = false;
    }
}
