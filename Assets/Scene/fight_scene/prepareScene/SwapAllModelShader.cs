using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapAllModelShader : MonoBehaviour {

    public void arrangeAllModelShader(int myFocusingLocalID,IDictionary<int, GameObject> ModelDicBasedOnLocalID)
    {
        if (myFocusingLocalID != -1)        
        {
            foreach (KeyValuePair<int, GameObject> _set in ModelDicBasedOnLocalID)
            {
                if (_set.Key != myFocusingLocalID)
                {
                    _set.Value.GetComponent<BO_Health>().swapShader("ShurikenMagic/TransparentRim");
                }
                else
                {
                    if (_set.Value != null)
                    {
                        _set.Value.GetComponent<BO_Health>().restoreMyDefaultMaterialsShaders();
                    }
                }
            }
        }else{
            foreach (KeyValuePair<int, GameObject> _set in ModelDicBasedOnLocalID)
            {
                if (_set.Value != null)
                {
                    _set.Value.GetComponent<BO_Health>().restoreMyDefaultMaterialsShaders();
                }
            }
        }
    }
}
