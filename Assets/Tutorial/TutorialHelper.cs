using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using mainMenu;

public class TutorialHelper : MonoBehaviour
{
    public Button MemberEditButton;
    public Button SkillEditButton;

    public RectTransform SkillBoxAndNineSlotT;

    public static TutorialHelper target;

    // 这个结构代表了教程的顺序, 很大的特点在于可加入重复元素。典型的如后退菜单
    List<MainSceneProcess> TutorialProcesses = new List<MainSceneProcess>(); 

    void Awake()
    {
        target = this;
    }

    public void Test()
    {
        GoToMemberDetail goToMemberDetail = new GoToMemberDetail();
        OpenSkillEdit openSkillEdit = new OpenSkillEdit();
        SkillEditA1Try skillEditA1Try = new SkillEditA1Try();
        SkillEditA2Try skillEditA2Try = new SkillEditA2Try();
        SkillEditA3Try skillEditA3Try = new SkillEditA3Try();
        SkillEditTry_A1Filled skillEditTry_A1Filled = new SkillEditTry_A1Filled();
        SkillEditTry_A2Filled skillEditTry_A2Filled = new SkillEditTry_A2Filled();
        SkillEditTry_A3Filled skillEditTry_A3Filled = new SkillEditTry_A3Filled();
        ALineConfirm aLineConfirm = new ALineConfirm();
        ReturnOne returnOne = new ReturnOne();

        ProcessesRunner.Tutorial.AddNewProcess(MainSceneStep.GoToMemberDetail, goToMemberDetail);
        ProcessesRunner.Tutorial.AddNewProcess(MainSceneStep.OpenSkillEdit, openSkillEdit);

        ProcessesRunner.Tutorial.AddNewProcess(MainSceneStep.SkillEditTry_A1Selected, skillEditA1Try);
        ProcessesRunner.Tutorial.AddNewProcess(MainSceneStep.SkillEditTry_A2Selected, skillEditA2Try);
        ProcessesRunner.Tutorial.AddNewProcess(MainSceneStep.SkillEditTry_A3Selected, skillEditA3Try);
        ProcessesRunner.Tutorial.AddNewProcess(MainSceneStep.SkillEditTry_A1Filled, skillEditTry_A1Filled);
        ProcessesRunner.Tutorial.AddNewProcess(MainSceneStep.SkillEditTry_A2Filled, skillEditTry_A2Filled);
        ProcessesRunner.Tutorial.AddNewProcess(MainSceneStep.SkillEditTry_A3Filled, skillEditTry_A3Filled);

        ProcessesRunner.Tutorial.AddNewProcess(MainSceneStep.ALineConfirm, aLineConfirm);
        ProcessesRunner.Tutorial.AddNewProcess(MainSceneStep.TutorialReturn, returnOne);

        TutorialProcesses.Add(goToMemberDetail);
        TutorialProcesses.Add(openSkillEdit);
        TutorialProcesses.Add(skillEditA1Try);
        TutorialProcesses.Add(skillEditA2Try);
        TutorialProcesses.Add(skillEditA3Try);
        TutorialProcesses.Add(skillEditTry_A1Filled);
        TutorialProcesses.Add(skillEditTry_A2Filled);
        TutorialProcesses.Add(skillEditTry_A3Filled);
        TutorialProcesses.Add(aLineConfirm);
        TutorialProcesses.Add(returnOne);

        for (int i = 0; i < TutorialProcesses.Count; i++)
        {
            if (i != TutorialProcesses.Count - 1)
            {
                TutorialProcesses[i].nextProcessStep = TutorialProcesses[i + 1].Step;
            }
        }
    }
}