using Microsoft.Toolkit.Uwp.UI.Animations;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using System.Collections.Specialized;
using Windows.System;
using System.IO;
using System.Runtime.Serialization;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Net.Security;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.Foundation;
using System.Linq;
using Windows.UI.Xaml.Media.Imaging;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Windows.Storage.Pickers;
using Windows.Storage;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ChatAppUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Content lastChat = new Content(id: "", name: "", content: "", image: "", hide: "Collapsed", height: "0", timerow: "16", radius: "0, 7, 7, 7", imagevisibility: "Collapsed", horizonalign: "Left");

        public ObservableCollection<Contact> listFriend = new ObservableCollection<Contact>
        {
        };

        public ObservableCollection<Contact> listGroup = new ObservableCollection<Contact>
        {
        };

        public ObservableCollection<Contact> listWorld = new ObservableCollection<Contact>
        {
        };

        public ObservableCollection<Contact> listSearch = new ObservableCollection<Contact>
        {
        };

        public static ObservableCollection<Content> listContent = new ObservableCollection<Content>
        {
        };

        public static ObservableCollection<Content> listContentGroup = new ObservableCollection<Content>
        {
        };

        Contact friending;
        Queue<string> qSend = new Queue<string>();
        Queue<string> q2 = new Queue<string>();
        Queue<string> qHandle = new Queue<string>();
        bool isTextNull = true;
        bool isContinue = true;
        bool isSended = false;
        int isFriend = 0;
        string friendId = "";
        string groupId = "";
        string removeId = "";
        bool accepted = false;
        Socket socketSt;
        Socket socketServerFile;
        short typeSearch;
        private string HOST = "192.168.23.117";
        private string HOSTFILE = "192.168.23.117";

        private const int BUFFER_SIZE = 8192;
        private const int PORT_NUMBER = 9123;
        private int PORTFILE_NUMBER;

        string cutIndex = "-cutindex-";
        StorageFile fileSend;

        Task Send;
        CancellationToken ctSend;
        CancellationTokenSource tsSend = new CancellationTokenSource();
        Task Listen;
        CancellationToken ctListen;
        CancellationTokenSource tsListen = new CancellationTokenSource();
        Task Handle;
        CancellationToken ctHandle;
        CancellationTokenSource tsHandle = new CancellationTokenSource();

        public MainPage()
        {
            this.InitializeComponent();
            applyAcrylicAccent(MainGrid);
            ApplicationViewTitleBar formattableTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            formattableTitleBar.ButtonForegroundColor = Color.FromArgb(255, 30, 30, 30);
            formattableTitleBar.ButtonBackgroundColor = Colors.Transparent;
            formattableTitleBar.ButtonHoverBackgroundColor = Color.FromArgb(20, 0, 0, 0);
            formattableTitleBar.ButtonPressedBackgroundColor = Color.FromArgb(50, 0, 0, 0);
            formattableTitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            ApplicationView.PreferredLaunchViewSize = new Size(1000, 600);

            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            grid.Background = new SolidColorBrush(Color.FromArgb(120, 255, 255, 255));
            Window.Current.Activated += Current_Activated;
            SystemNavigationManager.GetForCurrentView().BackRequested += (s, a) =>
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                    a.Handled = true;
                }
            };
            imgAvatar.Glyph = Login.image;
            txtHi.Text = "Hi   " + Login.name;
            listViewContact.ItemsSource = listFriend;
            HamburgerMenu.SelectedIndex = 0;
            listViewContent.ItemsSource = listContent;
            ((INotifyCollectionChanged)listContent).CollectionChanged += ListView_CollectionChangedAsync;
            listContent.Add(lastChat);
            listContentGroup.Add(lastChat);
        }

        private void SendMessQueue()
        {
            ctSend = tsSend.Token;
            Send = Task.Factory.StartNew(async () =>
            {
                try
                {
                    string[] ctn;
                    while (true)
                    {
                        if (isContinue)
                        {
                            if (qSend.Count > 0)
                            {
                                socketSt.Send(Encoding.UTF8.GetBytes(qSend.Dequeue()));
                                ctn = DetachContent(q2.Dequeue());
                                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                {
                                    AddContentChat(ctn[0], ctn[1], ctn[2], ctn[3]);
                                });
                                isContinue = false;
                                //while (true)
                                //{
                                //    if (isSended)
                                //    {
                                //        isSended = false;
                                //        break;
                                //    }
                                //    socketSt.Send(Encoding.UTF8.GetBytes("issending"));
                                //}
                            }
                        }
                    }
                }
                catch (Exception ex)
                { }
            }, ctSend);
        }

        private void HandleMessQueue()
        {
            ctHandle = tsHandle.Token;
            Handle = Task.Factory.StartNew(async () =>
            {
                try
                {
                    string[] ctn;
                    while (true)
                    {
                        if (qHandle.Count > 0)
                        {
                            string request = qHandle.Dequeue();
                            ctn = DetachContent(request);

                            switch (ctn[0])
                            {
                                case "continue":
                                    while (!isContinue) { isContinue = true; }
                                    break;
                                case "issended":
                                    isSended = true;
                                    break;
                                case "sendfile":
                                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                                    {
                                        var dialog = new MessageDialog(ctn[2] + " want send a file '" + ctn[4] + "'.", "Send file");
                                        dialog.Commands.Add(new UICommand { Label = "OK", Id = 0 });
                                        dialog.Commands.Add(new UICommand { Label = "CANCEL", Id = 1 });
                                        var res = await dialog.ShowAsync();

                                        if ((int)res.Id == 0)
                                        {
                                            string content = "acceptsendfile" + cutIndex + ctn[1] + cutIndex + ctn[3] + cutIndex + ctn[4] + cutIndex + ctn[5];
                                            socketSt.Send(Encoding.UTF8.GetBytes(content));
                                            //dialog = new MessageDialog("Accept file.", "Send file");
                                            //await dialog.ShowAsync();
                                        }
                                    });
                                    break;
                                case "portfile":
                                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                    {
                                        try
                                        {
                                            if (ctn[2] == Login.id)
                                            {
                                                PORTFILE_NUMBER = int.Parse(ctn[1]);
                                                string filePath = ctn[4];
                                                //IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORTFILE_NUMBER);
                                                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(HOSTFILE), PORTFILE_NUMBER);
                                                socketServerFile = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                                                socketServerFile.Connect(iep);
                                                Task.Factory.StartNew(async () =>
                                                {
                                                    string path = filePath;
                                                    await Task.Delay(500);

                                                    byte[] data = new byte[BUFFER_SIZE];
                                                    int rec = socketServerFile.Receive(data);
                                                    string req = Encoding.UTF8.GetString(data, 0, rec);
                                                    if (req == "readysend")
                                                    {
                                                        await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                                        {
                                                            ReadFileAsync(path, socketServerFile);
                                                        });

                                                    }

                                                });
                                            }
                                            if (ctn[3] == Login.id)
                                            {
                                                PORTFILE_NUMBER = int.Parse(ctn[1]);
                                                string fileName = ctn[4];
                                                //IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORTFILE_NUMBER);
                                                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(HOSTFILE), PORTFILE_NUMBER);
                                                socketServerFile = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                                                socketServerFile.Connect(iep);
                                                Task.Factory.StartNew(async () =>
                                                {
                                                    string name = fileName;
                                                    await Task.Delay(500);

                                                    byte[] fileData = new byte[1024 * 10000];
                                                    int receivedBytesLen = socketServerFile.Receive(fileData);
                                                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                                    {
                                                        PickFolder(name, fileData, receivedBytesLen);
                                                    });
                                                });
                                            }
                                        }
                                        catch (Exception ex) { }
                                        //var dialog = new MessageDialog(ctn[1], "Port file");
                                        //await dialog.ShowAsync();
                                    });
                                    break;
                                case "chatmess":
                                    if (ctn[5] == "chatfriend")
                                    {
                                        await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                        {
                                            GetContentChat(ctn[1], ctn[2], ctn[3], ctn[4]);
                                            socketSt.Send(Encoding.UTF8.GetBytes("continue"));
                                        });
                                    }
                                    else if (ctn[5] == "chatgroup")
                                    {
                                        await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                        {
                                            GetContentChatGroup(ctn[1], ctn[2], ctn[3], ctn[4]);
                                            socketSt.Send(Encoding.UTF8.GetBytes("continue"));
                                        });
                                    }
                                    else
                                    {
                                        await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                        {
                                            GetContentChat(ctn[1], ctn[2], ctn[3], ctn[4]);
                                            socketSt.Send(Encoding.UTF8.GetBytes("continue"));
                                        });
                                    }
                                    break;
                                case "loadfriend":
                                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                    {
                                        LoadFriend(ctn[1], ctn[2], ctn[3], ctn[4], ctn[5]);
                                        socketSt.Send(Encoding.UTF8.GetBytes("continue"));
                                    });
                                    break;
                                case "loadworld":
                                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                    {
                                        LoadWorld(ctn[1], ctn[2], ctn[3], ctn[4], ctn[5]);
                                        socketSt.Send(Encoding.UTF8.GetBytes("continue"));
                                    });
                                    break;
                                case "changepassword":
                                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                                    {
                                        string dia = "";
                                        if (ctn[1] == "true")
                                        {
                                            dia = "Change password success!";
                                        }
                                        else
                                        {
                                            dia = "Change password fail. Check your old password!";
                                        }
                                        var dialog = new MessageDialog(dia, "Change password result");
                                        await dialog.ShowAsync();
                                        socketSt.Send(Encoding.UTF8.GetBytes("continue"));
                                    });
                                    break;
                                case "changenickname":
                                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                                    {
                                        string dia = "";
                                        if (ctn[1] == "true")
                                        {
                                            dia = "Change nick name success!";
                                            Login.name = ctn[2];
                                        }
                                        else
                                        {
                                            dia = "Change nick name fail!";
                                        }
                                        var dialog = new MessageDialog(dia, "Change nick name result");
                                        await dialog.ShowAsync();
                                        socketSt.Send(Encoding.UTF8.GetBytes("continue"));
                                    });
                                    break;
                                case "updateonline":
                                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                    {
                                        UpdateOnline(ctn[1]);
                                    });
                                    break;
                                case "updateoffline":
                                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                    {
                                        UpdateOffline(ctn[1]);
                                    });
                                    break;
                                case "checkfriend":
                                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                    {
                                        CheckFriend(int.Parse(ctn[1].Trim()), int.Parse(ctn[2].Trim()));
                                        socketSt.Send(Encoding.UTF8.GetBytes("continue"));
                                    });
                                    break;
                                case "makefriend":
                                    if (ctn[1] == "success")
                                    {
                                        if (isFriend == 0)
                                        {
                                            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                            {
                                                txtMakeFriend.Text = "Waiting for Accept!";
                                                socketSt.Send(Encoding.UTF8.GetBytes("continue"));
                                                isFriend = 1;
                                            });
                                        }
                                        else if (isFriend == 1)
                                        {
                                            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                            {
                                                txtMakeFriend.Text = "Make Friends to chat!";
                                                socketSt.Send(Encoding.UTF8.GetBytes("continue"));
                                                isFriend = 0;
                                            });
                                        }
                                        if (isFriend == 2)
                                        {
                                            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                            {
                                                txtMakeFriend.Text = "Let Chat in tab Friends!";
                                                accepted = true;
                                                makeFriendZoomOut.Begin();
                                                fonticonmakeFriendZoomOut.Begin();
                                                socketSt.Send(Encoding.UTF8.GetBytes("continue"));
                                                isFriend = 0;
                                                RemoveFromWorld(removeId);
                                            });
                                        }
                                    }
                                    break;
                            }

                        }
                    }
                }
                catch (Exception ex)
                { }
            }, ctHandle);
        }

        private void ListenChatMess()
        {
            ctListen = tsListen.Token;
            Listen = Task.Factory.StartNew(() =>
            {
                try
                {
                    socketSt = Login.socketSt;
                    string response = "loadPeople";
                    socketSt.Send(Encoding.UTF8.GetBytes(response));

                    byte[] data = new byte[BUFFER_SIZE];
                    string request = "";
                    int rec = 0;
                    while (true)
                    {
                        rec = socketSt.Receive(data);
                        request = Encoding.UTF8.GetString(data, 0, rec);
                        qHandle.Enqueue(request);

                    }
                }
                catch (Exception ex)
                { }
            }, ctListen);
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

            if (HamburgerMenu.IsPaneOpen == true)
                HamburgerMenu.IsPaneOpen = false;
        }

        private void Current_Activated(object sender, WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == CoreWindowActivationState.Deactivated)
            {
                grid.Background = new SolidColorBrush(Color.FromArgb(255, 220, 220, 220));
            }
            else
            {
                grid.Background = new SolidColorBrush(Color.FromArgb(120, 255, 255, 255));
            }
        }

        private void HamburgerMenu_OnItemClick(object sender, ItemClickEventArgs e)
        {
            ContentGrid.DataContext = e.ClickedItem;
            var menuItem = e.ClickedItem as HamburgerMenuItem;
            if (menuItem.Label == "Friends")
            {
                if (typeSearch == 1)
                {
                    try
                    {
                        Contact c = listViewContact.SelectedItem as Contact;
                        friending = new Contact(c.Id, c.Name, c.Content, c.Image, c.Online);
                        avatarChating.ImageSource = new BitmapImage(new Uri(@"ms-appx:///Assets/Photos/" + c.Image));
                        txtChating.Text = c.Name;

                        listContent = new ObservableCollection<Content>();
                        ((INotifyCollectionChanged)listContentGroup).CollectionChanged -= ListView_CollectionChangedAsync;
                        ((INotifyCollectionChanged)listContent).CollectionChanged += ListView_CollectionChangedAsync;
                        listViewContent.ItemsSource = listContent;
                        listContent.Add(lastChat);

                        friendId = c.Id;
                        string content = "loadchatfriend" + cutIndex + Login.id + cutIndex + friendId;
                        socketSt.Send(Encoding.UTF8.GetBytes(content));
                    }
                    catch { }

                }
                typeSearch = 0;
                listViewContact.ItemsSource = listFriend;
                txtSearch.Text = "";
            }
            if (menuItem.Label == "Groups")
            {
                typeSearch = 1;
                listViewContact.ItemsSource = listFriend;
                txtSearch.Text = "";
                avatarChating.ImageSource = new BitmapImage(new Uri(@"ms-appx:///Assets/Photos/" + "60838637_p4_master1200.jpg"));
                txtChating.Text = "Group";

                ((INotifyCollectionChanged)listContent).CollectionChanged -= ListView_CollectionChangedAsync;
                ((INotifyCollectionChanged)listContentGroup).CollectionChanged += ListView_CollectionChangedAsync;
                listViewContent.ItemsSource = listContentGroup;
                listContentGroup.Add(lastChat);
                listContentGroup.RemoveAt(listContentGroup.Count - 1);

                logoChatApp.Visibility = Visibility.Collapsed;
                txtChatApp.Visibility = Visibility.Collapsed;
                stkHi.Visibility = Visibility.Collapsed;
                stkMakeFriend.Visibility = Visibility.Collapsed;
                listViewContent.Visibility = Visibility.Visible;
                gridControl.Visibility = Visibility.Visible;


            }
            if (menuItem.Label == "World")
            {
                typeSearch = 2;
                var resultLinq = listWorld.OrderBy(x => x.Name);
                listWorld = new ObservableCollection<Contact>(resultLinq);
                listViewContact.ItemsSource = listWorld;
                txtSearch.Text = "";
            }
        }

        private void HamburgerMenu_OnOptionsItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = e.ClickedItem as HamburgerMenuItem;
            if (menuItem.Label == "Sign Out")
            {
                socketSt.Send(Encoding.UTF8.GetBytes("signout"));
                Login.socketSt = null;
                tsListen.Cancel();
                tsHandle.Cancel();
                tsSend.Cancel();
                Frame.Navigate(typeof(Login));
            }
            if (menuItem.Label == "Account")
            {
                popAccount.IsOpen = !popAccount.IsOpen;
                HamburgerMenu.SelectedIndex = typeSearch;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            gridEdgeBlur.Visibility = Visibility.Visible;
            gridEdgeBlur.Fade(value: 1f, duration: 100, delay: 0).StartAsync();
            Expand.Begin();
        }

        private async void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Collapse.Begin();
            await gridEdgeBlur.Fade(value: 0f, duration: 100, delay: 0).StartAsync();
            gridEdgeBlur.Visibility = Visibility.Collapsed;
        }

        private void gridEdge_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            colContent.Width = new GridLength(348 - e.NewSize.Width);
        }

        private void gridEdgeBlur_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (HamburgerMenu.IsPaneOpen == true)
                HamburgerMenu.IsPaneOpen = false;
        }

        private async void listViewContact_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Contact c;
                if (typeSearch == 0 || typeSearch == 1)
                {
                    c = listViewContact.SelectedItem as Contact;
                    friending = new Contact(c.Id, c.Name, c.Content, c.Image, c.Online);
                    avatarChating.ImageSource = new BitmapImage(new Uri(@"ms-appx:///Assets/Photos/" + c.Image));
                    txtChating.Text = c.Name;

                    listContent = new ObservableCollection<Content>();
                    ((INotifyCollectionChanged)listContentGroup).CollectionChanged -= ListView_CollectionChangedAsync;
                    ((INotifyCollectionChanged)listContent).CollectionChanged += ListView_CollectionChangedAsync;
                    listViewContent.ItemsSource = listContent;
                    listContent.Add(lastChat);

                    friendId = c.Id;
                    string content = "loadchatfriend" + cutIndex + Login.id + cutIndex + friendId;
                    socketSt.Send(Encoding.UTF8.GetBytes(content));

                    logoChatApp.Visibility = Visibility.Collapsed;
                    txtChatApp.Visibility = Visibility.Collapsed;
                    stkHi.Visibility = Visibility.Collapsed;
                    stkMakeFriend.Visibility = Visibility.Collapsed;
                    listViewContent.Visibility = Visibility.Visible;
                    gridControl.Visibility = Visibility.Visible;
                    if (typeSearch == 1)
                    {
                        typeSearch = 0;
                        HamburgerMenu.SelectedIndex = 0;
                    }
                }
                if (typeSearch == 2)
                {
                    c = listViewContact.SelectedItem as Contact;
                    friending = new Contact(c.Id, c.Name, c.Content, c.Image, c.Online);
                    avatarChating.ImageSource = new BitmapImage(new Uri(@"ms-appx:///Assets/Photos/" + c.Image));
                    txtChating.Text = c.Name;
                    string content = "checkfriend" + cutIndex + Login.id + cutIndex + c.Id;
                    socketSt.Send(Encoding.UTF8.GetBytes(content));

                    stkHi.Visibility = Visibility.Collapsed;
                    logoChatApp.Visibility = Visibility.Collapsed;
                    txtChatApp.Visibility = Visibility.Collapsed;
                    listViewContent.Visibility = Visibility.Collapsed;
                    stkMakeFriend.Visibility = Visibility.Visible;
                    gridControl.Visibility = Visibility.Collapsed;

                }

                await titleChating.Offset(offsetX: 0f,
                                offsetY: -10f,
                                duration: 0, delay: 0).StartAsync();
                await titleChating.Offset(offsetX: 0f,
                             offsetY: 0f,
                             duration: 400, delay: 0).StartAsync();

                accepted = false;
            }
            catch { }
        }

        private void btnSend_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (!isTextNull)
                btnSend.Background = new SolidColorBrush(Color.FromArgb(125, 214, 155, 220));
        }

        private void btnSend_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (!isTextNull)
                btnSend.Background = new SolidColorBrush(Color.FromArgb(125, 255, 175, 255));
        }

        private void txtContentSend_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textSend = txtContentSend.Text.Replace("\r", "");
            if (textSend != "")
            {
                btnSend.Background = new SolidColorBrush(Color.FromArgb(125, 255, 175, 255));
                isTextNull = false;
            }
            else
            {
                btnSend.Background = new SolidColorBrush(Color.FromArgb(25, 0, 0, 0));
                isTextNull = true;
            }

        }

        public static DependencyObject GetScrollViewer(DependencyObject o)
        {
            if (o is ScrollViewer)
            { return o; }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(o); i++)
            {
                var child = VisualTreeHelper.GetChild(o, i);

                var result = GetScrollViewer(child);
                if (result == null)
                {
                    continue;
                }
                else
                {
                    return result;
                }
            }
            return null;
        }

        private void OnScrollDown()
        {
            ScrollViewer scrollViewer = GetScrollViewer(listViewContent) as ScrollViewer;

            if (scrollViewer != null)
            {
                scrollViewer.ChangeView(0, scrollViewer.ScrollableHeight, 1);
            }
        }

        private async void ListView_CollectionChangedAsync(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                await Task.Delay(20);
                OnScrollDown();
            }
        }

        public void LoadFriend(string id, string name, string content, string image, string online)
        {
            if (online == "true")
                listFriend.Insert(0, new Contact(id, name, content, image, true));
            else
                listFriend.Insert(0, new Contact(id, name, content, image, false));
        }

        public void LoadWorld(string id, string name, string content, string image, string online)
        {
            if (!listFriend.Any(x => x.Id == id) && id != Login.id)
            {
                if (online == "true")
                    listWorld.Insert(0, new Contact(id, name, content, image, true));
                else
                    listWorld.Insert(0, new Contact(id, name, content, image, false));
            }
        }

        public void RemoveFromWorld(string id)
        {
            try
            {
                int i = 0;
                Contact cTemp;
                foreach (Contact c in listWorld)
                {
                    if (c.Id == id)
                    {
                        cTemp = new Contact(c.Id, c.Name, "", c.Image, c.Online);
                        listFriend.Add(cTemp);
                        break;
                    }
                    i++;
                }
                listWorld.RemoveAt(i);
            }
            catch (Exception ex) { }
        }


        public void CheckFriend(int i, int j)
        {
            if (i == 0 && j == 0)
            {
                txtMakeFriend.Text = "Make Friends to chat!";
                isFriend = 0;
            }
            if (i == 1 && j == 0)
            {
                txtMakeFriend.Text = "Waiting for Accept!!";
                isFriend = 1;
            }
            if (i == 0 && j == 1)
            {
                txtMakeFriend.Text = "Agree to make Friends!";
                isFriend = 2;
            }
        }

        public void AddContentChat(string id, string name, string content, string image)
        {
            if (typeSearch == 0)
            {
                if (listContent.Count > 1)
                {
                    if (listContent[listContent.Count - 2].Id == id)
                    {
                        if (listContent.Count > 2)
                        {
                            if (listContent[listContent.Count - 3].Id == id)
                            {
                                listContent[listContent.Count - 2].Radius = "7, 0, 0, 7";
                            }
                            else
                            {
                                listContent[listContent.Count - 2].Radius = "7, 6, 0, 7";
                            }
                        }
                        else
                        {
                            listContent[listContent.Count - 2].Radius = "7, 6, 0, 7";
                        }
                        listContent.Insert(listContent.Count - 1, new Content(id: id, name: name, content: content, image: image, hide: "Visible", height: "Auto", timerow: "0", radius: "7, 0, 7, 7", imagevisibility: "Collapsed", horizonalign: "Right"));
                    }
                    else
                    {
                        listContent.Insert(listContent.Count - 1, new Content(id: id, name: name, content: content, image: image, hide: "Visible", height: "Auto", timerow: "16", radius: "7, 0, 7, 7", imagevisibility: "Collapsed", horizonalign: "Right"));

                    }
                }
                else
                {
                    listContent.Insert(listContent.Count - 1, new Content(id: id, name: name, content: content, image: image, hide: "Visible", height: "Auto", timerow: "16", radius: "7, 0, 7, 7", imagevisibility: "Collapsed", horizonalign: "Right"));

                }
            }
            else if (typeSearch == 1)
            {
                if (listContentGroup.Count > 1)
                {
                    if (listContentGroup[listContentGroup.Count - 2].Id == id)
                    {
                        if (listContentGroup.Count > 2)
                        {
                            if (listContentGroup[listContentGroup.Count - 3].Id == id)
                            {
                                listContentGroup[listContentGroup.Count - 2].Radius = "7, 0, 0, 7";
                            }
                            else
                            {
                                listContentGroup[listContentGroup.Count - 2].Radius = "7, 6, 0, 7";
                            }
                        }
                        else
                        {
                            listContentGroup[listContentGroup.Count - 2].Radius = "7, 6, 0, 7";
                        }
                        listContentGroup.Insert(listContentGroup.Count - 1, new Content(id: id, name: name, content: content, image: image, hide: "Visible", height: "Auto", timerow: "0", radius: "7, 0, 7, 7", imagevisibility: "Collapsed", horizonalign: "Right"));
                    }
                    else
                    {
                        listContentGroup.Insert(listContentGroup.Count - 1, new Content(id: id, name: name, content: content, image: image, hide: "Visible", height: "Auto", timerow: "16", radius: "7, 0, 7, 7", imagevisibility: "Collapsed", horizonalign: "Right"));

                    }
                }
                else
                {
                    listContentGroup.Insert(listContentGroup.Count - 1, new Content(id: id, name: name, content: content, image: image, hide: "Visible", height: "Auto", timerow: "16", radius: "7, 0, 7, 7", imagevisibility: "Collapsed", horizonalign: "Right"));

                }
            }
        }

        public void GetContentChat(string id, string name, string content, string image)
        {
            try
            {
                Contact c = friending;
                if (id == c.Id)
                {
                    if (listContent.Count > 1)
                    {
                        if (listContent[listContent.Count - 2].Id == id)
                        {
                            if (listContent.Count > 2)
                            {
                                if (listContent[listContent.Count - 3].Id == id)
                                {
                                    listContent[listContent.Count - 2].Radius = "0, 7, 7, 0";
                                }
                                else
                                {
                                    listContent[listContent.Count - 2].Radius = "7, 7, 7, 0";
                                }
                            }
                            else
                            {
                                listContent[listContent.Count - 2].Radius = "7, 7, 7, 0";
                            }
                            listContent.Insert(listContent.Count - 1, new Content(id: id, name: name, content: content, image: image, hide: "Visible", height: "Auto", timerow: "0", radius: "0, 7, 7, 7", imagevisibility: "Collapsed", horizonalign: "Left"));
                        }
                        else
                        {
                            listContent.Insert(listContent.Count - 1, new Content(id: id, name: name, content: content, image: image, hide: "Visible", height: "Auto", timerow: "16", radius: "0, 7, 7, 7", imagevisibility: "Visible", horizonalign: "Left"));

                        }
                    }
                    else
                    {
                        listContent.Insert(listContent.Count - 1, new Content(id: id, name: name, content: content, image: image, hide: "Visible", height: "Auto", timerow: "16", radius: "0, 7, 7, 7", imagevisibility: "Visible", horizonalign: "Left"));

                    }
                }
                if (id == Login.id)
                {
                    AddContentChat(id, name, content, image);
                }
            }
            catch { }
        }

        public void GetContentChatGroup(string id, string name, string content, string image)
        {
            try
            {
                if (id != Login.id)
                {
                    if (listContentGroup.Count > 1)
                    {
                        if (listContentGroup[listContentGroup.Count - 2].Id == id)
                        {
                            if (listContentGroup.Count > 2)
                            {
                                if (listContentGroup[listContentGroup.Count - 3].Id == id)
                                {
                                    listContentGroup[listContentGroup.Count - 2].Radius = "0, 7, 7, 0";
                                }
                                else
                                {
                                    listContentGroup[listContentGroup.Count - 2].Radius = "7, 7, 7, 0";
                                }
                            }
                            else
                            {
                                listContentGroup[listContentGroup.Count - 2].Radius = "7, 7, 7, 0";
                            }
                            listContentGroup.Insert(listContentGroup.Count - 1, new Content(id: id, name: name, content: content, image: image, hide: "Visible", height: "Auto", timerow: "0", radius: "0, 7, 7, 7", imagevisibility: "Collapsed", horizonalign: "Left"));
                        }
                        else
                        {
                            listContentGroup.Insert(listContentGroup.Count - 1, new Content(id: id, name: name, content: content, image: image, hide: "Visible", height: "Auto", timerow: "16", radius: "0, 7, 7, 7", imagevisibility: "Visible", horizonalign: "Left"));
                        }
                    }
                    else
                    {
                        listContentGroup.Insert(listContentGroup.Count - 1, new Content(id: id, name: name, content: content, image: image, hide: "Visible", height: "Auto", timerow: "16", radius: "0, 7, 7, 7", imagevisibility: "Visible", horizonalign: "Left"));

                    }
                }
                if (id == Login.id)
                {
                    AddContentChat(id, name, content, image);
                }
            }
            catch { }
        }
        private void btnSend_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SendContent();
        }

        private void txtContentSend_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter && Window.Current.CoreWindow.GetKeyState(VirtualKey.Shift).HasFlag(CoreVirtualKeyStates.Down))
            {
                int cursorPos = txtContentSend.SelectionStart;
                txtContentSend.Text = txtContentSend.Text.Insert(cursorPos, "\r");
                txtContentSend.SelectionStart = cursorPos + 1;
            }
        }

        private void txtContentSend_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter && !Window.Current.CoreWindow.GetKeyState(VirtualKey.Shift).HasFlag(CoreVirtualKeyStates.Down))
            {
                SendContent();
            }
        }

        public void SendContent()
        {
            if (!isTextNull)
            {
                string textSend = txtContentSend.Text;
                while (textSend.Substring(textSend.Length - 1, 1) == "\r")
                {
                    textSend = textSend.Substring(0, textSend.Length - 1);
                }

                string logtime = DateTime.Now.ToString("dd/MM/yy H:mm");
                string content;
                if (typeSearch == 0)
                {
                    Contact c = friending;
                    friendId = c.Id;
                    content = "chatmess" + cutIndex + Login.id + cutIndex + Login.name + cutIndex + textSend + cutIndex + Login.image + cutIndex + "chatfriend" + cutIndex + friendId + cutIndex + logtime;
                }
                else
                    content = "chatmess" + cutIndex + Login.id + cutIndex + Login.name + cutIndex + textSend + cutIndex + Login.image + cutIndex + "chatgroup" + cutIndex + " " + cutIndex + logtime;

                qSend.Enqueue(content);
                q2.Enqueue(Login.id + cutIndex + Login.name + cutIndex + textSend + cutIndex + Login.image);
                txtContentSend.Text = "";
            }
        }

        private async void btnFile_TappedAsync(object sender, TappedRoutedEventArgs e)
        {
            if (typeSearch == 0)
            {
                FileOpenPicker picker = new FileOpenPicker();
                picker.FileTypeFilter.Add("*");
                StorageFile file = await picker.PickSingleFileAsync();
                fileSend = file;
                if (file != null)
                {
                    // Application now has read/write access to the picked file
                    //var dialog = new MessageDialog(file.Name + " - " + file.Path);
                    //await dialog.ShowAsync();
                    string content = "sendfile" + cutIndex + Login.id + cutIndex + Login.name + cutIndex + friendId + cutIndex + file.Name + cutIndex + file.Path;
                    socketSt.Send(Encoding.UTF8.GetBytes(content));

                }
            }
        }

        public string[] DetachContent(string content)
        {
            try
            {
                return content.Split(new string[] { cutIndex }, StringSplitOptions.None);
            }
            catch
            {
                return null;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListenChatMess();
            HandleMessQueue();
            SendMessQueue();

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (typeSearch == 0)
                SearchFriends();
            if (typeSearch == 1)
                SearchGroups();
            if (typeSearch == 2)
                SearchWorld();
        }

        public void SearchFriends()
        {
            if (txtSearch.Text != "")
            {
                var linqResults = listFriend.Where(x => x.Name.ToLower().Contains(txtSearch.Text.ToLower()));
                listSearch = new ObservableCollection<Contact>(linqResults);
                listViewContact.ItemsSource = listSearch;
            }
            else
            {
                listViewContact.ItemsSource = listFriend;
            }
        }

        public void SearchGroups()
        {
            if (txtSearch.Text != "")
            {
                var linqResults = listGroup.Where(x => x.Name.ToLower().Contains(txtSearch.Text.ToLower()));
                listSearch = new ObservableCollection<Contact>(linqResults);
                listViewContact.ItemsSource = listSearch;
            }
            else
            {
                listViewContact.ItemsSource = listGroup;
            }

        }

        public void SearchWorld()
        {
            if (txtSearch.Text != "")
            {
                var linqResults = listWorld.Where(x => x.Name.ToLower().Contains(txtSearch.Text.ToLower()));
                listSearch = new ObservableCollection<Contact>(linqResults);
                listViewContact.ItemsSource = listSearch;
            }
            else
            {
                listViewContact.ItemsSource = listGroup;
            }
        }

        private void stkMakeFriend_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (!accepted)
            {
                makeFriendZoomIn.Begin();
                fonticonmakeFriendZoomIn.Begin();
            }
        }

        private void stkMakeFriend_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (!accepted)
            {
                makeFriendZoomOut.Begin();
                fonticonmakeFriendZoomOut.Begin();
            }
        }

        private void stkMakeFriend_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string content;
            Contact c;
            if (!accepted)
            {
                if (isFriend == 0)
                {
                    c = friending;
                    content = "addfriend" + cutIndex + Login.id + cutIndex + c.Id;
                    socketSt.Send(Encoding.UTF8.GetBytes(content));
                }
                if (isFriend == 1)
                {
                    c = friending;
                    content = "cancelfriend" + cutIndex + Login.id + cutIndex + c.Id;
                    socketSt.Send(Encoding.UTF8.GetBytes(content));
                }
                if (isFriend == 2)
                {
                    c = friending;
                    removeId = c.Id;
                    content = "acceptfriend" + cutIndex + removeId + cutIndex + Login.id;
                    socketSt.Send(Encoding.UTF8.GetBytes(content));
                }
            }
        }


        public async void PickFolder(string name, byte[] fileData, int lenght)
        {
            var folderPicker = new FolderPicker();
            folderPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add("*");

            StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                byte[] saveData = new byte[lenght];
                Array.Copy(fileData, saveData, lenght);
                StorageFile sampleFile = await folder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteBytesAsync(sampleFile, saveData);
            }
        }

        public async void ReadFileAsync(string path, Socket socket)
        {
            StorageFile file = fileSend;
            byte[] fileData;
            using (Stream stream = await file.OpenStreamForReadAsync())
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    fileData = memoryStream.ToArray();
                }
            }
            socket.Send(fileData);
        }

        private void btnChangeName_Click(object sender, RoutedEventArgs e)
        {
            txtNick.Text = Login.name;
            popAccount.IsOpen = false;
            gridChangeName.Visibility = Visibility.Visible;
            gridHide.Visibility = Visibility.Visible;
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            popAccount.IsOpen = false;
            pbOld.Password = "";
            pbNew.Password = "";
            gridChangePass.Visibility = Visibility.Visible;
            gridHide.Visibility = Visibility.Visible;

        }

        private void gridHide_Tapped(object sender, TappedRoutedEventArgs e)
        {
            gridChangeName.Visibility = Visibility.Collapsed;
            gridChangePass.Visibility = Visibility.Collapsed;
            gridHide.Visibility = Visibility.Collapsed;
        }

        private async void btnChangePass_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (pbOld.Password.Length < 6 || pbNew.Password.Length < 6)
            {
                var dialog = new MessageDialog("Password must be at least 6 characters!", "Error");
                await dialog.ShowAsync();
            }
            else
            {
                string content = "changepassword" + cutIndex + Login.id + cutIndex + pbOld.Password + cutIndex + pbNew.Password;
                socketSt.Send(Encoding.UTF8.GetBytes(content));
            }
        }

        private async void btnChangeNick_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (txtNick.Text.Length < 1)
            {
                var dialog = new MessageDialog("Nick name must be at least 1 characters!", "Error");
                await dialog.ShowAsync();
            }
            else
            {
                string content = "changenickname" + cutIndex + Login.id + cutIndex + txtNick.Text;
                socketSt.Send(Encoding.UTF8.GetBytes(content));
            }
        }

        public void UpdateOnline(string id)
        {
            foreach (Contact c in listFriend)
            {
                if (c.Id == id)
                {
                    c.Online = true;
                    listFriend.Remove(c);
                    listFriend.Insert(0, c);
                    return;
                }
            }
            foreach (Contact c in listWorld)
            {
                if (c.Id == id)
                {
                    c.Online = true;
                    listWorld.Remove(c);
                    listWorld.Insert(0, c);
                    return;
                }

            }
        }

        public void UpdateOffline(string id)
        {
            foreach (Contact c in listFriend)
            {
                if (c.Id == id)
                {
                    c.Online = false;
                    listFriend.Remove(c);
                    listFriend.Add(c);
                    return;
                }
            }
            foreach (Contact c in listWorld)
            {
                if (c.Id == id)
                {
                    c.Online = false;
                    listWorld.Remove(c);
                    listWorld.Add(c);
                    return;
                }

            }
        }
    }
}
