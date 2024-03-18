using ChatServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace ChatServer
{
    public partial class Form1 : Form
    {
        private List<User> users = new List<User>();
        private static TcpListener listener;
        private static bool isServerRunning = true;
        private static readonly object lockObj = new object();
        private int connections = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void startServer()
        {
            listener = new TcpListener(IPAddress.Any, 8080);
            listener.Start();
            UpdateStatusLabel("Server is online");

            while (isServerRunning)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    ThreadPool.QueueUserWorkItem(new WaitCallback(HandleClient), client);

                    labelCounter.Invoke((MethodInvoker)delegate {
                        connections++;
                        labelCounter.Text = connections.ToString();
                    });
                }
                catch (SocketException)
                {
                    break;
                }
            }

            listener.Stop();
            UpdateStatusLabel("Server is offline");
        }

        private void HandleClient(object clientObj)
        {
            TcpClient tcpClient = (TcpClient)clientObj;

            try
            {
                
                NetworkStream clientStream = tcpClient.GetStream();
                byte[] buffer = new byte[4096];
                int bytesRead = clientStream.Read(buffer, 0, buffer.Length);
                string firstMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                string[] splitFirstMessage = firstMessage.Split(new char[] {'#'});
                if (splitFirstMessage[0] != "FirstStep")
                {
                    tcpClient.Close();
                }
                else
                {
                    
                    string username = splitFirstMessage[1];
                    int age = int.Parse(splitFirstMessage[2]);
                    string locationL = splitFirstMessage[3];
                    string locationA = splitFirstMessage[4];
                    //StringBuilder photo = new StringBuilder();
                    //photo.Append(splitFirstMessage[5]);
                    User currentClient = new User(username, age, " ", locationL, locationA, tcpClient);
                    lock (lockObj)
                    {
                        if (!users.Any(x => x.Name == username))
                        {
                            BroadcastNewUser(currentClient);
                            users.Add(currentClient);
                        }

                    }
                    string listOfClients = $"Users&{string.Join("&", users.Select(x => x.ToString()))}\r\n";
                    byte[] listOfClientsInBytes = Encoding.UTF8.GetBytes(listOfClients);
                    clientStream.Write(listOfClientsInBytes, 0, listOfClientsInBytes.Length);
                    UpdateConnectedClientsGrid();
                    Invoke((MethodInvoker)delegate
                    {
                        rtbIncomingMsg.AppendText($"{firstMessage}\n");
                    });

                    while (true)
                    {
                        bytesRead = clientStream.Read(buffer, 0, buffer.Length);

                        if (bytesRead == 0)
                        {
                            User client = users.Find(x => x.Connection == tcpClient);
                            lock (lockObj)
                            {
                                users.Remove(client);
                            }

                            Invoke((MethodInvoker)delegate
                            {
                                UpdateConnectedClientsGrid();
                                BroadcastRemovedUser(client.Name);
                            });

                            tcpClient.Close();
                            break;
                        }

                        string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                        string[] splittedMessage = receivedMessage.Split(new char[] { '&' });
                        if (splittedMessage[0] == "P2PMessage")
                        {
                            sendMessageP2P(splittedMessage[1], splittedMessage[2], splittedMessage[3]);
                        }

                        //Invoke((MethodInvoker)delegate
                        //{
                        //    rtbIncomingMsg.AppendText($"{receivedMessage}\n");
                        //});

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }


        private void UpdateConnectedClientsGrid()
        {
            if (gridClients.InvokeRequired)
            {
                gridClients.Invoke(new Action(() => UpdateConnectedClientsGrid()));
            }
            else
            {
                gridClients.Rows.Clear();
                foreach (var user in users)
                {
                    gridClients.Rows.Add(user.Name);
                }
            }
        }
        private void BroadcastNewUser(User newUser)
        {
            string message = $"NewUser&{newUser.ToString()}\r\n";
            lock (lockObj)
            {
                foreach (var user in users)
                {
                    try
                    {
                        SendToUser(message, user.Connection);
                        Invoke((MethodInvoker)delegate
                        {
                            rtbSent.AppendText(message);
                        });
                    }
                    catch{}
                }
            }
        }
        private void BroadcastRemovedUser(String removedUser)
        {
            string message = $"RemovedUser&{removedUser}\r\n";
            lock (lockObj)
            {
                foreach (var user in users)
                {
                    try
                    {
                        SendToUser(message, user.Connection);
                        Invoke((MethodInvoker)delegate
                        {
                            rtbSent.AppendText(message);
                        });
                    }
                    catch{}
                }
            }
        }
        private void SendToUser(string messsage, TcpClient receiver)
        {
            try
            {
                NetworkStream userStream = receiver.GetStream();
                byte[] messageInBytes = Encoding.UTF8.GetBytes($"{messsage}\r\n");
                userStream.Write(messageInBytes, 0, messageInBytes.Length);
                Invoke((MethodInvoker)delegate
                {
                    rtbSent.AppendText($"{messsage}\n");
                });
            }
            catch{}
        }


        private void UpdateStatusLabel(string text)
        {
            if (labelStatus.InvokeRequired)
            {
                labelStatus.Invoke(new Action(() => labelStatus.Text = text));
                UpdateConnectedClientsGrid();
            }
            else
            {
                labelStatus.Text = text;
            }
        }
        private void sendMessageP2P(string sender, string receiver, string message)
        {
            string completeMessage = $"P2PMessage&{sender}&{receiver}&{message}";
            try
            {
                TcpClient userClient = users.First(x => x.Name == receiver).Connection;
                NetworkStream userStream = userClient.GetStream();
                byte[] sendMessage = Encoding.UTF8.GetBytes(completeMessage + "\r\n");
                userStream.Write(sendMessage, 0, sendMessage.Length);
                Invoke((MethodInvoker)delegate
                {
                    rtbSent.AppendText(message);
                });
            }
            catch{}
        }


        private void btnStartServer_Click_1(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(_ => startServer()));
        }

        private void btnStopServer_Click_1(object sender, EventArgs e)
        {
            isServerRunning = false;
            listener?.Stop();
            UpdateStatusLabel("Server is offline");
        }
    } // class
} // namespace
