using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PageControl : MonoBehaviour
{
    public List<GameObject> pageList = new List<GameObject>();
    int currentPage = 0;
    int pageMax = 0;
    public TextMeshProUGUI pageText;
    private void Awake()
    {
        pageMax = pageList.Count;
        PageMove(0);
    }
    void PageMove(int Num)
    {
        if(currentPage+Num<0)
        {
            currentPage = 0;
            return;
        }
        if (currentPage + Num >= pageMax)
        {
            currentPage = pageMax-1;
            return;
        }
        pageList[currentPage].SetActive(false);
        currentPage += Num;
        pageList[currentPage].SetActive(true);
        WritePageNumText();
    }
    void WritePageNumText()
    {
        pageText.text = (currentPage + 1).ToString() + "/" + pageMax.ToString();
    }
    public void NextPage()
    {
        PageMove(1);
    }
    public void PreviousPage()
    {
        PageMove(-1);
    }
}
