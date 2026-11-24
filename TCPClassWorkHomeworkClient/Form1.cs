using System.Net.Sockets;
using System.Net;
using System.Text;
namespace TCPClassWorkHomeworkClient
{

    public partial class Form1 : Form
    {
        TcpClient client = null!;
        NetworkStream stream = null!;
        CancellationTokenSource cts = new CancellationTokenSource();

        public Form1()
        {
            InitializeComponent();
            client = new TcpClient();

        }

        private void ConnectionButton_Click(object sender, EventArgs e)
        {
            if (client != null && client.Connected)
            {
                MessageBox.Show("Allready Connected");
                return;
            }

            try
            {
                client = new TcpClient();
                client.Connect(new IPEndPoint(IPAddress.Loopback, 10000));
                stream = client.GetStream();
                MessageBox.Show("Connecting successfully");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("You not connected");
                return;
            }

            try
            {
                byte[] msg = Encoding.UTF8.GetBytes("Hi to server");
                stream.Write(msg, 0, msg.Length);
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string answer = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                textBox1.Text = answer;
            }
            catch( Exception ex)
            {  MessageBox.Show(ex.Message); }
            finally { client.Close(); }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            stream?.Close();
            client?.Close();
        }
    }
}
