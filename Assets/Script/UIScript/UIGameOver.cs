using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField]
    private GameObject UIDataBase;
    [SerializeField]
    private GameObject GameOverUI;
    [SerializeField]
    bool isGameOver = false;
    bool isRefClear = true;
    // Start is called before the first frame update
    void Start()
    {
        CheckGameOverRef();
    }

    // Update is called once per frame
    void Update()
    {
        InitIsGameOver();
        GameOver();
    }

    private void GameOver()
    {
        if (isRefClear) return;

        if (isGameOver)
        {

            CallGameOver();
        }
        else
        {
            CloseGameOver();
        }
    }
    private void CallGameOver()
    {
        GameOverUI.SetActive(true);
    }
    private void CloseGameOver()
    {
        GameOverUI.SetActive(false);
        
    }
    private void InitIsGameOver()
    {
        if (isRefClear) return;
        isGameOver = UIDataBase.GetComponent<UIDataBase>().pisGameOver;
    }
    private void CheckGameOverRef()
    {
        if (UIDataBase == null || GameOverUI == null)
        {
            isRefClear = true;
        }
        else
        {
            isRefClear = false;
        }
    }
}
