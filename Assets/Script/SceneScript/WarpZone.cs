using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpZone: MonoBehaviour
{
    [SerializeField]
    private string warpScene;
    static private int staticWarpID;

    [SerializeField]
    Transform player;
    [SerializeField]
    Transform warpTransform;
    [SerializeField]
    int warpID;
    [SerializeField]
    bool isInBoss;

    #region 페이드아웃 구현

    public bool boolOfFadeOut;
    public bool boolOfFadein;

    public RectTransform gameObjectOfFade;

    public float floatOfDelay;
    public float floatOfDelayTime;



    #endregion
    private void Awake()
    {
        if (staticWarpID == warpID && warpTransform != null) player.transform.localPosition = warpTransform.localPosition;
        if (isInBoss)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;

        }
    }
    private void Start()
    {
        Fadein();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            staticWarpID = warpID;
            FadeOut();
        }
    }

    private void Update()
    {
        DelayPlus();


        
    }


    private void DelayPlus()
    {
        if (boolOfFadeOut == false && boolOfFadein == false)
        {
            return;
        }


        floatOfDelayTime += Time.deltaTime;

        if(floatOfDelayTime > floatOfDelay)
        {
            floatOfDelayTime = floatOfDelay;
            
        }

        float anchoredPositions = 4000 * ((floatOfDelay - floatOfDelayTime)/ floatOfDelay);

        if (boolOfFadeOut == true)
        {
            gameObjectOfFade.localPosition = new Vector2(anchoredPositions, 0);

        }

        if (boolOfFadein == true)
        {
            gameObjectOfFade.localPosition = new Vector2(4000 - anchoredPositions, 0);

        }

        if (floatOfDelayTime >= floatOfDelay)
        {
            boolOfFadeOut = false;
            boolOfFadein = false;
            floatOfDelayTime = 0;
        }

    }

    private void FadeOut()
    {
        boolOfFadeOut = true;
        StartCoroutine(LoadScenes());
    }

    private void Fadein()
    {
        gameObjectOfFade.localPosition = new Vector2(0, 0);

        boolOfFadein = true;

    }

    public void MoveScene()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    IEnumerator LoadScenes()
    {
        yield return new WaitForSeconds(floatOfDelay+0.3f);
        LoadingSceneController.LoadScene(warpScene);

    }

}
