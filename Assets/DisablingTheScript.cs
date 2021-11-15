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
            if (BoxsList[i].GetComponent<enabled>().enabled == true) //в этом цикле мы пробегаемся по всем кубам и если у них включен скрипт , мы даём им возможность двигаться
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
