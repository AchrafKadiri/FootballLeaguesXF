using FootballLeaguesXF.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeaguesXF.IServices
{
    public interface INavigationService
    {

        /// <summary>
        /// Set the Application root page to a TViewModel instance
        /// </summary>
        /// <typeparam name="TViewModel">Page type to be used.</typeparam>
        /// <returns></returns>
        Task SetMainPage<TViewModel>(NavParams param = null);
        /// <summary>
        /// Navigates back one page on the navigation stack
        /// </summary>
        /// <returns></returns>
        Task BackAsync();
        /// <summary>
        /// Navigates back to the root on the navigation stack.
        /// </summary>
        /// <returns></returns>
        Task BackToRootAsync();
        /// <summary>
        /// Navigates to TViewModel adding it to the navigation stack
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <returns></returns>
        Task NavigateToAsync<TViewModel>(NavParams param = null);

        /// <summary>
        /// Navigates modal to TViewModel
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <returns></returns>
        Task NavigateModalToAsync<TViewModel>(NavParams param = null);

        /// <summary>
        /// Navigates modal to TViewModel with param
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <returns></returns>
        Task BackModalAsync();
    }
}
