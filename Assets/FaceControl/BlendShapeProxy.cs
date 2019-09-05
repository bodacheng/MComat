using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using VRM;

public class BlendShapeProxy : MonoBehaviour
{
    // happy , fierce ,pain , surprised , 
    //public VRMBlendShapeProxy VRMBlendShapeProxy;
    
    /// <summary>
    /// 以下参数为本模块进程控制相关
    /// </summary>
    private IEnumerator MenuProcess;
    private bool processEnded = false;
    private void setProcessStartEnd(bool a)
    {
        processEnded = a;
    }
    public void triggerMainProcess(IEnumerator _process)
    {
        StartCoroutine(this.MainProcess(_process));
    }
    private IEnumerator giveProcessStartEndFlag(IEnumerator _process)
    {
        setProcessStartEnd(false);
        yield return _process;
        setProcessStartEnd(true);
    }
    private IEnumerator MainProcess(IEnumerator _process)//这个函数是供外界调用的。
    {
        if (MenuProcess != null)
        {
            while (!processEnded)
            {
                yield return null;
            };
        }
        MenuProcess = giveProcessStartEndFlag(_process);
        yield return MenuProcess;
    }
    
    //IEnumerator SetValueGradually(BlendShapeKey blendShapeKey,float target_value,int lastframes)
    //{
    //    if (VRMBlendShapeProxy != null)
    //    {
    //        float currentvalue = VRMBlendShapeProxy.GetValue(blendShapeKey);
    //        float acc = (target_value - currentvalue) / lastframes;
    //        for (int i = 0; i < lastframes; i++)
    //        {
    //            currentvalue += acc;
    //            VRMBlendShapeProxy.ImmediatelySetValue(blendShapeKey,currentvalue);
    //            yield return null;
    //        }           
    //    }
    //    yield break;
    //}
    
    //public void setBlendShapeGrdually(BlendShapeKey blendShapeKey,float value,int lastFrames)
    //{
    //    triggerMainProcess(SetValueGradually(blendShapeKey,value,lastFrames));
    //}
    
    //public void setBlendShape(BlendShapeKey blendShapeKey,float value)
    //{
    //    if (VRMBlendShapeProxy != null)
    //    {
    //        VRMBlendShapeProxy.AccumulateValue(blendShapeKey,value);
    //        //VRMBlendShapeProxy.Apply();
    //    }
    //}
    
    //public void returnToDefault()
    //{
    //    if (VRMBlendShapeProxy != null)
    //    {
    //        //VRMBlendShapeProxy.SetValue();
    //        //VRMBlendShapeProxy.Apply();
    //    }
    //}
}
