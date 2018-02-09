using FootballLeaguesXF.IServices;
using FootballLeaguesXF.Services;
using FootballLeaguesXF.Views;
using Unity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballLeaguesXF.ViewModels
{
    public static class InitViewAndViewModels
    {
        public static void RegisterViewsAndViewModels(IUnityContainer container, IPageFactoryService pageFactoryService)
        {
            // Services
            container.RegisterType<IApiService, ApiService>();
            container.RegisterType<ICompetitionsService, CompetitionsService>();
            container.RegisterType<ITeamsService, TeamsService>();
            container.RegisterType<ILeagueTableService, LeagueTableService>();
            container.RegisterType<ICacheService, CacheService>();

            // RegisterForNavigation
            pageFactoryService.RegisterForNavigation<CompetitionsPage, CompetitionsViewModel>();
            pageFactoryService.RegisterForNavigation<TeamsPage, TeamsViewModel>();
            pageFactoryService.RegisterForNavigation<LeagueTablePage, LeagueTableViewModel>();

        }
    }
}
