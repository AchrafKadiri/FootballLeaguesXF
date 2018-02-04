using FootballLeaguesXF.IServices;
using FootballLeaguesXF.Services;
using FootballLeaguesXF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity;
using Xamarin.Forms;

namespace FootballLeaguesXF
{
	public partial class App : Application
	{
        public IUnityContainer container;

        public App ()
		{
			InitializeComponent();
            RegisterTypes();
            MainPage = new FootballLeaguesXF.Views.CompetitionsPage();
		}

        private void RegisterTypes()
        {
            var container = new UnityContainer();
            
            //Structural Registration
            container.RegisterInstance<App>(this);

            // Services
            container.RegisterType<ICompetitionsService, CompetitionsService>();
            container.RegisterType<IApiService, ApiService>();
            container.RegisterType<CompetitionsViewModel>();
         
            this.container = container;
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
