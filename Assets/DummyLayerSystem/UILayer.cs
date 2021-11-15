using UnityEngine;

public class UILayer : MonoBehaviour
{
    private string index;
    
    public string Index
    {
        get => index;
        set => index = value;
    }

    public virtual void OnDestroy()
    {
        
    }
}
