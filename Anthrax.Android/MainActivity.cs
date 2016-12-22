using Android.App;
using Android.Graphics;
using Android.Widget;
using Android.OS;
using Anthrax.Lib;
using Anthrax.Lib.Json;
using Anthrax.Android.Models;
using Android.Views;
using System.Threading.Tasks;
using System.Timers;

namespace Anthrax.Android
{
    [Activity(Label = "Anthrax 4 Xbox", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        // singleton control variables essentially, why find them every time?
        private Button btnReinit, btnXboxToggle;
        private TextView tvIPAddress, tvXboxStatus;

        // Initialize App Activity
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            // Set up designer controls
            btnReinit = FindViewById<Button>(Resource.Id.btnReinitialize);
            btnXboxToggle = FindViewById<Button>(Resource.Id.btnXboxToggle);
            tvIPAddress = FindViewById<TextView>(Resource.Id.tvIPAddress);
            tvXboxStatus = FindViewById<TextView>(Resource.Id.tvXboxStatus);

            // Set up events
            btnReinit.Click += Reinitialize;
            btnXboxToggle.Click += ToggleXboxPower;

            // Initialize
            await Initialize();
        }

        /// <summary>
        /// This sends the command to turn the power on and then reinitializes for up-to-date statistics.
        /// </summary>
        private async void ToggleXboxPower(object sender, System.EventArgs e)
        {
            btnXboxToggle.Enabled = false;
            var req = new JsonRequest(GetString(Resource.String.ApiUrl));
            var res = req.Post(null);
            await res.WaitForCompletion();
            Reinitialize(null, null);
        }

        /// <summary>
        /// Gets needed statistics and displays them.
        /// </summary>
        /// <returns></returns>
        private async Task Initialize()
        {
            var req = new JsonRequest(GetString(Resource.String.ApiUrl));
            var res = req.Get();
            await res.WaitForCompletion();
            if (res.Status == 0)
            {
                var model = new StatusModel(res.Data);
                tvIPAddress.Text = model.IPAddress;
                tvXboxStatus.Text = model.XboxOnline ? "Online" : "Offline";
                tvXboxStatus.SetTextColor(model.XboxOnline ? Color.Green : Color.Red);
                
                btnReinit.Enabled = true;
                if (!model.XboxOnline)
                    btnXboxToggle.Enabled = true;
            }
        }

        /// <summary>
        /// Calls initialization logic to get up-to-date statistics.
        /// </summary>
        private async void Reinitialize(object sender, System.EventArgs e)
        {
            btnReinit.Enabled = false;
            await Initialize();
        }
    }
}

