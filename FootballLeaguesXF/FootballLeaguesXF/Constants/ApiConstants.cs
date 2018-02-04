using System;
using System.Collections.Generic;
using System.Text;

namespace FootballLeaguesXF.Constants
{
    public enum ApiUris
    {
        AllCompetitions_Get,      
        AllTeamsByCompetition_Get      
    }

    public class ApiConstants
    {
        public const string API_HOST = "api.football-data.org";
        public const string API_PROTOCOL = "http";
        public const string GetAllCompetitions = "v1/competitions";
        public const string GetAllTeamsByCompetition = "api/competitions/{0}/teams";
    }
}
