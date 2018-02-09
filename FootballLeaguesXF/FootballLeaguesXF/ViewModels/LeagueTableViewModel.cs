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
    public class LeagueTableViewModel : BaseViewModel, INavigationAware
    {
        ILeagueTableService leagueTableService;

        public LeagueTableViewModel(INavigationService navigationService, ILeagueTableService leagueTableService) : base(navigationService)
        {
            this.leagueTableService = leagueTableService;
        }

        private List<Standing> _table;
        public List<Standing> Table
        {
            get { return _table; }
            set { SetProperty(ref _table, value); }
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

   
        async Task Load()
        {
            try
            {
                var table = await leagueTableService.GetLeagueTable(Competition.id,false);

                Set(table);
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

        void Set(List<Standing> table)
        {
            int rank = 1;

            foreach(Standing s in table)
            {
                if (s.rank == 0)
                {
                    s.rank = rank;
                    rank++;
                }
            }

            Table = table.ToList();

        }

        public override void NavigateTo(NavParams navParams)
        {
            base.NavigateTo(navParams);

            Competition = navParams.Get<RootObject>("competitionSelected") as RootObject;

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
