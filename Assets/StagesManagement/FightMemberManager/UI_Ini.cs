#if UNITY_EDITOR
using UnityEngine;

public partial class StageEditor{

    bool Initialized;
    GUIStyle ButtonStyle;
    GUIStyle ButtonHasUnit;
    GUIStyle AddDeleteMember;
    GUIStyle ButtonStyle_selected;
    GUIStyle ButtonStyle_save;
    GUIStyle ButtonStyle_NineAndTwo;
    GUIStyle ButtonStyle_NineAndTwo_Selected;
    GUIStyle Big_title;
    GUIStyle Title;
    GUIStyle AttackRangeToggleGUI;
    
    void UIParamIni()
    {
        ButtonStyle = new GUIStyle(GUI.skin.button);
        ButtonStyle.normal.textColor = Color.red;
        ButtonStyle.fixedWidth = 100f;
        ButtonStyle.alignment = TextAnchor.MiddleCenter;
        
        ButtonHasUnit = new GUIStyle(GUI.skin.button);
        ButtonHasUnit.normal.textColor = Color.green;
        ButtonHasUnit.fixedWidth = 100f;
        ButtonHasUnit.alignment = TextAnchor.MiddleCenter;
        
        AddDeleteMember = new GUIStyle(GUI.skin.button);
        AddDeleteMember.normal.textColor = new Color(1, 0.3f, 0f);
        AddDeleteMember.fixedWidth = 50f;
        AddDeleteMember.alignment = TextAnchor.MiddleCenter;
        
        ButtonStyle_selected = new GUIStyle(GUI.skin.button);
        ButtonStyle_selected.normal.textColor = Color.yellow;
        ButtonStyle_selected.fixedWidth = 100f;
        ButtonStyle_selected.alignment = TextAnchor.MiddleCenter;
        
        ButtonStyle_save = new GUIStyle(GUI.skin.button);
        ButtonStyle_save.normal.textColor = Color.blue;
        ButtonStyle_save.fixedWidth = 200f;
        ButtonStyle_save.alignment = TextAnchor.MiddleCenter;
        
        Title = new GUIStyle(GUI.skin.label);
        Title.normal.textColor = Color.blue;
        Title.alignment = TextAnchor.MiddleCenter;
        
        Big_title = new GUIStyle(GUI.skin.label);
        Big_title.normal.textColor = Color.red;
        Big_title.alignment = TextAnchor.UpperLeft;
        
        ButtonStyle_NineAndTwo = new GUIStyle(GUI.skin.button);
        ButtonStyle_NineAndTwo.normal.textColor = Color.blue;
        ButtonStyle_NineAndTwo.fixedWidth = 80f;
        ButtonStyle_NineAndTwo.alignment = TextAnchor.MiddleCenter;
        
        ButtonStyle_NineAndTwo_Selected = new GUIStyle(GUI.skin.button);
        ButtonStyle_NineAndTwo_Selected.normal.textColor = Color.yellow;
        ButtonStyle_NineAndTwo_Selected.fixedWidth = 80f;
        ButtonStyle_NineAndTwo_Selected.alignment = TextAnchor.MiddleCenter;
        
        AttackRangeToggleGUI = new GUIStyle(GUI.skin.toggle)
        {
            margin = new RectOffset(1, 1, 11, 11),
            alignment = TextAnchor.MiddleCenter,
            stretchWidth = false
        };
        
        // 关卡编辑器下，技能配置文件定走resource文件夹，所以不需要走SkillsConfigInfos.loadAllSkillConfigs(), 同理角色配置文件也是
        SkillConfigTable.LoadAllSkillConfigs();
        Units.LoadByResource();
        Units.RefreshDic();
    }
}
#endif