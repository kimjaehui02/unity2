using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject UIDataBase;
    [SerializeField]
    private GameObject PauseMenuUI;
    [SerializeField]
    bool isPaused = false;
    bool isRefClear = true;

    // Start is called before the first frame update
    void Start()
    {
        CheckPausedMenuRef();
    }

    // Update is called once per frame
    void Update()
    {
        InitIsPaused();
        PauseMenu();
    }

    private void PauseMenu()
    {
        if (isRefClear) return;

        if (isPaused)
        {
            CallPauseMenu();
        }
        else
        {
            ClosePauseMenu();
        }
    }
    public void CallPauseMenu()
    {
        PauseMenuUI.SetActive(true);
    }
    public void ClosePauseMenu()
    {
        PauseMenuUI.SetActive(false);

    }
    private void InitIsPaused()
    {
        if (isRefClear) return;
        isPaused = UIDataBase.GetComponent<UIDataBase>().pisPaused;
    }
    private void CheckPausedMenuRef()
    {
        if (UIDataBase == null || PauseMenuUI == null)
        {
            isRefClear = true;
        }
        else
        {
            isRefClear = false;
        }
    }
}
