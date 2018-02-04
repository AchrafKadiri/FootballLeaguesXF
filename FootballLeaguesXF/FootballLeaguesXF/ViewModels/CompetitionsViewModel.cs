using FootballLeaguesXF.Extensions;
using FootballLeaguesXF.IServices;
using FootballLeaguesXF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeaguesXF.ViewModels
{
    public class CompetitionsViewModel : BaseViewModel
    {
        ICompetitionsService competitionsService;

        public CompetitionsViewModel(ICompetitionsService competitionsService)
        {
            this.competitionsService = competitionsService;
            Load();
        }

        private List<RootObject> _competitions;
        public List<RootObject> Competitions
        {
            get { return _competitions; }
            set { SetProperty(ref _competitions, value); }
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
                await base.ProcessException(ex).DisplayAlertWithRet("Loading Information");
            }
        }

        void Set(IEnumerable<RootObject> competitions)
        {
            Competitions = competitions.ToList();
        }
    }
}
