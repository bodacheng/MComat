using System;

namespace Api.Dto.Model {
    
    [Serializable]
    public class RankOpponentsModel
    {
        public PlayerArenaRankInfo strongTeam;
        public PlayerArenaRankInfo normalTeam1;
        public PlayerArenaRankInfo normalTeam2;
        public PlayerArenaRankInfo weakTeam;
    }
}