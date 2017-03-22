using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using _2017redrock1.HttpRequest;
using System.Threading.Tasks;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace _2017redrock1
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void SearcBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            string url = "http://i.y.qq.com/s.music/fcgi-bin/search_for_qq_cp?g_tk=5381&uin=0&format=jsonp&inCharset=utf8&outCharset=utf-8&notice=0&platform=h5&needNewCode=1&w={0}&t=0&flag=1&ie=utf-8&sem=1&aggr=0&p=1&remoteplace=txt.mqq.all&_=1460982060643";
            url = url.Replace("{0}", SearcBox.Text);
            await GetList(url);
        }
        public async Task GetList(string url)
        {
            string x= await HttpRequest.HttpRequest.GetRequest(url);
            List<Song> list = new List<Song>();
            list = HttpRequest.HttpRequest.GetList(list, x);
            ListItem.ItemsSource = list;
        }
    }
}
