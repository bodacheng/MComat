using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUpScenePresentation : MonoBehaviour
{
    [Space(7)]
    [Header("LoadingProcess")]
    public LoadingCanvas _LoadingCanvas;
    
    [Space(7)]
    [Header("ResourceLordSceneStarter")]
    public ResourceLordSceneStarter _resourceLordSceneStarter;

    [Space(7)]
    [Header("开发公司商标")]
    public Image logo;
    public Image bigPic;
    
    public bool pProcessFinished = false;
    
    // step1 商标显示
    // step2 可skip的小动画
    // step3 标题
    
    void Start()
    {
        StartCoroutine(_resourceLordSceneStarter.ResourcePrepareProcess());
        StartCoroutine(presentationProcess());
    }
    
    void Update()
    {
        if (pProcessFinished && _resourceLordSceneStarter.dProcessFinished)
            SceneManager.LoadScene(1);
    }
    
    public IEnumerator presentationProcess()
    {
        _LoadingCanvas.LightUp();
        yield return new WaitForSeconds(1f);
        _LoadingCanvas.DarkOff(1);
        yield return new WaitForSeconds(1f);
        logo.gameObject.SetActive(false);
        bigPic.gameObject.SetActive(true);
        yield return new WaitForSeconds(7f);
         _LoadingCanvas.LightUp();
         pProcessFinished = true;
        yield break;
    }
}
