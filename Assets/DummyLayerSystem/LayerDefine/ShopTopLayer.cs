using UnityEngine;

namespace mainMenu
{
    public class ShopTopLayer : UILayer
    {
        [SerializeField] AdsBtnRender adsBtnRender;
        [SerializeField] RectTransform productParent;
        [SerializeField] RectTransform beginnerBundleParent;
        
        public void Initialize()
        {
            adsBtnRender.Setup();
            RefreshSize();
        }

        void RefreshSize()
        {
            productParent.sizeDelta = new Vector2(480 * productParent.childCount,productParent.sizeDelta.y);
        }

        public void ShowBeginnerBundle(bool on)
        {
            beginnerBundleParent.gameObject.SetActive(on);
            RefreshSize();
        }
    }
}