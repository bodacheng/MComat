using System.Collections.Generic;

namespace FXKnowledge
{
    class Country
    {
        private string key;
        private Bank bank;
        private FXBank fxBank;
        private List<Trader> traders;
    }
    
    // 存储银行
    class Bank
    {
        private double amount;
        private float interest_rate;
    }
    
    class FXBank
    {
        private IDictionary<string, double> wallet = new Dictionary<string, double>();
        private float interest_rate;
        
        public float ExchangeRate(string useKey, string targetKey)
        {
            return 0.6f;
        }
    }
    
    class Trader
    {
        /// <summary>
        /// 购买外汇的话你手上不同的钱按理说是在不同国家的银行里，否则没法解释隔夜利息这一说
        /// </summary>
        private IDictionary<string, double> wallet = new Dictionary<string, double>();
        
        void FXTrade(string useKey, string targetKey, double target_amount, FXBank fxBank)
        {
            double CurrentM = wallet[useKey];
            double CurrentT = wallet[targetKey];
            float exchangeRate = fxBank.ExchangeRate(useKey, targetKey);
            double useMoneyM = target_amount / exchangeRate;
            wallet[useKey] = CurrentM - useMoneyM;
            wallet[targetKey] = CurrentT + target_amount;
        }
    }
    
    public class EconomicWorld
    {
        private Country CountryA, CountryB;
    }
}
