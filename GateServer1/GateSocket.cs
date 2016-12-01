#region [ KIMMA Co.,Ltd. Copyright (C) 2012 ]
//-------------------------------------------------------------------------------------
// 开发单位：湖南金码智能设备制造有限公司
// 业务模块：网关通信通用组件（非DTU）
// 业务功能：网关Socket通信处理类（非DTU）
// 创建标识：2012-10-30		谷霖
// 修改标识：
//           1、2015-01-16 谷霖
//           增加支付宝付款码上传数据
//-------------------------------------------------------------------------------------
#endregion

using System;
using System.Text;
using System.IO;
using System.Data.SQLite;
using System.Data;
using System.Threading;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

using GateNetData;
using GateServer1.Common;

namespace GateServer1
{
    public class GateSocket
    {
        #region 修改时间声明
        //struct for date/time apis 
        public struct SystemTime
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int SetLocalTime(ref SystemTime lpSystemTime);

        private static void SetTime(string setTime)
        {
            SystemTime systNew = new SystemTime();
            try
            {
                // 设置属性 
                systNew.wYear = Convert.ToInt16("20" + setTime.Substring(0, 2));
                systNew.wMonth = Convert.ToInt16(setTime.Substring(2, 2));
                systNew.wDay = Convert.ToInt16(setTime.Substring(4, 2));

                systNew.wHour = Convert.ToInt16(setTime.Substring(6, 2));
                systNew.wMinute = Convert.ToInt16(setTime.Substring(8, 2));
                systNew.wSecond = Convert.ToInt16(setTime.Substring(10, 2));

                // 调用API，更新系统时间 
                SetLocalTime(ref systNew);
            }
            catch
            {
            }
        }
        #endregion

        #region 错误代码声明

        private const int SUC = 0;// 成功
        private const int ERR_NOINIT = 1;// 没有初始化类
        private const int ERR_DATAINVALID = 2;// 传入参数无效
        private const int ERR_NODBFILE = 3;// 数据文件不存在
        private const int ERR_DBFAIL = 4;// 操作数据失败
        private const int ERR_OTHER = 9;// 其它错误

        #endregion

        #region 公共属性

        private string m_Version = "1.0.0";
        /// <summary>
        /// 版本
        /// </summary>
        public string Version
        {
            get { return m_Version; }
        }

        private string m_SoftName = "GS-N-161110-01";
        /// <summary>
        /// 组件名称
        /// </summary>
        public string SoftName
        {
            get { return m_SoftName; }
        }

        private string m_ServerIp = "gate.kivend.net";
        /// <summary>
        /// 网关服务器IP地址或域名，初始值为gate.kivend.net
        /// </summary>
        public string ServerIp
        {
            get { return m_ServerIp; }
            set { m_ServerIp = value; }
        }

        private int m_ServerPort = 5006;
        /// <summary>
        /// 网关服务器端口，初始值为5006
        /// </summary>
        public int ServerPort
        {
            get { return m_ServerPort; }
            set { m_ServerPort = value; }
        }

        private string m_VmCode = string.Empty;
        /// <summary>
        /// 机器出厂编号（通信识别号）
        /// </summary>
        public string VmCode
        {
            get { return m_VmCode; }
            set { m_VmCode = value; }
        }

        private string m_VmPwd = string.Empty;
        /// <summary>
        /// 机器连接密码
        /// </summary>
        public string VmPwd
        {
            get { return m_VmPwd; }
            set { m_VmPwd = value; }
        }

        private bool m_SocketStatus = false;
        /// <summary>
        /// 网络连接当前状态 Fasle：离线 True：联机
        /// </summary>
        public bool SocketStatus
        {
            get { return m_SocketStatus; }
        }

        private int m_HeartBeatTime = 30;
        /// <summary>
        /// 心跳包时间间隔，以秒为单位，初始值为30秒
        /// </summary>
        public int HeartBeatTime
        {
            get { return m_HeartBeatTime; }
            set { m_HeartBeatTime = value; }
        }

        private int m_SendDataOutTime = 3;
        /// <summary>
        /// 发送数据超时时间，以秒为单位，初始值为3秒
        /// </summary>
        public int SendDataOutTime
        {
            get { return m_SendDataOutTime; }
            set { m_SendDataOutTime = value; }
        }

        private int m_RecDataOutTime = 5;
        /// <summary>
        /// 接收数据超时时间，以秒为单位，初始值为5秒
        /// </summary>
        public int RecDataOutTime
        {
            get { return m_RecDataOutTime; }
            set { m_RecDataOutTime = value; }
        }

        private int m_SendDataMaxNum = 10;
        /// <summary>
        /// 发送数据的最大允许发送次数，超过该次数的数据将不再发送，初始值为10
        /// </summary>
        public int SendDataMaxNum
        {
            get { return m_SendDataMaxNum; }
            set { m_SendDataMaxNum = value; }
        }

        private bool m_IsLogToFile = true;
        /// <summary>
        /// 是否记录通信日志到文件，False：不记录 True：记录
        /// </summary>
        public bool IsLogToFile
        {
            get { return m_IsLogToFile; }
            set { m_IsLogToFile = value; }
        }

        private bool m_IsLogToQueue = false;
        /// <summary>
        /// 是否记录通信日志到队列，False：不记录 True：记录
        /// </summary>
        public bool IsLogToQueue
        {
            get { return m_IsLogToQueue; }
            set { m_IsLogToQueue = value; }
        }

        private bool m_IsEnable = true;
        /// <summary>
        /// 是否使能通信，False：不通信 True：通信，初始值为True
        /// </summary>
        public bool IsEnable
        {
            get { return m_IsEnable; }
            set { m_IsEnable = value; }
        }

        private string m_ErrMsg = "";
        /// <summary>
        /// 系统错误信息
        /// </summary>
        public string ErrMsg
        {
            get { return m_ErrMsg; }
        }

        #endregion

        #region 变量

        /// <summary>
        /// 当前交易号
        /// </summary>
        private string m_BusId = string.Empty;

        private NetDataOper m_NetDataOper = new NetDataOper();

        /// <summary>
        /// 日志处理对象
        /// </summary>
        public LogHelper LogOper = new LogHelper();

        #endregion

        #region 公共接口函数

        #region 初始化及释放函数

        /// <summary>
        /// 初始化服务器通信
        /// </summary>
        /// <returns>初始化结果，false：失败 true：成功</returns>
        public int Initialize()
        {
            int intErrCode = SUC;

            m_IsInitialize = false;

            // 检测相关属性有效性
            if ((string.IsNullOrEmpty(m_VmCode)) ||
                (string.IsNullOrEmpty(m_VmPwd)) ||
                (string.IsNullOrEmpty(m_ServerIp)))
            {
                return ERR_DATAINVALID;
            }

            try
            {
                // 获取所在路径
                m_AppPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                m_AppPath = m_AppPath.Substring(0, m_AppPath.LastIndexOf('\\'));

                // 检测是否存在数据库文件夹名称
                if (!System.IO.Directory.Exists(m_AppPath + "\\db"))
                {
                    // 不存在，则创建
                    System.IO.Directory.CreateDirectory(m_AppPath + "\\db");
                }

                // 检测是否存在数据库文件
                if (!System.IO.File.Exists(m_AppPath + "\\" + m_DbFilePath))
                {
                    // 不存在
                    return ERR_NODBFILE;
                }

                // 初始化日志属性
                LogOper.LogName = "Log_GateWay";
                LogOper.IsWriteLogToFile = m_IsLogToFile;
                LogOper.IsWriteLogToQueue = m_IsLogToQueue;

                m_IsInitialize = true;

                //启动服务器通信控制线程
                m_TrdSendNetData = new Thread(new ThreadStart(SendNetMain));
                m_TrdSendNetData.IsBackground = true;
                m_TrdSendNetData.Start();
            }
            catch (Exception ex)
            {
                return ERR_OTHER;
            }
            return intErrCode;
        }

        /// <summary>
        /// 关闭服务器通信
        /// </summary>
        public void Displose()
        {
            m_Close = true;

            CloseGateServer();

            #region 检测工作线程是否已经全部结束

            int intTrdEndCount = 0;
            int intCheckOutNum = 0;

            while (true)
            {
                if (CheckTrdEnd(m_TrdSendNetData))
                {
                    intTrdEndCount++;
                }

                if (intTrdEndCount >= 1)
                {
                    // 工作线程全部结束
                    break;
                }

                Thread.Sleep(10);

                intCheckOutNum++;
                if (intCheckOutNum > 600)
                {
                    // 检测超时
                    break;
                }
            }

            #endregion
        }

        /// <summary>
        /// 检测服务器网络是否正常
        /// </summary>
        /// <returns>结果 True：正常 False：异常</returns>
        public bool CheckGateStatus()
        {
            //bool result = false;
            //string strCheckIp = strServerIp;

            //// 根据IP或域名解析通信服务器的真正IP地址
            //try
            //{
            //    if (string.IsNullOrEmpty(strCheckIp))
            //    {
            //        strCheckIp = "www.baidu.com";
            //    }
            //    IPAddress[] IPs = Dns.GetHostAddresses(strCheckIp);
            //    if (IPs.Length > 0)
            //    {
            //        result = blnSocketStatus;
            //    }
            //}
            //catch
            //{
            //    result = false;
            //}

            return m_SocketStatus;
        }

        /// <summary>
        /// 获取待发数据总数量
        /// </summary>
        /// <returns>待发数据总数量</returns>
        public int GetWaitDataNum()
        {
            int intDataCount = 0;

            if (CheckIsInitialize() != SUC)
            {
                return 0;
            }
            intDataCount = m_NetDataOper.GetNetDataCount();

            return intDataCount;
        }

        /// <summary>
        /// 清除待发数据
        /// </summary>
        /// <returns></returns>
        public int ClearNetData()
        {
            return m_NetDataOper.ClearNetData();
        }

        #endregion

        #region 业务公共函数接口

        #region 发送实时网络数据

        /// <summary>
        /// 发送实时网络数据
        /// </summary>
        /// <param name="cmdType">命令类型</param>
        /// <param name="operType">操作类型</param>
        /// <param name="netInfo">包体内容</param>
        /// <param name="recData">返回的包体数据</param>
        /// <returns>发送结果 True：成功 False：失败</returns>
        //public int SendData(string cmdType, string operType, string netInfo, out string recData)
        //{
        //    recData = "";

        //    int intErrCode = SUC;

        //    // 检测是否初始化了类
        //    if (CheckIsInitialize() != SUC)
        //    {
        //        return ERR_NOINIT;
        //    }

        //    #region 检测数据有效性


        //    #endregion

        //    beginSendRealData = true;

        //    // 检查当前是否正在发送数据
        //    while (sendOperIng)
        //    {
        //        Thread.Sleep(10);
        //    }

        //    byte[] SendData = null;
        //    bool blnResult = true;

        //    strRecData = "";

        //    // 判断和服务器的连接是否正常
        //    if (!blnSocketStatus)
        //    {
        //        // 和服务器的连接没有建立或者建立失败，重新连接 
        //        blnResult = SendConnectData();
        //    }

        //    if (blnResult)
        //    {
        //        // 封装数据包
        //        SendData = PackNetDataToSend(cmdType, operType, "",netInfo);

        //        // 发送数据
        //        blnResult = SendNetData(SendData);

        //        if (blnResult)
        //        {
        //            // 处理接收到的回复数据
        //            blnResult = AnaRecData(cmdType, operType);
        //        }
        //    }

        //    recData = strRecData;

        //    beginSendRealData = false;

        //    return intErrCode;
        //}

        #endregion

        #region 连接初始化后的相关数据汇报

        /// <summary>
        /// 连接初始化后汇报货道编号
        /// </summary>
        /// <param name="totalNum">货道总数量</param>
        /// <param name="paCodeInfo">货道编号组合，如：A1A2</param>
        /// <returns>结果 False：成功 True：失败</returns>
        public int InitAsileList(int totalNum,string paCodeInfo)
        {
            return m_NetDataOper.InitAsileList(totalNum, paCodeInfo);
        }

        /// <summary>
        /// 连接初始化后汇报货道状态
        /// </summary>
        /// <param name="totalNum">货道总数量</param>
        /// <param name="paStatusInfo">货道状态组合</param>
        /// <returns>结果 False：成功 True：失败</returns>
        public int InitAsileStatus(int totalNum, string paStatusInfo)
        {
            return m_NetDataOper.InitAsileStatus(totalNum, paStatusInfo);
        }

        /// <summary>
        /// 连接初始化后汇报货道价格
        /// </summary>
        /// <param name="totalNum">货道总数量</param>
        /// <param name="paStatusInfo">货道价格组合</param>
        /// <returns>结果 False：成功 True：失败</returns>
        public int InitPaPrice(int totalNum, string paPriceInfo)
        {
            return m_NetDataOper.InitPaPrice(totalNum, paPriceInfo);
        }

        /// <summary>
        /// 连接初始化后汇报部件状态
        /// </summary>
        /// <param name="deviceStatus">部件状态列表，各值之间以*隔开
        /// 门状态、硬币器状态、纸币器状态、掉货检测状态、读卡器状态、
        /// 驱动板0状态、温度传感器状态、驱动板0的温度、
        /// 驱动板1的状态、驱动板1的探头状态、驱动板1的温度
        /// 01*02*00*02*00*02*02*39
        /// </param>
        /// <returns></returns>
        public int InitDeviceStatus(string deviceStatus)
        {
            return m_NetDataOper.InitDeviceStatus(deviceStatus);
        }

        /// <summary>
        /// 初始化软件版本信息
        /// </summary>
        /// <param name="softVer">软件版本</param>
        /// <param name="phoneNum">手机号码</param>
        /// <returns></returns>
        public int InitSoftInfo(string softVer, string phoneNum)
        {
            return m_NetDataOper.InitSoftInfo(softVer, phoneNum);
        }

        #endregion 

        #region 机器运行及设备状态汇报

        /// <summary>
        /// 保存管理员操作日志信息
        /// </summary>
        /// <param name="userId">管理员帐号或卡号</param>
        /// <param name="operContent">操作内容</param>
        /// <returns>结果 False：成功 True：失败</returns>
        public int UpdateOperLog(string vmId,string userId, string operContent)
        {
            return UpdateOperLog(vmId,userId, operContent);
        }

        /// <summary>
        /// 保存终端参数信息
        /// </summary>
        /// <param name="paraCode">参数编号</param>
        /// <param name="newValue">参数新值</param>
        /// <param name="oldValue">参数旧值</param>
        /// <param name="operUser">设置修改人</param>
        /// <returns>结果 False：失败 True：成功</returns>
        public int UpdateParameter(string paraCode,string newValue,string oldValue,string operUser)
        {
            return m_NetDataOper.UpdateParameter(paraCode, newValue, oldValue, operUser);
        }

        /// <summary>
        /// 保存机器温度状态
        /// </summary>
        /// <param name="tempNo">温区编号</param>
        /// <param name="status">温度状态 00：正常 01：温度值超范围 02：线路不通或未接温度传感器</param>
        /// <param name="tempValue">温度值</param>
        /// <returns>结果 False：成功 True：失败</returns>
        public int UpdateTempStatus(string tempNo, string status, string tempValue)
        {
            return m_NetDataOper.UpdateTempStatus(tempNo, status, tempValue);
        }

        /// <summary>
        /// 保存门控状态
        /// </summary>
        /// <param name="doorStatus">门控状态 0：关闭 1：打开 2：故障</param>
        /// <returns>结果 False：成功 True：失败</returns>
        public int UpdateDoorStatus(string doorStatus)
        {
            return m_NetDataOper.UpdateDoorStatus(doorStatus);
        }

        /// <summary>
        /// 保存刷卡器状态信息
        /// </summary>
        /// <param name="status">刷卡器状态 1：故障 2：正常</param>
        /// <returns>结果 False：成功 True：失败</returns>
        public int UpdateCardStatus(string status)
        {
            return m_NetDataOper.UpdateCardStatus(status);
        }

        /// <summary>
        /// 保存纸币器状态信息
        /// </summary>
        /// <param name="status">纸币器状态</param>
        /// <returns>结果 True：成功 False：失败</returns>
        public int UpdateCashStatus(string status)
        {
            return m_NetDataOper.UpdateCashStatus(status);
        }

        /// <summary>
        /// 保存硬币器状态信息
        /// </summary>
        /// <param name="status">硬币器状态</param>
        /// <returns>结果 True：成功 False：失败</returns>
        public int UpdateCoinStatus(string status)
        {
            return m_NetDataOper.UpdateCoinStatus(status);
        }

        /// <summary>
        /// 保存驱动板状态信息
        /// </summary>
        /// <param name="status">驱动板状态</param>
        /// <returns>结果 True：成功 False：失败</returns>
        public int UpdateMainBoardStatus(string status)
        {
            return m_NetDataOper.UpdateMainBoardStatus(status);
        }

        /// <summary>
        /// 保存掉货检测状态信息
        /// </summary>
        /// <param name="status">掉货检测状态</param>
        /// <returns>结果 True：成功 False：失败</returns>
        public int UpdateDropCheckStatus(string status)
        {
            return m_NetDataOper.UpdateDropCheckStatus(status);
        }

        /// <summary>
        /// 保存升降系统状态信息
        /// </summary>
        /// <param name="status">升降系统状态</param>
        /// <returns>结果 True：成功 False：失败</returns>
        public int UpdateUpDownStatus(string status)
        {
            return m_NetDataOper.UpdateUpDownStatus(status);
        }

        /// <summary>
        /// 保存货道状态信息
        /// </summary>
        /// <param name="status">货道状态 0：正常 1：异常</param>
        /// <param name="paNum">货道号</param>
        /// <returns>结果 False：成功 True：失败</returns>
        public int UpdatePaStatus(string status, string paNum)
        {
            // 检测是否初始化了类
            if (CheckIsInitialize() != SUC)
            {
                return ERR_NOINIT;
            }

            return m_NetDataOper.UpdatePaStatus(status, paNum);
        }

        #endregion

        #region 货币汇报接口函数

        /// <summary>
        /// 货币汇报
        /// </summary>
        /// <param name="busId">交易号</param>
        /// <param name="moneyType">货币类型 0：硬币 1：纸币</param>
        /// <param name="operType">操作类型 0：收币 1：找零 2：吞币</param>
        /// <param name="amount">货币值</param>
        /// <param name="num">数量</param>
        /// <returns>结果 True：成功 False：失败</returns>
        public int OperMoney(string busId,string moneyType, string operType, int amount, int num)
        {
            // 检测是否初始化了类
            if (CheckIsInitialize() != SUC)
            {
                return ERR_NOINIT;
            }

            return m_NetDataOper.OperMoney(busId,moneyType, operType, amount, num);
        }

        #endregion

        #region 出货汇报接口函数

        /// <summary>
        /// 出货汇报
        /// </summary>
        /// <param name="busId">交易号</param>
        /// <param name="paNum">货道编码</param>
        /// <param name="sellPrice">销售价格</param>
        /// <param name="mcdCode">商品编码</param>
        /// <param name="num">销售数量</param>
        /// <returns>结果 True：成功 False：失败</returns>
        public int SellGoods(string busId,string paNum, int sellPrice, string mcdCode, int num,string dropResult)
        {
            // 交易号/货道/价格/编码/数量/时间
            // 14 EE 24 36 
            // 2A 
            // 38 36 30 交易号
            // 2A 
            // 41 35 货道
            // 2A 
            // 32 35 30 价格
            // 2A 
            // 30 30 30 30 30 30 30 30 商品编码
            // 2A 
            // 31 数量
            // 2A 
            // 0D 0B 20 0C 1E 时间
            // 0A 2F 0F 00 

            // 检测是否初始化了类
            if (CheckIsInitialize() != SUC)
            {
                return ERR_NOINIT;
            }
            LogOper.AddLogData("出货：" + busId + "  " + paNum + "  " + sellPrice + "  " + mcdCode + "  " + num.ToString() + "  " + dropResult);
            return m_NetDataOper.SellGoods(busId, paNum, sellPrice, mcdCode, num, dropResult);
        }

        #endregion

        #region 刷卡汇报接口函数

        /// <summary>
        /// POS刷卡
        /// </summary>
        /// <param name="busId">交易号</param>
        /// <param name="cardType">卡类型</param>
        /// <param name="cardNum">卡号</param>
        /// <param name="phyNo">物理卡号</param>
        /// <param name="posMoney">刷卡金额</param>
        /// <param name="banFee">扣款后余额</param>
        /// <param name="cardSerNo">刷卡流水号</param>
        /// <param name="cardResult">刷卡结果</param>
        /// <param name="errCode">结果代码</param>
        /// <returns>结果 True：成功 False：失败</returns>
        public int PosCard(string busId,string cardType, string cardNum,string phyNo,
            int posMoney, int banFee,
            string cardSerNo, string cardResult, string errCode)
        {
            // 流水号/卡别/卡号/金额/卡流水号/结果
            // 14 18 2D 49 
            // 2A 
            // 38 37 32 交易号
            // 2A 
            // 32 卡类型
            // 2A 
            // 31 30 30 30 30 30 30 30 36 31 卡号
            // 2A 
            // 32 35 30 2B 33 36 30 30 刷卡金额+扣款后余额
            // 2A 
            // 32 35 刷卡流水号
            // 2A 
            // 30 // 结果
            // 2A 
            // 0D 0B 20 0E 3B 时间
            // 0A 3F 0F 00 

            // 检测是否初始化了类
            if (CheckIsInitialize() != SUC)
            {
                return ERR_NOINIT;
            }

            return m_NetDataOper.PosCard(busId, cardType, cardNum, phyNo,
            posMoney, banFee,
            cardSerNo, cardResult, errCode);
        }

        /// <summary>
        /// 武汉通刷卡—成功
        /// </summary>
        /// <param name="busId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public int PosCard_WH_Suc(string busId, string data)
        {
            return m_NetDataOper.PosCard_WH_Suc(busId, data);
        }

        #endregion

        #region 支付宝接口函数 2015-01-16

        /// <summary>
        /// 条形码扫描在线支付结果
        /// </summary>
        /// <param name="busId">交易号</param>
        /// <param name="payType">支付类型</param>
        /// <param name="payNum">支付账号</param>
        /// <param name="payCode">条形码</param>
        /// <param name="money">扣款金额</param>
        /// <param name="payResult">支付结果 0：成功 其它：失败</param>
        /// <param name="asileCode">货道号</param>
        /// <param name="sellResult">出货结果 0：成功 其它：失败</param>
        /// <returns></returns>
        public int BarCode_Pay_Result(string busId, string payType, string payNum, string payCode,
            int money, string payResult, string asileCode, string sellResult)
        {
            // 检测是否初始化了类
            if (CheckIsInitialize() != SUC)
            {
                return ERR_NOINIT;
            }

            return m_NetDataOper.BarCode_Pay_Result(busId, payType, payNum, payCode, money, payResult, asileCode, sellResult);
        }

        /// <summary>
        /// 条形码扫描在线冲正
        /// </summary>
        /// <returns></returns>
        public int BarCode_Pay_Return(string busId, string payCode, int money)
        {
            // 检测是否初始化了类
            if (CheckIsInitialize() != SUC)
            {
                return ERR_NOINIT;
            }
            return m_NetDataOper.BarCode_Pay_Return(busId, payCode, money);
        }

        /// <summary>
        /// 微信取货码结果提交
        /// </summary>
        /// <param name="busId"></param>
        /// <param name="payCode"></param>
        /// <param name="money"></param>
        /// <param name="payType"></param>
        /// <returns></returns>
        public int WxTakeCode_Pay(string busId, string payCode, int money, string payType)
        {
            // 检测是否初始化了类
            if (CheckIsInitialize() != SUC)
            {
                return ERR_NOINIT;
            }
            return m_NetDataOper.WxTakeCode_Pay(busId, payCode, money,payType);
        }

        #endregion

        #region 机器维护接口函数

        /// <summary>
        /// 更新货道价格
        /// </summary>
        /// <param name="areaType">范围类型 1：单货道 2：单层 3：整机</param>
        /// <param name="code">设定对象 货道编号、层号、ZZ</param>
        /// <param name="value">设定值</param>
        /// <param name="userCode">管理员逻辑卡号</param>
        /// <returns></returns>
        public int SetAsilePrice(string areaType,string code,string value,string userCode)
        {
            return m_NetDataOper.SetAsileInfo("5", areaType, code, value, userCode);
        }

        /// <summary>
        /// 更新货道弹簧圈数
        /// </summary>
        /// <param name="areaType">范围类型 1：单货道 2：单层 3：整机</param>
        /// <param name="code">设定对象 货道编号、层号、ZZ</param>
        /// <param name="value">设定值</param>
        /// <param name="userCode">管理员逻辑卡号</param>
        /// <returns></returns>
        public int SetAsileSprintNum(string areaType, string code, string value, string userCode)
        {
            return m_NetDataOper.SetAsileInfo("6", areaType, code, value, userCode);
        }

        /// <summary>
        /// 更新货道库存
        /// </summary>
        /// <param name="areaType">范围类型 1：单货道 2：单层 3：整机</param>
        /// <param name="code">设定对象 货道编号、层号、ZZ</param>
        /// <param name="value">设定值</param>
        /// <param name="userCode">管理员逻辑卡号</param>
        /// <returns></returns>
        public int SetAsileStock(string areaType, string code, string value, string userCode)
        {
            return m_NetDataOper.SetAsileInfo("7", areaType, code, value, userCode);
        }

        /// <summary>
        /// 更新货道商品
        /// </summary>
        /// <param name="areaType">范围类型 1：单货道 2：单层 3：整机</param>
        /// <param name="code">设定对象 货道编号、层号、ZZ</param>
        /// <param name="value">设定值</param>
        /// <param name="userCode">管理员逻辑卡号</param>
        /// <returns></returns>
        public int SetAsileGoods(string areaType, string code, string value, string userCode)
        {
            return m_NetDataOper.SetAsileInfo("8", areaType, code, value, userCode);
        }

        #endregion

        #endregion

        #endregion

        #region 私有函数（网络通信处理操作）

        #region 变量

        /// <summary>
        /// 通信控制线程
        /// </summary>
        private Thread m_TrdSendNetData;

        /// <summary>
        /// 网络通信Socket连接对象
        /// </summary>
        private Socket m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        /// <summary>
        /// 构造用于接收的字节缓冲
        /// </summary>
        private Byte[] m_RecvBytes = new Byte[256];

        /// <summary>
        /// 当前是否需要发送实时数据，False：没有 True：需要
        /// </summary>
        private bool m_BeginSendRealData = false;

        /// <summary>
        /// 当前是否正在发送数据，False：没有发送 True：正在发送
        /// </summary>
        private bool m_SendOperIng = false;

        /// <summary>
        /// 返回包体数据
        /// </summary>
        private string m_RecData = "";

        /// <summary>
        /// 是否结束 False：未结束 True：结束
        /// </summary>
        private bool m_Close = false;

        /// <summary>
        /// 网络数据字节接收缓冲区
        /// </summary>
        private string m_RecBuff = "";

        /// <summary>
        /// 字符串序列
        /// </summary>
        private StringBuilder m_Sb = new StringBuilder();

        /// <summary>
        /// 所在路径
        /// </summary>
        private string m_AppPath = "";

        /// <summary>
        /// 网络数据存储库名称
        /// </summary>
        private string m_DbFilePath = "db\\NetDataInfo.db";

        /// <summary>
        /// 经过解析后的通信服务器的IP地址
        /// </summary>
        private string m_NetServerIp = "";

        /// <summary>
        /// 是否初始化了业务类
        /// </summary>
        private bool m_IsInitialize = false;

        #endregion

        #region 网络通信操作

        /// <summary>
        /// 检测是否初始化类
        /// </summary>
        /// <returns>结果 True：已初始化 False：没有初始化</returns>
        private int CheckIsInitialize()
        {
            int intErrCode = SUC;
            if (!m_IsInitialize)
            {
                intErrCode = ERR_NOINIT;
            }

            return intErrCode;
        }

        /// <summary>
        /// 根据IP或域名解析通信服务器的真正IP地址
        /// </summary>
        /// <returns>结果 False：失败 True：成功</returns>
        private bool AnalyzeServerIp()
        {
            bool result = false;

            try
            {
                IPAddress[] IPs = Dns.GetHostAddresses(m_ServerIp);
                if (IPs.Length > 0)
                {
                    foreach (IPAddress ipa in IPs)
                    {
                        if (ipa.AddressFamily == AddressFamily.InterNetwork)
                        {
                            m_NetServerIp = ipa.ToString();// 获取IP4地址
                            result = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 检测上次的错误信息是否与本次一样
                if (m_ErrMsg != ex.Message)
                {
                    // 不一样
                    m_ErrMsg = ex.Message;
                    LogOper.AddLogData("Ana Ip Err:" + m_ErrMsg + ",IP:" + m_ServerIp + ":" + m_NetServerIp);
                }
                else
                {
                    // 一样
                    LogOper.AddLogData("Ana Ip Err:" + "Same");
                }
                if (string.IsNullOrEmpty(m_NetServerIp))
                {
                    m_NetServerIp = m_ServerIp;
                }
                //strNetServerIp = strServerIp;
            }
            finally
            {
                // 检测IP地址有效性
                IPAddress ip;
                if (IPAddress.TryParse(m_NetServerIp, out ip))
                {
                    // IP地址合法
                    result = true;
                }
                else
                {
                    // IP地址不合法
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// 检测某工作线程是否已经结束
        /// </summary>
        /// <param name="thread">线程</param>
        /// <returns>结果 True：已经结束 False：没有结束</returns>
        private bool CheckTrdEnd(Thread thread)
        {
            bool result = false;

            if (thread != null)
            {
                if (!thread.IsAlive)
                {
                    result = true;
                }
            }
            else
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 连接通信服务器
        /// </summary>
        /// <returns>结果True：成功 False：失败</returns>
        private bool ConnectGateServer()
        {
            bool result = false;

            try
            {
                // 解析IP地址
                result = AnalyzeServerIp();

                if (!result)
                {
                    return false;
                }

                IPAddress serverIp = IPAddress.Parse(m_NetServerIp);

                IPEndPoint iep = new IPEndPoint(serverIp, m_ServerPort);

                if (m_Socket.Connected)
                {
                    LogOper.AddLogData("Close Socket");
                    m_Socket.Shutdown(SocketShutdown.Both);

                    //socket.Disconnect(true); 
                }

                m_Socket.Close();

                LogOper.AddLogData("Creat Socket...");

                m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                m_Socket.ReceiveTimeout = m_SendDataOutTime * 1000;

                LogOper.AddLogData("Creat Socket Suc");

                Thread.Sleep(200);

                // 连接通信网关
                m_Socket.Connect(iep);

                m_SocketStatus = true;

                LogOper.AddLogData("Connect Suc " + m_NetServerIp + ":" + m_ServerPort.ToString());

                return true;
            }
            catch (Exception ex)
            {
                // 检测上次的错误信息是否与本次一样
                if (m_ErrMsg != ex.Message)
                {
                    // 不一样
                    m_ErrMsg = ex.Message;
                    LogOper.AddLogData("Connect Err:" + m_ErrMsg + ",Ip:" + m_NetServerIp);
                }
                else
                {
                    // 一样
                    LogOper.AddLogData("Connect Err:" + "Same");
                }

                m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                m_Socket.ReceiveTimeout = m_SendDataOutTime * 1000;

                return false;
            }
        }

        /// <summary>
        /// 发送连接数据包
        /// </summary>
        /// <returns>连接结果</returns>
        private bool SendConnectData()
        {
            byte[] SendData = null;

            // 和服务器的连接没有建立或者建立失败，重新连接 
            Thread.Sleep(50);

            bool blnResult = ConnectGateServer();
            if (blnResult)
            {
                // 封装连接建立数据包
                //byte[] bytConnectData = Encoding.UTF8.GetBytes("*" + strVmCode + "*" + "000000" + "*" + strVmPwd);
                //string strConnectData = ByteArrayToHexString(bytConnectData, bytConnectData.Length);
                SendData = m_NetDataOper.PackConnectData(m_VmCode, m_VmPwd);// PackNetDataToSend("1F", "30", strConnectData);

                // 发送数据
                bool blnIsErrData = false;
                blnResult = SendNetData(SendData, out blnIsErrData);
                if (blnResult)
                {
                    // 处理接收到的回复数据
                    blnResult = AnaRecData("1F", "30");
                }
                else
                {
                    if (blnIsErrData)
                    {
                        blnResult = true;
                    }
                }
            }

            return blnResult;
        }

        /// <summary>
        /// 关闭和通信服务器的连接
        /// </summary>
        /// <returns>结果True：成功 False：失败</returns>
        private bool CloseGateServer()
        {
            try
            {
                if (m_Socket.Connected)
                {
                    LogOper.AddLogData("Close Connect");

                    m_Socket.Shutdown(SocketShutdown.Both);

                    m_Socket.Close();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private string m_ResetData_HistoryHour = string.Empty;

        /// <summary>
        /// 发送网络数据主线程
        /// </summary>
        private void SendNetMain()
        {
            string strSeqId = "";
            string strNetInfo = "";
            string strCmdType = "";
            string strOperType = "";
            string strId = "";
            bool blnResult = false;
            int intTotalNum = 0;
            int intDelayNum = 0;

            // 心跳包发送次数
            int intHeartNum = 0;
            int intHeartDealy = m_HeartBeatTime / 3;

            byte[] SendData = null;
            bool blnIsErrData = false;

            string strResetData_NowHour = string.Empty;

            while (!m_Close)
            {
                Thread.Sleep(50);

                try
                {
                    if (m_IsEnable)
                    {
                        #region 系统正常运行

                        // 间隔3秒
                        intDelayNum++;

                        if (intDelayNum > 60)
                        {
                            intDelayNum = 0;

                            if (!m_BeginSendRealData)
                            {
                                // 当前没有需要实时发送的数据

                                #region 发送连接、心跳及非实时数据

                                // 判断和服务器的连接是否正常
                                if (!m_SocketStatus)
                                {
                                    // 和服务器的连接没有建立或者建立失败，重新连接 
                                    if (!m_BeginSendRealData)
                                    {
                                        m_SendOperIng = true;
                                        blnResult = SendConnectData();
                                        m_SendOperIng = false;
                                    }
                                }
                                else
                                {
                                    // 获取要发送的网络数据
                                    try
                                    {
                                        if (!m_BeginSendRealData)
                                        {
                                            DataSet db = new DataSet();
                                            intTotalNum = 0;
                                            db = m_NetDataOper.QueryNetData();
                                            if (db.Tables.Count > 0)
                                            {
                                                #region 查询是否有需要发送的网络数据

                                                if (db.Tables[0].Rows.Count > 0)
                                                {
                                                    intTotalNum = db.Tables[0].Rows.Count;

                                                    for (int i = 0; i < db.Tables[0].Rows.Count; i++)
                                                    {
                                                        if (m_BeginSendRealData)
                                                        {
                                                            // 如果现在需要发送实时数据，退出
                                                            break;
                                                        }

                                                        m_SendOperIng = true;

                                                        Thread.Sleep(30);
                                                        if (m_Close)
                                                        {
                                                            return;
                                                        }
                                                        // 获取要发送的某条网络数据
                                                        strId = db.Tables[0].Rows[i]["id"].ToString();
                                                        strSeqId = db.Tables[0].Rows[i]["seqid"].ToString();
                                                        strNetInfo = db.Tables[0].Rows[i]["netinfo"].ToString();
                                                        strCmdType = db.Tables[0].Rows[i]["cmdtype"].ToString();
                                                        strOperType = db.Tables[0].Rows[i]["opertype"].ToString();

                                                        // 封装要发送的网络数据，并发送

                                                        // 封装数据
                                                        SendData = m_NetDataOper.PackNetDataToSend(strCmdType, strOperType, strSeqId,strNetInfo);

                                                        // 发送数据
                                                        blnResult = SendNetData(SendData, out blnIsErrData);
                                                        if (blnResult)
                                                        {
                                                            // 处理接收到的回复数据
                                                            blnResult = AnaRecData(strCmdType, strOperType);
                                                            if (blnResult)
                                                            {
                                                                // 发送成功，删除该数据记录
                                                                m_NetDataOper.DeleteNetData(Convert.ToInt32(strId));
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!blnIsErrData)
                                                            {
                                                                // 有一个发送失败，则退出本次发送
                                                                break;
                                                            }
                                                            else
                                                            {

                                                            }
                                                        }
                                                        ////if (!blnResult)
                                                        ////{
                                                        ////    // 如果发送失败，则发送次数进行更改
                                                        ////    m_NetDataOper.UpdateNetData(Convert.ToInt32(strId));
                                                        ////}

                                                        m_SendOperIng = false;
                                                    }
                                                }

                                                #endregion

                                                #region 每隔一个小时重新刷新下发送次数超过限制的数据

                                                ////strResetData_NowHour = DateTime.Now.ToString("HH");
                                                ////if (m_ResetData_HistoryHour != strResetData_NowHour)
                                                ////{
                                                ////    m_ResetData_HistoryHour = strResetData_NowHour;
                                                ////    m_NetDataOper.ResetNetData();
                                                ////}

                                                #endregion

                                                // 在没有要发送的数据的时候，发送心跳包保持连接
                                                if (intTotalNum == 0)
                                                {
                                                    intHeartNum++;
                                                    if (intHeartNum > intHeartDealy)
                                                    {
                                                        intHeartNum = 0;

                                                        m_SendOperIng = true;

                                                        // 发送心跳包
                                                        // 封装心跳包数据
                                                        SendData = m_NetDataOper.PackHeatData();// PackNetDataToSend("FF", "", "", "");

                                                        // 发送数据
                                                        SendNetData(SendData,out blnIsErrData);

                                                        m_SendOperIng = false;
                                                    }
                                                }
                                            }
                                        }

                                    }
                                    catch
                                    {
                                    }
                                }

                                #endregion
                            }
                        }

                        #endregion
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 发送及接收数据
        /// </summary>
        /// <param name="sendData">要发送的数据字节组</param>
        /// <returns></returns>
        private bool SendNetData(byte[] sendData,out bool isErrData)
        {
            m_RecBuff = "";
            m_Sb.Remove(0, m_Sb.Length);
            m_RecData = "";
            isErrData = false;

            if (sendData.Length == 0)
            {
                return false;
            }
            if (sendData.Length > 10)
            {
                if ((sendData[2] == 0x00) && (sendData[3] == 0x00))
                {
                    ////LogOper.AddLogData("S Error " + sendData.Length.ToString());
                    isErrData = true;
                    return false;
                }
            }

            try
            {
                LogOper.AddLogData("S:" + ByteArrayToHexString(sendData, sendData.Length));

                m_RecvBytes = new byte[256];

                m_Socket.Send(sendData, sendData.Length, SocketFlags.None);

                // 接收服务器的应答.
                int bytLen = m_Socket.Receive(m_RecvBytes, m_RecvBytes.Length, SocketFlags.None);

                TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);

                while (bytLen > 0)
                {
                    // 加入字符串缓存
                    m_Sb.Append(ByteArrayToHexString(m_RecvBytes, bytLen));
                    m_RecBuff = m_Sb.ToString();

                    // 再次接受，看看后面还有没有数据.
                    //bytLen = socket.Receive(recvBytes, recvBytes.Length, SocketFlags.None);

                    // 完整性判断
                    if (m_RecBuff.Contains("00"))
                    {
                        // 简单判断数据是否有效
                        if (m_RecBuff.Substring(0, 2) == "1D")
                        {
                            m_SocketStatus = true;
                        }
                        else
                        {
                            m_SocketStatus = false;
                        }
                        // 清除字符对列
                        m_Sb.Remove(0, m_Sb.Length);
                        break;
                    }

                    // 判断是否超时
                    TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
                    if (ts2.Subtract(ts1).TotalMilliseconds > m_RecDataOutTime * 1000)
                    {
                        m_RecBuff = "";
                        m_RecData = "";
                        m_SocketStatus = false;
                        LogOper.AddLogData("Out");
                        break;
                    }
                }

                if (sendData[0] != 0xFF)
                {
                    // 不写心跳包日志
                    LogOper.AddLogData("R:" + m_RecBuff);
                }
            }
            catch (Exception ex)
            {
                // 检测上次的错误信息是否与本次一样
                if (m_ErrMsg != ex.Message)
                {
                    // 不一样
                    m_ErrMsg = ex.Message;
                    LogOper.AddLogData("R Err:" + ex.Message + ",D:" + m_RecBuff);
                }
                else
                {
                    // 一样
                    LogOper.AddLogData("R Err:" + "Same" + ",D:" + m_RecBuff);
                }

                m_SocketStatus = false;
            }
            return m_SocketStatus;
        }

        /// <summary>
        /// 解析接收到的回复数据
        /// </summary>
        /// <param name="cmdType">命令类型</param>
        /// <param name="netInfo">操作类型</param>
        /// <returns></returns>
        private bool AnaRecData(string cmdType, string operType)
        {
            bool result = false;

            m_RecData = "";

            try
            {
                #region 检测数据有效性

                // 检测接收数据长度有效性
                if (m_RecvBytes.Length < 4)
                {
                    return false;
                }

                // 检测接收数据包的第一位和最后一位是否符合规定
                if ((m_RecvBytes[0] != 0x1D))
                {
                    return false;
                }

                #endregion

                #region 判断类型

                switch (cmdType)
                {
                    case "1F":// 连接应答包

                        // 获取服务器时间
                        #region

                        try
                        {
                            #region 获取时间
                            string strNewTime = "";
                            strNewTime = (Convert.ToInt32(m_RecvBytes[2]) - 48).ToString() +
                                (Convert.ToInt32(m_RecvBytes[3]) - 48).ToString() +
                                (Convert.ToInt32(m_RecvBytes[4]) - 48).ToString() +
                                (Convert.ToInt32(m_RecvBytes[5]) - 48).ToString() +
                                (Convert.ToInt32(m_RecvBytes[6]) - 48).ToString() +
                                (Convert.ToInt32(m_RecvBytes[7]) - 48).ToString() +
                                (Convert.ToInt32(m_RecvBytes[8]) - 48).ToString() +
                                (Convert.ToInt32(m_RecvBytes[9]) - 48).ToString() +
                                (Convert.ToInt32(m_RecvBytes[10]) - 48).ToString() +
                                (Convert.ToInt32(m_RecvBytes[11]) - 48).ToString() +
                                "0" +
                                "0";
                            #endregion

                            SetTime(strNewTime);
                        }
                        catch
                        {
                        }

                        #endregion

                        result = true;

                        break;

                    case "11":// 参数设置上报
                    case "12":// 机器状态上报
                    case "14":// 机器销售上报
                    case "17":// 支付宝上报
                    case "21":// 

                        // 获取结果代码
                        if (m_RecvBytes[2] == 0x30)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }

                        break;
                }

                #endregion
            }
            catch
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 把字节数组转为十六进制字符串
        /// </summary>
        /// <param name="data">byte(字节型) 字节数组</param>
        /// <param name="length">要转换的长度</param>
        /// <returns>string(字符型)，转换后的十六进制字符串</returns>
        private string ByteArrayToHexString(byte[] data, int length)
        {
            try
            {
                StringBuilder sb = new StringBuilder(length * 3);
                for (int i = 0; i < length; i++)
                {
                    sb.Append(Convert.ToString(data[i], 16).PadLeft(2, '0').PadRight(3, ' '));
                }
                return sb.ToString().ToUpper();
            }
            catch
            {
                return "";
            }
        }

        #endregion

        #endregion
    }
}
