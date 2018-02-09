using FootballLeaguesXF.IServices;
using FootballLeaguesXF.Plugins;
using FootballLeaguesXF.Services;
using FootballLeaguesXF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity;
using Unity.Lifetime;
using Xamarin.Forms;

namespace FootballLeaguesXF
{
	public partial class App : Application
	{
        public IUnityContainer container;
        public INavigationService navigationService;
        public IPageFactoryService pageFactoryService;

        public App ()
		{
			InitializeComponent();
            RegisterTypes();
            navigationService.SetMainPage<CompetitionsViewModel>().Wait();
        }

        private void RegisterTypes()
        {
            var container = new UnityContainer();
            var pageFactoryService = new PageFactoryService(container);

            //Structural Registration
            container.RegisterInstance<App>(this);
            container.RegisterInstance<IPageFactoryService>(pageFactoryService);
            container.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());

            var fs = DependencyService.Get<IFileService>(); fs.SandboxTag = "FootballLeaguesXF"; container.RegisterInstance<IFileService>(fs);

            //Views and ViewModels
            InitViewAndViewModels.RegisterViewsAndViewModels(container, pageFactoryService);

            //Global varaibles.
            navigationService = container.Resolve<INavigationService>();
            this.container = container;
            this.pageFactoryService = pageFactoryService;
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
