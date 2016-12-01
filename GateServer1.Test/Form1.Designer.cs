namespace GateServer1.Test
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnMainBoardStatus = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDeviceStatus = new System.Windows.Forms.Button();
            this.btnSoftVer = new System.Windows.Forms.Button();
            this.btnCashStatus = new System.Windows.Forms.Button();
            this.btnDropStatus = new System.Windows.Forms.Button();
            this.btnCoinStatus = new System.Windows.Forms.Button();
            this.btnSendDoor = new System.Windows.Forms.Button();
            this.btnSendCardKind = new System.Windows.Forms.Button();
            this.btnPaStatus = new System.Windows.Forms.Button();
            this.btnSendTemp = new System.Windows.Forms.Button();
            this.btnPosCard = new System.Windows.Forms.Button();
            this.btnRecCash10 = new System.Windows.Forms.Button();
            this.btnGetWaitNum = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnPosWh = new System.Windows.Forms.Button();
            this.btnCreateNewBus = new System.Windows.Forms.Button();
            this.btnInitAsileStatus = new System.Windows.Forms.Button();
            this.btnInitAsileList = new System.Windows.Forms.Button();
            this.btnRecCoin1 = new System.Windows.Forms.Button();
            this.btnSellGoods = new System.Windows.Forms.Button();
            this.btnChangeCoin1 = new System.Windows.Forms.Button();
            this.btnTun6 = new System.Windows.Forms.Button();
            this.btnGetNetStatus = new System.Windows.Forms.Button();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtVmCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtServerIp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSendPara = new System.Windows.Forms.Button();
            this.btWxTakeCode = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(521, 10);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(126, 54);
            this.btnConnect.TabIndex = 25;
            this.btnConnect.Text = "连接通信网关";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnMainBoardStatus
            // 
            this.btnMainBoardStatus.Location = new System.Drawing.Point(116, 67);
            this.btnMainBoardStatus.Name = "btnMainBoardStatus";
            this.btnMainBoardStatus.Size = new System.Drawing.Size(85, 32);
            this.btnMainBoardStatus.TabIndex = 22;
            this.btnMainBoardStatus.Text = "驱动板状态";
            this.btnMainBoardStatus.UseVisualStyleBackColor = true;
            this.btnMainBoardStatus.Click += new System.EventHandler(this.btnMainBoardStatus_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btWxTakeCode);
            this.groupBox1.Controls.Add(this.btnDeviceStatus);
            this.groupBox1.Controls.Add(this.btnSoftVer);
            this.groupBox1.Controls.Add(this.btnMainBoardStatus);
            this.groupBox1.Controls.Add(this.btnCashStatus);
            this.groupBox1.Controls.Add(this.btnDropStatus);
            this.groupBox1.Controls.Add(this.btnCoinStatus);
            this.groupBox1.Controls.Add(this.btnSendDoor);
            this.groupBox1.Controls.Add(this.btnSendCardKind);
            this.groupBox1.Controls.Add(this.btnPaStatus);
            this.groupBox1.Controls.Add(this.btnSendTemp);
            this.groupBox1.Location = new System.Drawing.Point(24, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(624, 114);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "机器状态汇报";
            // 
            // btnDeviceStatus
            // 
            this.btnDeviceStatus.Location = new System.Drawing.Point(323, 67);
            this.btnDeviceStatus.Name = "btnDeviceStatus";
            this.btnDeviceStatus.Size = new System.Drawing.Size(85, 32);
            this.btnDeviceStatus.TabIndex = 24;
            this.btnDeviceStatus.Text = "部件状态";
            this.btnDeviceStatus.UseVisualStyleBackColor = true;
            this.btnDeviceStatus.Click += new System.EventHandler(this.btnDeviceStatus_Click);
            // 
            // btnSoftVer
            // 
            this.btnSoftVer.Location = new System.Drawing.Point(213, 67);
            this.btnSoftVer.Name = "btnSoftVer";
            this.btnSoftVer.Size = new System.Drawing.Size(85, 32);
            this.btnSoftVer.TabIndex = 23;
            this.btnSoftVer.Text = "软件版本";
            this.btnSoftVer.UseVisualStyleBackColor = true;
            this.btnSoftVer.Click += new System.EventHandler(this.btnSoftVer_Click);
            // 
            // btnCashStatus
            // 
            this.btnCashStatus.Location = new System.Drawing.Point(432, 20);
            this.btnCashStatus.Name = "btnCashStatus";
            this.btnCashStatus.Size = new System.Drawing.Size(77, 32);
            this.btnCashStatus.TabIndex = 21;
            this.btnCashStatus.Text = "纸币器状态";
            this.btnCashStatus.UseVisualStyleBackColor = true;
            this.btnCashStatus.Click += new System.EventHandler(this.btnCashStatus_Click);
            // 
            // btnDropStatus
            // 
            this.btnDropStatus.Location = new System.Drawing.Point(24, 67);
            this.btnDropStatus.Name = "btnDropStatus";
            this.btnDropStatus.Size = new System.Drawing.Size(86, 32);
            this.btnDropStatus.TabIndex = 20;
            this.btnDropStatus.Text = "掉货检测状态";
            this.btnDropStatus.UseVisualStyleBackColor = true;
            this.btnDropStatus.Click += new System.EventHandler(this.btnDropStatus_Click);
            // 
            // btnCoinStatus
            // 
            this.btnCoinStatus.Location = new System.Drawing.Point(323, 20);
            this.btnCoinStatus.Name = "btnCoinStatus";
            this.btnCoinStatus.Size = new System.Drawing.Size(89, 32);
            this.btnCoinStatus.TabIndex = 19;
            this.btnCoinStatus.Text = "硬币器状态";
            this.btnCoinStatus.UseVisualStyleBackColor = true;
            this.btnCoinStatus.Click += new System.EventHandler(this.btnCoinStatus_Click);
            // 
            // btnSendDoor
            // 
            this.btnSendDoor.Location = new System.Drawing.Point(24, 20);
            this.btnSendDoor.Name = "btnSendDoor";
            this.btnSendDoor.Size = new System.Drawing.Size(72, 32);
            this.btnSendDoor.TabIndex = 10;
            this.btnSendDoor.Text = "门控数据";
            this.btnSendDoor.UseVisualStyleBackColor = true;
            this.btnSendDoor.Click += new System.EventHandler(this.btnSendDoor_Click);
            // 
            // btnSendCardKind
            // 
            this.btnSendCardKind.Location = new System.Drawing.Point(528, 20);
            this.btnSendCardKind.Name = "btnSendCardKind";
            this.btnSendCardKind.Size = new System.Drawing.Size(87, 32);
            this.btnSendCardKind.TabIndex = 11;
            this.btnSendCardKind.Text = "刷卡器状态";
            this.btnSendCardKind.UseVisualStyleBackColor = true;
            this.btnSendCardKind.Click += new System.EventHandler(this.btnSendCardKind_Click);
            // 
            // btnPaStatus
            // 
            this.btnPaStatus.Location = new System.Drawing.Point(213, 20);
            this.btnPaStatus.Name = "btnPaStatus";
            this.btnPaStatus.Size = new System.Drawing.Size(92, 32);
            this.btnPaStatus.TabIndex = 18;
            this.btnPaStatus.Text = "货道状态信息";
            this.btnPaStatus.UseVisualStyleBackColor = true;
            this.btnPaStatus.Click += new System.EventHandler(this.btnPaStatus_Click);
            // 
            // btnSendTemp
            // 
            this.btnSendTemp.Location = new System.Drawing.Point(116, 20);
            this.btnSendTemp.Name = "btnSendTemp";
            this.btnSendTemp.Size = new System.Drawing.Size(75, 32);
            this.btnSendTemp.TabIndex = 12;
            this.btnSendTemp.Text = "温度信息";
            this.btnSendTemp.UseVisualStyleBackColor = true;
            this.btnSendTemp.Click += new System.EventHandler(this.btnSendTemp_Click);
            // 
            // btnPosCard
            // 
            this.btnPosCard.Location = new System.Drawing.Point(209, 78);
            this.btnPosCard.Name = "btnPosCard";
            this.btnPosCard.Size = new System.Drawing.Size(65, 32);
            this.btnPosCard.TabIndex = 19;
            this.btnPosCard.Text = "POS刷卡";
            this.btnPosCard.UseVisualStyleBackColor = true;
            this.btnPosCard.Click += new System.EventHandler(this.btnPosCard_Click);
            // 
            // btnRecCash10
            // 
            this.btnRecCash10.Location = new System.Drawing.Point(159, 24);
            this.btnRecCash10.Name = "btnRecCash10";
            this.btnRecCash10.Size = new System.Drawing.Size(118, 32);
            this.btnRecCash10.TabIndex = 14;
            this.btnRecCash10.Text = "收纸币10元";
            this.btnRecCash10.UseVisualStyleBackColor = true;
            this.btnRecCash10.Click += new System.EventHandler(this.btnRecCash10_Click);
            // 
            // btnGetWaitNum
            // 
            this.btnGetWaitNum.Location = new System.Drawing.Point(347, 355);
            this.btnGetWaitNum.Name = "btnGetWaitNum";
            this.btnGetWaitNum.Size = new System.Drawing.Size(118, 32);
            this.btnGetWaitNum.TabIndex = 37;
            this.btnGetWaitNum.Text = "获取待发数据数量";
            this.btnGetWaitNum.UseVisualStyleBackColor = true;
            this.btnGetWaitNum.Click += new System.EventHandler(this.btnGetWaitNum_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(501, 355);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(118, 32);
            this.btnClose.TabIndex = 36;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnPosWh);
            this.groupBox2.Controls.Add(this.btnCreateNewBus);
            this.groupBox2.Controls.Add(this.btnInitAsileStatus);
            this.groupBox2.Controls.Add(this.btnInitAsileList);
            this.groupBox2.Controls.Add(this.btnPosCard);
            this.groupBox2.Controls.Add(this.btnRecCoin1);
            this.groupBox2.Controls.Add(this.btnRecCash10);
            this.groupBox2.Controls.Add(this.btnSellGoods);
            this.groupBox2.Controls.Add(this.btnChangeCoin1);
            this.groupBox2.Controls.Add(this.btnTun6);
            this.groupBox2.Location = new System.Drawing.Point(28, 216);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(619, 127);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "机器销售";
            // 
            // btnPosWh
            // 
            this.btnPosWh.Location = new System.Drawing.Point(521, 78);
            this.btnPosWh.Name = "btnPosWh";
            this.btnPosWh.Size = new System.Drawing.Size(90, 32);
            this.btnPosWh.TabIndex = 23;
            this.btnPosWh.Text = "武汉通刷卡";
            this.btnPosWh.UseVisualStyleBackColor = true;
            this.btnPosWh.Click += new System.EventHandler(this.btnPosWh_Click);
            // 
            // btnCreateNewBus
            // 
            this.btnCreateNewBus.Location = new System.Drawing.Point(18, 78);
            this.btnCreateNewBus.Name = "btnCreateNewBus";
            this.btnCreateNewBus.Size = new System.Drawing.Size(74, 32);
            this.btnCreateNewBus.TabIndex = 22;
            this.btnCreateNewBus.Text = "开始交易";
            this.btnCreateNewBus.UseVisualStyleBackColor = true;
            this.btnCreateNewBus.Click += new System.EventHandler(this.btnCreateNewBus_Click);
            // 
            // btnInitAsileStatus
            // 
            this.btnInitAsileStatus.Location = new System.Drawing.Point(413, 78);
            this.btnInitAsileStatus.Name = "btnInitAsileStatus";
            this.btnInitAsileStatus.Size = new System.Drawing.Size(92, 32);
            this.btnInitAsileStatus.TabIndex = 21;
            this.btnInitAsileStatus.Text = "汇报货道状态";
            this.btnInitAsileStatus.UseVisualStyleBackColor = true;
            this.btnInitAsileStatus.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnInitAsileList
            // 
            this.btnInitAsileList.Location = new System.Drawing.Point(290, 78);
            this.btnInitAsileList.Name = "btnInitAsileList";
            this.btnInitAsileList.Size = new System.Drawing.Size(99, 32);
            this.btnInitAsileList.TabIndex = 20;
            this.btnInitAsileList.Text = "汇报货道编号";
            this.btnInitAsileList.UseVisualStyleBackColor = true;
            this.btnInitAsileList.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnRecCoin1
            // 
            this.btnRecCoin1.Location = new System.Drawing.Point(18, 24);
            this.btnRecCoin1.Name = "btnRecCoin1";
            this.btnRecCoin1.Size = new System.Drawing.Size(118, 32);
            this.btnRecCoin1.TabIndex = 13;
            this.btnRecCoin1.Text = "收硬币1元";
            this.btnRecCoin1.UseVisualStyleBackColor = true;
            this.btnRecCoin1.Click += new System.EventHandler(this.btnRecCoin1_Click);
            // 
            // btnSellGoods
            // 
            this.btnSellGoods.Location = new System.Drawing.Point(112, 78);
            this.btnSellGoods.Name = "btnSellGoods";
            this.btnSellGoods.Size = new System.Drawing.Size(75, 32);
            this.btnSellGoods.TabIndex = 17;
            this.btnSellGoods.Text = "出货";
            this.btnSellGoods.UseVisualStyleBackColor = true;
            this.btnSellGoods.Click += new System.EventHandler(this.btnSellGoods_Click);
            // 
            // btnChangeCoin1
            // 
            this.btnChangeCoin1.Location = new System.Drawing.Point(303, 24);
            this.btnChangeCoin1.Name = "btnChangeCoin1";
            this.btnChangeCoin1.Size = new System.Drawing.Size(118, 32);
            this.btnChangeCoin1.TabIndex = 15;
            this.btnChangeCoin1.Text = "找零硬币1元";
            this.btnChangeCoin1.UseVisualStyleBackColor = true;
            this.btnChangeCoin1.Click += new System.EventHandler(this.btnChangeCoin1_Click);
            // 
            // btnTun6
            // 
            this.btnTun6.Location = new System.Drawing.Point(444, 24);
            this.btnTun6.Name = "btnTun6";
            this.btnTun6.Size = new System.Drawing.Size(118, 32);
            this.btnTun6.TabIndex = 16;
            this.btnTun6.Text = "吞币6元";
            this.btnTun6.UseVisualStyleBackColor = true;
            this.btnTun6.Click += new System.EventHandler(this.btnTun6_Click);
            // 
            // btnGetNetStatus
            // 
            this.btnGetNetStatus.Location = new System.Drawing.Point(211, 355);
            this.btnGetNetStatus.Name = "btnGetNetStatus";
            this.btnGetNetStatus.Size = new System.Drawing.Size(118, 32);
            this.btnGetNetStatus.TabIndex = 38;
            this.btnGetNetStatus.Text = "获取当前连接状态";
            this.btnGetNetStatus.UseVisualStyleBackColor = true;
            this.btnGetNetStatus.Click += new System.EventHandler(this.btnGetNetStatus_Click);
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(358, 47);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(132, 21);
            this.txtPwd.TabIndex = 33;
            this.txtPwd.Text = "000000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(272, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 32;
            this.label4.Text = "机器连接密码：";
            // 
            // txtVmCode
            // 
            this.txtVmCode.Location = new System.Drawing.Point(108, 51);
            this.txtVmCode.Name = "txtVmCode";
            this.txtVmCode.Size = new System.Drawing.Size(132, 21);
            this.txtVmCode.TabIndex = 31;
            this.txtVmCode.Text = "0000000006";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 30;
            this.label3.Text = "机器出厂编号：";
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(358, 12);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(132, 21);
            this.txtServerPort.TabIndex = 29;
            this.txtServerPort.Text = "5006";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(272, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 28;
            this.label2.Text = "通信网关端口：";
            // 
            // txtServerIp
            // 
            this.txtServerIp.Location = new System.Drawing.Point(108, 10);
            this.txtServerIp.Name = "txtServerIp";
            this.txtServerIp.Size = new System.Drawing.Size(132, 21);
            this.txtServerIp.TabIndex = 27;
            this.txtServerIp.Text = "gate.kivend.net";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "通信网关IP：";
            // 
            // btnSendPara
            // 
            this.btnSendPara.Location = new System.Drawing.Point(48, 349);
            this.btnSendPara.Name = "btnSendPara";
            this.btnSendPara.Size = new System.Drawing.Size(86, 32);
            this.btnSendPara.TabIndex = 39;
            this.btnSendPara.Text = "上传参数";
            this.btnSendPara.UseVisualStyleBackColor = true;
            this.btnSendPara.Click += new System.EventHandler(this.btnSendPara_Click);
            // 
            // btWxTakeCode
            // 
            this.btWxTakeCode.Location = new System.Drawing.Point(510, 67);
            this.btWxTakeCode.Name = "btWxTakeCode";
            this.btWxTakeCode.Size = new System.Drawing.Size(85, 32);
            this.btWxTakeCode.TabIndex = 25;
            this.btWxTakeCode.Text = "微信取货码";
            this.btWxTakeCode.UseVisualStyleBackColor = true;
            this.btWxTakeCode.Click += new System.EventHandler(this.btWxTakeCode_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 396);
            this.Controls.Add(this.btnSendPara);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnGetWaitNum);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnGetNetStatus);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtVmCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtServerPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtServerIp);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "金码网关通信通用组件测试（非DTU）";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnMainBoardStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCashStatus;
        private System.Windows.Forms.Button btnDropStatus;
        private System.Windows.Forms.Button btnCoinStatus;
        private System.Windows.Forms.Button btnSendDoor;
        private System.Windows.Forms.Button btnSendCardKind;
        private System.Windows.Forms.Button btnPaStatus;
        private System.Windows.Forms.Button btnSendTemp;
        private System.Windows.Forms.Button btnPosCard;
        private System.Windows.Forms.Button btnRecCash10;
        private System.Windows.Forms.Button btnGetWaitNum;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRecCoin1;
        private System.Windows.Forms.Button btnSellGoods;
        private System.Windows.Forms.Button btnChangeCoin1;
        private System.Windows.Forms.Button btnTun6;
        private System.Windows.Forms.Button btnGetNetStatus;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtVmCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtServerIp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnInitAsileList;
        private System.Windows.Forms.Button btnInitAsileStatus;
        private System.Windows.Forms.Button btnCreateNewBus;
        private System.Windows.Forms.Button btnSendPara;
        private System.Windows.Forms.Button btnSoftVer;
        private System.Windows.Forms.Button btnDeviceStatus;
        private System.Windows.Forms.Button btnPosWh;
        private System.Windows.Forms.Button btWxTakeCode;
    }
}

