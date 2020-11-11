using System;

namespace Api.Dto.Model {
    
    [Serializable]
    public class RankOpponentsModel
    {
        OneTeam strongTeam;
        OneTeam normalTeam1;
        OneTeam normalTeam2;
        OneTeam weakTeam;
    }
}