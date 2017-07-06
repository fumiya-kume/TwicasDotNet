using Prism.Mvvm;
using Reactive.Bindings;
using System.Reactive.Linq;
using System;
using TwicasDotNet;
using System.Windows;
using System.Windows.Controls;

namespace TwicasDotNet.Sample.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly AuthClient authClient = new AuthClient();

        public ReactiveProperty<string> ClientID { get; set; } = new ReactiveProperty<string>();
        public ReactiveProperty<string> AuthURL { get; set; } = new ReactiveProperty<string>();
        public ReactiveCommand AuthCommand { get; set; } = new ReactiveCommand();
        public ReactiveCommand<string> AuthNavigateCommand { get; set; } = new ReactiveCommand<string>();

        public MainWindowViewModel()
        {
            AuthCommand
                .Where(_ => !string.IsNullOrWhiteSpace(ClientID.Value))
                .Subscribe(_ =>
                 {
                     AuthURL.Value = authClient.GetAuthURL(ClientID.Value);
                 });

            AuthURL
                .Where(_ => !string.IsNullOrWhiteSpace(AuthURL.Value))
                .Subscribe(_ =>
            {
                var AccessKey = authClient.GetAccessTokenFromCallbackURL(AuthURL.Value);
                if (string.IsNullOrWhiteSpace(AccessKey)) MessageBox.Show("Access Key Denied");
                else MessageBox.Show($"AccessKey is {AccessKey}");
                
            });
        }
    }
}
