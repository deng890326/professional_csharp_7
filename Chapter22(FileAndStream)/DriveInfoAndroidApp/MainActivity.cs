using DriveInfoLib;

namespace DriveInfoAndroidApp
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            TextView textView = FindViewById<TextView>(Resource.Id.textView)!;
            //foreach (string driveInfo in DriveInfoSamples.GetDriveInfoTexts())
            //{
            //    textView.Text += $"{driveInfo}\n";
            //}
            foreach (string specialFolderInfo in DriveInfoSamples.GetSpecialFolderInfoTexts())
            {
                textView.Text += $"{specialFolderInfo}\n";
            }
        }
    }
}