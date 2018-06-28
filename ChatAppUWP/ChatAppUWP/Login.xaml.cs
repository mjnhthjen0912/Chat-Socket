using Microsoft.Toolkit.Uwp.UI.Animations.Behaviors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ChatAppUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {

        public static ContentControl contentPlaceSt;
        public static Fade signUpFadeSt;
        public static Fade signInFadeSt;
        public static Signup signUpSt;
        public static Sigin signInSt;
        public static StreamSocket socket;
        public static Socket socketSt;
        public static string id;
        public static string name;
        public static string image;
        //Stream streamOut;
        //StreamWriter writer;
        private string HOST = "192.168.23.117";
        private const int BUFFER_SIZE = 8192;
        private const int PORT_NUMBER = 9123;
        public static string cutIndex = "-cutindex-";

        public Login()
        {
            this.InitializeComponent();
            applyAcrylicAccent(MainGrid);
            ApplicationViewTitleBar formattableTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            formattableTitleBar.ButtonForegroundColor = Color.FromArgb(255, 30, 30, 30);
            formattableTitleBar.ButtonBackgroundColor = Colors.Transparent;
            formattableTitleBar.ButtonHoverBackgroundColor = Color.FromArgb(20, 0, 0, 0);
            formattableTitleBar.ButtonPressedBackgroundColor = Color.FromArgb(50, 0, 0, 0);
            formattableTitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
        }


        private void applyAcrylicAccent(Panel panel)
        {
            _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            _hostSprite = _compositor.CreateSpriteVisual();
            _hostSprite.Size = new Vector2((float)panel.ActualWidth, (float)panel.ActualHeight);

            ElementCompositionPreview.SetElementChildVisual(panel, _hostSprite);
            _hostSprite.Brush = _compositor.CreateHostBackdropBrush();
        }

        Compositor _compositor;
        SpriteVisual _hostSprite;

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_hostSprite != null)
                _hostSprite.Size = e.NewSize.ToVector2();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            contentPlaceSt = contentPlace;
            signInSt = signIn;
            signIn.loginFrm = this;
            signUpSt = signUp;
            signIn.Visibility = Visibility.Visible;
            signInFade.Value = 1;
            signInFadeSt = signInFade;
            signUpFadeSt = signUpFade;
            if (socketSt == null)
                ConnectServer();
        }

        private async void ConnectServer()
        {
            //socket = new StreamSocket();
            //HostName serverHost = new HostName("localhost");
            //await socket.ConnectAsync(serverHost, PORT_NUMBER.ToString());
            //await socket.UpgradeToSslAsync(SocketProtectionLevel.Tls12, serverHost);
            try
            {
                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(HOST), PORT_NUMBER);
                //IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORT_NUMBER);
                //IPAddress localAddr = IPAddress.Parse("192.168.1.38");
                socketSt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socketSt.Connect(iep);
            }
            catch(Exception ex) {

            }
        }

        public static async Task<string> SignUp(string acc, string pass)
        {
            try
            {
                //Stream streamOut = socket.OutputStream.AsStreamForWrite();
                //StreamWriter writer = new StreamWriter(streamOut);
                //string request = "signup" + cutIndex + acc.Trim() + cutIndex + pass.Trim();
                //await writer.WriteLineAsync(request);
                //await writer.FlushAsync();

                //Stream streamIn = socket.InputStream.AsStreamForRead();
                //StreamReader reader = new StreamReader(streamIn);
                //string response = await reader.ReadLineAsync();

                string response = "signup" + cutIndex + acc.Trim() + cutIndex + pass.Trim();
                socketSt.Send(Encoding.UTF8.GetBytes(response));

                byte[] data = new byte[BUFFER_SIZE];
                int rec = socketSt.Receive(data);
                string request = Encoding.UTF8.GetString(data, 0, rec);

                return request;

            }
            catch { }
            return "Tạo tài khoản thất bại!";
        }


        public static async Task<string> SignIn(string acc, string pass)
        {
            try
            {
                //Stream streamOut = socket.OutputStream.AsStreamForWrite();
                //StreamWriter writer = new StreamWriter(streamOut);
                //string request = "signin" + cutIndex + acc.Trim() + cutIndex + pass.Trim();
                //await writer.WriteLineAsync(request);
                //await writer.FlushAsync();

                //Stream streamIn = socket.InputStream.AsStreamForRead();
                //StreamReader reader = new StreamReader(streamIn);
                //string response = await reader.ReadLineAsync();

                //streamOut = socket.OutputStream.AsStreamForWrite();
                //writer = new StreamWriter(streamOut);
                //await writer.WriteLineAsync("continue");
                //await writer.FlushAsync();

                string response = "signin" + cutIndex + acc.Trim() + cutIndex + pass.Trim();
                socketSt.Send(Encoding.UTF8.GetBytes(response));

                byte[] data = new byte[BUFFER_SIZE];

                socketSt.Send(Encoding.UTF8.GetBytes("continue"));
                int rec = socketSt.Receive(data);
                string request = Encoding.UTF8.GetString(data, 0, rec);

                string[] ctn = DetachContent(request);
                if (ctn[0] == "success")
                {
                    id = acc;
                    name = ctn[1].Trim();
                    image = ctn[2].Trim();
                }
                return request;

            }
            catch (Exception ex) { }
            return "Đăng nhập thất bại!";
        }

        public static string[] DetachContent(string content)
        {
            try
            {
                return content.Split(new string[] { cutIndex }, StringSplitOptions.None);
            }
            catch { }
            return null;
        }
    }
}
