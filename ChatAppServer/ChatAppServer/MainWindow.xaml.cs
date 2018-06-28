using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
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
using System.Windows.Threading;

namespace ChatAppServer
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
        private const int PORT_NUMBER = 9123;
        private const int PORTFILE_NUMBER = 9124;
        private string HOSTFILE = "192.168.23.117";
        Socket server;
        public static Socket serverFile;
        public static DataTable world;
        public static List<Client> listClient = new List<Client>();
        public static ObservableCollection<LogFile> listLogFile = new ObservableCollection<LogFile>();
        public static ObservableCollection<User> listUser = new ObservableCollection<User>();
        List<Thread> listThread = new List<Thread>();
        public static List<Task> listTask = new List<Task>();
        public static string cutIndex = "-cutindex-";
        public static string logFilePath;
        bool isStart = false;

        private void initServer()
        {
            //IPEndPoint iepfile = new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORTFILE_NUMBER);
            IPEndPoint iepfile = new IPEndPoint(IPAddress.Parse(HOSTFILE), PORTFILE_NUMBER);
            serverFile = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverFile.Connect(iepfile);

            IPEndPoint iep = new IPEndPoint(IPAddress.Any, PORT_NUMBER);
            //IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORT_NUMBER);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //serverCertificate = new X509Certificate2("pca-cert.pem", "password");

            world = LoadWorld();

            foreach (DataRow row in world.Rows)
            {
                listUser.Add(new User(row[0].ToString()));
            }
            listviewUser.ItemsSource = listUser;

            DirectoryInfo di = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory + "\\LogFile");
            FileInfo[] files = di.GetFiles("*.txt");

            foreach (FileInfo file in files)
            {
                listLogFile.Add(new LogFile(file.Name, file.FullName));
                logFilePath = file.FullName;
            }
            listviewLogFile.ItemsSource = listLogFile;
            if (files.Length == 0)
                logFilePath = System.AppDomain.CurrentDomain.BaseDirectory + "\\LogFile\\LogFile_001.txt";

            WriteLog(DateTime.Now + " - " + "Server start...");
            server.Bind(iep);
            server.Listen(10);
            Thread thread = new Thread(new ThreadStart(this.Run));
            thread.IsBackground = true;
            thread.Start();


        }

        public void Run()
        {
            try
            {
                while (true)
                {
                    Dispatcher.Invoke(() =>
                    {
                        txt.Text += "\nĐang chờ kết nối...";
                    });

                    Socket client = server.Accept();

                    Dispatcher.Invoke(() =>
                    {
                        txt.Text += "\nChấp nhận kết nối từ: " + client.RemoteEndPoint.ToString();
                        WriteLog(DateTime.Now.ToString() + " - " + "Accept connect from " + client.RemoteEndPoint.ToString());
                    });
                    listClient.Add(new Client(client, ""));
                    Service service = new Service(client, "", txt, listClient[listClient.Count - 1], listviewUser);
                }
            }
            catch (Exception ex) { }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!isStart)
            {
                initServer();
            }

        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            foreach(Client c in listClient)
            {
                c.Socket.Shutdown(SocketShutdown.Both);
                c.Socket.Close();

            }
            listClient.Clear();
            listUser.Clear();
            listLogFile.Clear();
            txt.Text += "\nĐóng kết nối.";
            server.Close();
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

        public static void WriteLog(string log)
        {
            try
            {
                using (StreamWriter file = new StreamWriter(logFilePath, true))
                {
                    file.WriteLine(log);
                    file.Close();
                }

            }
            catch { }
        }

        public static bool CheckExists(string acc)
        {
            string query = @"Select count(*) From Account acc Where ID=N'" + acc + "'";

            DataProvider dataProvider = new DataProvider();
            try
            {
                dataProvider.connect();
                int row = dataProvider.ExecuteScalar(query);
                if (row > 0)
                    return true;
                else return false;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dataProvider.disconnect();
            }
            return true;
        }

        public static string CheckLogin(string acc, string pass)
        {
            string query = @"Select * From Account acc Where ID=N'" + acc + "' and  PASSWORD=N'" + pass + "'";

            DataProvider dataProvider = new DataProvider();
            try
            {
                dataProvider.connect();
                DataTable dt = dataProvider.ExecuteQuery_DataTable(query);
                string rt = "";
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        rt = "success" + cutIndex + row[2].ToString() + cutIndex + row[3].ToString();
                    }
                }
                return rt;
            }
            catch (Exception ex)
            {
                return "";
            }
            finally
            {
                dataProvider.disconnect();
            }
        }

        public static bool SignUp(string acc, string pass)
        {
            string query = @"Insert Into Account(ID, PASSWORD, NAME, IMAGE) Values(N'" + acc + "', N'" + pass + "', N'" + acc + "', N'60838637_p8_master1200.jpg') ";

            DataProvider dataProvider = new DataProvider();
            try
            {
                dataProvider.connect();
                return dataProvider.ExecuteUpdateQuery(query);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                dataProvider.disconnect();
            }
        }

        public static bool AddFriendTable(string acc1, string acc2)
        {
            string query = @"IF EXISTS (
                        SELECT *
                        FROM sys.tables
                        WHERE name = '#" + acc1 + "_" + acc2 + @"')
                        DROP TABLE #" + acc1 + "_" + acc2 + @"
                        CREATE TABLE " + acc1 + "_" + acc2 + @" (
                            Stt int IDENTITY(1,1) NOT NULL,
                            Content nvarchar(max),
                            IdChat varchar(20),
                            LogTime varchar(20),
                            TypeMess smallint
                        )";


            DataProvider dataProvider = new DataProvider();
            try
            {
                dataProvider.connect();
                return dataProvider.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                dataProvider.disconnect();
            }
        }

        public static bool AddFriend(string acc, string acc2)
        {
            string query = @"Insert Into FRIENDS(ID1, ID2, LASTMESS, TYPEMESS, STATUS) Values(N'" + acc + "', N'" + acc2 + "', N'', 0, 1) ";

            DataProvider dataProvider = new DataProvider();
            try
            {
                dataProvider.connect();
                return dataProvider.ExecuteUpdateQuery(query);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                dataProvider.disconnect();
            }
        }

        public static bool AcceptFriend(string acc, string acc2)
        {
            string query = @"UPDATE FRIENDS Set STATUS = 2 Where ID1= N'" + acc + "' and ID2 = N'" + acc2 + "'";

            DataProvider dataProvider = new DataProvider();
            try
            {
                if (AddFriendTable(acc, acc2))
                {
                    dataProvider.connect();
                    return dataProvider.ExecuteUpdateQuery(query);
                }
                else return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                dataProvider.disconnect();
            }
        }

        public static bool CancelFriend(string acc, string acc2)
        {
            string query = @"Delete FRIENDS Where ID1= N'" + acc + "' and ID2 = N'" + acc2 + "'";

            DataProvider dataProvider = new DataProvider();
            try
            {
                dataProvider.connect();
                return dataProvider.ExecuteUpdateQuery(query);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                dataProvider.disconnect();
            }
        }

        public static int CheckFriend(string acc, string acc2)
        {
            string query = @"Select * from FRIENDS fr Where (fr.ID1 = N'" + acc + "' And fr.ID2 = N'" + acc2 + "') ";

            DataProvider dataProvider = new DataProvider();
            try
            {
                dataProvider.connect();
                DataTable dt = dataProvider.ExecuteQuery_DataTable(query);
                int i = 0;
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            i = int.Parse(row[4].ToString());
                        }
                    }
                }
                return i;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                dataProvider.disconnect();
            }
        }

        public static int CheckFriendRev(string acc, string acc2)
        {
            string query = @"Select * from FRIENDS fr Where (fr.ID1 = N'" + acc2 + "' And fr.ID2 = N'" + acc + "') ";

            DataProvider dataProvider = new DataProvider();
            try
            {
                dataProvider.connect();
                DataTable dt = dataProvider.ExecuteQuery_DataTable(query);
                int i = 0;
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            i = int.Parse(row[4].ToString());
                        }
                    }
                }
                return i;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                dataProvider.disconnect();
            }
        }

        public static DataTable LoadFriend(string acc)
        {
            string query = @"Select * From FRIENDS fr, Account acc Where (fr.ID1 = N'" + acc + "' Or fr.ID2 = N'" + acc + "') and acc.ID <> N'" + acc + "' and (acc.ID = fr.ID2 or acc.ID =fr.ID1) and fr.STATUS = 2";

            DataProvider dataProvider = new DataProvider();
            try
            {
                dataProvider.connect();
                DataTable dt = dataProvider.ExecuteQuery_DataTable(query);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                dataProvider.disconnect();
            }
        }

        public static DataTable LoadWorld()
        {
            string query = @"Select * From Account acc";

            DataProvider dataProvider = new DataProvider();
            try
            {
                dataProvider.connect();
                DataTable dt = dataProvider.ExecuteQuery_DataTable(query);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                dataProvider.disconnect();
            }
        }

        public static DataTable GetFriendTableName(string acc, string acc2)
        {
            string query = @"Select * From FRIENDS fr Where (fr.ID1 = N'" + acc + "' And fr.ID2 = N'" + acc2 + "') Or (fr.ID1 = N'" + acc2 + "' And fr.ID2 = N'" + acc + "')";


            DataProvider dataProvider = new DataProvider();
            try
            {
                dataProvider.connect();
                DataTable dt = dataProvider.ExecuteQuery_DataTable(query);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                dataProvider.disconnect();
            }
        }

        public static bool AddFriendMess(string acc, string acc2, string accchat, string content, string logtime)
        {

            string query = @"";

            DataProvider dataProvider = new DataProvider();
            try
            {
                string tableName = "";
                DataTable dt = GetFriendTableName(acc, acc2);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            tableName = row[0].ToString() + "_" + row[1].ToString();
                        }
                    }
                }
                query = @"Insert Into " + tableName + "(Content, IdChat, LogTime, TypeMess) Values(N'" + content + "', N'" + accchat + "', N'" + logtime + "', 0) ";
                dataProvider.connect();
                return dataProvider.ExecuteUpdateQuery(query);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                dataProvider.disconnect();
            }
        }

        public static DataTable LoadFriendMess(string acc, string acc2)
        {

            string query = @"";

            DataProvider dataProvider = new DataProvider();
            try
            {
                string tableName = "";
                DataTable dt = GetFriendTableName(acc, acc2);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            tableName = row[0].ToString() + "_" + row[1].ToString();
                        }
                    }
                }
                query = @"select * from (Select Top 20 * From " + tableName + " tb, Account a where tb.IdChat = a.ID order by tb.Stt Desc) as t Order by t.Stt Asc";
                dataProvider.connect();
                return dataProvider.ExecuteQuery_DataTable(query);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                dataProvider.disconnect();
            }
        }

        public static bool ChangePassword(string acc, string passOld, string passNew)
        {
            string query = @"UPDATE Account Set PASSWORD = N'" + passNew + "' Where ID= N'" + acc + "' and PASSWORD = N'" + passOld + "'";

            DataProvider dataProvider = new DataProvider();
            try
            {
                dataProvider.connect();
                return dataProvider.ExecuteUpdateQuery(query);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                dataProvider.disconnect();
            }
        }

        public static bool ChangeNickname(string acc, string nick)
        {
            string query = @"UPDATE Account Set NAME = N'" + nick + "' Where ID= N'" + acc + "'";

            DataProvider dataProvider = new DataProvider();
            try
            {
                dataProvider.connect();
                return dataProvider.ExecuteUpdateQuery(query);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                dataProvider.disconnect();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (LogFile item in listviewLogFile.SelectedItems)
                {
                    string name = item.Name;
                    string path = item.Path;
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    foreach (LogFile l in listLogFile)
                    {
                        if (l.Name == name)
                            listLogFile.Remove(l);
                    }
                }
            }
            catch (Exception ex) { }
        }

    }

    public class User
    {
        string _id;
        string _online;
        string _ip;

        public User(string id)
        {
            Id = id;
            Online = "offline";
            Ip = "";
        }

        public string Id { get => _id; set => _id = value; }
        public string Online { get => _online; set => _online = value; }
        public string Ip { get => _ip; set => _ip = value; }
    }

    public class LogFile
    {
        string _name;
        string _path;

        public LogFile(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public string Name { get => _name; set => _name = value; }
        public string Path { get => _path; set => _path = value; }
    }

    public class Client
    {
        Socket _socket;
        string _id;
        bool isSignIn = false;

        public Client(Socket socket, string id)
        {
            _socket = socket;
            _id = id;
        }

        public Socket Socket { get => _socket; set => _socket = value; }
        public string Id { get => _id; set => _id = value; }
        public bool IsSignIn { get => isSignIn; set => isSignIn = value; }
    }

    public class Service
    {
        Socket _socket;
        string _id;
        TextBlock _txt;
        Client _item;
        public Task _task;
        private const int BUFFER_SIZE = 8192;
        ListView _lv;

        public Service(Socket socket, string id, TextBlock txt, Client item, ListView lv)
        {
            _socket = socket;
            _id = id;
            _txt = txt;
            _item = item;
            _task = Task.Factory.StartNew(Run);
            _lv = lv;
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
                    //if (ischat)
                    //{
                    //    _socket.Send(Encoding.UTF8.GetBytes("continue"));
                    //}

                    rec = _socket.Receive(data);
                    string func = Encoding.UTF8.GetString(data, 0, rec);

                    if (!func.Equals(""))
                    {
                        ctn = MainWindow.DetachContent(func);
                        if (ctn[0] != null)
                        {
                            ischat = false;
                            switch (ctn[0])
                            {
                                case "signout":
                                    _socket.Shutdown(SocketShutdown.Both);
                                    _socket.Close();
                                    Application.Current.Dispatcher.Invoke(() =>
                                    {
                                        MainWindow.listClient.Remove(_item);
                                        _txt.Text += "\n" + _id + " thoát.";
                                    });
                                    return;
                                case "signup":
                                    Application.Current.Dispatcher.Invoke(() =>
                                    {
                                        _txt.Text += "\n" + ctn[1] + " - " + ctn[2];
                                    });
                                    if (MainWindow.CheckExists(ctn[1]))
                                    {
                                        _socket.Send(Encoding.UTF8.GetBytes("Tài khoản đã tồn tại!"));

                                        MainWindow.WriteLog(DateTime.Now.ToString() + " - " + "WarningLog" + " - " + ctn[1] + " - " + "SignUpExists");
                                    }
                                    else
                                    {
                                        if (MainWindow.SignUp(ctn[1].Trim(), ctn[2].Trim()))
                                        {
                                            _socket.Send(Encoding.UTF8.GetBytes("Tạo tài khoản thành công!"));
                                            MainWindow.WriteLog(DateTime.Now.ToString() + " - " + "InformationLog" + " - " + ctn[1] + " - " + "SignUp");
                                        }
                                        else
                                        {
                                            _socket.Send(Encoding.UTF8.GetBytes("Tạo tài khoản thất bại!"));
                                            MainWindow.WriteLog(DateTime.Now.ToString() + " - " + "InformationLog" + " - " + ctn[1] + " - " + "SignUpFail");
                                        }
                                    }
                                    break;
                                case "signin":
                                    Application.Current.Dispatcher.Invoke(() =>
                                    {
                                        _txt.Text = "";
                                        _txt.Text += "\n" + ctn[1] + " - " + ctn[2]; ;
                                    });
                                    string log = MainWindow.CheckLogin(ctn[1].Trim(), ctn[2].Trim());
                                    if (log != "")
                                    {
                                        _id = ctn[1].Trim();
                                        _item.Id = ctn[1].Trim();
                                        bool isOnl = false;
                                        foreach (Client c in MainWindow.listClient)
                                        {
                                            if(c.Id==_id && c!=_item)
                                            {
                                                isOnl = true;
                                                break;
                                            }
                                        }
                                        rec = _socket.Receive(data);
                                        func = Encoding.UTF8.GetString(data, 0, rec);

                                       
                                        
                                        if (isOnl)
                                        { _socket.Send(Encoding.UTF8.GetBytes("Tài khoản này đang online.")); break; }
                                            
                                        Application.Current.Dispatcher.Invoke(() =>
                                        {
                                            _txt.Text += "\n" + func;

                                            MainWindow.WriteLog(DateTime.Now.ToString() + " - " + "InformationLog" + " - " + _id + " - " + "SignIn");
                                        });
                                        _socket.Send(Encoding.UTF8.GetBytes(log));


                                        Application.Current.Dispatcher.Invoke(() =>
                                        {
                                            _txt.Text += "\nLoad friend " + ctn[1];
                                        });

                                        rec = _socket.Receive(data);
                                        func = Encoding.UTF8.GetString(data, 0, rec);

                                        if (func.Trim() == "loadPeople")
                                        {
                                            DataTable dt1 = MainWindow.LoadFriend(_id);
                                            if (dt1 != null)
                                            {
                                                if (dt1.Rows.Count > 0)
                                                {
                                                    foreach (DataRow row in dt1.Rows)
                                                    {
                                                        bool isonline = false;
                                                        foreach (Client c in MainWindow.listClient)
                                                        {
                                                            if (c.Id == row[5].ToString())
                                                            {
                                                                isonline = true;
                                                                break;
                                                            }
                                                        }
                                                        if (isonline)
                                                            log = "loadfriend" + MainWindow.cutIndex + row[5].ToString() + MainWindow.cutIndex + row[7].ToString() + MainWindow.cutIndex + row[2].ToString() + MainWindow.cutIndex + row[8].ToString() + MainWindow.cutIndex + "true";
                                                        else
                                                            log = "loadfriend" + MainWindow.cutIndex + row[5].ToString() + MainWindow.cutIndex + row[7].ToString() + MainWindow.cutIndex + row[2].ToString() + MainWindow.cutIndex + row[8].ToString() + MainWindow.cutIndex + "false";

                                                        _socket.Send(Encoding.UTF8.GetBytes(log));

                                                        rec = _socket.Receive(data);
                                                        func = Encoding.UTF8.GetString(data, 0, rec);
                                                    }
                                                }
                                            }
                                            Application.Current.Dispatcher.Invoke(() =>
                                            {
                                                _txt.Text += "\nLoad world " + ctn[1];
                                            });
                                            dt1 = MainWindow.world;
                                            if (dt1 != null)
                                            {
                                                if (dt1.Rows.Count > 0)
                                                {
                                                    foreach (DataRow row in dt1.Rows)
                                                    {
                                                        bool isonline = false;
                                                        foreach (Client c in MainWindow.listClient)
                                                        {
                                                            if (c.Id == row[0].ToString())
                                                            {
                                                                isonline = true;
                                                                break;
                                                            }
                                                        }
                                                        if (isonline)
                                                            log = "loadworld" + MainWindow.cutIndex + row[0].ToString() + MainWindow.cutIndex + row[2].ToString() + MainWindow.cutIndex + "Chưa kết bạn!" + MainWindow.cutIndex + row[3].ToString() + MainWindow.cutIndex + "true";
                                                        else
                                                            log = "loadworld" + MainWindow.cutIndex + row[0].ToString() + MainWindow.cutIndex + row[2].ToString() + MainWindow.cutIndex + "Chưa kết bạn!" + MainWindow.cutIndex + row[3].ToString() + MainWindow.cutIndex + "false";

                                                        _socket.Send(Encoding.UTF8.GetBytes(log));
                                                        rec = _socket.Receive(data);
                                                        func = Encoding.UTF8.GetString(data, 0, rec);
                                                    }
                                                }
                                            }
                                        }

                                        foreach (Client client in MainWindow.listClient)
                                        {
                                            if (client.Id != _id && client.IsSignIn)
                                                client.Socket.Send(Encoding.UTF8.GetBytes("updateonline" + MainWindow.cutIndex + _id));
                                        }
                                        _item.IsSignIn = true;
                                        foreach (User user in MainWindow.listUser)
                                        {
                                            if (user.Id == _id)
                                            {
                                                Application.Current.Dispatcher.Invoke(() =>
                                                {
                                                    MainWindow.listUser.Remove(user);
                                                    User u = new User(_id);
                                                    u.Online = "online";
                                                    u.Ip = _socket.RemoteEndPoint.ToString();
                                                    MainWindow.listUser.Insert(0, u);
                                                });

                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        _socket.Send(Encoding.UTF8.GetBytes("Đăng nhập thất bại!"));
                                        MainWindow.WriteLog(DateTime.Now.ToString() + " - " + "WarningLog" + " - " + ctn[1] + " - " + "SignInFail");
                                    }
                                    break;

                                case "loadchatfriend":
                                    DataTable dt = MainWindow.LoadFriendMess(ctn[1], ctn[2]);
                                    if (dt != null)
                                    {
                                        if (dt.Rows.Count > 0)
                                        {
                                            foreach (DataRow row in dt.Rows)
                                            {
                                                string log1 = "chatmess" + MainWindow.cutIndex + row[2].ToString() + MainWindow.cutIndex + row[7].ToString() + MainWindow.cutIndex + row[1].ToString() + MainWindow.cutIndex + row[8].ToString() + MainWindow.cutIndex + row[3].ToString();

                                                _socket.Send(Encoding.UTF8.GetBytes(log1));
                                                rec = _socket.Receive(data);
                                            }
                                        }
                                    }
                                    break;
                                case "checkfriend":
                                    int i = MainWindow.CheckFriend(ctn[1], ctn[2]);
                                    int j = MainWindow.CheckFriendRev(ctn[1], ctn[2]);
                                    _socket.Send(Encoding.UTF8.GetBytes("checkfriend" + MainWindow.cutIndex + i.ToString() + MainWindow.cutIndex + j.ToString()));

                                    rec = _socket.Receive(data);
                                    func = Encoding.UTF8.GetString(data, 0, rec);
                                    break;
                                case "changepassword":
                                    if (MainWindow.ChangePassword(ctn[1], ctn[2], ctn[3]))
                                    {
                                        _socket.Send(Encoding.UTF8.GetBytes("changepassword" + MainWindow.cutIndex + "true"));

                                        rec = _socket.Receive(data);
                                        func = Encoding.UTF8.GetString(data, 0, rec);

                                        Application.Current.Dispatcher.Invoke(() =>
                                        {
                                            MainWindow.WriteLog(DateTime.Now.ToString() + " - " + "InformationLog" + " - " + _id + " - " + "ChangePassword");
                                        });
                                    }
                                    else
                                    {
                                        _socket.Send(Encoding.UTF8.GetBytes("changepassword" + MainWindow.cutIndex + "false"));

                                        rec = _socket.Receive(data);
                                        func = Encoding.UTF8.GetString(data, 0, rec);

                                    }
                                    break;
                                case "changenickname":
                                    if (MainWindow.ChangeNickname(ctn[1], ctn[2]))
                                    {
                                        _socket.Send(Encoding.UTF8.GetBytes("changenickname" + MainWindow.cutIndex + "true" + MainWindow.cutIndex + ctn[2]));

                                        rec = _socket.Receive(data);
                                        func = Encoding.UTF8.GetString(data, 0, rec);
                                        Application.Current.Dispatcher.Invoke(() =>
                                        {
                                            MainWindow.WriteLog(DateTime.Now.ToString() + " - " + "InformationLog" + " - " + _id + " - " + "ChangeName");
                                        });
                                    }
                                    else
                                    {
                                        _socket.Send(Encoding.UTF8.GetBytes("changenickname" + MainWindow.cutIndex + "false" + MainWindow.cutIndex + ctn[2]));

                                        rec = _socket.Receive(data);
                                        func = Encoding.UTF8.GetString(data, 0, rec);

                                    }
                                    break;
                                case "addfriend":
                                    if (MainWindow.AddFriend(ctn[1], ctn[2]))
                                        _socket.Send(Encoding.UTF8.GetBytes("makefriend" + MainWindow.cutIndex + "success"));
                                    else
                                        _socket.Send(Encoding.UTF8.GetBytes("makefriend" + MainWindow.cutIndex + "fail"));

                                    rec = _socket.Receive(data);
                                    func = Encoding.UTF8.GetString(data, 0, rec);
                                    break;
                                case "acceptfriend":
                                    if (MainWindow.AcceptFriend(ctn[1], ctn[2]))
                                        _socket.Send(Encoding.UTF8.GetBytes("makefriend" + MainWindow.cutIndex + "success"));
                                    else
                                        _socket.Send(Encoding.UTF8.GetBytes("makefriend" + MainWindow.cutIndex + "fail"));

                                    rec = _socket.Receive(data);
                                    func = Encoding.UTF8.GetString(data, 0, rec);
                                    break;
                                case "cancelfriend":
                                    if (MainWindow.CancelFriend(ctn[1], ctn[2]))
                                        _socket.Send(Encoding.UTF8.GetBytes("makefriend" + MainWindow.cutIndex + "success"));
                                    else
                                        _socket.Send(Encoding.UTF8.GetBytes("makefriend" + MainWindow.cutIndex + "fail"));

                                    rec = _socket.Receive(data);
                                    func = Encoding.UTF8.GetString(data, 0, rec);
                                    break;
                                case "acceptsendfile":
                                    string idSend = ctn[1];
                                    string idReceive = ctn[2];
                                    string fileName = ctn[3];
                                    string filePath = ctn[4];
                                    MainWindow.serverFile.Send(Encoding.UTF8.GetBytes(func));
                                    rec = MainWindow.serverFile.Receive(data);
                                    func = Encoding.UTF8.GetString(data, 0, rec);
                                    ctn = MainWindow.DetachContent(func);
                                    if (ctn[0] == "portfile")
                                    {
                                        foreach (Client client in MainWindow.listClient)
                                        {
                                            if (client.Id == idReceive && client.Id != "")
                                            {
                                                client.Socket.Send(Encoding.UTF8.GetBytes("portfile" + MainWindow.cutIndex + ctn[2] + MainWindow.cutIndex + idSend + MainWindow.cutIndex + idReceive + MainWindow.cutIndex + fileName));
                                            }
                                            if (client.Id == idSend && client.Id != "")
                                            {
                                                client.Socket.Send(Encoding.UTF8.GetBytes("portfile" + MainWindow.cutIndex + ctn[1] + MainWindow.cutIndex + idSend + MainWindow.cutIndex + idReceive + MainWindow.cutIndex + filePath + MainWindow.cutIndex + fileName));
                                            }
                                        }

                                    }
                                    break;
                                case "sendfile":
                                    foreach (Client client in MainWindow.listClient)
                                    {
                                        if (client.Id == ctn[3] && client.Id != "")
                                        {
                                            client.Socket.Send(Encoding.UTF8.GetBytes(func));
                                        }
                                    }
                                    break;
                                case "chatmess":
                                    if (ctn[5] == "chatfriend")
                                    {
                                        MainWindow.AddFriendMess(ctn[1], ctn[6], ctn[1], ctn[3], ctn[7]);
                                        foreach (Client client in MainWindow.listClient)
                                        {
                                            if (client.Id == ctn[6] && client.Id != "")
                                            {
                                                client.Socket.Send(Encoding.UTF8.GetBytes(func));
                                                //rec = client.Socket.Receive(data);
                                            }
                                        }
                                        //rec = _socket.Receive(data);
                                        //_socket.Send(Encoding.UTF8.GetBytes("issended"));
                                        //ischat = true;


                                        _socket.Send(Encoding.UTF8.GetBytes("continue"));
                                    }
                                    else if (ctn[5] == "chatgroup")
                                    {
                                        foreach (Client client in MainWindow.listClient)
                                        {
                                            if (client.Id != ctn[1] && client.Id != "")
                                            {
                                                client.Socket.Send(Encoding.UTF8.GetBytes(func));
                                                //rec = client.Socket.Receive(data);
                                            }
                                        }
                                        //rec = _socket.Receive(data);
                                        //_socket.Send(Encoding.UTF8.GetBytes("issended"));
                                        //ischat = true;


                                        _socket.Send(Encoding.UTF8.GetBytes("continue"));
                                    }
                                    break;
                            }

                            //if (ctn[0] == "signout")
                            //{
                            //    _socket.Shutdown(SocketShutdown.Both);
                            //    _socket.Close();
                            //    Application.Current.Dispatcher.Invoke(() =>
                            //    {
                            //        MainWindow.listClient.Remove(_item);
                            //        _txt.Text += "\n" + _id + " thoát.";
                            //    });
                            //    return;
                            //}
                            //if (ctn[0] == "signup")
                            //{
                            //    Application.Current.Dispatcher.Invoke(() =>
                            //    {
                            //        _txt.Text += "\n" + ctn[1] + " - " + ctn[2];
                            //    });
                            //    if (MainWindow.CheckExists(ctn[1]))
                            //    {
                            //        _socket.Send(Encoding.UTF8.GetBytes("Tài khoản đã tồn tại!"));

                            //        MainWindow.WriteLog(DateTime.Now.ToString() + " - " + "WarningLog" + " - " + ctn[1] + " - " + "SignUpExists");
                            //    }
                            //    else
                            //    {
                            //        if (MainWindow.SignUp(ctn[1].Trim(), ctn[2].Trim()))
                            //        {
                            //            _socket.Send(Encoding.UTF8.GetBytes("Tạo tài khoản thành công!"));
                            //            MainWindow.WriteLog(DateTime.Now.ToString() + " - " + "InformationLog" + " - " + ctn[1] + " - " + "SignUp");
                            //        }
                            //        else
                            //        {
                            //            _socket.Send(Encoding.UTF8.GetBytes("Tạo tài khoản thất bại!"));
                            //            MainWindow.WriteLog(DateTime.Now.ToString() + " - " + "InformationLog" + " - " + ctn[1] + " - " + "SignUpFail");
                            //        }
                            //    }
                            //}
                            //if (ctn[0] == "signin")
                            //{
                            //    Application.Current.Dispatcher.Invoke(() =>
                            //    {
                            //        _txt.Text = "";
                            //        _txt.Text += "\n" + ctn[1] + " - " + ctn[2]; ;
                            //    });
                            //    string log = MainWindow.CheckLogin(ctn[1].Trim(), ctn[2].Trim());
                            //    if (log != "")
                            //    {
                            //        _id = ctn[1].Trim();
                            //        _item.Id = ctn[1].Trim();

                            //        rec = _socket.Receive(data);
                            //        func = Encoding.UTF8.GetString(data, 0, rec);
                            //        Application.Current.Dispatcher.Invoke(() =>
                            //        {
                            //            _txt.Text += "\n" + func;

                            //            MainWindow.WriteLog(DateTime.Now.ToString() + " - " + "InformationLog" + " - " + _id + " - " + "SignIn");
                            //        });
                            //        _socket.Send(Encoding.UTF8.GetBytes(log));


                            //        Application.Current.Dispatcher.Invoke(() =>
                            //        {
                            //            _txt.Text += "\nLoad friend " + ctn[1];
                            //        });

                            //        rec = _socket.Receive(data);
                            //        func = Encoding.UTF8.GetString(data, 0, rec);

                            //        if (func.Trim() == "loadPeople")
                            //        {
                            //            DataTable dt = MainWindow.LoadFriend(_id);
                            //            if (dt != null)
                            //            {
                            //                if (dt.Rows.Count > 0)
                            //                {
                            //                    foreach (DataRow row in dt.Rows)
                            //                    {
                            //                        bool isonline = false;
                            //                        foreach (Client c in MainWindow.listClient)
                            //                        {
                            //                            if (c.Id == row[5].ToString())
                            //                            {
                            //                                isonline = true;
                            //                                break;
                            //                            }
                            //                        }
                            //                        if (isonline)
                            //                            log = "loadfriend" + MainWindow.cutIndex + row[5].ToString() + MainWindow.cutIndex + row[7].ToString() + MainWindow.cutIndex + row[2].ToString() + MainWindow.cutIndex + row[8].ToString() + MainWindow.cutIndex + "true";
                            //                        else
                            //                            log = "loadfriend" + MainWindow.cutIndex + row[5].ToString() + MainWindow.cutIndex + row[7].ToString() + MainWindow.cutIndex + row[2].ToString() + MainWindow.cutIndex + row[8].ToString() + MainWindow.cutIndex + "false";

                            //                        _socket.Send(Encoding.UTF8.GetBytes(log));

                            //                        rec = _socket.Receive(data);
                            //                        func = Encoding.UTF8.GetString(data, 0, rec);
                            //                    }
                            //                }
                            //            }
                            //            Application.Current.Dispatcher.Invoke(() =>
                            //            {
                            //                _txt.Text += "\nLoad world " + ctn[1];
                            //            });
                            //            dt = MainWindow.world;
                            //            if (dt != null)
                            //            {
                            //                if (dt.Rows.Count > 0)
                            //                {
                            //                    foreach (DataRow row in dt.Rows)
                            //                    {
                            //                        bool isonline = false;
                            //                        foreach (Client c in MainWindow.listClient)
                            //                        {
                            //                            if (c.Id == row[0].ToString())
                            //                            {
                            //                                isonline = true;
                            //                                break;
                            //                            }
                            //                        }
                            //                        if (isonline)
                            //                            log = "loadworld" + MainWindow.cutIndex + row[0].ToString() + MainWindow.cutIndex + row[2].ToString() + MainWindow.cutIndex + "Chưa kết bạn!" + MainWindow.cutIndex + row[3].ToString() + MainWindow.cutIndex + "true";
                            //                        else
                            //                            log = "loadworld" + MainWindow.cutIndex + row[0].ToString() + MainWindow.cutIndex + row[2].ToString() + MainWindow.cutIndex + "Chưa kết bạn!" + MainWindow.cutIndex + row[3].ToString() + MainWindow.cutIndex + "false";

                            //                        _socket.Send(Encoding.UTF8.GetBytes(log));
                            //                        rec = _socket.Receive(data);
                            //                        func = Encoding.UTF8.GetString(data, 0, rec);
                            //                    }
                            //                }
                            //            }
                            //        }

                            //        foreach (Client client in MainWindow.listClient)
                            //        {
                            //            if (client.Id != _id && client.IsSignIn)
                            //                client.Socket.Send(Encoding.UTF8.GetBytes("updateonline" + MainWindow.cutIndex + _id));
                            //        }
                            //        _item.IsSignIn = true;
                            //        foreach (User user in MainWindow.listUser)
                            //        {
                            //            if (user.Id == _id)
                            //            {
                            //                Application.Current.Dispatcher.Invoke(() =>
                            //                {
                            //                    MainWindow.listUser.Remove(user);
                            //                    User u = new User(_id);
                            //                    u.Online = "online";
                            //                    u.Ip = _socket.RemoteEndPoint.ToString();
                            //                    MainWindow.listUser.Insert(0, u);
                            //                });

                            //                break;
                            //            }
                            //        }
                            //    }
                            //    else
                            //    {
                            //        _socket.Send(Encoding.UTF8.GetBytes("Đăng nhập thất bại!"));
                            //        MainWindow.WriteLog(DateTime.Now.ToString() + " - " + "WarningLog" + " - " + ctn[1] + " - " + "SignInFail");
                            //    }
                            //}
                            //if (ctn[0] == "loadchatfriend")
                            //{
                            //    DataTable dt = MainWindow.LoadFriendMess(ctn[1], ctn[2]);
                            //    if (dt != null)
                            //    {
                            //        if (dt.Rows.Count > 0)
                            //        {
                            //            foreach (DataRow row in dt.Rows)
                            //            {
                            //                string log = "chatmess" + MainWindow.cutIndex + row[2].ToString() + MainWindow.cutIndex + row[7].ToString() + MainWindow.cutIndex + row[1].ToString() + MainWindow.cutIndex + row[8].ToString() + MainWindow.cutIndex + row[3].ToString();

                            //                _socket.Send(Encoding.UTF8.GetBytes(log));
                            //                rec = _socket.Receive(data);
                            //            }
                            //        }
                            //    }
                            //}
                            //if (ctn[0] == "checkfriend")
                            //{
                            //    int i = MainWindow.CheckFriend(ctn[1], ctn[2]);
                            //    int j = MainWindow.CheckFriendRev(ctn[1], ctn[2]);
                            //    _socket.Send(Encoding.UTF8.GetBytes("checkfriend" + MainWindow.cutIndex + i.ToString() + MainWindow.cutIndex + j.ToString()));

                            //    rec = _socket.Receive(data);
                            //    func = Encoding.UTF8.GetString(data, 0, rec);
                            //}
                            //if (ctn[0] == "changepassword")
                            //{
                            //    if (MainWindow.ChangePassword(ctn[1], ctn[2], ctn[3]))
                            //    {
                            //        _socket.Send(Encoding.UTF8.GetBytes("changepassword" + MainWindow.cutIndex + "true"));

                            //        rec = _socket.Receive(data);
                            //        func = Encoding.UTF8.GetString(data, 0, rec);

                            //        Application.Current.Dispatcher.Invoke(() =>
                            //        {
                            //            MainWindow.WriteLog(DateTime.Now.ToString() + " - " + "InformationLog" + " - " + _id + " - " + "ChangePassword");
                            //        });
                            //    }
                            //    else
                            //    {
                            //        _socket.Send(Encoding.UTF8.GetBytes("changepassword" + MainWindow.cutIndex + "false"));

                            //        rec = _socket.Receive(data);
                            //        func = Encoding.UTF8.GetString(data, 0, rec);

                            //    }
                            //}
                            //if (ctn[0] == "changenickname")
                            //{
                            //    if (MainWindow.ChangeNickname(ctn[1], ctn[2]))
                            //    {
                            //        _socket.Send(Encoding.UTF8.GetBytes("changenickname" + MainWindow.cutIndex + "true" + MainWindow.cutIndex + ctn[2]));

                            //        rec = _socket.Receive(data);
                            //        func = Encoding.UTF8.GetString(data, 0, rec);
                            //        Application.Current.Dispatcher.Invoke(() =>
                            //        {
                            //            MainWindow.WriteLog(DateTime.Now.ToString() + " - " + "InformationLog" + " - " + _id + " - " + "ChangeName");
                            //        });
                            //    }
                            //    else
                            //    {
                            //        _socket.Send(Encoding.UTF8.GetBytes("changenickname" + MainWindow.cutIndex + "false" + MainWindow.cutIndex + ctn[2]));

                            //        rec = _socket.Receive(data);
                            //        func = Encoding.UTF8.GetString(data, 0, rec);

                            //    }
                            //}
                            //if (ctn[0] == "addfriend")
                            //{
                            //    if (MainWindow.AddFriend(ctn[1], ctn[2]))
                            //        _socket.Send(Encoding.UTF8.GetBytes("makefriend" + MainWindow.cutIndex + "success"));
                            //    else
                            //        _socket.Send(Encoding.UTF8.GetBytes("makefriend" + MainWindow.cutIndex + "fail"));

                            //    rec = _socket.Receive(data);
                            //    func = Encoding.UTF8.GetString(data, 0, rec);
                            //}
                            //if (ctn[0] == "acceptfriend")
                            //{
                            //    if (MainWindow.AcceptFriend(ctn[1], ctn[2]))
                            //        _socket.Send(Encoding.UTF8.GetBytes("makefriend" + MainWindow.cutIndex + "success"));
                            //    else
                            //        _socket.Send(Encoding.UTF8.GetBytes("makefriend" + MainWindow.cutIndex + "fail"));

                            //    rec = _socket.Receive(data);
                            //    func = Encoding.UTF8.GetString(data, 0, rec);
                            //}
                            //if (ctn[0] == "cancelfriend")
                            //{
                            //    if (MainWindow.CancelFriend(ctn[1], ctn[2]))
                            //        _socket.Send(Encoding.UTF8.GetBytes("makefriend" + MainWindow.cutIndex + "success"));
                            //    else
                            //        _socket.Send(Encoding.UTF8.GetBytes("makefriend" + MainWindow.cutIndex + "fail"));

                            //    rec = _socket.Receive(data);
                            //    func = Encoding.UTF8.GetString(data, 0, rec);
                            //}
                            //if (ctn[0] == "acceptsendfile")
                            //{
                            //    string idSend = ctn[1];
                            //    string idReceive = ctn[2];
                            //    string fileName = ctn[3];
                            //    string filePath = ctn[4];
                            //    MainWindow.serverFile.Send(Encoding.UTF8.GetBytes(func));
                            //    rec = MainWindow.serverFile.Receive(data);
                            //    func = Encoding.UTF8.GetString(data, 0, rec);
                            //    ctn = MainWindow.DetachContent(func);
                            //    if (ctn[0] == "portfile")
                            //    {
                            //        foreach (Client client in MainWindow.listClient)
                            //        {
                            //            if (client.Id == idReceive && client.Id != "")
                            //            {
                            //                client.Socket.Send(Encoding.UTF8.GetBytes("portfile" + MainWindow.cutIndex + ctn[2] + MainWindow.cutIndex + idSend + MainWindow.cutIndex + idReceive + MainWindow.cutIndex + fileName));
                            //            }
                            //            if (client.Id == idSend && client.Id != "")
                            //            {
                            //                client.Socket.Send(Encoding.UTF8.GetBytes("portfile" + MainWindow.cutIndex + ctn[1] + MainWindow.cutIndex + idSend + MainWindow.cutIndex + idReceive + MainWindow.cutIndex + filePath + MainWindow.cutIndex + fileName));
                            //            }
                            //        }

                            //    }
                            //}
                            //if (ctn[0] == "sendfile")
                            //{
                            //    foreach (Client client in MainWindow.listClient)
                            //    {
                            //        if (client.Id == ctn[3] && client.Id != "")
                            //        {
                            //            client.Socket.Send(Encoding.UTF8.GetBytes(func));
                            //        }
                            //    }
                            //}
                            //if (ctn[0] == "chatmess")
                            //{
                            //    if (ctn[5] == "chatfriend")
                            //    {
                            //        MainWindow.AddFriendMess(ctn[1], ctn[6], ctn[1], ctn[3], ctn[7]);
                            //        foreach (Client client in MainWindow.listClient)
                            //        {
                            //            if (client.Id == ctn[6] && client.Id != "")
                            //            {
                            //                client.Socket.Send(Encoding.UTF8.GetBytes(func));
                            //                //rec = client.Socket.Receive(data);
                            //            }
                            //        }
                            //        rec = _socket.Receive(data);
                            //        _socket.Send(Encoding.UTF8.GetBytes("issended"));
                            //        ischat = true;
                            //    }
                            //    else if (ctn[5] == "chatgroup")
                            //    {
                            //        foreach (Client client in MainWindow.listClient)
                            //        {
                            //            if (client.Id != ctn[1] && client.Id != "")
                            //            {
                            //                client.Socket.Send(Encoding.UTF8.GetBytes(func));
                            //                //rec = client.Socket.Receive(data);
                            //            }
                            //        }
                            //        rec = _socket.Receive(data);
                            //        _socket.Send(Encoding.UTF8.GetBytes("issended"));
                            //        ischat = true;
                            //    }
                            //}

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
                foreach (Client client in MainWindow.listClient)
                {
                    if (client.Id != _id && client.IsSignIn)
                        client.Socket.Send(Encoding.UTF8.GetBytes("updateoffline" + MainWindow.cutIndex + _id));
                }
                foreach (User user in MainWindow.listUser)
                {
                    if (user.Id == _id)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            MainWindow.listUser.Remove(user);
                            User u = new User(_id);
                            u.Online = "offline";
                            u.Ip = "";
                            MainWindow.listUser.Add(u);
                        });

                        break;
                    }
                }
                Application.Current.Dispatcher.Invoke(() =>
                {
                    _txt.Text += "\n" + _id + " thoát.";
                    MainWindow.WriteLog(DateTime.Now.ToString() + " - " + "ErrorLog" + " - " + _id + " - " + "Disconnect");
                });

                MainWindow.listClient.Remove(_item);
                return;
            }
        }
    }

}
