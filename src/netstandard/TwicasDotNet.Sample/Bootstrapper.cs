using Microsoft.Practices.Unity;
using Prism.Unity;
using TwicasDotNet.Sample.Views;
using System.Windows;

namespace TwicasDotNet.Sample
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
