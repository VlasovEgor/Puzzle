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
            for (int j = 0; j < 5; j++) //здесь мы создаем много одинаковых €чеек по координатам где уже есть или ещЄ будут кубы
            {
                GameObject newCell = Instantiate(Cell, new Vector3(transform.position.x + 10 * j, transform.position.y, transform.position.z + 10 * i), Quaternion.identity);
                CellArray[i, j] = newCell;
                CellArray[i, j].AddComponent<CellSettings>();
            }
        }
    }
    private void Update()
    {
        FillingInTheCells(); //эти два метода можно было бы оптимизировать, потому что вызывать их каждый кадр это довольно жЄстко
        Victory();              //но это уже совсем друга€ истори€
    }
    public void FillingInTheCells()
    {

        for (int i = 4; i >= 0; i--) //мы берЄм каждую €чейку и пробегаемс€ по каждому блоку и кубу чтоб найти ближайший
        {
            for (int j = 0; j < 5; j++)
            {
                bool _fullcell = false;
                for (int k = 0; k < BoxList.BoxsList.Count; k++)
                {
                    float _distancebox = Vector3.Distance(CellArray[i, j].transform.position, BoxList.BoxsList[k].transform.position);

                    if (_distancebox < 0.8f) //если нашли куб по тем же координатам ,  €чейка принимает цвет этого куба и блокирарует себ€, чтоб другие кубы не могли в нее попасть
                    {
                        CellArray[i, j].GetComponent<CellSettings>().Cell_Is_Full = true;
                        CellArray[i, j].GetComponent<CellSettings>().Color = BoxList.BoxsList[k].GetComponent<MoveBlocks>().Color;
                        _fullcell = true;
                    }

                }
                if (_fullcell == false) // если не нашли , то €чейка становитс€ свободной
                {
                    CellArray[i, j].GetComponent<CellSettings>().Cell_Is_Full = false;
                    CellArray[i, j].GetComponent<CellSettings>().Color = Color.black;
                }
                for (int m = 0; m < BoxList.BlockList.Count; m++) //тут мы проворачивает тоже самое только со списком блоков которые нельз€ двигать
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
    public void Victory() //в задании сказано что нужно расположить столбцы в соотвествии с цветами выше, но у мен€ это не сделано(простите)
    {                     //у мен€ не важно какой цвет будет в столбце главное чтоб во всех строках этого столбца был один цвет
                          //впринципе изначальноеусловие сделать не сложно, но врем€ уже поджимает =(
        bool _same_colors = false;
        int _identical_columns = 0;
        for (int j = 0; j < 5; j++) //здесь мы берЄм все столбики
        {
            if (j % 2 == 0) //нас интересуют только 0,2 и 4 столбики их и выбираем
            {
                for (int i = 3; i >= 0; i--) //дальше пробегаемс€ по сточкам в этих столбах
                {
                    if (CellArray[i + 1, j].GetComponent<CellSettings>().Color != CellArray[i, j].GetComponent<CellSettings>().Color) //если найдена хоть одна пара с разными цветами 
                    {                                                                                                                 //то всЄ, ценок
                        _same_colors = true;
                    }
                }
                if (_same_colors == false)//если не нашлась мы это запоминаем и смотрим на другие столбики
                {
                    _identical_columns++;
                }
            }
        }
        if (_identical_columns == 3)// если они все одинаковые то мы победили
        {
            VictoryText.text = "“ы победил";
        }
    }
}
