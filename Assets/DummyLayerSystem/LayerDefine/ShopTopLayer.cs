using UnityEngine;

namespace mainMenu
{
    public class ShopTopLayer : UILayer
    {
        [SerializeField] AdsBtnRender adsBtnRender;
        
        public void Initialize()
        {
            adsBtnRender.Setup();
        }
    }
}