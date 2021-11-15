using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Matrix : MonoBehaviour
{
    public GameObject Cell;
    public GameObject[,] CellArray = new GameObject[5, 5];
    DisablingTheScript BoxList;
    public Text VictoryText;

    void Start()
    {
        BoxList = FindObjectOfType<DisablingTheScript>();
        for (int i = 4; i >= 0; i--)
        {
            for (int j = 0; j < 5; j++) //����� �� ������� ����� ���������� ����� �� ����������� ��� ��� ���� ��� ��� ����� ����
            {
                GameObject newCell = Instantiate(Cell, new Vector3(transform.position.x + 10 * j, transform.position.y, transform.position.z + 10 * i), Quaternion.identity);
                CellArray[i, j] = newCell;
                CellArray[i, j].AddComponent<CellSettings>();
            }
        }
    }
    private void Update()
    {
        FillingInTheCells(); //��� ��� ������ ����� ���� �� ��������������, ������ ��� �������� �� ������ ���� ��� �������� �����
        Victory();              //�� ��� ��� ������ ������ �������
    }
    public void FillingInTheCells()
    {

        for (int i = 4; i >= 0; i--) //�� ���� ������ ������ � ����������� �� ������� ����� � ���� ���� ����� ���������
        {
            for (int j = 0; j < 5; j++)
            {
                bool _fullcell = false;
                for (int k = 0; k < BoxList.BoxsList.Count; k++)
                {
                    float _distancebox = Vector3.Distance(CellArray[i, j].transform.position, BoxList.BoxsList[k].transform.position);

                    if (_distancebox < 0.8f) //���� ����� ��� �� ��� �� ����������� ,  ������ ��������� ���� ����� ���� � ����������� ����, ���� ������ ���� �� ����� � ��� �������
                    {
                        CellArray[i, j].GetComponent<CellSettings>().Cell_Is_Full = true;
                        CellArray[i, j].GetComponent<CellSettings>().Color = BoxList.BoxsList[k].GetComponent<MoveBlocks>().Color;
                        _fullcell = true;
                    }

                }
                if (_fullcell == false) // ���� �� ����� , �� ������ ���������� ���������
                {
                    CellArray[i, j].GetComponent<CellSettings>().Cell_Is_Full = false;
                    CellArray[i, j].GetComponent<CellSettings>().Color = Color.black;
                }
                for (int m = 0; m < BoxList.BlockList.Count; m++) //��� �� ������������� ���� ����� ������ �� ������� ������ ������� ������ �������
                {
                    float _distanceblock = Vector3.Distance(CellArray[i, j].transform.position, BoxList.BlockList[m].gameObject.transform.position);
                    if (_distanceblock < 1f)
                    {
                        CellArray[i, j].GetComponent<CellSettings>().Cell_Is_Full = true;
                    }
                }

            }
        }
    }
    public void Victory() //� ������� ������� ��� ����� ����������� ������� � ����������� � ������� ����, �� � ���� ��� �� �������(��������)
    {                     //� ���� �� ����� ����� ���� ����� � ������� ������� ���� �� ���� ������� ����� ������� ��� ���� ����
                          //��������� ������������������ ������� �� ������, �� ����� ��� ��������� =(
        bool _same_colors = false;
        int _identical_columns = 0;
        for (int j = 0; j < 5; j++) //����� �� ���� ��� ��������
        {
            if (j % 2 == 0) //��� ���������� ������ 0,2 � 4 �������� �� � ��������
            {
                for (int i = 3; i >= 0; i--) //������ ����������� �� ������� � ���� �������
                {
                    if (CellArray[i + 1, j].GetComponent<CellSettings>().Color != CellArray[i, j].GetComponent<CellSettings>().Color) //���� ������� ���� ���� ���� � ������� ������� 
                    {                                                                                                                 //�� ��, �����
                        _same_colors = true;
                    }
                }
                if (_same_colors == false)//���� �� ������� �� ��� ���������� � ������� �� ������ ��������
                {
                    _identical_columns++;
                }
            }
        }
        if (_identical_columns == 3)// ���� ��� ��� ���������� �� �� ��������
        {
            VictoryText.text = "�� �������";
        }
    }
}
