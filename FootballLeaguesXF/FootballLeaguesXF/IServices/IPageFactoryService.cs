using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FootballLeaguesXF.IServices
{
    public interface IPageFactoryService
    {

        void RegisterForNavigation<TViewModel, TView>();

        Page Resolve<TViewModel>();
    }
}
