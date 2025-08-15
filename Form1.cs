using DBDesign.PosiStageDotNet;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Fx5UC_Read
{
    public partial class Form1 : Form
    {

        /// <summary>
        /// Private variable declaration
        /// </summary>
        private Socket _socket;

        //Declare PSN server and tracker
        private PsnServer psnServer;
        private PsnTracker tracker, tracker1, tracker2, tracker3;

        public Form1()
        {
            InitializeComponent();
            NetworkChange.NetworkAvailabilityChanged += OnNetworkAvailabilityChanged;

            // Initialize server and a dummy tracker
            var adapterIp = IPAddress.Parse("192.168.1.150"); // Set correct address
            psnServer = new PsnServer("PLC PSN Server", adapterIp);

            tracker = new PsnTracker(0, "200", Tuple.Create(0f, 0f, 0f));

            psnServer.SetTrackers(tracker);

        }

        //**************************************************** Form Functions ****************************************************//

        /// <summary>
        /// Function : 
        /// Check the network availability.
        /// When plc disconnect after the connection and connect it display a message.
        /// If ethernet is disable, it shows the message. Same with WIFI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            try
            {
                if (!e.IsAvailable)
                {
                    if (_socket.Connected)
                    {

                        _socket.Shutdown(SocketShutdown.Both);
                        timer1.Enabled = false;
                        _socket.DisconnectAsync(true);

                        if (!btnConnect.Enabled)
                        {
                            btnConnect.Invoke(new MethodInvoker(delegate
                            {
                                btnConnect.Enabled = true;
                            }));
                            btnStatus.BackColor = Color.Red;
                        }

                        MessageBox.Show("Ethernet is disconnect", "Ethernet Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (e.IsAvailable)
                    {
                        MessageBox.Show("Ethernet is connected", "Ethernet Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string ipAddress = txtIpAddress.Text;
                        int port = Int32.Parse(txtPortNo.Text);
                        ConnectToDevice(ipAddress, port);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Function : Ping the target ip address.
        /// </summary>
        /// <param name="targetAddress"></param>
        /// <returns></returns>
        private bool TryPing(string targetAddress)
        {
            try
            {
                using (Ping pingSender = new Ping())
                {
                    PingReply reply = pingSender.Send(targetAddress, 1000);

                    return reply.Status == IPStatus.Success;

                }
                ;
            }
            catch (PingException ex)
            {
                MessageBox.Show($"Ping Issue : {ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Function : Try to Connect the Plc Async mode
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        private async Task<bool> TryConnectToPlcAsync(string ip, int port)
        {
            try
            {
                if (!string.IsNullOrEmpty(ip) && port != 0 && TryPing(ip)) // port cannot be null since int is value type
                {
                    _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    await _socket.ConnectAsync(IPAddress.Parse(ip), port);

                    return true; // successfully connected
                }
                else
                {
                    MessageBox.Show("Ip Address or Port Number is Empty or NULL", "Connection Issue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false; // return false when input invalid
                }
            }
            catch
            {
                MessageBox.Show("Connection Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // connection failed
            }
        }

        /// <summary>
        /// Function : 
        /// This function connect to external device using socket.
        /// Pass ip address and port number To method TryConnectToPlcAsync
        /// once device connected successfully it disable the connect button
        /// and change connection status Red to Green.
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <param name="port"></param>
        private async void ConnectToDevice(string ipaddress, int port)
        {
            try
            {
                var isAnyNetworkAvailable = NetworkInterface.GetIsNetworkAvailable();

                if (isAnyNetworkAvailable)
                {
                    if (!string.IsNullOrEmpty(ipaddress) && port != 0)
                    {
                        bool isConnect = await TryConnectToPlcAsync(ipaddress, port).ConfigureAwait(false);
                        if (isConnect)
                        {
                            btnConnect.Invoke(new MethodInvoker(delegate
                            {
                                btnConnect.Enabled = false;
                            }));
                            btnStatus.BackColor = Color.LawnGreen;
                            MessageBox.Show("Connected Successfully!!", "Connection Status", MessageBoxButtons.OK);
                        }
                        else
                        {
                            if (!btnConnect.Enabled) btnConnect.Enabled = true;

                            btnStatus.BackColor = Color.Red;
                            MessageBox.Show("Connection Failed", "Connection Status", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ip Address or Port Number is Empty or NULL", "Connection Issue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Your PC / Laptop is not connected with ethernet.", "Connection Issue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Function : Converts an array of hexadecimal string values to a byte array.
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        private static byte[] StringsToBytes(string[] src)
        {
            byte[] returnBytes = new byte[src.Length];
            try
            {
                for (int i = 0; i < src.Length; i++)
                {
                    returnBytes[i] = Convert.ToByte(src[i], 16);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return returnBytes;
        }

        /// <summary>
        /// Function : Send and receive message using socket.
        /// </summary>
        /// <param name="sendMessage"></param>
        /// <returns></returns>
        private async Task<byte[]> SendAndReceive(string[] sendMessage)
        {

            byte[]? byteReceiveMessage = null;
            int receiveSize = 0;

            byte[] byteSendMessage = StringsToBytes(sendMessage); // Assuming this function exists and behaves similarly in C#

            try
            {
                if (_socket.Connected)
                {

                    await _socket.SendAsync(byteSendMessage, SocketFlags.None);
                    _socket.SendBufferSize = byteSendMessage.Length;

                    do
                    {
                        int available = _socket.Available;
                        if (available > 0)
                        {
                            byteReceiveMessage = new byte[available];
                            receiveSize = await _socket.ReceiveAsync(byteReceiveMessage, SocketFlags.None);
                        }
                        else
                        {
                            receiveSize = 0;
                        }
                    }
                    while (receiveSize == 0);

                    return byteReceiveMessage;
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Socket error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null; // Ensures all code paths return a value
        }

        /// <summary>
        /// Read Data Register of PLC
        /// </summary>
        private async Task<List<float>> ReadDataRegisters(Control dataRegister, Control rdValue)
        {
            // Read D
            string DeviceNo_D = dataRegister.Text; 
            string DeviceNo2_D = Convert.ToInt32(DeviceNo_D).ToString("X").PadLeft(6, '0').Substring(0, 6);
            string DeviceNo15_D = DeviceNo2_D.Substring(4, 2);
            string DeviceNo16_D = DeviceNo2_D.Substring(2, 2);
            string DeviceNo17_D = DeviceNo2_D.Substring(0, 2);

            List<float> data = new List<float>();
            string[] sendmessage = new string[21];

            sendmessage[0] = "50";
            sendmessage[1] = "00";
            sendmessage[2] = "00";
            sendmessage[3] = "FF";
            sendmessage[4] = "FF";
            sendmessage[5] = "03";
            sendmessage[6] = "00";
            sendmessage[7] = "0C";
            sendmessage[8] = "00";
            sendmessage[9] = "10";
            sendmessage[10] = "00";
            sendmessage[11] = "01";
            sendmessage[12] = "04";
            sendmessage[13] = "00";
            sendmessage[14] = "00";
            sendmessage[15] = DeviceNo15_D;
            sendmessage[16] = DeviceNo16_D;
            sendmessage[17] = DeviceNo17_D;
            sendmessage[18] = "A8";
            sendmessage[19] = "01";
            sendmessage[20] = "00";

            byte[] bytereceiveMessage4 = await SendAndReceive(sendmessage);
            byte[] arrays =  bytereceiveMessage4[11..13];

            var a200 = BitConverter.ToInt16(arrays, 0);
            
            data.Add(a200);

            return data;
        }

        //**************************************************** Form Events ****************************************************//

        /// <summary>
        ///  Event: ONFORMCLOSE SOCKET AND FORM IS CLOSE.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                psnServer.StopSending();
                psnServer.Dispose();

                if (_socket != null && _socket.Connected)
                {
                    _socket.Shutdown(SocketShutdown.Both);
                    _socket.DisconnectAsync(true);
                    _socket.Close();

                }
                for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                {
                    var form = Application.OpenForms[i];
                    form.Close();
                    form.Dispose();
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Event: Connect button 
        /// To establish the connection with external device
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string ipAddress = txtIpAddress.Text;
                int port = Int32.Parse(txtPortNo.Text);

                ConnectToDevice(ipAddress, port);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event of Timer Component: 
        /// It help to read data from plc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void timer1_Tick(object sender, EventArgs e)
        {
            var startTime = DateTime.Now;
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            try
            {

                List<float> data = await ReadDataRegisters(txtDataRegAddress, txtDataRegRdVal);

                txtDataRegRdVal.Text = data[0].ToString();

                var updatetracker = tracker.WithPosition(Tuple.Create(data[0], 0f, 0f));

                psnServer.SetTrackers(updatetracker);

            }
            catch (Exception ex)
            {
                // Handle any exceptions from the tasks.
                timer1.Enabled = false;
                timer1.Stop();
                MessageBox.Show($"An error occurred during data retrieval: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally 
            {
                stopwatch.Stop();
                var endTime = DateTime.Now;
                System.Diagnostics.Debug.WriteLine($"Start Time: {startTime.ToString("HH:mm:ss.fff")}, End Time: {endTime.ToString("HH:mm:ss.fff")}, Execution Time: {stopwatch.ElapsedMilliseconds} ms");
            }
         
        }

        /// <summary>
        /// Event: 
        /// Start Timer to Read Data from plc. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartDataRdVal_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
            psnServer.StartSending();
        }

        /// <summary>
        /// Event: 
        /// it stops the function to read data from the plc. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopDataRdVal_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Stop();
            psnServer.StopSending();
        }
    }
}
