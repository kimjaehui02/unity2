using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneLoad : MonoBehaviour
{

    #region �� ����
    //[Header("Scenes")]
    //[SerializeField]
    //SceneAsset MainMenu;
    //[SerializeField]
    //SceneAsset SafeZone;
    //[SerializeField]
    //SceneAsset Boss;
    //[SerializeField]
    //SceneAsset UITemplete;

    public enum SceneNames
    {
        None,
        MainMenu,
        SafeZone,
        Boss,
        UITemplete
    }
    public SceneNames LoadSceneName;
    #endregion

    #region �⺻�Լ�
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
    
    public void  UILoadScene()
    {
        if(LoadSceneName == SceneNames.MainMenu)
        {
            LoadingSceneController.LoadScene("MainMenu");
        }
        else if(LoadSceneName == SceneNames.SafeZone)
        {
            LoadingSceneController.LoadScene("SafeZone1");
        }
        else if(LoadSceneName == SceneNames.Boss)
        {
            LoadingSceneController.LoadScene("CubeRix");
        }
        else if (LoadSceneName == SceneNames.UITemplete)
        {
            LoadingSceneController.LoadScene("UITemplete");
        }
        //�߰����� ���� ���� ������ �����ؼ� ���
        
    }
    public void UiExitScene()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif  
    }


}
