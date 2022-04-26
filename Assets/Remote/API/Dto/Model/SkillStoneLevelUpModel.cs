using System.Collections.Generic;

namespace dataAccess
{
    public class SkillStoneLevelUpModel
    {
        public List<string> warnMessage;
        public List<string> StonesToDelete;
        
        public void LocalAnalysis(SkillStoneLevelUpForm form)
        {
            StonesToDelete = new List<string>();
            if (form.M1Stone != null)
            {
                StonesToDelete.Add(form.M1Stone);
            }
            if (form.M2Stone != null)
            {
                StonesToDelete.Add(form.M2Stone);
            }
            if (form.M3Stone != null)
            {
                StonesToDelete.Add(form.M3Stone);
            }
            if (form.M4Stone != null)
            {
                StonesToDelete.Add(form.M4Stone);
            }
        }
    }
}