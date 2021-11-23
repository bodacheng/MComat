using UnityEngine;
using UnityEngine.EventSystems;
using mainMenu;

public partial class StoneCell : MonoBehaviour, IDropHandler
{
    public void RemoveToTemp()
    {
        UpdateMyItem();
        if (myDadItem)
        {
            myDadItem._using = false;
            myDadItem.gameObject.transform.SetParent(PreScene.target.stonesTempContainer);
        }
        UpdateMyItem();
    }
}
