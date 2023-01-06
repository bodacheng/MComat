using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class GotchaLayer : UILayer
{
    [SerializeField] private List<DropTablePage> dropTables;
    [SerializeField] private Button getAllSKBtn;
    [SerializeField] private Button getAllMBtn;
    [SerializeField] private Button remove25StonesBtn;
    [SerializeField] private Button left, right;
    
    public void Setup(Action<string,string,int> execute, Action<string> dropTableInfo, Action<int, List<DropTablePage>> indexAction,
        Action getAllSK, Action getAllM, Action remove25Stones)
    {
        for (var i = 0; i < dropTables.Count; i++)
        {
            var dropTable = dropTables[i];
            dropTable.Setup(execute, dropTableInfo);
        }
        
        left.onClick.AddListener(() => { indexAction(-1, dropTables);});
        right.onClick.AddListener(() => { indexAction(1, dropTables);});
        
        indexAction(0, dropTables);
        
        getAllSKBtn.gameObject.SetActive(true);
        getAllMBtn.gameObject.SetActive(true);
        remove25StonesBtn.gameObject.SetActive(true);

        getAllSKBtn.onClick.AddListener(()=>getAllSK());
        getAllMBtn.onClick.AddListener(()=>getAllM());
        remove25StonesBtn.onClick.AddListener(()=> remove25Stones());
    }
}