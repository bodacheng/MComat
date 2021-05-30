using dataAccess;
using UnityEngine;

public class ShowAllMyStoneLevel : MonoBehaviour
{
    int phase = 1;
    public void _ShowAllMyStoneLevel()
    {
        if (phase == 1)
        {
            Stones.ShowAllMyStoneLevel();
            phase = 2;
        }else{
            Stones.CloseAllMyStoneFloatInfo();
            phase = 1; 
        }        
    }
}
