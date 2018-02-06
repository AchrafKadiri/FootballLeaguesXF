using FootballLeaguesXF.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using Xamarin.Forms;

namespace FootballLeaguesXF.Services
{
    public class PageFactoryService : IPageFactoryService
    {
        private IUnityContainer container;

        readonly IDictionary<Type, Type> viewModel2ViewList = new Dictionary<Type, Type>();

        //ctor
        public PageFactoryService(IUnityContainer Container)
        {
            this.container = Container;
        }

        /// <summary>
        /// Register a View type and corresponding ViewModel for future navigation
        /// </summary>
        /// <typeparam name="TView">Page type</typeparam>
        /// <typeparam name="TViewModel">ViewModel type</typeparam>
        public void RegisterForNavigation<TView, TViewModel>()
        {
            viewModel2ViewList[typeof(TViewModel)] = typeof(TView);

            container.RegisterType<TViewModel>();
            container.RegisterType<TView>();
        }

        public Page Resolve<TViewModel>()
        {
            var viewmodel = container.Resolve<TViewModel>();

            var viewtype = viewModel2ViewList[typeof(TViewModel)];
            var view = container.Resolve(viewtype) as Xamarin.Forms.Page;

            if (view == null || viewmodel == null)
                throw new Exception("Trying to resolve an unregistered page. Please call RegisterForNavigation for this view and viewmodel.");

            view.BindingContext = viewmodel;

            return view;
        }
    }
}
