using System;
using mainMenu;
using dataAccess;
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
    
    private int index;
    public void Setup(Action<string,string,int> nine, Action<string> dropTableInfo, Action getAllSK, Action getAllM, Action remove25Stones)
    {
        void MoveNext(int next)
        {
            if (next > 0)
            {
                index = index + 1;
                if (index == dropTables.Count)
                {
                    index = 0;
                }
            }
            else if (next < 0)
            {
                index = index - 1;
                if (index < 0)
                {
                    index = dropTables.Count - 1;
                }
            }
            else
            {
                index = 0;
            }
            
            for (int i = 0; i < dropTables.Count; i++)
            {
                var dropTable = dropTables[i];
                dropTable.parentT.gameObject.SetActive(index == i);
            }
        }
        
        for (int i = 0; i < dropTables.Count; i++)
        {
            var dropTable = dropTables[i];
            dropTable.Setup(nine, dropTableInfo);
        }
        
        left.onClick.AddListener(() => { MoveNext(-1);});
        right.onClick.AddListener(() => { MoveNext(1);});

        MoveNext(0);
        
        getAllSKBtn.gameObject.SetActive(true);
        getAllMBtn.gameObject.SetActive(true);
        remove25StonesBtn.gameObject.SetActive(true);

        getAllSKBtn.onClick.AddListener(()=>getAllSK());
        getAllMBtn.onClick.AddListener(()=>getAllM());
        remove25StonesBtn.onClick.AddListener(()=> remove25Stones());
    }
}