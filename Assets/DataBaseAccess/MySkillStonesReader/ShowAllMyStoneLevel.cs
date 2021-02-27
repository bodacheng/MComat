using dataAccess;
using UnityEngine;

public class ShowAllMyStoneLevel : MonoBehaviour
{
    int phase = 1;
    public void _ShowAllMyStoneLevel()
    {
        if (phase == 1)
        {
            MySkillStones.ShowAllMyStoneLevel();
            phase = 2;
        }else{
            MySkillStones.CloseAllMyStoneFloatInfo();
            phase = 1; 
        }        
    }
}
