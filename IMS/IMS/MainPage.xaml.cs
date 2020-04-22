using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Media.Imaging;

namespace IMS
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            GetJsonData();
        }


        void GetJsonData()
        {
            WebClient client = new WebClient();
            string reply = client.DownloadString("http://bachelorproject.patrickgilstad.tk/productrequest.php");

            ProductList ObjContactList = new ProductList();
            ObjContactList = JsonConvert.DeserializeObject<ProductList>(reply);

            //Binding listview with json string     
            listviewProducts.ItemsSource = ObjContactList.product;

            ObjContactList.product.ForEach(delegate(Product temp)
            {
                Debug.WriteLine("OUTPUT OF OBJECT " + temp.name + ": " + temp.desc);
            });
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContactEntryPage());

        }
    }
}
