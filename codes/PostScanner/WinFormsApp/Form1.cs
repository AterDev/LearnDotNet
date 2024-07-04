using System.Net;
using ScannerLib;

namespace WinFormsApp;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private async void scanBtn_Click(object sender, EventArgs e)
    {
        // ��ȡ�ؼ�ֵ 
        var ipStr = IPBox.Text;
        var startPortStr = startBox.Text;
        var endPortStr = endBox.Text;

        // �ж��û�����
        if (string.IsNullOrWhiteSpace(ipStr))
        {
            MessageBox.Show("ip����Ϊ��");
        }

        if (string.IsNullOrWhiteSpace(startPortStr) || string.IsNullOrWhiteSpace(endPortStr))
        {
            MessageBox.Show("�˿ڲ���Ϊ��");
        }

        if (int.TryParse(startPortStr, out var startPort) && int.TryParse(endPortStr, out var endPort))
        {
            if (startPort > endPort)
            {
                MessageBox.Show("��ʼ�˿ڲ��ɴ��ڽ����˿�");
            }

            if (IPAddress.TryParse(ipStr, out var ip))
            {
                scanBtn.Enabled = false;
                resultBox.Text = "ɨ����..";
                // ��ʼ�˿�ɨ��
                var helper = new ScannerHelper(ip, startPort, endPort);

                // ʹ��Task.Run���첽ִ��ScanPortsTask����
                var ports = await Task.Run(() => helper.ScanPortsTask());

                if (ports.Count > 0)
                {
                    var content = string.Join(Environment.NewLine, ports);
                    resultBox.Text = "���ŵĶ˿�" + Environment.NewLine + content;
                }
                else
                {
                    resultBox.Text = "ɨ�������û�п��ŵĶ˿�";
                }
                scanBtn.Enabled = true;
            }
            else
            {
                MessageBox.Show("ip��ַ���Ϸ�");
            }
        }
        else
        {
            MessageBox.Show("�˿ڱ���Ϊ����");
        }
    }
}
