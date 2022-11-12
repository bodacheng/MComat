using dataAccess;
using UnityEngine;
using UnityEngine.UI;

public class ShowAllMyStoneLevel : MonoBehaviour
{
    [SerializeField] private Toggle toggle;

    void Awake()
    {
        toggle.onValueChanged.AddListener(x =>
        {
            if (x)
            {
                Stones.ShowAllMyStoneLevel();
            }
            else
            {
                Stones.CloseAllMyStoneFloatInfo();
            }
        });
    }
}
