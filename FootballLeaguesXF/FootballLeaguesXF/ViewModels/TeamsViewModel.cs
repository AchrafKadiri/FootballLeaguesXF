using FootballLeaguesXF.Extensions;
using FootballLeaguesXF.IServices;
using FootballLeaguesXF.Model;
using FootballLeaguesXF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FootballLeaguesXF.ViewModels
{
    public class TeamsViewModel : BaseViewModel, INavigationAware
    {
        ITeamsService teamsService;

        public TeamsViewModel(INavigationService navigationService, ITeamsService teamsService) : base(navigationService)
        {
            this.teamsService = teamsService;
        }

        private List<Team> _teams;
        public List<Team> Teams
        {
            get { return _teams; }
            set { SetProperty(ref _teams, value); }
        }

        private RootObject _competition;
        public RootObject Competition
        {
            get { return _competition; }
            set { SetProperty(ref _competition, value); }
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }

        //private Team _teamselected;
        //public Team CompetitionSelected
        //{
        //    get { return _teamselected; }
        //    set
        //    {
        //        _teamselected = value;
        //        if (value != null)
        //        {
        //            base.NavigationService.NavigateToAsync<TeamsViewModel>();
        //            SetProperty(ref _teamselected, null);
        //        }
        //    }
        //}

        async Task Load()
        {
            try
            {
                var teams = await teamsService.GetTeams(Competition.id);

                Set(teams);
            }
            catch (Exception ex)
            {
                bool response = await base.ProcessException(ex).DisplayAlertWithRet("Loading Information");

                if (!response)
                {
                    IsRefreshing = false;
                }
            }
            finally
            {
                IsRefreshing = false;
                IsBusy = false;
            }
        }

        void Set(IEnumerable<Team> teams)
        {
            Teams = teams.ToList();
        }

        public override void NavigateTo(NavParams navParams)
        {
            base.NavigateTo(navParams);

            Competition= navParams.Get<RootObject>("competitionSelected") as RootObject;

            Load().ToTaskRun();
        }

        public override void NavigateFrom(NavParams navParams)
        {
            base.NavigateFrom(navParams);       
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        IsRefreshing = true;
                        await Load();
                    }
                    catch (Exception ex)
                    {

                        await base.ProcessException(ex).DisplayAlert("Loading Information");
                    }
                    finally
                    {
                        IsRefreshing = false;
                    }

                });
            }
        }
    }
}
