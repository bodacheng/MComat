namespace HittingDetection
{
    public class E_Damage
    {
        FightAttriCalReference Attacker_Health;
        FightAttriCalReference Damaged_Health;
        public Position_set Position_set;
        
        public E_Damage() { }
        public E_Damage(FightAttriCalReference Attacker_Health, Position_set Position_set)
        {
            this.Attacker_Health = Attacker_Health;
            this.Position_set = Position_set;
        }
        
        public FightAttriCalReference GetAttackerHealthBody()
        {
            return Attacker_Health;
        }
        
        public void SetAttackerHealthBody(FightAttriCalReference BO_Health)
        {
            Attacker_Health = BO_Health;
        }
        
        public void SetDamagedHealthBody(FightAttriCalReference b)
        {
            Damaged_Health = b;
        }
        
        public FightAttriCalReference GetDamagedHealthBody()
        {
            return Damaged_Health;
        }
    }
}
