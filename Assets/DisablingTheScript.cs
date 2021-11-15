using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablingTheScript : MonoBehaviour
{
    public List<GameObject> BoxsList = new List<GameObject>();
    public List<GameObject> BlockList = new List<GameObject>();
    void Update()
    {
        for (int i = 0; i < BoxsList.Count; i++)
        {
            if (BoxsList[i].GetComponent<enabled>().enabled == true) //� ���� ����� �� ����������� �� ���� ����� � ���� � ��� ������� ������ , �� ��� �� ����������� ���������
            {
                BoxsList[i].GetComponent<MoveBlocks>().enabled = true;
            }
            else
            {
                BoxsList[i].GetComponent<MoveBlocks>().enabled = false;
            }
        }
    }
}
