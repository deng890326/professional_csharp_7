using Android.Views;
using DISampleLib;
using Microsoft.Extensions.DependencyInjection;

namespace AndroidClient
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            App.CurrentActivity = this;
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            serviceScope = App.Current.ServiceProvider.CreateScope();
            viewModel = serviceScope.ServiceProvider.GetRequiredService<ShowMessageViewModel>();

            FindViewById<Button>(Resource.Id.button).Click += MainActivity_Click;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            serviceScope.Dispose();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.message:
                    viewModel.ShowMessageCommand.Execute(null);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu? menu)
        {
            base.OnCreateOptionsMenu(menu);
            MenuInflater.Inflate(Resource.Menu.main, menu);
            return true;
        }

        private void MainActivity_Click(object? sender, EventArgs e)
        {
            viewModel.ShowMessageCommand.Execute(null);
        }

        private IServiceScope serviceScope;
        private ShowMessageViewModel viewModel;
    }
}