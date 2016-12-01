using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using GateServer1;

namespace GateServer1.Test
{
    public partial class Form1 : Form
    {
        private GateSocket gateSocket = new GateSocket();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            gateSocket.ServerIp = txtServerIp.Text;
            gateSocket.ServerPort = Convert.ToInt32(txtServerPort.Text);
            gateSocket.VmCode = txtVmCode.Text;
            gateSocket.VmPwd = txtPwd.Text;

            gateSocket.Initialize();
        }

        private void btnSendTemp_Click(object sender, EventArgs e)
        {
            gateSocket.UpdateTempStatus("1", "0", "19");
        }

        private void btnSendCardKind_Click(object sender, EventArgs e)
        {
            gateSocket.UpdateCardStatus("01");
        }

        private void btnSendDoor_Click(object sender, EventArgs e)
        {
            gateSocket.UpdateDoorStatus("00");
        }

        private void btnPaStatus_Click(object sender, EventArgs e)
        {
            gateSocket.UpdatePaStatus("09", "A8");
        }

        private void btnCoinStatus_Click(object sender, EventArgs e)
        {
            gateSocket.UpdateCoinStatus("02");
        }

        private void btnDropStatus_Click(object sender, EventArgs e)
        {
            gateSocket.UpdateDropCheckStatus("02");
        }

        private void btnCashStatus_Click(object sender, EventArgs e)
        {
            gateSocket.UpdateCashStatus("02");
        }

        private void btnMainBoardStatus_Click(object sender, EventArgs e)
        {
            gateSocket.UpdateMainBoardStatus("01");
        }

        private void btnRecCoin1_Click(object sender, EventArgs e)
        {
            gateSocket.OperMoney("1","0", "0", 100, 1);
        }

        private void btnRecCash10_Click(object sender, EventArgs e)
        {
            gateSocket.OperMoney("1","1", "0", 1000, 1);
        }

        private void btnChangeCoin1_Click(object sender, EventArgs e)
        {
            gateSocket.OperMoney("1","0", "1", 100, 1);
        }

        private void btnTun6_Click(object sender, EventArgs e)
        {
            gateSocket.OperMoney("1","0", "2", 600, 1);
        }

        private void btnSellGoods_Click(object sender, EventArgs e)
        {
            gateSocket.SellGoods("1","A5", 250, "", 1,"3");
        }

        private void btnPosCard_Click(object sender, EventArgs e)
        {
            gateSocket.PosCard("1","2", "1000000001","", 100, 350, "22", "0", "");
        }

        private void btnGetNetStatus_Click(object sender, EventArgs e)
        {
            bool blnNetStatus = gateSocket.SocketStatus;

            string strNetStatus = "";

            switch (blnNetStatus)
            {
                case true:
                    strNetStatus = "联机";
                    break;

                default:
                    strNetStatus = "离线";
                    break;
            }

            MessageBox.Show("当前连接状态为：" + strNetStatus, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGetWaitNum_Click(object sender, EventArgs e)
        {
            int dataCount = gateSocket.GetWaitDataNum();

            MessageBox.Show("当前待发数据数量为：" + dataCount.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            gateSocket.Displose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gateSocket.InitAsileList(5, "1112131415");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gateSocket.InitPaPrice(3, "200,506,200,");
        }

        private void btnCreateNewBus_Click(object sender, EventArgs e)
        {
        }

        private void btnSendPara_Click(object sender, EventArgs e)
        {
            ////gateSocket.UpdateParameter("146", "13187007287", "13012345678", "123");
            gateSocket.SetAsileStock("2", "A", "5", "");
        }

        private void btnSoftVer_Click(object sender, EventArgs e)
        {
            gateSocket.InitSoftInfo("KVM1.0.0", "13187007287");
        }

        private void btnDeviceStatus_Click(object sender, EventArgs e)
        {
            gateSocket.InitDeviceStatus("01*02*00*02*00*02*02*39");
        }

        private void btnPosWh_Click(object sender, EventArgs e)
        {
            gateSocket.PosCard_WH_Suc("3", "5FAC00020000102700032755120150107132445000028068027110110846027A841E6761101000010270003275520150107132354000000010001251013000010270003275520150107132445000002008030F28386C080000000000000000D2CC");
        }

        private void btWxTakeCode_Click(object sender, EventArgs e)
        {
            gateSocket.WxTakeCode_Pay("000234", "123456", 1, "82");
        }
    }
}
