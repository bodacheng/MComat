
public class PFSKDefine
{
    public string CatalogVersion = "";
    public Item[] Catalog;
    public string[] DropTables = new string[] { };

    public class Item
    {
        public string ItemId;
        public string ItemClass;
        public string CatalogVersion;
        public string DisplayName;
        public string Description;
        public string VirtualCurrencyPrices;
        public string RealCurrencyPrices;
        public string[] Tags = new string[] { };
        public CustomData CustomData;
        public Consumable Consumable = new Consumable();
        public string Container;
        public string Bundle;
        public bool CanBecomeCharacter;
        public bool IsStackable;
        public bool IsTradable;
        public string ItemImageUrl;
        public bool IsLimitedEdition;
        public int InitialLimitedEditionCount;
        public string ActivatedMembership;
        
    }

    public class CustomData
    {
        public string exp;
        public string equipingC;
    }

    public class Consumable
    {
        public string UsageCount;
        public string UsagePeriod;
        public string UsagePeriodGroup;
    }
}


