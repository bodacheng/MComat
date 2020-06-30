using UnityEngine;

// 这个东西的存在是为了给一些不方便添加SingleThreadProcesser又有ienumator要偶尔运行的模块来调用
public class ExtraProcesser : MonoBehaviour
{
    public SingleThreadProcesser _A;
    public static SingleThreadProcesser target;

    void Start()
    {
        target = _A;
    }
}