using System.Collections.Generic;
using System;

namespace Api.Dto.Model {
    
    [Serializable]
    public class GetRankOpponentsModel
    {
        OneTeam strongTeam;
        OneTeam normalTeam1;
        OneTeam normalTeam2;
        OneTeam weakTeam;
    }
}