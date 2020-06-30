using dataAccess;
using UnityEngine;

public class ShowAllMyStoneLevel : MonoBehaviour
{
    int phase = 1;
    public void _ShowAllMyStoneLevel()
    {
        if (phase == 1)
        {
            MySkillStonesReader.ShowAllMyStoneLevel();
            phase = 2;
        }else{
            MySkillStonesReader.CloseAllMyStoneFloatInfo();
            phase = 1; 
        }        
    }
}
