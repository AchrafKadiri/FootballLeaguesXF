using FootballLeaguesXF.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FootballLeaguesXF.Services
{
    public class NavigationService : INavigationService
    {
        //REFERENCE: http://blog.adamkemp.com/2014/09/navigation-in-xamarinforms_2.html

        private IPageFactoryService pageFactoryService;
        private App app;

        public NavigationService(IPageFactoryService pageFactoryService, App application)
        {
            this.pageFactoryService = pageFactoryService;
            this.app = application;

        }
        private Page current
        {
            get
            {
                var navigation = app.MainPage as IPageContainer<Page>;
                return navigation?.CurrentPage;
            }
        }

        private INavigation Navigation
        {
            //get { return _navigation.Value; }
            get
            {
                var navigation = app.MainPage as IPageContainer<Page>;
                return navigation.CurrentPage.Navigation;
            }
        }

        public async Task BackAsync()
        {
            await Navigation.PopAsync();
        }

        public async Task BackModalAsync()
        {
            await Navigation.PopModalAsync();
        }

        public async Task BackToRootAsync()
        {
            await Navigation.PopToRootAsync();
        }

        public async Task NavigateToAsync<TViewModel>(NavParams param = null)
        {
            var pageTo = await HonorNavigateAware<TViewModel>(param);

            await Navigation.PushAsync(pageTo);
        }

        public async Task NavigateModalToAsync<TViewModel>(NavParams param = null)
        {
            var pageTo = await HonorNavigateAware<TViewModel>(param);

            await Navigation.PushModalAsync(pageTo);
        }

        public async Task SetMainPage<TViewModel>(NavParams navParams = null)
        {
            try
            {
                var page = await HonorNavigateAware<TViewModel>(navParams);

                app.MainPage = new NavigationPage(page);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private Task<Page> HonorNavigateAware<TViewModelTo>(NavParams navParams = null)
        {
            var pageto = pageFactoryService.Resolve<TViewModelTo>();
            var pagefrom = current;

            var fromAwareVM = pagefrom?.BindingContext as INavigationAware;
            var toAwareVM = pageto.BindingContext as INavigationAware;

            navParams = navParams ?? new NavParams();

            if (fromAwareVM != null)
                fromAwareVM.NavigateFrom(navParams);

            if (toAwareVM != null)
                toAwareVM.NavigateTo(navParams);

            return Task.FromResult(pageto);
        }
    }
}
