#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Singleton;

public partial class StageEditor {
    
    int _selectedUnitIndex;
    string _focusingPosID;
    
    async void Members(FightMembers target)
    {
        EditorGUILayout.LabelField(" Enemies infos ", _title);
        GUILayout.BeginHorizontal();

        Texture2D GetIconTexture2D(Sprite icon)
        {
            Texture2D croppedTexture = new Texture2D(10,10);
            if (icon == null)
            {
                icon = SlotEmpty;
            }
            
            croppedTexture = new Texture2D( (int)icon.rect.width, (int)icon.rect.height );
            var pixels = icon.texture.GetPixels(
                (int)icon.textureRect.x, 
                (int)icon.textureRect.y, 
                (int)icon.textureRect.width, 
                (int)icon.textureRect.height );
            croppedTexture.SetPixels( pixels );
            croppedTexture.Apply();
            return croppedTexture;
        }

        var left = target.EnemySets.Get(0, 1);
        var leftIcon = left != null ? await UnitIconDic.Load(left.r_id) : null;
        _unitBtnContent = new GUIContent(GetIconTexture2D(leftIcon));
        if (GUILayout.Button(_unitBtnContent, _focusingPosID == 1.ToString() ? _unitIconSelectedStyle : _unitIconStyle))
        {
            _selectedUnitIndex = 0;
            _focusingPosID = 1.ToString();
            _focusingUnitInfo = target.EnemySets.Get(0, 1);
            _targetSlot = 0;
        }
        
        var mid = target.EnemySets.Get(0, 0);
        var midIcon = mid != null ? await UnitIconDic.Load(mid.r_id) : null;
        _unitBtnContent = new GUIContent(GetIconTexture2D(midIcon));
        if (GUILayout.Button(_unitBtnContent, _focusingPosID == 0.ToString() ? _unitIconSelectedStyle : _unitIconStyle))
        {
            _selectedUnitIndex = 0;
            _focusingPosID = 0.ToString();
            _focusingUnitInfo = target.EnemySets.Get(0, 0);
            _targetSlot = 0;
        }
        
        var right = target.EnemySets.Get(0, 2);
        var rightIcon = right != null ? await UnitIconDic.Load(right.r_id) : null;
        _unitBtnContent = new GUIContent(GetIconTexture2D(rightIcon));
        if (GUILayout.Button(_unitBtnContent, _focusingPosID == 2.ToString() ? _unitIconSelectedStyle : _unitIconStyle))
        {
            _selectedUnitIndex = 0;
            _focusingPosID = 2.ToString();
            _focusingUnitInfo = target.EnemySets.Get(0, 2);
            _targetSlot = 0;
        }
        GUILayout.EndHorizontal();
    }
}
#endif