using FootballLeaguesXF.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballLeaguesXF.IServices
{
    public interface INavigationAware
    {
        void NavigateTo(NavParams navParams);

        void NavigateFrom(NavParams navParams);
    }
}
