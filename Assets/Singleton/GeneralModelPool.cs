using System.Collections.Generic;

public class GeneralModelPool {

    static GeneralModelPool instance;
    public static GeneralModelPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GeneralModelPool();
            }
            return instance;
        }
    }
    
    public IDictionary<string, CharPool> ModelDic = new Dictionary<string, CharPool>();
}