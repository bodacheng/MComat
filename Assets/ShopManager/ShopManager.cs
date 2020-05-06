using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public Canvas ShopTop;
    public static ShopManager target;

    void Awake()
    {
        target = this;
    }
}
