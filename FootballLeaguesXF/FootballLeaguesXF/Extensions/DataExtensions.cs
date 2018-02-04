using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FootballLeaguesXF.Extensions
{
    public static class DataExtensions
    {
        //String
        public static string ToValidString(this string me)
        {
            return string.IsNullOrWhiteSpace(me) ? "<Empty>" : me;
        }
        public static string IsNullOrString(this string me)
        {
            return string.IsNullOrWhiteSpace(me) ? string.Empty : me;
        }
        /// <summary>
        /// Return name of valid image or URL to it.
        /// </summary>
        /// <param name="me">String holding a possible valid path to image.</param>
        /// <returns>path to image</returns>
        public static string ToValidImagePath(this string me)
        {
            return Uri.IsWellFormedUriString(me, UriKind.Absolute) ? me :
                 "person_placeholder.jpg";
        }

        //Task
        public static Task ToTaskRun(this Task me)
        {
            return Task.Run(async () => { await me; });
        }
        public static Task<T> ToTaskRun<T>(this Task<T> me)
        {
            return Task.Run<T>(async () => { return await me; });
        }

        //Guid
        public static Guid ToGuid(this string me)
        {
            return Guid.TryParse(me, out Guid output) ? output : Guid.Empty;
        }

        //Object
        public static bool IsNull(this object me)
        {
            return me == null;
        }

        //List
        public static int IsNullOrCount(this IEnumerable<object> me)
        {
            return me == null ? 0 : me.Count();
        }

        //DisplayAlert
        public static async Task DisplayAlert(this string message, string title = null)
        {
            //await App.Current.MainPage.DisplayAlert(title, message, "OK");
            Device.BeginInvokeOnMainThread(async () => await App.Current.MainPage.DisplayAlert(title, message, "OK"));
        }


        /// <summary>
        /// Function which displays and alert.
        /// </summary>
        /// <param name="message">The message from the alert.</param>
        /// <param name="title">Title from the alert.</param>
        /// <returns>Returns false if the users clicks on OK button.</returns>
        public static async Task<bool> DisplayAlertWithRet(this string message, string title = null)
        {
            return await App.Current.MainPage.DisplayAlert(title, message, null, "OK");
        }
    }
}
