using System.Collections;
using UnityEngine;

public class StartUpPresentation : MonoBehaviour
{
    [Space(7)]
    [Header("Starter")]
    public Starter Starter;

    [Space(7)]
    [Header("ResourceLordSceneStarter")]
    public ResourceDownLoad ResourceDownLoad;
    
    void Start()
    {
        StartCoroutine(WholeProcess());
    }

    IEnumerator WholeProcess()
    {
        yield return ResourceDownLoad.ResourcePrepareProcess();
        if (FightGlobalSetting._programMode == FightGlobalSetting.ProgramMode.skillShow)
        {
            Starter.ToSkillShowerMode();
        }
        else
        {
            Starter.BeginNetMode();
        }
    }
}
