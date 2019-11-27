using UnityEngine;

public class PinchZoom
{
    public Camera camera;
    
    float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.    
    Touch touchZero;
    Touch touchOne;
    float touchZeroscreenposx;
    float touchZeroscreenposy;
    Vector2 touchZeroPrevPos;
    Vector2 touchOnePrevPos;
    float prevTouchDeltaMag;
    float touchDeltaMag;
    float deltaMagnitudeDiff;
    
    public void localUpdate()
    {
        // If there are two touches on the device...
        if (UnityEngine.Input.touchCount == 2)
        {
            // Store both touches.
            touchZero = UnityEngine.Input.GetTouch(0);
            touchOne = UnityEngine.Input.GetTouch(1);
            touchZeroscreenposx = touchZero.position.x / Screen.width;
            touchZeroscreenposy = touchZero.position.y / Screen.height;
            
            if (touchZeroscreenposx < 0.1f || touchZeroscreenposx > 0.5f || touchZeroscreenposy < 0.1f || touchZeroscreenposy > 0.8f)
            {
                return;// 点击位置太靠近屏幕边缘。只有在画面左边的手指操作才能zoom相机。
            }

            // Find the position in the previous frame of each touch.
            touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
            if (camera.orthographic)
            {
                // ... change the orthographic size based on the change in distance between the touches.
                camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
                // Make sure the orthographic size never drops below zero.
                camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
            }
            else
            {
                // Otherwise change the field of view based on the change in distance between the touches.
                camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
                // Clamp the field of view to make sure it's between 0 and 180.
                camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 15f, 25f);
            }
        }
    }
}
