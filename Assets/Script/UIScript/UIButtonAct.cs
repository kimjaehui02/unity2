using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonAct : MonoBehaviour
{
    [SerializeField]
    private GameObject UIDataBase;
    public GameObject PlayersPausedButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClosePausedMenuInteraction()
    {
        PlayersPausedButton.GetComponent<Players>().SetIsPaused(false);
    }
}
