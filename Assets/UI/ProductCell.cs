using UnityEngine;
using UnityEngine.UI;

public class ProductCell : MonoBehaviour
{
    [SerializeField] private string product_id;
    [SerializeField] private Text msg;
    [SerializeField] private Text price;
    [SerializeField] private P3Button btn;

    void Start()
    {
        btn.SetListener(()=> AndroidIAPExample.target.BuyProductID(product_id));
    }
}
