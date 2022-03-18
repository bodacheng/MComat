using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwipe : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 90;
    
    Vector2 sPos; //タッチした座標
    float wid = 100, hei = 100; //スクリーンサイズ
    float left_right, left_right_old,　_z; //変数
    
    public void OnPointerDown()
    {
        left_right_old = left_right;
        sPos = Input.mousePosition;
    }
    
    public void OnHold()
    {
        //回転
        left_right = left_right_old + (sPos.x - Input.mousePosition.x) * rotateSpeed/ wid; //横移動量(-1<tx<1)
    }
}
