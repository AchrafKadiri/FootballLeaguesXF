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
    public class CompetitionsViewModel : BaseViewModel, INavigationAware
    {
        ICompetitionsService competitionsService;

        public CompetitionsViewModel(INavigationService navigationService,ICompetitionsService competitionsService) : base(navigationService)
        {
            this.competitionsService = competitionsService;
        }

        private List<RootObject> _competitions;
        public List<RootObject> Competitions
        {
            get { return _competitions; }
            set { SetProperty(ref _competitions, value); }
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }

        private RootObject _competitionSelected;
        public RootObject CompetitionSelected
        {
            get { return _competitionSelected; }
            set
            {
                _competitionSelected = value;
                if (value != null)
                {
                    base.NavigationService.NavigateToAsync<TeamsViewModel>();
                    SetProperty(ref _competitionSelected, null);
                }
            }
        }

        async Task Load()
        {
            try
            {
                var competitions = await competitionsService.GetCompetitions();

                Set(competitions);
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

        void Set(IEnumerable<RootObject> competitions)
        {
            Competitions = competitions.ToList();
        }

        public override void NavigateTo(NavParams navParams)
        {
            base.NavigateTo(navParams);

           
            Load().ToTaskRun();
        }

        public override void NavigateFrom(NavParams navParams)
        {
            base.NavigateFrom(navParams);

            navParams.Add("competitionSelected", _competitionSelected);
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
