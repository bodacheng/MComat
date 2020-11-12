using System;

namespace Api.Dto.Model {
    
    [Serializable]
    public class RankOpponentsModel
    {
        public OneTeam strongTeam;
        public OneTeam normalTeam1;
        public OneTeam normalTeam2;
        public OneTeam weakTeam;
    }
}