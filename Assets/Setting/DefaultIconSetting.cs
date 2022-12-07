using UnityEngine;

[CreateAssetMenu(fileName = "DefaultIconSetting", menuName = "ScriptableObjects/DefaultIconSetting", order = 4)]
public class DefaultIconSetting : ScriptableObject
{
    [SerializeField] private Sprite coinIcon;
    [SerializeField] private Sprite diamondIcon;

    public static Sprite _coinIcon;
    public static Sprite _diamondIcon;

    public void Initialise()
    {
        _coinIcon = coinIcon;
        _diamondIcon = diamondIcon;
    }
}
