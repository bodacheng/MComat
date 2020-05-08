using UnityEngine;
using UnityEngine.UI;

public class TutorialHelper : MonoBehaviour
{
    public Button MemberEditButton;
    public Button SkillEditButton;

    public static TutorialHelper target;

    void Awake()
    {
        target = this;
    }
}