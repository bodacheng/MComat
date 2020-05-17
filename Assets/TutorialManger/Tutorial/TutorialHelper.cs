using UnityEngine;
using UnityEngine.UI;

public class TutorialHelper : MonoBehaviour
{
    public Button ArcadeMode;
    public Button MemberEditButton;
    public Button SkillEditButton;
    public RectTransform SkillBoxAndNineSlotT;

    public static TutorialHelper target;

    void Awake()
    {
        target = this;
    }
}