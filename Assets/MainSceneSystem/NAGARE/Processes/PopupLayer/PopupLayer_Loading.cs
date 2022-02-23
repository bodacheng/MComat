using UnityEngine;

public partial class PopupLayer : UILayer {
    
    public static void Loading(string description, GameObject hook)
    {
        var layer = Open(hook);
        layer.DarkOff(0.8f,0.5f);
        layer.info.text = description;
        layer.loadingIcon.SetActive(true);
    }
}
