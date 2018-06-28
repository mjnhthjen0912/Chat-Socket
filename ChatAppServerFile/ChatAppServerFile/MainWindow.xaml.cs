using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatAppServerFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private const int BUFFER_SIZE = 8192;
        private const int PORT_NUMBER = 9124;

        List<Port> listPort = new List<Port>();
        static Socket serverFile;
        static Socket serverReceive;
        static Socket serverSend;
        static Socket serverService;
        public static List<Client> listClient = new List<Client>();
        List<Thread> listThread = new List<Thread>();
        public static List<Task> listTask = new List<Task>();
        public static string cutIndex = "-cutindex-";

        private void initServer()
        {
            //IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORT_NUMBER);
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, PORT_NUMBER);
            serverFile = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverFile.Bind(iep);
            serverFile.Listen(10);
            txt.Text += "\nĐang chờ kết nối từ server service...";
            txt.Text += "\nChấp nhận kết nối của server service.";
            serverService = serverFile.Accept();

            for(int i = 9125; i<9300; i++)
            {
                Port p = new Port();
                p.PortNumber = i;
                p.IsUsed = false;
                listPort.Add(p);
            }


            Thread threadService = new Thread(new ThreadStart(this.ListenServerService));
            threadService.IsBackground = true;
            threadService.Start();
        }

        static void ServerReceive(int portReceive)
        {
            try
            {
                //IPEndPoint iep1 = new IPEndPoint(IPAddress.Parse("127.0.0.1"), portReceive);
                IPEndPoint iep1 = new IPEndPoint(IPAddress.Any, portReceive);
                serverReceive = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverReceive.Bind(iep1);
                serverReceive.Listen(10);
                while (true)
                {

                }
            }
            catch (Exception ex) { }
        }

        static void ServerSend(int portSend)
        {
            try
            {
                //IPEndPoint iep2 = new IPEndPoint(IPAddress.Parse("127.0.0.1"), portSend);
                IPEndPoint iep2 = new IPEndPoint(IPAddress.Any, portSend);
                serverSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSend.Bind(iep2);
                serverSend.Listen(10);

                while (true)
                {


                }
            }
            catch (Exception ex) { }
        }

        public void ListenServerService()
        {
            try
            {
                byte[] data = new byte[BUFFER_SIZE];
                string[] ctn;
                int rec;
                while (true)
                {

                    rec = serverService.Receive(data);
                    string func = Encoding.UTF8.GetString(data, 0, rec);

                    if (!func.Equals(""))
                    {
                        ctn = MainWindow.DetachContent(func);
                        if (ctn[0] != null)
                        {
                            if (ctn[0] == "acceptsendfile")
                            {
                                int portReceive = 0;
                                int portSend = 0;
                                bool isContinue = false;
                                foreach(Port p in listPort)
                                {
                                    if(!p.IsUsed && !isContinue)
                                    {
                                        portReceive = p.PortNumber;
                                        p.IsUsed = true;
                                        isContinue = true;
                                    }
                                    else if (!p.IsUsed && isContinue)
                                    {
                                        portSend = p.PortNumber;
                                        p.IsUsed = true;
                                        break;
                                    }
                                }
                                serverService.Send(Encoding.UTF8.GetBytes("portfile" + cutIndex + portReceive + cutIndex + portSend));
                                
                                Thread threadReceive = new Thread((ThreadStart) =>
                                {
                                    try
                                    {
                                        int portRec = portReceive;
                                        int portSe = portSend;
                                        //IPEndPoint iep1 = new IPEndPoint(IPAddress.Parse("127.0.0.1"), portRec);
                                        IPEndPoint iep1 = new IPEndPoint(IPAddress.Any, portRec);
                                        serverReceive = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                                        serverReceive.Bind(iep1);
                                        serverReceive.Listen(10);

                                        Dispatcher.Invoke(() =>
                                        {
                                            txt.Text += "\nĐang chờ kết nối từ port " + portRec;
                                        });

                                        Socket clientSend = serverReceive.Accept();

                                        Dispatcher.Invoke(() =>
                                        {
                                            txt.Text += "\nChấp nhận kết nối từ " + clientSend.RemoteEndPoint.ToString();
                                        });

                                        Client cl = new Client();
                                        cl.Port = portRec;
                                        cl.Socket = clientSend;
                                        listClient.Add(cl);
                                        foreach (Client c in listClient)
                                        {
                                            if (c.Port == portSe)
                                            {
                                                clientSend.Send(Encoding.UTF8.GetBytes("readysend"));
                                                byte[] fileData = new byte[1024 * 10000];
                                                int receivedBytesLen = clientSend.Receive(fileData);
                                                byte[] saveData = new byte[receivedBytesLen];
                                                Array.Copy(fileData, saveData, receivedBytesLen);
                                                c.Socket.Send(saveData);
                                                Thread.Sleep(500);
                                                clientSend.Shutdown(SocketShutdown.Both);
                                                clientSend.Close();
                                                c.Socket.Shutdown(SocketShutdown.Both);
                                                c.Socket.Close();
                                                listClient.Remove(cl);
                                                listClient.Remove(c);
                                                //foreach (Port p in listPort)
                                                //{
                                                //    if (p.PortNumber == portRec)
                                                //    {
                                                //        p.IsUsed = false;
                                                //    }
                                                //    if (p.PortNumber == portSend)
                                                //    {
                                                //        p.IsUsed = false;
                                                //    }
                                                //}
                                            }
                                        }

                                    }
                                    catch (Exception ex) {

                                    }
                                });
                                threadReceive.IsBackground = true;
                                threadReceive.Start();

                                Thread threadSend = new Thread((ThreadStart) =>
                                {
                                    try
                                    {
                                        int portSe = portSend;
                                        int portRec = portReceive;
                                        //IPEndPoint iep2 = new IPEndPoint(IPAddress.Parse("127.0.0.1"), portSe);
                                        IPEndPoint iep2 = new IPEndPoint(IPAddress.Any, portSe);
                                        serverSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                                        serverSend.Bind(iep2);
                                        serverSend.Listen(10);

                                        Dispatcher.Invoke(() =>
                                        {
                                            txt.Text += "\nĐang chờ kết nối từ port " + portSe;
                                        });
                                        
                                        Socket clientReceive = serverSend.Accept();

                                        Dispatcher.Invoke(() =>
                                        {
                                            txt.Text += "\nChấp nhận kết nối từ " + clientReceive.RemoteEndPoint.ToString();
                                        });

                                        Client cl = new Client();
                                        cl.Port = portSe;
                                        cl.Socket = clientReceive;
                                        listClient.Add(cl);
                                        foreach (Client c in listClient)
                                        {
                                            if (c.Port == portRec)
                                            {
                                                c.Socket.Send(Encoding.UTF8.GetBytes("readysend"));
                                                byte[] fileData = new byte[1024 * 10000];
                                                int receivedBytesLen = c.Socket.Receive(fileData);
                                                byte[] saveData = new byte[receivedBytesLen];
                                                Array.Copy(fileData, saveData, receivedBytesLen);
                                                clientReceive.Send(saveData);
                                                Thread.Sleep(500);
                                                clientReceive.Shutdown(SocketShutdown.Both);
                                                clientReceive.Close();
                                                c.Socket.Shutdown(SocketShutdown.Both);
                                                c.Socket.Close();
                                                listClient.Remove(cl);
                                                listClient.Remove(c);
                                                //foreach (Port p in listPort)
                                                //{
                                                //    if (p.PortNumber == portRec)
                                                //    {
                                                //        p.IsUsed = false;
                                                //    }
                                                //    if (p.PortNumber == portSend)
                                                //    {
                                                //        p.IsUsed = false;
                                                //    }
                                                //}
                                            }
                                        }
                                    }
                                    catch (Exception ex) { }
                                });
                                threadSend.IsBackground = true;
                                threadSend.Start();
                            }

                        }

                    }

                }
            }
            catch (Exception ex) { }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            initServer();
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


    public class Client
    {
        Socket _socket;
        int _port;

        public int Port { get => _port; set => _port = value; }
        public Socket Socket { get => _socket; set => _socket = value; }
    }

    public class Port
    {
        int portNumber;
        bool isUsed;


        public int PortNumber { get => portNumber; set => portNumber = value; }
        public bool IsUsed { get => isUsed; set => isUsed = value; }
    }

    public class Service
    {
        Socket _socket;
        string _id;
        TextBlock _txt;
        Client _item;
        public Task _task;
        private const int BUFFER_SIZE = 8192;
        NetworkStream ns;

        public Service(Socket socket, string id, TextBlock txt, Client item)
        {
            _socket = socket;
            _id = id;
            _txt = txt;
            _item = item;
            _task = Task.Factory.StartNew(Run);

        }

        void EndTask(Task<bool> ts)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                bool rs = ts.Result;
                _txt.Text += "\nTask complete: " + _task.IsCompleted.ToString();
            });
        }

        public void Run()
        {
            byte[] data = new byte[BUFFER_SIZE];
            string[] ctn;
            bool ischat = false;
            int rec;
            try
            {
                while (true)
                {
                    if (ischat)
                    {
                        _socket.Send(Encoding.UTF8.GetBytes("continue"));
                    }

                    rec = _socket.Receive(data);
                    string func = Encoding.UTF8.GetString(data, 0, rec);

                    if (!func.Equals(""))
                    {
                        ctn = MainWindow.DetachContent(func);
                        if (ctn[0] != null)
                        {
                            if (ctn[0] == "signout")
                            {
                                _socket.Shutdown(SocketShutdown.Both);
                                _socket.Close();
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    MainWindow.listClient.Remove(_item);
                                    _txt.Text += "\n" + _id + " thoát.";
                                });
                                return;
                            }

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    _txt.Text += "\n" + _id + " thoát.";
                });
                MainWindow.listClient.Remove(_item);
                return;
            }
        }
    }
}
