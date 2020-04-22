using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IMS
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactEntryPage : ContentPage
	{
		public ContactEntryPage ()
		{
			InitializeComponent ();
            startDatePicker.MaximumDate = DateTime.Now.AddDays(14);
		}

        private void OnCallButton(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("tel:+353871904703"));
        }

        private void OnEmailButton(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("mailto:patpat@live.no"));
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());

        }

  
        public async void OnPostButton (object sender, EventArgs eventar)
        {
            var fname = fullName.Text;
            String pnumber = phoneNumber.Text;
            Regex phone = new Regex("[0-9]{10}");
            var dateSelector = startDatePicker.Date;

            HttpClient _client;
            _client = new HttpClient();

            var postUrl = "http://bachelorproject.patrickgilstad.tk/connectdb.php?";
            postUrl += "fname=" + fname;
            postUrl += "&pnumber=" + pnumber;
            postUrl += "&date=" + dateSelector.ToString("ddMMyyyy");

            if (phone.IsMatch(pnumber))
            {

                Debug.WriteLine(postUrl);

                var uri = new Uri(string.Format(postUrl, string.Empty));

                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Success", "Call request sent!", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Something went wrong, try again!", "OK");
                }

            }

            else
            {
                await DisplayAlert("Error", "Invalid phone number, please check your entry!", "OK");

            }

        }

    }
}