using TMPro;
using UnityEngine;

public class ProductCell : MonoBehaviour
{
    [SerializeField] private string product_id;
    [SerializeField] private TextMeshProUGUI msg;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private P3Button btn;

    void Start()
    {
        btn.AddListener(()=> AndroidIAPExample.target.BuyProductID(product_id));
    }
}
