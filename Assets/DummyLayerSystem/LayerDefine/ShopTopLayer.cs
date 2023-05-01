using System.Collections.Generic;
using UnityEngine;

namespace mainMenu
{
    public class ShopTopLayer : UILayer
    {
        [SerializeField] AdsBtnRender adsBtnRender;
        [SerializeField] RectTransform productParent;
        [SerializeField] ProductCell[] stoneBundleProductCells;
        
        public void Initialize()
        {
            adsBtnRender.Setup();
            RefreshSize();
        }

        void RefreshSize()
        {
            var rectTransform = productParent.GetComponent<RectTransform>();
            int activeChildCount = 0;
            // 遍历所有子物体
            for (int i = 0; i < rectTransform.childCount; i++)
            {
                Transform child = rectTransform.GetChild(i);

                // 检查子物体的激活状态
                if (child.gameObject.activeInHierarchy)
                {
                    activeChildCount++;
                }
            }
            productParent.sizeDelta = new Vector2(480 * activeChildCount,productParent.sizeDelta.y);
        }

        public void ShowStoneBundle(List<string> showTargetProductIds)
        {
            foreach (var productCell in stoneBundleProductCells)
            {
                productCell.gameObject.SetActive(showTargetProductIds.Contains(productCell.productId));
            }
            RefreshSize();
        }

        public void DisableStoneBundle(string productId)
        {
            foreach (var productCell in stoneBundleProductCells)
            {
                if (productId == productCell.productId)
                    productCell.gameObject.SetActive(false);
            }
            RefreshSize();
        }
    }
}