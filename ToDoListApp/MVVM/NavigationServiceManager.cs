using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Prism.Common;
using Prism.Navigation;
using Xamarin.Forms;

namespace ToDoListApp.XAML.Behaviors
{
    public class NavigationServiceManager : INavigationService, INavigationServiceManager
    {
        private Dictionary<INavigationService, int> _instances;
        private int _i = 0;
        private INavigationService _topMostNavigationService;

        public NavigationServiceManager()
        {
            _instances = new Dictionary<INavigationService, int>();
        }

        public void Attach(INavigationService ns)
        {

            var pageProps = ns.GetType().GetTypeInfo().DeclaredProperties.ToList();

            var pns = ns as IPageAware;

            var page = pns?.Page;

            var topPage = page?.Navigation.NavigationStack.LastOrDefault();


            var pagePrp = GetProperty(
                ns.GetType().
                    GetTypeInfo(),
                "Page");

            var pageProp = pageProps.FirstOrDefault(x => x.Name == "Page");

            this._topMostNavigationService = ns;
        }

        private static PropertyInfo GetProperty(TypeInfo typeInfo, string propertyName)
        {
            var propertyInfo = typeInfo.GetDeclaredProperty(propertyName);
            if (propertyInfo == null && typeInfo.BaseType != null)
            {
                propertyInfo = GetProperty(typeInfo.BaseType.GetTypeInfo(), propertyName);
            }
            return propertyInfo;
        }

        public Task<bool> GoBackAsync(NavigationParameters parameters = null, bool? useModalNavigation = null, bool animated = true)
        {
            return GetCurrentNavigationService().GoBackAsync(parameters, useModalNavigation, animated);
        }

        public Task NavigateAsync(Uri uri, NavigationParameters parameters = null, bool? useModalNavigation = null,
            bool animated = true)
        {
            return GetCurrentNavigationService().NavigateAsync(uri, parameters, useModalNavigation, animated);
        }

        public Task NavigateAsync(string name, NavigationParameters parameters = null, bool? useModalNavigation = null,
            bool animated = true)
        {
            return GetCurrentNavigationService().NavigateAsync(name, parameters, useModalNavigation, animated);
        }

        private INavigationService GetCurrentNavigationService()
        {
            var topPage = PageUtilities.GetCurrentPage(Application.Current.MainPage);

            var topPageNavigationService = (topPage?.BindingContext as BaseViewModel)?.NavigationServicePublic;

            return topPageNavigationService ?? _topMostNavigationService;
        }
    }
}