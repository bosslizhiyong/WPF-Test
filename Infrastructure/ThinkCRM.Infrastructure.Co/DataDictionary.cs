using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkCRM.Infrastructure.Co
{
    public class DataDictionary
    {
        #region 状态
        /// <summary>
        /// 状态
        /// </summary>
        [Flags]
        public enum Status
        {
            /// <summary>
            /// 正常
            /// </summary>
            [Description("正常")]
            Normal = 0,
            /// <summary>
            /// 删除
            /// </summary>
            [Description("删除")]
            Delete = 1
        }
        #endregion

        #region 流水编号类别
        /// <summary>
        /// 流水编号类别
        /// </summary>
        [Flags]
        public enum SequenceType
        {
            /// <summary>
            /// 员工编号
            /// </summary>
            [Description("员工编号")]
            Employee = 1,
            /// <summary>
            /// 往来单位编号
            /// </summary>
            [Description("供应商编号")]
            Business = 2,
            /// <summary>
            /// 产品编号
            /// </summary>
            [Description("产品编号")]
            Product = 3,
            /// <summary>
            /// 仓库编号
            /// </summary>
            [Description("仓库编号")]
            Storage = 4,
            /// <summary>
            /// 销售对账单
            /// </summary>
            [Description("销售对账单")]
            SalesBillOrder = 5,
            /// <summary>
            /// 采购对账单
            /// </summary>
            [Description("采购对账单")]
            PurchaseBillOrder = 6,
            /// <summary>
            /// 推荐表
            /// </summary>
            [Description("推荐表")]
            Recommend = 7,
            /// <summary>
            /// 常规推送表
            /// </summary>
            [Description("常规推送表")]
            PlatformPushGeneral = 8,
            /// <summary>
            /// 新品推送表
            /// </summary>
            [Description("新品推送表")]
            PlatformPushNew = 9,
            /// <summary>
            /// 促销推送表
            /// </summary>
            [Description("促销推送表")]
            PlatformPromote = 10,
            /// <summary>
            /// 物料编号
            /// </summary>
            [Description("物料编号")]
            Materiel = 11
        }
        #endregion

        #region 应用程序(对应表:CM_Application)
        /// <summary>
        /// 应用程序,对应表:CM_Application
        /// </summary>
        [Flags]
        public enum ApplicationType
        {
            /// <summary>
            /// 企业门户
            /// </summary>
            [Description("企业系统")]
            Enterprise = 2,
            /// <summary>
            /// 往来单位
            /// </summary>
            [Description("往来单位")]
            Business = 3,
            /// <summary>
            /// API手机
            /// </summary>
            [Description("API手机")]
            API = 5
        }
        #endregion

        #region 数据库状态
        /// <summary>
        /// 数据库状态
        /// </summary>
        [Flags]
        public enum DatabaseStatus
        {
            /// <summary>
            /// 配置不存在
            /// </summary>
            [Description("配置不存在")]
            NoneConnectionString = -4,
            /// <summary>
            /// 连接失败
            /// </summary>
            [Description("连接失败")]
            Failure = -3,
            /// <summary>
            /// 未创建
            /// </summary>
            [Description("未创建")]
            NoneCreate = -2,
            /// <summary>
            /// 未初始化
            /// </summary>
            [Description("未初始化")]
            NoneInitialize = -1,
            /// <summary>
            /// 正常
            /// </summary>
            [Description("正常")]
            Normal = 0
        }
        #endregion

        #region 数据库类型
        /// <summary>
        /// 数据库类型
        /// </summary>
        [Flags]
        public enum DatabaseType
        {
            /// <summary>
            /// 主要
            /// </summary>
            [Description("主要")]
            Primary = 0,
            /// <summary>
            /// 次要
            /// </summary>
            [Description("次要")]
            Backup = 1
        }
        #endregion

        #region 参数编号
        /// <summary>
        /// 参数编号
        /// </summary>
        [Flags]
        public enum ParameterCodes
        {
            /// <summary>
            /// 数据库备份
            /// </summary>
            [Description("数据库备份")]
            P001,
            /// <summary>
            /// 邮箱参数
            /// </summary>
            [Description("邮箱参数")]
            P002,
            /// <summary>
            /// 客户端更新文件夹路径
            /// </summary>
            [Description("客户端更新文件夹路径")]
            P005,
            /// <summary>
            /// 上传文件夹路径
            /// </summary>
            [Description("上传文件夹路径")]
            P006,
            /// <summary>
            /// 日志文件夹路径
            /// </summary>
            [Description("日志文件夹路径")]
            P007,
            /// <summary>
            /// 是否开启权限控制
            /// </summary>
            [Description("是否开启权限控制")]
            P008,
            /// <summary>
            /// 默认密码
            /// </summary>
            [Description("默认密码")]
            P009,
            /// <summary>
            /// 客户产品监控-间隔天数
            /// </summary>
            [Description("客户产品监控-间隔天数")]
            P010,
            /// <summary>
            /// 结算时间
            /// </summary>
            [Description("结算时间")]
            P011,
            /// <summary>
            /// 是否开启锁屏控制
            /// </summary>
            [Description("是否开启锁屏控制")]
            P012,
            /// <summary>
            /// 智能化仓库-最大月份
            /// </summary>
            [Description("智能化仓库-最大月份")]
            P013,
            /// <summary>
            /// 版本号
            /// </summary>
            [Description("版本号")]
            P014,
            /// <summary>
            /// 系统开关
            /// </summary>
            [Description("系统开关")]
            P015,
            /// <summary>
            /// 建议采购量公式
            /// </summary>
            [Description("建议采购量公式")]
            P016,
            /// <summary>
            /// 智能分析(很长时间没拿)
            /// </summary>
            [Description("智能分析(很长时间没拿)")]
            P017,
            /// <summary>
            /// 智能分析(很长时间没到货)
            /// </summary>
            [Description("智能分析(很长时间没到货)")]
            P018,
            /// <summary>
            /// 智能化仓库-最小库存量
            /// </summary>
            [Description("智能化仓库-最小库存量")]
            P019,
            /// <summary>
            /// 客户产品监控-最大月份
            /// </summary>
            [Description("客户产品监控-最大月份")]
            P020,
            /// <summary>
            /// 智能分析(收款偏移天数)
            /// </summary>
            [Description("智能分析(收款偏移天数)")]
            P021,
            /// <summary>
            /// 智能分析(付款偏移天数)
            /// </summary>
            [Description("智能分析(付款偏移天数)")]
            P022,
            /// <summary>
            /// 数量金额-小数位数
            /// </summary>
            [Description("数量金额-小数位数")]
            P023,
            /// <summary>
            /// 价格计算
            /// </summary>
            [Description("价格计算")]
            P024,
            /// <summary>
            ///滞销品分析-天数
            /// </summary>
            [Description("滞销品分析-天数")]
            P025,
            /// <summary>
            /// 滞销品分析-销售比例
            /// </summary>
            [Description("滞销品分析-销售比例")]
            P026,
            /// <summary>
            /// 分页范围
            /// </summary>
            [Description("分页范围")]
            P027,
            /// <summary>
            /// 订单收发货日期必填
            /// </summary>
            [Description("订单收发货日期必填")]
            P028,
            /// <summary>
            /// 出入库、退货单据收付退款日期必填
            /// </summary>
            [Description("出入库、退货单据收付退款日期必填")]
            P029,
            /// <summary>
            /// 税率
            /// </summary>
            [Description("税率")]
            P030,
            /// <summary>
            /// 智能化仓库-分仓库计算平均值
            /// </summary>
            [Description("智能化仓库-分仓库计算平均值")]
            P031,
            /// <summary>
            /// 关闭服务器是否关闭客户端
            /// </summary>
            [Description("关闭服务器是否关闭客户端")]
            P032,
            /// <summary>
            /// 客户最近价与设置的默认售价不一致提醒
            /// </summary>
            [Description("客户最近价与设置的默认售价不一致提醒")]
            P033,
            /// <summary>
            /// 服务端开关状态
            /// </summary>
            [Description("服务端开关状态")]
            P034,
            /// <summary>
            /// 开启账套
            /// </summary>
            [Description("开启账套")]
            P035,
            /// <summary>
            /// 特价产品允许使用促销价和修改折扣率
            /// 允许特价产品在销售订单、出库单、退货单中修改折扣率和使用促销价
            /// </summary>
            [Description("特价产品允许使用促销价和修改折扣率")]
            P036,
            /// <summary>
            /// 促销价产品允许修改折扣率
            /// 允许促销价产品在销售订单、出库单、退货单中的产品使用了促销价并可修改折扣率
            /// </summary>
            [Description("促销价产品允许修改折扣率")]
            P037,
            /// <summary>
            /// 系统默认页面
            /// </summary>
            [Description("系统默认页面")]
            P038,
            /// <summary>
            /// POS客户默认结算方式
            /// </summary>
            [Description("POS客户默认结算方式")]
            P039,
            /// <summary>
            /// POS销售打印方式
            /// </summary>
            [Description("POS销售打印方式")]
            P040,
            /// <summary>
            /// POS销售单保存后
            /// </summary>
            [Description("POS销售单保存后")]
            P041,
            /// <summary>
            /// POS销售单默认价
            /// </summary>
            [Description("POS销售单默认价")]
            P042,
            /// <summary>
            /// 数量输入默认使用
            /// </summary>
            [Description("数量输入默认使用")]
            P043,
            /// <summary>
            /// 销售版本
            /// </summary>
            [Description("销售版本")]
            P044
        }
        #endregion

        #region 线程编号
        /// <summary>
        /// 线程编号
        /// </summary>
        [Flags]
        public enum ThreadCodes
        {
            /// <summary>
            /// 备份数据库
            /// </summary>
            [Description("备份数据库")]
            Backup,
            /// <summary>
            /// 发送消息
            /// </summary>
            [Description("发送消息")]
            Message,
            /// <summary>
            /// 发送邮件
            /// </summary>
            [Description("发送邮件")]
            Email,
            /// <summary>
            /// 智能化仓库
            /// </summary>
            [Description("智能化仓库")]
            Warehouse,
            /// <summary>
            /// 智能分析(客户产品监控)
            /// </summary>
            [Description("智能分析(客户产品监控)")]
            Sales_BusPro_Monitor,
            /// <summary>
            /// 智能分析(很长时间没到货)
            /// </summary>
            [Description("智能分析(很长时间没到货)")]
            Purchase_NotArrive,
            /// <summary>
            /// 智能分析(很长时间没拿)
            /// </summary>
            [Description("智能分析(很长时间没拿)")]
            Sales_NotTake,
            /// <summary>
            /// 智能分析(未拿的推荐品)
            /// </summary>
            [Description("智能分析(未拿的推荐品)")]
            Sales_NotTakeRec,
            /// <summary>
            /// 智能分析(应到未到款)
            /// </summary>
            [Description("智能分析(应到未到款)")]
            Receivable_NotReceive,
            /// <summary>
            /// 智能分析(应付未付款)
            /// </summary>
            [Description("智能分析(应付未付款)")]
            Payable_NotPay,
            /// <summary>
            /// 客户拿货报表(往年数据)
            /// </summary>
            [Description("客户拿货报表(往年数据)")]
            Report_SalesTake,
            /// <summary>
            /// 体检数据分析
            /// </summary>
            [Description("体检数据分析")]
            Examination,
            /// <summary>
            /// 同步企业信息
            /// </summary>
            [Description("同步企业信息")]
            Enterprise,
            /// <summary>
            /// 智能分析(很长时间没发货)
            /// </summary>
            [Description("智能分析(很长时间没发货)")]
            Sales_NotThair,
            /// <summary>
            /// 分享查看分析
            /// </summary>
            [Description("分享查看分析")]
            ShareWatchLog,
            /// <summary>
            /// 合同到期
            /// </summary>
            [Description("合同到期")]
            Agreement,
            /// <summary>
            /// 采购未审核
            /// </summary>
            [Description("采购未审核")]
            PurchaseAudit,
            /// <summary>
            /// 销售未审核
            /// </summary>
            [Description("销售未审核")]
            SalesAudit,
            /// <summary>
            /// 通知公告
            /// </summary>
            [Description("通知公告")]
            InformNotice
        }
        #endregion

        #region 行政区域类别
        /// <summary>
        /// 区域类别
        /// </summary>
        [Flags]
        public enum RegionType
        {
            /// <summary>
            /// 大区
            /// </summary>
            [Description("大区")]
            Region = 1,
            /// <summary>
            ///省
            /// </summary>
            [Description("省")]
            Province = 2,
            /// <summary>
            ///市
            /// </summary>
            [Description("市")]
            City = 3,
            /// <summary>
            ///区县
            /// </summary>
            [Description("区县")]
            County = 4
        }
        #endregion

        #region 用户状态
        /// <summary>
        /// 用户状态
        /// </summary>
        [Flags]
        public enum UserStatus
        {
            /// <summary>
            /// 注册
            /// </summary>
            [Description("注册")]
            Register = 0,
            /// <summary>
            /// 激活
            /// </summary>
            [Description("激活")]
            Active = 1,
            /// <summary>
            /// 冻结
            /// </summary>
            [Description("冻结")]
            Freeze = 2
        }
        #endregion

        #region 性别
        /// <summary>
        /// 性别
        /// </summary>
        [Flags]
        public enum Sex
        {
            /// <summary>
            /// 男
            /// </summary>
            [Description("男")]
            Male = 1,
            /// <summary>
            /// 女
            /// </summary>
            [Description("女")]
            Female = 2
        }
        #endregion

        #region 在职状态
        /// <summary>
        /// 在职状态
        /// </summary>
        [Flags]
        public enum InStatus
        {
            /// <summary>
            /// 正式
            /// </summary>
            [Description("正式")]
            OnPost = 0,
            /// <summary>
            /// 试用
            /// </summary>
            [Description("试用")]
            OnTrial = 1,
            /// <summary>
            /// 实习
            /// </summary>
            [Description("实习")]
            OnTrain = 2,
            /// <summary>
            /// 离职
            /// </summary>
            [Description("离职")]
            OnLeave = 3
        }
        #endregion

        #region 单位性质
        /// <summary>
        /// 单位性质
        /// </summary>
        [Flags]
        public enum BusinessProperty
        {
            /// <summary>
            /// 供应商
            /// </summary>
            [Description("供应商")]
            Supplier = 1,
            /// <summary>
            /// 客户
            /// </summary>
            [Description("客户")]
            Customer = 2,
            /// <summary>
            /// 物流
            /// </summary>
            [Description("物流")]
            Logistics = 3,
            /// <summary>
            /// 加工商
            /// </summary>
            [Description("加工商")]
            Converter = 4
        }
        #endregion

        #region 合同状态
        /// <summary>
        /// 合同状态
        /// </summary>
        [Flags]
        public enum AgreementStatus
        {
            /// <summary>
            /// 谈判中
            /// </summary>
            [Description("谈判中")]
            Negotiations = 1,
            /// <summary>
            /// 等待签合同
            /// </summary>
            [Description("等待签合同")]
            Wait = 2,
            /// <summary>
            /// 已签合同
            /// </summary>
            [Description("已签合同")]
            Checked = 3,
            /// <summary>
            /// 合同到期
            /// </summary>
            [Description("合同到期")]
            Due = 4
        }
        #endregion

        #region 经营档次
        /// <summary>
        /// 经营档次
        /// </summary>
        [Flags]
        public enum Grades
        {
            /// <summary>
            /// 低端
            /// </summary>
            [Description("低端")]
            Low = 1,
            /// <summary>
            /// 中端
            /// </summary>
            [Description("中端")]
            Middle = 2,
            /// <summary>
            /// 高端
            /// </summary>
            [Description("高端")]
            High = 3
        }
        #endregion

        #region 结算方式
        /// <summary>
        /// 结算方式
        /// </summary>
        [Flags]
        public enum AccountType
        {
            /// <summary>
            /// 月结
            /// </summary>
            [Description("月结")]
            Month = 1,
            /// <summary>
            /// 出货后
            /// </summary>
            [Description("出货后")]
            Hold = 2,
            /// <summary>
            /// 代收款
            /// </summary>
            [Description("代收款")]
            Collection = 3,
            /// <summary>
            /// 定期
            /// </summary>
            [Description("定期")]
            Fix = 4,
            /// <summary>
            /// 现金
            /// </summary>
            [Description("现金")]
            Cash = 5,
            /// <summary>
            /// 汇款
            /// </summary>
            [Description("汇款")]
            Transfer = 6,
            /// <summary>
            /// 自提
            /// </summary>
            [Description("自提")]
            Pick = 7,
            /// <summary>
            /// 压批
            /// </summary>
            [Description("压批")]
            Press = 8,
            /// <summary>
            /// 出货前
            /// </summary>
            [Description("出货前")]
            PreHold = 9
        }
        #endregion

        #region 收付款账户类型
        /// <summary>
        /// 收付款账户类型
        /// </summary>
        [Flags]
        public enum BankAccountType
        {
            /// <summary>
            /// 现金账户
            /// </summary>
            [Description("现金账户")]
            Cash = 1,
            /// <summary>
            /// 银行账户
            /// </summary>
            [Description("银行账户")]
            Bank = 2,
            /// <summary>
            /// 虚拟账户
            /// </summary>
            [Description("虚拟账户")]
            Virtual = 3

        }
        #endregion

        #region 产品单位类别
        /// <summary>
        /// 产品单位类别
        /// </summary>
        [Flags]
        public enum UnitType
        {
            /// <summary>
            /// 基本单位
            /// </summary>
            [Description("基本单位")]
            BaseUnit = 1,
            /// <summary>
            /// 辅助单位
            /// </summary>
            [Description("辅助单位")]
            AssistantUint = 2
        }
        #endregion

        #region 产品类型
        /// <summary>
        /// 产品类型
        /// </summary>
        [Flags]
        public enum ProductType
        {
            /// <summary>
            /// 常规品
            /// </summary>
            [Description("常规品")]
            General = 0,
            /// <summary>
            /// 新品
            /// </summary>
            [Description("新品")]
            New = 1,
            /// <summary>
            /// 淘汰品
            /// </summary>
            [Description("淘汰品")]
            DieOut = 2
        }
        #endregion

        #region 产品属性
        /// <summary>
        /// 产品属性
        /// </summary>
        [Flags]
        public enum ProductProperty
        {
            /// <summary>
            /// 产品
            /// </summary>
            [Description("产品")]
            Product = 5,
            /// <summary>
            /// 成品
            /// </summary>
            [Description("成品")]
            Finished = 1,
            /// <summary>
            /// 半成品    
            /// </summary>
            [Description("半成品")]
            SemiFinished = 2,
            /// <summary>
            /// 原料
            /// </summary>
            [Description("原料")]
            Material = 3,
            /// <summary>
            /// 辅料
            /// </summary>
            [Description("辅料")]
            Accessories = 4
        }
        #endregion

        #region 产品来源
        /// <summary>
        /// 产品来源
        /// </summary>
        [Flags]
        public enum ProductSource
        {
            /// <summary>
            /// 生产
            /// </summary>
            [Description("生产")]
            Self = 1,
            /// <summary>
            /// 采购
            /// </summary>
            [Description("采购")]
            Buy = 2,
            ///// <summary>
            ///// 外协
            ///// </summary>
            //[Description("外协")]
            //Out = 3
        }
        #endregion

        #region 价格体系类型
        /// <summary>
        /// 价格体系类型
        /// </summary>
        [Flags]
        public enum SeriesType
        {
            /// <summary>
            /// 销售价格体系
            /// </summary>
            [Description("销售价格体系")]
            Sales = 1,
            /// <summary>
            /// 采购价格体系
            /// </summary>
            [Description("采购价格体系")]
            Purchase = 2,
            /// <summary>
            /// 生产采购价格体系
            /// </summary>
            [Description("生产采购价格体系")]
            ProPurchase = 3
        }
        #endregion

        #region 订单类型
        /// <summary>
        /// 订单类型
        /// </summary>
        [Flags]
        public enum OrderType
        {
            /// <summary>
            /// 采购订单
            /// </summary>
            [Description("采购订单")]
            PurchaseOrder = 1,
            /// <summary>
            /// 采购入库单
            /// </summary>
            [Description("采购入库单")]
            PurchaseInOrder = 2,
            /// <summary>
            /// 采购退货单
            /// </summary>
            [Description("采购退货单")]
            PurchaseReturnOrder = 3,
            /// <summary>
            /// 付款结算单
            /// </summary>
            [Description("付款结算单")]
            PayCheckOrder = 4,
            /// <summary>
            /// 销售订单
            /// </summary>
            [Description("销售订单")]
            SalesOrder = 5,
            /// <summary>
            /// 销售出库单
            /// </summary>
            [Description("销售出库单")]
            SalesOutOrder = 6,
            /// <summary>
            /// 销售退货单
            /// </summary>
            [Description("销售退货单")]
            SalesReturnOrder = 7,
            /// <summary>
            /// 收款结算单
            /// </summary>
            [Description("收款结算单")]
            ReceiveCheckOrder = 8,
            /// <summary>
            /// 一般费用单
            /// </summary>
            [Description("一般费用单")]
            GeneralOrder = 9,
            /// <summary>
            /// 其他收入单
            /// </summary>
            [Description("其他收入单")]
            OtherOrder = 10,
            /// <summary>
            /// 盘点生成单
            /// </summary>
            [Description("盘点生成单")]
            CreateInvOrder = 11,
            /// <summary>
            /// 代付结算单
            /// </summary>
            [Description("代付结算单")]
            DFPayCheckOrder = 12,
            /// <summary>
            /// 运费结算单
            /// </summary>
            [Description("运费结算单")]
            YFPayCheckOrder = 13,
            /// <summary>
            /// 代收结算单
            /// </summary>
            [Description("代收结算单")]
            DSReceiveCheckOrder = 14,
            /// <summary>
            /// 调拨单
            /// </summary>
            [Description("调拨单")]
            AllocationOrder = 15,
            /// <summary>
            /// 采购换货单
            /// </summary>
            [Description("采购换货单")]
            PurchaseExchangeOrder = 16,
            /// <summary>
            /// 销售换货单
            /// </summary>
            [Description("销售换货单")]
            SalesExchangeOrder = 17,
            /// <summary>
            /// 生产领料出库单
            /// </summary>
            [Description("生产领料出库单")]
            ProduceOutOrder = 18,
            /// <summary>
            /// 生产退料入库单
            /// </summary>
            [Description("生产退料入库单")]
            ProduceReturnOrder = 19,
            /// <summary>
            /// 生产成品入库单
            /// </summary>
            [Description("生产成品入库单")]
            ProduceInOrder = 20,
            /// <summary>
            /// 报溢单
            /// </summary>
            [Description("报溢单")]
            OverOrder = 21,
            /// <summary>
            /// 报损单
            /// </summary>
            [Description("报损单")]
            LossOrder = 22,
            /// <summary>
            /// 赠送单
            /// </summary>
            [Description("赠送单")]
            FreeOrder = 23,
            /// <summary>
            /// 盘点录入单
            /// </summary>
            [Description("盘点录入单")]
            InventoryOrder = 24,
            /// <summary>
            /// 采购申请单表
            /// </summary>
            [Description("采购申请单表")]
            ApplyOrder = 25,
            /// <summary>
            /// 生产采购订单
            /// </summary>
            [Description("生产采购订单")]
            ProPurchaseOrder = 26,
            /// <summary>
            /// 生产采购入库单
            /// </summary>
            [Description("生产采购入库单")]
            ProPurchaseInOrder = 27,
            /// <summary>
            /// 生产采购退货单
            /// </summary>
            [Description("生产采购退货单")]
            ProPurchaseReturnOrder = 28,
            /// <summary>
            /// 生产BOM订单
            /// </summary>
            [Description("生产BOM单")]
            BomOrder = 29,
            /// <summary>
            /// 外协订单
            /// </summary>
            [Description("外协订单")]
            ForeignOrder = 30,
            /// <summary>
            /// 外协出库单
            /// </summary>
            [Description("外协出库单")]
            ForeignOutOrder = 31,
            /// <summary>
            /// 外协入库单
            /// </summary>
            [Description("外协入库单")]
            ForeignInOrder = 32,
            /// <summary>
            /// 生产任务单
            /// </summary>
            [Description("生产任务单")]
            TaskOrder = 33,
            /// <summary>
            /// 计划BOM单
            /// </summary>
            [Description("计划BOM单")]
            PlanBomOrder = 34,
            /// <summary>
            /// 计划明细单
            /// </summary>
            [Description("计划明细单")]
            PlanLine = 35,
            /// <summary>
            /// 生产工单
            /// </summary>
            [Description("生产工单")]
            WorkOrder = 36,
            /// <summary>
            /// 生产领料单
            /// </summary>
            [Description("生产领料单")]
            WorkRequireOrder = 37,
            /// <summary>
            /// 生产报工单
            /// </summary>
            [Description("生产报工单")]
            WorkSheetOrder = 38,
            /// <summary>
            /// 验收入库单
            /// </summary>
            [Description("验收入库单")]
            WorkInOrder = 39
        }
        #endregion  

        #region 汇总
        /// <summary>
        /// 汇总
        /// </summary>
        [Flags]
        public enum PriceTrend
        {
            /// <summary>
            /// 产品采购价格走势图(均价)
            /// </summary>
            [Description("产品采购价格走势图(均价)")]
            PurchasePriceTrend = 1,
            /// <summary>
            /// 产品销售价格走势图(均价)
            /// </summary>
            [Description("产品销售价格走势图(均价)")]
            SalesPriceTrend = 2,
            /// <summary>
            /// 跟单人—客户销售汇总明细表
            /// </summary>
            [Description("跟单人—客户销售汇总明细表")]
            EmployeeCustomerSalesLine = 3,
            /// <summary>
            /// 品牌—客户销售汇总表
            /// </summary>
            [Description("品牌—客户销售汇总明细表")]
            BrandNameCustomerSalesLine = 4,
            /// <summary>
            /// 品牌—客户销售汇总表
            /// </summary>
            [Description("品牌—客户销售汇总表")]
            BrandNameCustomerSales = 5,
            /// <summary>
            /// 品牌—客户销售汇总表
            /// </summary>
            [Description("跟单人—客户销售汇总表")]
            EmpBusSalesSummary = 6
        }
        #endregion

        #region 出库单类型
        /// <summary>
        /// 出库单类型
        /// </summary>
        [Flags]
        public enum OutOrderType
        {
            /// <summary>
            /// 普通
            /// </summary>
            [Description("普通")]
            Ordinary = 0,
            /// <summary>
            /// POS开单
            /// </summary>
            [Description("POS单")]
            PosOpenOrder = 1
        }
        #endregion

        #region 收款结算单类型
        /// <summary>
        /// 收款结算单类型
        /// </summary>
        [Flags]
        public enum ReceiveCheckOrderType
        {
            /// <summary>
            /// 普通
            /// </summary>
            [Description("普通")]
            Ordinary = 0,
            /// <summary>
            /// POS开单
            /// </summary>
            [Description("POS单")]
            PosOpenOrder = 1
        }
        #endregion

        #region 订单流水号
        /// <summary>
        /// 订单流水号
        /// </summary>
        [Flags]
        public enum OrderCodeType
        {
            /// <summary>
            /// 采购订单
            /// </summary>
            [Description("采购订单")]
            PurchaseOrder = 1,
            /// <summary>
            /// 采购入库单
            /// </summary>
            [Description("采购入库单")]
            PurchaseInOrder = 2,
            /// <summary>
            /// 采购退货单
            /// </summary>
            [Description("采购退货单")]
            PurchaseReturnOrder = 3,
            /// <summary>
            /// 付款结销单
            /// </summary>
            [Description("付款结销单")]
            PayCheckOrder = 4,
            /// <summary>
            /// 销售订单
            /// </summary>
            [Description("销售订单")]
            SalesOrder = 5,
            /// <summary>
            /// 销售出库单
            /// </summary>
            [Description("销售出库单")]
            SalesOutOrder = 6,
            /// <summary>
            /// 销售退货单
            /// </summary>
            [Description("销售退货单")]
            SalesReturnOrder = 7,
            /// <summary>
            /// 收款结算单
            /// </summary>
            [Description("收款结算单")]
            ReceiveCheckOrder = 8,
            /// <summary>
            /// 一般费用单
            /// </summary>
            [Description("一般费用单")]
            GeneralOrder = 9,
            /// <summary>
            /// 其他收入单
            /// </summary>
            [Description("其他收入单")]
            OtherOrder = 10,
            /// <summary>
            /// 盘点生成单
            /// </summary>
            [Description("盘点生成单")]
            CreateInvOrder = 11,
            /// <summary>
            /// 代付结销单
            /// </summary>
            [Description("代付结销单")]
            DFPayCheckOrder = 12,
            /// <summary>
            /// 运费结销单
            /// </summary>
            [Description("运费结销单")]
            YFPayCheckOrder = 13,
            /// <summary>
            /// 代收结算单
            /// </summary>
            [Description("代收结算单")]
            DSReceiveCheckOrder = 14,
            /// <summary>
            /// 调拨单
            /// </summary>
            [Description("调拨单")]
            AllocationOrder = 15,
            /// <summary>
            /// 采购换货单
            /// </summary>
            [Description("采购换货单")]
            PurchaseExchangeOrder = 16,
            /// <summary>
            /// 销售换货单
            /// </summary>
            [Description("销售换货单")]
            SalesExchangeOrder = 17,
            /// <summary>
            /// 生产领料出库单
            /// </summary>
            [Description("生产领料出库单")]
            ProduceOutOrder = 18,
            /// <summary>
            /// 生产退料入库单
            /// </summary>
            [Description("生产退料入库单")]
            ProduceReturnOrder = 19,
            /// <summary>
            /// 生产成品入库单
            /// </summary>
            [Description("生产成品入库单")]
            ProduceInOrder = 20,
            /// <summary>
            /// 报溢单
            /// </summary>
            [Description("报溢单")]
            OverOrder = 21,
            /// <summary>
            /// 报损单
            /// </summary>
            [Description("报损单")]
            LossOrder = 22,
            /// <summary>
            /// 赠送单
            /// </summary>
            [Description("赠送单")]
            FreeOrder = 23,
            /// <summary>
            /// 跟车配送单
            /// </summary>
            [Description("跟车配送单")]
            DistributeOrder = 24,
            /// <summary>
            /// POS销售单
            /// </summary>
            [Description("POS销售单")]
            PosOpenOrder = 25,
            /// <summary>
            /// POS结算单
            /// </summary>
            [Description("POS结算单")]
            PosCheckOrder = 26,
            /// <summary>
            /// 盘点录入单
            /// </summary>
            [Description("盘点录入单")]
            InventoryOrder = 27,
            /// <summary>
            /// 报价单
            /// </summary>
            [Description("报价单")]
            SalesQuotateOrder = 28,
            /// <summary>
            /// 期初库存单
            /// </summary>
            [Description("期初库存单")]
            BeginningStock = 29,
            /// <summary>
            /// 生产任务单
            /// </summary>
            [Description("生产任务单")]
            TaskOrder = 30,
            /// <summary>
            /// 采购申请单表
            /// </summary>
            [Description("采购申请单表")]
            ApplyOrder = 31,

            /// <summary>
            /// 物料清单表
            /// </summary>
            [Description("物料清单表")]
            MaterielRepertoire = 32,
            /// <summary>
            /// 工序清单表
            /// </summary>
            [Description("工序清单表")]
            ProcedureRepertoire = 33,
            /// <summary>
            /// 外协订单
            /// </summary>
            [Description("外协订单")]
            ForeignOrder = 34,
            /// <summary>
            /// 外协出库单
            /// </summary>
            [Description("外协出库单")]
            ForeignOutOrder = 35,
            /// <summary>
            /// 外协入库单
            /// </summary>
            [Description("外协入库单")]
            ForeignInOrder = 36,
            /// <summary>
            /// 生产采购订单
            /// </summary>
            [Description("生产采购订单")]
            ProPurchaseOrder = 37,
            /// <summary>
            /// 生产采购入库单
            /// </summary>
            [Description("生产采购入库单")]
            ProPurchaseInOrder = 38,
            /// <summary>
            /// 生产采购退货单
            /// </summary>
            [Description("生产采购退货单")]
            ProPurchaseReturnOrder = 39,
            /// <summary>
            /// 生产工单
            /// </summary>
            [Description("生产工单")]
            WorkOrder = 40,
            /// <summary>
            /// 生产领料单
            /// </summary>
            [Description("生产领料单")]
            WorkRequireOrder=41,
            /// <summary>
            /// 生产工单
            /// </summary>
            [Description("生产工单")]
            WorkSheetOrder=42,
             /// <summary>
            /// 自制生产验收入库单
            /// </summary>
            [Description("自制生产验收入库单")]
            WorkInOrder=43
        }
        #endregion

        #region 订单日志行为
        /// <summary>
        /// 订单日志行为
        /// </summary>
        [Flags]
        public enum OperateType
        {
            /// <summary>
            /// 审核行为
            /// </summary>
            [Description("审核行为")]
            Audit = 1,
            /// <summary>
            /// 执行行为
            /// </summary>
            [Description("执行行为")]
            Execute = 2,
            /// <summary>
            /// 其他行为
            /// </summary>
            [Description("其他行为")]
            Other = 3
        }
        #endregion

        #region 订单执行状态
        /// <summary>
        /// 订单执行状态
        /// </summary>
        [Flags]
        public enum OrderStatus
        {
            /// <summary>
            /// 未完成
            /// </summary>
            [Description("未完成")]
            Unfinished = 0,
            /// <summary>
            /// 已完成
            /// </summary>
            [Description("已完成")]
            Finished = 1,
            /// <summary>
            /// 已终止
            /// </summary>
            [Description("已终止")]
            Ended = 2
        }
        #endregion

        #region 采购类型
        /// <summary>
        /// 采购类型
        /// </summary>
        [Flags]
        public enum PurchaseType
        {
            /// <summary>
            /// 销售采购
            /// </summary>
            [Description("销售采购")]
            SalesPurchase = 1,
            /// <summary>
            /// 库存采购
            /// </summary>
            [Description("库存采购")]
            StoragePurchase = 2,
            /// <summary>
            /// 新品采购
            /// </summary>
            [Description("新品采购")]
            NewPurchase = 3
        }
        #endregion

        #region  采购订单方式类型
        /// <summary>
        /// 采购方式
        /// </summary>
        [Flags]
        public enum PurchaseWayType
        {
            /// <summary>
            /// 库存采购
            /// </summary>
            [Description("手工")]
            GeneralPurchase = 0,

            /// <summary>
            /// 自动化采购
            /// </summary>
            [Description("自动")]
            AutoPurchase = 1,

        }
        #endregion

        #region 销售类型
        /// <summary>
        /// 销售类型
        /// </summary>
        [Flags]
        public enum SalesType
        {
            /// <summary>
            /// 采购销售
            /// </summary>
            [Description("采购销售")]
            PurchaseSales = 1,
            /// <summary>
            /// 生产销售
            /// </summary>
            [Description("生产销售")]
            ProduceSales = 2,
            /// <summary>
            /// 库存销售
            /// </summary>
            [Description("库存销售")]
            StorageSales = 3
        }
        #endregion

        #region 销售属性
        /// <summary>
        /// 销售属性
        /// </summary>
        [Flags]
        public enum SalesProperty
        {
            /// <summary>
            /// 促销品
            /// </summary>
            [Description("促销品")]
            Promote = 1,
            /// <summary>
            /// 新品
            /// </summary>
            [Description("新品")]
            New = 2,
            /// <summary>
            /// 淘汰品
            /// </summary>
            [Description("淘汰品")]
            DieOut = 3
        }
        #endregion

        #region 盘点类型
        /// <summary>
        /// 盘点类型
        /// </summary>
        [Flags]
        public enum InventoryType
        {
            /// <summary>
            /// 红色盘亏
            /// </summary>
            [Description("红色盘亏")]
            InventoryLoss = 1,
            /// <summary>
            /// 绿色盘盈
            /// </summary>
            [Description("绿色盘盈")]
            InventoryProfit = 2,
            /// <summary>
            /// 蓝色平仓
            /// </summary>
            [Description("蓝色平仓")]
            InventoryNot = 3
        }
        #endregion

        #region 调拨类型
        /// <summary>
        /// 调拨类型
        /// </summary>
        [Flags]
        public enum AllocationType
        {
            /// <summary>
            /// 同价调拨
            /// </summary>
            [Description("同价调拨")]
            Same = 1,
            /// <summary>
            /// 变价调拨
            /// </summary>
            [Description("变价调拨")]
            Difference = 2
        }
        #endregion

        #region 应付款类型
        /// <summary>
        /// 应付款类型
        /// </summary>
        [Flags]
        public enum PayableType
        {
            /// <summary>
            /// 采购预付款
            /// </summary>
            [Description("采购预付款")]
            PrePay = 1,
            /// <summary>
            /// 采购应付款
            /// </summary>
            [Description("采购应付款")]
            Pay = 2,
            /// <summary>
            /// 采购退货款
            /// </summary>
            [Description("采购退货款")]
            ReturnPay = 3,
            /// <summary>
            /// 物流代付款
            /// </summary>
            [Description("物流代付款")]
            LogisticsPay = 4,
            /// <summary>
            /// 运费应付款
            /// </summary>
            [Description("运费应付款")]
            ExpressPay = 5,
            /// <summary>
            /// 采购退换货款
            /// </summary>
            [Description("采购退换货款")]
            ExchangePay = 6,
            /// <summary>
            /// 期初应付款
            /// </summary>
            [Description("期初应付款")]
            EarlyPay = 7,
            /// <summary>
            /// 运费代付款
            /// </summary>
            [Description("运费代付款")]
            ExpressProxy = 8,
            /// <summary>
            /// 生产采购预付款
            /// </summary>
            [Description("生产采购预付款")]
            ProPrePay = 9,
            /// <summary>
            /// 生产采购应付款
            /// </summary>
            [Description("生产采购应付款")]
            ProPay = 10,
            /// <summary>
            /// 生产采购退货款
            /// </summary>
            [Description("生产采购退货款")]
            ProReturnPay = 11,
            /// <summary>
            /// 外协预付款
            /// </summary>
            [Description("外协预付款")]
            ForeignPrePay = 12,
            /// <summary>
            /// 外协应付款
            /// </summary>
            [Description("外协应付款")]
            ForeignPay = 13
        }
        #endregion

        #region 付款类型
        /// <summary>
        /// 付款类型
        /// </summary>
        [Flags]
        public enum PayType
        {
            /// <summary>
            /// 采购预付款
            /// </summary>
            [Description("采购预付款")]
            PrePay = 1,
            /// <summary>
            /// 付款结销
            /// </summary>
            [Description("付款结销")]
            Pay = 2,
            /// <summary>
            /// 采购退款
            /// </summary>
            [Description("采购退款")]
            ReturnPay = 3,
            /// <summary>
            /// 运费付款
            /// </summary>
            [Description("运费付款")]
            ExpressPay = 4,
            /// <summary>
            /// 一般费用
            /// </summary>
            [Description("一般费用")]
            GeneralPay = 5,
            /// <summary>
            /// 采购退换货款
            /// </summary>
            [Description("采购退换货款")]
            ExchangePay = 6,
            /// <summary>
            /// 生产采购预付款
            /// </summary>
            [Description("生产采购预付款")]
            ProPrePay = 7,
            /// <summary>
            /// 外协预付款
            /// </summary>
            [Description("外协预付款")]
            ForeignPrePay = 8
        }
        #endregion

        #region 应收款类型
        /// <summary>
        /// 应收款类型
        /// </summary>
        [Flags]
        public enum ReceivableType
        {
            /// <summary>
            /// 客户预付款
            /// </summary>
            [Description("客户预付款")]
            PreReceive = 1,
            /// <summary>
            /// 销售应收款
            /// </summary>
            [Description("销售应收款")]
            Receive = 2,
            /// <summary>
            /// 销售退货款
            /// </summary>
            [Description("销售退货款")]
            ReturnReceive = 3,
            /// <summary>
            /// 物流代收款
            /// </summary>
            [Description("物流代收款")]
            LogisticsReceive = 4,
            /// <summary>
            /// 销售退换货款
            /// </summary>
            [Description("销售退换货款")]
            ExchangeReceive = 5,
            /// <summary>
            /// 期初应收款
            /// </summary>
            [Description("期初应收款")]
            EarlyReceive = 6,
            /// <summary>
            /// 赠送应收款
            /// </summary>
            [Description("赠送应收款")]
            FreeReceive = 7,
            /// <summary>
            /// 运费代付款
            /// </summary>
            [Description("运费代付款")]
            ExpressProxy = 8
        }
        #endregion

        #region 收款类型
        /// <summary>
        /// 收款类型
        /// </summary>
        [Flags]
        public enum ReceiveType
        {
            /// <summary>
            /// 客户预付款
            /// </summary>
            [Description("客户预付款")]
            PreReceive = 1,
            /// <summary>
            /// 收款结销
            /// </summary>
            [Description("收款结销")]
            Receive = 2,
            /// <summary>
            /// 销售退款
            /// </summary>
            [Description("销售退款")]
            ReturnReceive = 3,
            /// <summary>
            /// 其他收入
            /// </summary>
            [Description("其他收入)")]
            OtherReceive = 4,
            /// <summary>
            /// 销售退换货款
            /// </summary>
            [Description("销售退换货款")]
            ExchangeReceive = 5
        }
        #endregion

        #region 付款结销类型
        /// <summary>
        /// 付款结销类型
        /// </summary>
        [Flags]
        public enum PayCheckType
        {
            /// <summary>
            /// 采购预付款
            /// </summary>
            [Description("采购预付款")]
            PrePay = 1,
            /// <summary>
            /// 付款结销
            /// </summary>
            [Description("付款结销")]
            Pay = 2,
            /// <summary>
            /// 采购退款
            /// </summary>
            [Description("采购退款")]
            ReturnPay = 3,
            /// <summary>
            /// 物流运费
            /// </summary>
            [Description("物流运费")]
            ExpressPay = 4,
            /// <summary>
            /// 物流代付
            /// </summary>
            [Description("物流代付")]
            LogisticsPay = 5,
            /// <summary>
            /// 采购退换货款
            /// </summary>
            [Description("采购退换货款")]
            ExchangePay = 6,
            /// <summary>
            /// 生产采购预付款
            /// </summary>
            [Description("生产采购预付款")]
            ProPrePay = 7,
            /// <summary>
            /// 外协预付款
            /// </summary>
            [Description("外协预付款")]
            ForeignPrePay = 8
        }
        #endregion

        #region 收款结销类型
        /// <summary>
        /// 收款结销类型
        /// </summary>
        [Flags]
        public enum ReceiveCheckType
        {
            /// <summary>
            /// 客户预付款
            /// </summary>
            [Description("客户预付款")]
            PreReceive = 1,
            /// <summary>
            /// 收款结销
            /// </summary>
            [Description("收款结销")]
            Receive = 2,
            /// <summary>
            /// 销售退款)
            /// </summary>
            [Description("销售退款)")]
            ReturnReceive = 3,
            /// <summary>
            /// 物流代收
            /// </summary>
            [Description("物流代收")]
            LogisticsReceive = 4,
            /// <summary>
            /// 销售退换货款
            /// </summary>
            [Description("销售退换货款")]
            ExchangeReceive = 5
        }
        #endregion

        #region 结销状态
        /// <summary>
        /// 结销状态
        /// </summary>
        [Flags]
        public enum CheckStatus
        {
            /// <summary>
            /// 未结销
            /// </summary>
            [Description("未")]
            CheckNone = 1,
            /// <summary>
            /// 部份结销
            /// </summary>
            [Description("部分")]
            CheckPart = 2,
            /// <summary>
            /// 已结销
            /// </summary>
            [Description("已")]
            CheckAll = 3
        }
        #endregion

        #region 审核状态
        /// <summary>
        /// 审核状态
        /// </summary>
        [Flags]
        public enum AuditStatus
        {
            /// <summary>
            /// 待审核
            /// </summary>
            [Description("待审核")]
            Waiting = 1,
            /// <summary>
            /// 已审核
            /// </summary>
            [Description("已审核")]
            Audited = 2,
            /// <summary>
            /// 已退回
            /// </summary>
            [Description("已退回")]
            Returned = 3,
            /// <summary>
            /// 已作废
            /// </summary>
            [Description("已作废")]
            Destroyed = 4,
            /// <summary>
            /// 已红冲
            /// </summary>
            [Description("已红冲")]
            Rushed = 5
        }
        #endregion

        #region 物料状态(物料清单)
        /// <summary>
        /// 物料状态
        /// </summary>
        [Flags]
        public enum MaterialRepertoireStatus
        {
            /// <summary>
            /// 待申请 (采购)
            /// </summary>
            [Description("待申请")]
            WaitingApply = 1,
            /// <summary>
            /// 已申请(采购)
            /// </summary>
            [Description("已申请")]
            Apply = 2,
            /// <summary>
            /// 已采购 (生产采购单)
            /// </summary>
            [Description("已采购")]
            Purchase = 3,
            /// <summary>
            /// 未完成 (生产物料)
            /// </summary>
            [Description("未完成")]
            NotFinish = 4,
            /// <summary>
            /// 已完成 (采购物料--入库已完成  生产物料--外协已完成)
            /// </summary>
            [Description("已完成")]
            Finish = 5,
        }
        #endregion

        #region 日期筛选类型
        /// <summary>
        /// 日期筛选类型
        /// </summary>
        [Flags]
        public enum DateType
        {
            /// <summary>
            /// 今天
            /// </summary>
            [Description("今天")]
            Today = 1,
            /// <summary>
            /// 昨天
            /// </summary>
            [Description("昨天")]
            Yesterday = 2,
            /// <summary>
            /// 本周
            /// </summary>
            [Description("本周")]
            ThisWeek = 3,
            /// <summary>
            /// 上一周
            /// </summary>
            [Description("上一周")]
            LastWeek = 4,
            /// <summary>
            /// 本月
            /// </summary>
            [Description("本月")]
            ThisMonth = 5,
            /// <summary>
            /// 上一月
            /// </summary>
            [Description("上一月")]
            LastMonth = 6,
            /// <summary>
            /// 本年
            /// </summary>
            [Description("本年")]
            thisYear = 7,
            /// <summary>
            /// 去年
            /// </summary>
            [Description("去年")]
            LastYear = 8
        }
        #endregion

        #region 促销状态
        /// <summary>
        /// 促销状态
        /// </summary>
        [Flags]
        public enum PromoteStatus
        {
            /// <summary>
            /// 有效
            /// </summary>
            [Description("有效")]
            Valid = 0,
            /// <summary>
            /// 无效
            /// </summary>
            [Description("无效")]
            Invalid = 1
        }
        #endregion

        #region 促销类型
        /// <summary>
        /// 促销类型
        /// </summary>
        [Flags]
        public enum PromoteType
        {
            /// <summary>
            /// 次数
            /// </summary>
            [Description("次数")]
            Times = 1,
            /// <summary>
            /// 箱数
            /// </summary>
            [Description("箱数")]
            BoxAmount = 2,
            /// <summary>
            /// 时间
            /// </summary>
            [Description("时间")]
            TimeSpan = 3,
            /// <summary>
            /// 数量
            /// </summary>
            [Description("数量")]
            Amount = 4
        }
        #endregion

        #region 促销最低量类型
        /// <summary>
        /// 促销最低量类型
        /// </summary>
        [Flags]
        public enum LowestType
        {
            /// <summary>
            /// 箱数
            /// </summary>
            [Description("箱数")]
            BoxAmount = 1,
            /// <summary>
            /// 数量
            /// </summary>
            [Description("数量")]
            Amount = 2
        }
        #endregion

        #region 联系记录类型
        /// <summary>
        /// 联系记录类型
        /// </summary>
        [Flags]
        public enum ContactType
        {
            /// <summary>
            /// 来电记录
            /// </summary>
            [Description("来电记录")]
            Incoming = 1,
            /// <summary>
            /// 去电记录
            /// </summary>
            [Description("去电记录")]
            Electric = 2,
            /// <summary>
            /// 未接来电
            /// </summary>
            [Description("未接来电")]
            Missed = 3,
            /// <summary>
            /// 过去记录
            /// </summary>
            [Description("过去记录")]
            Go = 4,
            /// <summary>
            /// 过来记录
            /// </summary>
            [Description("过来记录")]
            Come = 5,
            /// <summary>
            /// 展会记录
            /// </summary>
            [Description("展会记录")]
            Exhibition = 6,
            /// <summary>
            /// 其他记录
            /// </summary>
            [Description("其他记录")]
            Other = 7
        }
        #endregion

        #region 联系记录执行状态
        /// <summary>
        /// 执行状态
        /// </summary>
        [Flags]
        public enum RecordStatus
        {
            /// <summary>
            /// 产生
            /// </summary>
            [Description("产生")]
            Emerge = 1,
            /// <summary>
            /// 执行
            /// </summary>
            [Description("执行")]
            Execute = 2,
            /// <summary>
            /// 重新执行
            /// </summary>
            [Description("重新执行")]
            ReExecute = 3,
            /// <summary>
            /// 完成
            /// </summary>
            [Description("完成")]
            Finish = 4
        }
        #endregion

        #region 邮件状态
        /// <summary>
        /// 邮件状态
        /// </summary>
        public enum EmailStatus
        {
            /// <summary>
            /// 草稿
            /// </summary>
            [Description("草稿")]
            Draft = 0,
            /// <summary>
            /// 待发送
            /// </summary>
            [Description("待发送")]
            Wait = 1,
            /// <summary>
            /// 已发送
            /// </summary>
            [Description("已发送")]
            Sended = 2
        }
        #endregion

        #region 邮件类型
        /// <summary>
        /// 邮件类型
        /// </summary>
        public enum EmailType
        {
            /// <summary>
            /// 普通邮件
            /// </summary>
            [Description("普通邮件")]
            OrdinaryEmail = 0,
            /// <summary>
            /// 订单邮件
            /// </summary>
            [Description("订单邮件")]
            OrderEmail = 1
        }
        #endregion

        #region 邮件结果状态
        public enum EmailResultStatus
        {
            /// <summary>
            /// 草稿
            /// </summary>
            [Description("草稿")]
            Draft = 0,
            /// <summary>
            /// 待发送
            /// </summary>
            [Description("待发送")]
            Wait = 1,
            /// <summary>
            /// 已发送
            /// </summary>
            [Description("已发送")]
            Sended = 2,
            /// <summary>
            /// 发送失败
            /// </summary>
            [Description("发送失败")]
            Failed = -1
        }
        #endregion

        #region 邮件优先级
        /// <summary>
        /// 邮件类型
        /// </summary>
        public enum EmailPriority
        {
            /// <summary>
            /// 非立即
            /// </summary>
            [Description("非立即")]
            NonImmediately = 0,
            /// <summary>
            /// 立即
            /// </summary>
            [Description("立即")]
            Immediately = 1
        }
        #endregion

        #region 短信状态
        /// <summary>
        /// 短信状态
        /// </summary>
        public enum SMSStatus
        {
            /// <summary>
            /// 草稿
            /// </summary>
            [Description("草稿")]
            Draft = 0,
            /// <summary>
            /// 待发送
            /// </summary>
            [Description("待发送")]
            Wait = 1,
            /// <summary>
            /// 已发送
            /// </summary>
            [Description("已发送")]
            Sended = 2
        }
        #endregion

        #region 短信类型
        /// <summary>
        /// 短信类型
        /// </summary>
        public enum SMSType
        {
            /// <summary>
            /// 一般短信
            /// </summary>
            [Description("一般短信")]
            GeneralMessage = 0,
            /// <summary>
            /// 生日提醒
            /// </summary>
            [Description("生日提醒")]
            BirthdayReminder = 1
        }
        #endregion

        #region 短信结果状态
        /// <summary>
        /// 短信结果状态
        /// </summary>
        public enum SMSResultStatus
        {
            /// <summary>
            /// 草稿
            /// </summary>
            [Description("草稿")]
            Draft = 0,
            /// <summary>
            /// 待发送
            /// </summary>
            [Description("待发送")]
            Wait = 1,
            /// <summary>
            /// 已发送
            /// </summary>
            [Description("已发送")]
            Sended = 2,
            /// <summary>
            /// 发送失败
            /// </summary>
            [Description("发送失败")]
            Failed = -1
        }
        #endregion

        #region 短信优先级
        /// <summary>
        /// 短信优先级
        /// </summary>
        public enum SMSPriority
        {
            /// <summary>
            /// 非立即
            /// </summary>
            [Description("非立即")]
            NonImmediately = 0,
            /// <summary>
            /// 立即
            /// </summary>
            [Description("立即")]
            Immediately = 1
        }
        #endregion

        #region 公告状态
        /// <summary>
        /// 公告状态
        /// </summary>
        public enum NoticeStatus
        {
            /// <summary>
            /// 草稿 
            /// </summary>
            [Description("草稿")]
            Draft = 0,
            /// <summary>
            /// 发布
            /// </summary>
            [Description("发布")]
            Publish = 1,
            /// <summary>
            /// 撤销
            /// </summary>
            [Description("撤销")]
            Revoke = 2,
        }
        #endregion

        #region 公告优先级
        /// <summary>
        /// 公告优先级
        /// </summary>
        public enum NoticePriority
        {
            /// <summary>
            /// 紧急 
            /// </summary>
            [Description("紧急")]
            Urgency = 1,
            /// <summary>
            /// 高
            /// </summary>
            [Description("高")]
            High = 2,
            /// <summary>
            /// 普通
            /// </summary>
            [Description("普通")]
            General = 3
        }
        #endregion

        #region 消息状态
        /// <summary>
        /// 消息状态
        /// </summary>
        public enum MessageStatus
        {
            /// <summary>
            /// 草稿
            /// </summary>
            [Description("草稿")]
            Draft = 0,
            /// <summary>
            /// 待发送
            /// </summary>
            [Description("待发送")]
            Wait = 1,
            /// <summary>
            /// 已发送
            /// </summary>
            [Description("已发送")]
            Sended = 2
        }
        #endregion

        #region 消息类型
        /// <summary>
        /// 消息类型
        /// </summary>
        public enum MessageType
        {
            /// <summary>
            /// 一般短信
            /// </summary>
            [Description("一般消息")]
            GeneralMessage = 0,
            /// <summary>
            /// 发货提醒
            /// </summary>
            [Description("发货提醒")]
            ShipmentsReminder = 1
        }
        #endregion

        #region 消息结果状态
        /// <summary>
        /// 消息结果状态
        /// </summary>
        public enum MessageResultStatus
        {
            /// <summary>
            /// 草稿
            /// </summary>
            [Description("草稿")]
            Draft = 0,
            /// <summary>
            /// 待发送
            /// </summary>
            [Description("待发送")]
            Wait = 1,
            /// <summary>
            /// 已发送
            /// </summary>
            [Description("已发送")]
            Sended = 2,
            /// <summary>
            /// 发送失败
            /// </summary>
            [Description("发送失败")]
            Failed = -1
        }
        #endregion

        #region 消息优先级
        /// <summary>
        /// 消息优先级
        /// </summary>
        public enum MessagePriority
        {
            /// <summary>
            /// 非立即
            /// </summary>
            [Description("非立即")]
            NonImmediately = 0,
            /// <summary>
            /// 立即
            /// </summary>
            [Description("立即")]
            Immediately = 1
        }
        #endregion

        #region 消息阅读状态
        /// <summary>
        /// 消息阅读状态
        /// </summary>
        public enum MessageReadStatus
        {
            /// <summary>
            /// 未读
            /// </summary>
            [Description("未读")]
            Unread = 0,
            /// <summary>
            /// 已读
            /// </summary>
            [Description("已读")]
            Read = 1,
        }
        #endregion

        #region 任务来源
        /// <summary>
        /// 任务来源
        /// </summary>
        [Flags]
        public enum TaskSource
        {
            /// <summary>
            /// 自己任务
            /// </summary>
            [Description("自己任务")]
            Self = 0,
            /// <summary>
            /// 上级任务
            /// </summary>
            [Description("上级任务")]
            Higher = 1,
            /// <summary>
            /// 他人任务
            /// </summary>
            [Description("他人任务")]
            Other = 2,
            /// <summary>
            /// 系统任务
            /// </summary>
            [Description("系统任务")]
            System = 3
        }
        #endregion

        #region 共同任务
        /// <summary>
        /// 共同任务状态
        /// </summary>
        [Flags]
        public enum CommonStatus
        {
            /// <summary>
            /// 否
            /// </summary>
            [Description("否")]
            CommonYes = 0,
            /// <summary>
            /// 否
            /// </summary>
            [Description("是")]
            CommonNo = 1
        }
        #endregion

        #region 执行状态
        /// <summary>
        /// 执行状态
        /// </summary>
        [Flags]
        public enum ExecuteStatus
        {
            /// <summary>
            /// 产生
            /// </summary>
            [Description("产生")]
            Generate = 0,
            /// <summary>
            /// 待办
            /// </summary>
            [Description("待办")]
            Wait = 1,
            /// <summary>
            /// 执行中
            /// </summary>
            [Description("执行中")]
            Excuting = 2,
            /// <summary>
            /// 重新执行
            /// </summary>
            [Description("重新执行")]
            ReExcute = 3,
            /// <summary>
            /// 完成
            /// </summary>
            [Description("完成")]
            Finish = 4
        }
        #endregion

        #region 推荐类型
        /// <summary>
        /// 推荐类型
        /// </summary>
        public enum RecommendType
        {
            /// <summary>
            /// 促销
            /// </summary>
            [Description("促销")]
            Promotion = 1,
            /// <summary>
            /// 新品
            /// </summary>
            [Description("新品")]
            New = 2,
            /// <summary>
            /// 畅销
            /// </summary>
            [Description("畅销")]
            Best = 3,
            /// <summary>
            /// 常规
            /// </summary>
            [Description("常规")]
            General = 4,
            /// <summary>
            /// 功能
            /// </summary>
            [Description("功能")]
            Function = 5,
            /// <summary>
            /// 滞销
            /// </summary>
            [Description("滞销")]
            Poor = 6
        }
        #endregion

        #region 合同提醒时间
        /// <summary>
        /// 合同提醒时间
        /// </summary>
        public enum AgreementTimes
        {
            /// <summary>
            /// 1个月以上
            /// </summary>
            [Description("1个月以上")]
            MoreMonths = 1,
            /// <summary>
            /// 1个月
            /// </summary>
            [Description("1个月")]
            OneMonth = 2,
            /// <summary>
            /// 15天
            /// </summary>
            [Description("15天")]
            FifteenDays = 3,
            /// <summary>
            /// 1个星期
            /// </summary>
            [Description("1个星期")]
            SevenDays = 4,
            /// <summary>
            /// 今天
            /// </summary>
            [Description("今天")]
            OneDay = 5
        }
        #endregion

        #region 打印模式
        /// <summary>
        /// 打印模式
        /// </summary>
        [Flags]
        public enum PrintMode
        {
            /// <summary>
            /// 默认模式
            /// </summary>
            [Description("默认模式")]
            Default = 0,
            /// <summary>
            /// 连续打印
            /// </summary>
            [Description("连续打印")]
            ContinuousPrinting = 1,
            /// <summary>
            /// 自动填充空行
            /// </summary>
            [Description("填充空行")]
            AutomaticFillingLine = 2
        }
        #endregion

        #region 打印模板类型
        /// <summary>
        /// 打印模板类型
        /// </summary>
        [Flags]
        public enum PrintTemplateType
        {
            /// <summary>
            /// 采购订单
            /// </summary>
            [Description("采购订单")]
            PurchaseOrder = 1,
            /// <summary>
            /// 采购入库单
            /// </summary>
            [Description("采购入库单")]
            PurchaseInOrder = 2,
            /// <summary>
            /// 采购退货单
            /// </summary>
            [Description("采购退货单")]
            PurchaseReturnOrder = 3,
            /// <summary>
            /// 付款结算单
            /// </summary>
            [Description("付款结算单")]
            PayCheckOrder = 4,
            /// <summary>
            /// 销售订单
            /// </summary>
            [Description("销售订单")]
            SalesOrder = 5,
            /// <summary>
            /// 销售出库单
            /// </summary>
            [Description("销售出库单")]
            SalesOutOrder = 6,
            /// <summary>
            /// 销售退货单
            /// </summary>
            [Description("销售退货单")]
            SalesReturnOrder = 7,
            /// <summary>
            /// 收款结算单
            /// </summary>
            [Description("收款结算单")]
            ReceiveCheckOrder = 8,
            /// <summary>
            /// 一般费用单
            /// </summary>
            [Description("一般费用单")]
            GeneralOrder = 9,
            /// <summary>
            /// 其他收入单
            /// </summary>
            [Description("其他收入单")]
            OtherOrder = 10,
            /// <summary>
            /// 盘点生成单
            /// </summary>
            [Description("盘点生成单")]
            CreateInvOrder = 11,
            /// <summary>
            /// 代付结算单
            /// </summary>
            [Description("代付结算单")]
            DFPayCheckOrder = 12,
            /// <summary>
            /// 运费结算单
            /// </summary>
            [Description("运费结算单")]
            YFPayCheckOrder = 13,
            /// <summary>
            /// 代收结算单
            /// </summary>
            [Description("代收结算单")]
            DSReceiveCheckOrder = 14,
            /// <summary>
            /// 调拨单
            /// </summary>
            [Description("调拨单")]
            AllocationOrder = 15,
            /// <summary>
            /// 采购换货单
            /// </summary>
            [Description("采购换货单")]
            PurchaseExchangeOrder = 16,
            /// <summary>
            /// 销售换货单
            /// </summary>
            [Description("销售换货单")]
            SalesExchangeOrder = 17,
            /// <summary>
            /// 生产领料出库单(即生产计划单)
            /// </summary>
            [Description("生产领料出库单(即生产计划单)")]
            ProduceOutOrder = 18,
            /// <summary>
            /// 生产退料入库单
            /// </summary>
            [Description("生产退料入库单")]
            ProduceReturnOrder = 19,
            /// <summary>
            /// 生产成品入库单
            /// </summary>
            [Description("生产成品入库单")]
            ProduceInOrder = 20,
            /// <summary>
            /// 报溢单
            /// </summary>
            [Description("报溢单")]
            OverOrder = 21,
            /// <summary>
            /// 报损单
            /// </summary>
            [Description("报损单")]
            LossOrder = 22,
            /// <summary>
            /// 赠送单
            /// </summary>
            [Description("赠送单")]
            FreeOrder = 23,
            /// <summary>
            /// 采购单据列表
            /// </summary>
            [Description("采购单据列表")]
            PurchaseList = 24,
            /// <summary>
            /// 销售单据列表
            /// </summary>
            [Description("销售单据列表")]
            SalesList = 25,
            /// <summary>
            /// 库存单据列表
            /// </summary>
            [Description("库存单据列表")]
            InventoryList = 26,
            /// <summary>
            /// 生产单据列表
            /// </summary>
            [Description("生产单据列表")]
            ProduceList = 27,
            /// <summary>
            /// 收款单据列表
            /// </summary>
            [Description("收款单据列表")]
            ReceiveList = 28,
            /// <summary>
            /// 付款单据列表
            /// </summary>
            [Description("付款单据列表")]
            PayList = 29,
            /// <summary>
            /// 应收款
            /// </summary>
            [Description("应收款")]
            Receivable = 30,
            /// <summary>
            /// 应付款
            /// </summary>
            [Description("应付款")]
            Payable = 31,
            /// <summary>
            /// 销售对账单
            /// </summary>
            [Description("销售对账单")]
            SalesBillOrder = 32,
            /// <summary>
            /// 采购对账单
            /// </summary>
            [Description("采购对账单")]
            PurchaseBillOrder = 33,
            /// <summary>
            /// 客户欠款报表
            /// </summary>
            [Description("客户欠款报表")]
            BusinessArrears = 34,
            /// <summary>
            /// 收款明细查询
            /// </summary>
            [Description("收款明细查询")]
            ReceiveLineList = 35,
            /// <summary>
            /// 其他收入明细
            /// </summary>
            [Description("其他收入明细")]
            OtherLineList = 36,
            /// <summary>
            /// 付款明细查询
            /// </summary>
            [Description("付款明细查询")]
            PayLineList = 37,
            /// <summary>
            /// 一般费用明细
            /// </summary>
            [Description("一般费用明细")]
            GeneralLineList = 38,
            /// <summary>
            /// 采购明细查询
            /// </summary>
            [Description("采购明细查询")]
            PurchaseLineList = 39,
            /// <summary>
            /// 销售明细查询
            /// </summary>
            [Description("销售明细查询")]
            SalesLineList = 40,
            /// <summary>
            /// 库存单据明细查询
            /// </summary>
            [Description("库存单据明细查询")]
            InventoryLineList = 41,
            /// <summary>
            /// 生产明细查询
            /// </summary>
            [Description("生产明细查询")]
            ProduceLineList = 42,
            /// <summary>
            /// 采购欠款报表
            /// </summary>
            [Description("采购欠款报表")]
            PurchaseArrears = 43,
            /// <summary>
            /// 客户利润报表
            /// </summary>
            [Description("客户利润报表")]
            BusinessProfit = 44,
            /// <summary>
            /// 经营利润报表
            /// </summary>
            [Description("经营利润报表")]
            ManageProfit = 45,
            /// <summary>
            /// 促销明细列表
            /// </summary>
            [Description("促销明细列表")]
            PromoteLineList = 46,
            /// <summary>
            /// 跟车配送单
            /// </summary>
            [Description("跟车配送单")]
            DistributeOrder = 47,
            /// <summary>
            /// 销售提货单
            /// </summary>
            [Description("销售提货单")]
            SalesLadingOrder = 48,
            /// <summary>
            /// POS销售单
            /// </summary>
            [Description("POS销售单")]
            PosOpenOrder = 49,
            /// <summary>
            /// 盘点录入单
            /// </summary>
            [Description("盘点录入单")]
            InventoryOrder = 50,
            /// <summary>
            /// 报价单表
            /// </summary>
            [Description("报价单表")]
            SalesQuotateOrder = 51,
            /// <summary>
            /// 应付汇总管理
            /// </summary>
            [Description("应付汇总管理")]
            PayableSummary = 52,
            /// <summary>
            /// 应付汇总管理
            /// </summary>
            [Description("应收汇总管理")]
            ReceiveSummary = 53,
            /// <summary>
            /// 成本调价表
            /// </summary>
            [Description("成本调价表")]
            PurchaseAdjustCostPriceList = 54,
            /// <summary>
            /// 产品数量分析
            /// </summary>
            [Description("产品数量分析")]
            ProductAmountAnalysisLine = 55,
            /// <summary>
            /// 采购汇总(客户)
            /// </summary>
            [Description("采购汇总(客户)")]
            BusinessPurchaseSummary = 56,
            /// <summary>
            /// 采购汇总(产品)
            /// </summary>
            [Description("采购汇总(产品)")]
            ProductPurchaseSummary = 57,
            /// <summary>
            /// 采购汇总(业务员)
            /// </summary>
            [Description("采购汇总(业务员)")]
            EmployeePurchaseSummary = 58,
            /// <summary>
            /// 采购汇总(品牌)
            /// </summary>
            [Description("采购汇总(品牌)")]
            BrandPurchaseSummary = 59,
            /// <summary>
            /// 采购汇总(品牌)
            /// </summary>
            [Description("采购汇总(分类)")]
            CategoryPurchaseSummary = 60,
            /// <summary>
            /// 销售汇总(客户)
            /// </summary>
            [Description("销售汇总(客户)")]
            BusinessSalesSummary = 61,
            /// <summary>
            /// 销售汇总(产品)
            /// </summary>
            [Description("销售汇总(产品)")]
            ProductSalesSummary = 62,
            /// <summary>
            /// 销售汇总(业务员)
            /// </summary>
            [Description("销售汇总(业务员)")]
            EmployeeSalesSummary = 63,
            /// <summary>
            /// 销售汇总(品牌)
            /// </summary>
            [Description("销售汇总(品牌)")]
            BrandSalesSummary = 64,
            /// <summary>
            /// 销售汇总(分类)
            /// </summary>
            [Description("销售汇总(分类)")]
            CategorySalesSummary = 65,
            /// <summary>
            /// 未到货订单报表
            /// </summary>
            [Description("未到货订单报表")]
            NotArriveOrder = 66,
            /// <summary>
            /// 未出货订单报表
            /// </summary>
            [Description("未出货订单报表")]
            NotDeliveryOrder = 67,
            /// <summary>
            /// 现金银行汇总表
            /// </summary>
            [Description("现金银行汇总表")]
            AccountBankSummary = 68,
            /// <summary>
            /// 一般费用汇总表
            /// </summary>
            [Description("一般费用汇总表")]
            GeneralSummary = 69,
            /// <summary>
            /// 其他收入汇总表
            /// </summary>
            [Description("其他收入汇总表")]
            OtherSummary = 70,
            /// <summary>
            /// 滞销品分析
            /// </summary>
            [Description("滞销品分析")]
            PoorSalesLine = 71,
            /// <summary>
            /// 区域销售汇总
            /// </summary>
            [Description("区域销售汇总")]
            RegionSalesSummary = 72,
            /// <summary>
            /// 区域销售汇总明细
            /// </summary>
            [Description("区域销售汇总明细")]
            RegionBusSalesSummary = 73,
            /// <summary>
            /// 业务员销售汇总
            /// </summary>
            [Description("业务员销售汇总")]
            AnalysisEmployeeSalesSummary = 74,
            /// <summary>
            /// 业务员销售明细汇总
            /// </summary>
            [Description("业务员销售明细汇总")]
            AnalysisEmployeeBusSalesSummary = 75,
            /// <summary>
            /// 业务员应收款汇总表
            /// </summary>
            [Description("业务员应收款汇总表")]
            EmployeeReceivableSummary = 76,
            /// <summary>
            /// 业务员应收款明细表
            /// </summary>
            [Description("业务员应收款明细表")]
            EmployeeReceivableSummaryLine = 77,
            /// <summary>
            /// 业务员收入汇总表
            /// </summary>
            [Description("业务员收入汇总表")]
            EmployeeReceiveSummary = 78,
            /// <summary>
            /// 业务员收入明细表
            /// </summary>
            [Description("业务员收入明细表")]
            EmployeeReceiveSummaryLine = 79,
            /// <summary>
            /// 区域收入汇总表
            /// </summary>
            [Description("区域收入汇总表")]
            RegionReceiveSummary = 80,
            /// <summary>
            /// 区域收入明细表
            /// </summary>
            [Description("区域收入明细表")]
            RegionReceiveSummaryLine = 81,
            /// <summary>
            /// 区域应收款汇总表
            /// </summary>
            [Description("区域应收款汇总表")]
            RegionReceivableSummary = 82,
            /// <summary>
            /// 区域应收款明细表
            /// </summary>
            [Description("区域应收款明细表")]
            RegionReceivableSummaryLine = 83,
            /// <summary>
            /// 期初库存单
            /// </summary>
            [Description("期初库存单")]
            BeginningStock = 84,
            /// <summary>
            /// 业务员销售对比汇总
            /// </summary>
            [Description("业务员销售对比汇总")]
            EmployeeSalesContrastSummary = 85,
            /// <summary>
            /// 销售POS单
            /// </summary>
            [Description("销售POS单")]
            SalesPOSOrder = 86,
            /// <summary>
            /// 生产任务单
            /// </summary>
            [Description("生产任务单")]
            TaskOrder = 87,
            /// <summary>
            /// 生产任务单管理
            /// </summary>
            [Description("生产任务单管理")]
            TaskOrderList = 88,
            /// <summary>
            /// 采购申请单表
            /// </summary>
            [Description("采购申请单表")]
            ApplyOrder = 89,
            /// <summary>
            /// 生产采购订单
            /// </summary>
            [Description("生产采购订单")]
            ProPurchaseOrder = 90,
            /// <summary>
            /// 生产采购入库订单
            /// </summary>
            [Description("生产采购入库订单")]
            ProPurchaseInOrder = 91,
            /// <summary>
            /// 生产采购退货订单
            /// </summary>
            [Description("生产采购退货订单")]
            ProPurchaseReturnOrder = 92,

            /// <summary>
            /// 生产计划汇总单
            /// </summary>
            [Description("生产计划汇总单")]
            PlanList = 93,
            /// <summary>
            /// 生产物料单
            /// </summary>
            [Description("生产物料单")]
            MaterielRepertoire = 94,
            /// <summary>
            /// 生产工序单
            /// </summary>
            [Description("生产工序单")]
            ProcedureRepertoire = 95,
            /// <summary>
            /// 物料采购单
            /// </summary>
            [Description("物料采购单")]
            PurchaseMaterielList = 96,
            /// <summary>
            /// 外协订单列表
            /// </summary>
            [Description("外协订单列表")]
            ForeignOrderList = 97,
            /// <summary>
            /// 外协订单列表
            /// </summary>
            [Description("外协出库单列表")]
            ForeignOutOrderList = 98,
            /// <summary>
            /// 外协订单列表
            /// </summary>
            [Description("外协入库单列表")]
            ForeignInOrderList = 99,
            /// <summary>
            /// 外协订单
            /// </summary>
            [Description("外协订单")]
            ForeignOrder = 100,
            /// <summary>
            /// 外协出库单
            /// </summary>
            [Description("外协出库单")]
            ForeignOutOrder = 101,
            /// <summary>
            /// 外协入库单
            /// </summary>
            [Description("外协入库单")]
            ForeignInOrder = 102,
            /// <summary>
            /// 生产采购订单查询(采购申请、生产采购订单)
            /// </summary>
            [Description("生产采购订单查询(采购申请、生产采购订单)")]
            ProPurchaseOrderList = 103,
            /// <summary>
            /// 生产采购订单明细
            /// </summary>
            [Description("生产采购订单明细")]
            ProPurchaseOrderLineList = 104,
            /// <summary>
            /// 生产采购单据查询(生产采购入库单、生产采购退货单)
            /// </summary>
            [Description("生产采购单据查询(生产采购入库单、生产采购退货单)")]
            ProPurchaseList = 105,
            /// <summary>
            /// 生产采购单据明细
            /// </summary>
            [Description("生产采购单据明细")]
            ProPurchaseLineList = 106,
            /// <summary>
            /// 采购订单查询(采购订单)
            /// </summary>
            [Description("采购订单查询(采购订单)")]
            PurchaseOrderList = 107,
            /// <summary>
            /// 采购订单明细
            /// </summary>
            [Description("采购订单明细")]
            PurchaseOrderLineList = 108,
            /// <summary>
            /// 销售订单查询(销售报价单、销售订单)
            /// </summary>
            [Description("销售订单查询(销售报价单、销售订单)")]
            SalesOrderList = 109,
            /// <summary>
            /// 销售订单明细
            /// </summary>
            [Description("销售订单明细")]
            SalesOrderLineList = 110,
            /// <summary>
            /// 生产工单
            /// </summary>
            [Description("生产工单")]
            WorkOrder = 111,
            /// <summary>
            /// 生产工单列表
            /// </summary>
            [Description("生产工单列表")]
            WorkOrderList = 112,
            /// <summary>
            /// 生产领料单
            /// </summary>
            [Description("生产领料单")]
            WorkRequireOrder=113,
            /// <summary>
            /// 生产领料单列表
            /// </summary>
            [Description("生产领料单列表")]
            WorkRequireOrderList=114,
            /// <summary>
            /// 生产报工单
            /// </summary>
            [Description("生产报工单")]
            WorkSheetOrder=115,
            /// <summary>
            /// 生产报工单列表
            /// </summary>
            [Description("生产报工单列表")]
            WorkSheetOrderList=116,
             /// <summary>
            /// 验收入库单
            /// </summary>
            [Description("验收入库单")]
            WorkInOrder=117,
            /// <summary>
            /// 验收入库单列表
            /// </summary>
            [Description("验收入库单列表")]
            WorkInOrderList=118,
            /// <summary>
            /// 生产成本调价表
            /// </summary>
            [Description("生产成本调价表")]
            ProduceCostPriceList = 119
        }
        #endregion

        #region 打印模板类别
        /// <summary>
        /// 打印模板类别
        /// </summary>
        [Flags]
        public enum PrintTemplateCategory
        {
            /// <summary>
            /// 企业信息
            /// </summary>
            [Description("企业信息")]
            Enterprise = 0,
            /// <summary>
            /// 单据主体
            /// </summary>
            [Description("单据主体")]
            Order = 1,
            /// <summary>
            /// 单据明细
            /// </summary>
            [Description("单据明细")]
            Line = 2,
            /// <summary>
            /// 单据汇总
            /// </summary>
            [Description("单据汇总")]
            LineTotal = 3,
            /// <summary>
            /// 账户明细
            /// </summary>
            [Description("账户明细")]
            AccountLine = 4
        }
        #endregion

        #region 个性化定制类型
        /// <summary>
        /// 个性化定制类型
        /// </summary>
        [Flags]
        public enum IndividualizeTypes
        {
            /// <summary>
            /// 个性工具栏
            /// </summary>
            [Description("个性工具栏")]
            Tools,
            /// <summary>
            /// 个性分页
            /// </summary>
            [Description("个性分页")]
            Pager,
            /// <summary>
            /// 个性表头
            /// </summary>
            [Description("个性表头")]
            GridHeader,
            /// <summary>
            /// 个人中心
            /// </summary>
            [Description("个人中心")]
            PersonalCenter,
        }
        #endregion

        #region 采购状态
        /// <summary>
        /// 自动化采购标识
        /// </summary>
        [Flags]
        public enum PurchaseTypes
        {
            /// <summary>
            /// 已采购
            /// </summary>
            [Description("已采购")]
            PurchaseYet = 1,
            /// <summary>
            /// 未采购
            /// </summary>
            [Description("未采购")]
            PurchaseNot = 0,
        }
        #endregion

        #region 忽略状态
        /// <summary>
        /// 忽略状态
        /// </summary>
        [Flags]
        public enum IsIgnoreTypes
        {
            /// <summary>
            /// 忽略
            /// </summary>
            [Description("忽略")]
            IsIgnoreYet = 1,
            /// <summary>
            /// 正常
            /// </summary>
            [Description("正常")]
            IsIgnoreNot = 0,
        }
        #endregion

        #region 标签类别
        /// <summary>
        /// 标签类别
        /// </summary>
        [Flags]
        public enum TagCategory
        {
            /// <summary>
            /// 单位附件
            /// </summary>
            [Description("单位附件")]
            BusinessFileTag = -1,
            /// <summary>
            /// 产品附件
            /// </summary>
            [Description("产品附件")]
            ProductFileTag = -2,
            /// <summary>
            /// 促销附件
            /// </summary>
            [Description("促销附件")]
            PromoteFileTag = -3,
            /// <summary>
            /// 物料附件
            /// </summary>
            [Description("物料附件")]
            MaterielFileTag = -4
        }
        #endregion

        #region 跟车单单据
        /// <summary>
        /// 跟车单单据
        /// </summary>
        [Flags]
        public enum RelateType
        {
            /// <summary>
            /// 出库单
            /// </summary>
            [Description("出库单")]
            SalesOutOrder = 1,
            /// <summary>
            /// 应收单据
            /// </summary>
            [Description("应收单据")]
            Receivable = 2,
            /// <summary>
            /// 退货款单据
            /// </summary>
            [Description("退货款单据")]
            ReturnReceive = 3
        }
        #endregion

        #region 价格体系公式
        /// <summary>
        /// 价格体系公式
        /// </summary>
        [Flags]
        public enum PriceSeries
        {
            /// <summary>
            ///进货价
            /// </summary>
            [Description("S1")]
            PurchasePrice = 1,
            /// <summary>
            /// 成本价
            /// </summary>
            [Description("S2")]
            CostPrice = 2,
            /// <summary>
            /// 销售价
            /// </summary>
            [Description("S3")]
            Price = 3,
            /// <summary>
            /// 促销价
            /// </summary>
            [Description("S4")]
            PromotePrice = 4,
        }
        #endregion

        #region 月份
        /// <summary>
        /// 状态
        /// </summary>
        [Flags]
        public enum Months
        {
            /// <summary>
            /// 1月
            /// </summary>
            [Description("1月")]
            One = 1,
            /// <summary>
            /// 2月
            /// </summary>
            [Description("2月")]
            Tow = 2,
            /// <summary>
            /// 3月
            /// </summary>
            [Description("3月")]
            Three = 3,
            /// <summary>
            /// 4月
            /// </summary>
            [Description("4月")]
            Four = 4,
            /// <summary>
            /// 5月
            /// </summary>
            [Description("5月")]
            Five = 5,
            /// <summary>
            /// 6月
            /// </summary>
            [Description("6月")]
            Six = 6,
            /// <summary>
            /// 7月
            /// </summary>
            [Description("7月")]
            Seven = 7,
            /// <summary>
            /// 8月
            /// </summary>
            [Description("8月")]
            Eight = 8,
            /// <summary>
            /// 9月
            /// </summary>
            [Description("9月")]
            Nine = 9,
            /// <summary>
            /// 10月
            /// </summary>
            [Description("10月")]
            Ten = 10,
            /// <summary>
            /// 11月
            /// </summary>
            [Description("11月")]
            Eleven = 11,
            /// <summary>
            /// 12月
            /// </summary>
            [Description("12月")]
            Twelve = 12,
        }
        #endregion

        #region  时间类型
        /// <summary>
        /// 时间类型
        /// </summary>
        [Flags]
        public enum TimeType
        {
            /// <summary>
            ///自定义
            /// </summary>           
            Custom = 0,
            /// <summary>
            /// 月份
            /// </summary>            
            Month = 1,
            /// <summary>
            /// 星期
            /// </summary>           
            Week = 2
        }
        #endregion

        #region   体检类别
        /// <summary>
        /// 体检完善
        /// </summary>
        [Flags]
        public enum ExaminationType
        {
            /// <summary>
            ///员工管理
            /// </summary>           
            CO_Employee = 1,
            /// <summary>
            /// 往来单位
            /// </summary>            
            CO_Business = 2,
            /// <summary>
            /// 产品管理
            /// </summary>           
            CO_Product = 3,
            /// <summary>
            ///仓库管理
            /// </summary>           
            CO_Storage = 4,
            /// <summary>
            /// 账户管理
            /// </summary>            
            CM_Account = 5,
            /// <summary>
            /// 标签管理
            /// </summary>           
            CM_Tags = 6,
            /// <summary>
            /// 车辆管理
            /// </summary>           
            CO_Vehicle = 7,
            /// <summary>
            /// 采购订单
            /// </summary>
            Pur_PurchaseOrder = 8,
            /// <summary>
            /// 采购入库单
            /// </summary>
            Pur_PurchaseInOrder = 9,
            /// <summary>
            /// 采购退货单
            /// </summary>
            Pur_PurchaseReturnOrder = 10,
            /// <summary>
            /// 销售订单
            /// </summary>
            Sal_SalesOrder = 11,
            /// <summary>
            /// 销售出库单
            /// </summary>
            Sal_SalesOutOrder = 12,
            /// <summary>
            /// 销售退货单
            /// </summary>
            Sal_SalesReturnOrder = 13,
        }
        #endregion

        #region  数据是否完善
        /// <summary>
        /// 数据是否完善
        /// </summary>
        [Flags]
        public enum IsPerfect
        {
            /// <summary>
            /// 通过
            /// </summary>           
            Pass = 1,
            /// <summary>
            /// 不通过
            /// </summary>            
            NotPass = 2,
        }
        #endregion

        #region  是否可以分析
        /// <summary>
        /// 是否可以分析
        /// </summary>
        [Flags]
        public enum IsAnalyze
        {
            /// <summary>
            /// 通过
            /// </summary>           
            Pass = 1,
            /// <summary>
            /// 不通过
            /// </summary>            
            NotPass = 2,
        }
        #endregion

        #region 成本价计算
        /// <summary>
        /// 成本价计算类型
        /// </summary>
        [Flags]
        public enum CalculateType
        {
            /// <summary>
            /// 忽略
            /// </summary>
            [Description("固定成本价")]
            None = 0,
            /// <summary>
            /// 正常
            /// </summary>
            [Description("加权平均")]
            WeightedAvg = 1,
        }
        #endregion

        #region 调价操作状态
        /// <summary>
        /// 调价操作状态
        /// </summary>
        [Flags]
        public enum AdjustType
        {
            /// <summary>
            /// 促销
            /// </summary>
            [Description("促销")]
            Promote = 1,
            /// <summary>
            /// 调价
            /// </summary>
            [Description("调价")]
            Adjust = 2
        }
        #endregion

        #region 调价处理状态
        /// <summary>
        /// 调价处理状态
        /// </summary>
        [Flags]
        public enum HandleStatus
        {
            /// <summary>
            /// 未处理
            /// </summary>
            [Description("未处理")]
            None = 0,
            /// <summary>
            /// 已处理
            /// </summary>
            [Description("已处理")]
            Handled = 1
        }
        #endregion

        #region 批量设置
        /// <summary>
        /// 批量设置
        /// </summary>
        [Flags]
        public enum BatchSetting
        {


            /// <summary>
            /// 性质
            /// </summary>
            [Description("性质")]
            Property = 1,
            /// <summary>
            /// 类别
            /// </summary>
            [Description("类别")]
            Category = 2,
            /// <summary>
            /// 品牌
            /// </summary>
            [Description("品牌")]
            Brand = 3,
            ///// <summary>
            ///// 产品类型
            ///// </summary>
            //[Description("产品类型")]
            //ProductType = 4,
            /// <summary>
            /// 产品仓位
            /// </summary>
            [Description("产品仓位")]
            ProductPosition = 5
        }
        #endregion

        #region 是否是国家默认
        /// <summary>
        /// 是否是国家默认
        /// </summary>
        [Flags]
        public enum IsDefault
        {
            /// <summary>
            /// 是国家默认
            /// </summary>
            [Description("是国家默认")]
            IsDefault = 0,
            /// <summary>
            /// 不是
            /// </summary>
            [Description("不是")]
            NotDefault = 1
        }
        #endregion

        #region 报表模式
        /// <summary>
        /// 报表模式
        /// </summary>
        [Flags]
        public enum ReportMode
        {
            /// <summary>
            /// 设计模式
            /// </summary>
            [Description("设计")]
            Design = 0,
            /// <summary>
            /// 预览模式
            /// </summary>
            [Description("预览")]
            Preview = 1
        }
        #endregion

        #region 是否配送
        /// <summary>
        /// 是否配送
        /// </summary>
        [Flags]
        public enum IsDistribute
        {
            /// <summary>
            /// 未配送
            /// </summary>
            [Description("未配送")]
            NotDistribute = 0,
            /// <summary>
            /// 已配送
            /// </summary>
            [Description("已配送")]
            Distributed = 1
        }
        #endregion

        #region 是否录入
        /// <summary>
        /// 是否录入
        /// </summary>
        [Flags]
        public enum Inventory
        {
            /// <summary>
            /// 未录入
            /// </summary>
            [Description("未录入")]
            NotInventory = 0,
            /// <summary>
            /// 录入
            /// </summary>
            [Description("录入")]
            Inventory = 1
        }
        #endregion

        #region  预设采购价格
        /// <summary>
        ///  预设采购价格
        /// </summary>
        [Flags]
        public enum PresetPurchasePrices
        {
            /// <summary>
            /// 无
            /// </summary>
            [Description("无")]
            NonePresetPrices = 0,
            /// <summary>
            /// 最新进价
            /// </summary>
            [Description("最新进价")]
            PresetPrices1 = 1,
            /// <summary>
            /// 预设进价二
            /// </summary>
            [Description("预设进价二")]
            PresetPrices2 = 2,
            /// <summary>
            /// 预设进价三
            /// </summary>
            [Description("预设进价三")]
            PresetPrices3 = 3,
            /// <summary>
            /// 进货价格
            /// </summary>
            [Description("进货价格")]
            PurchasePrice = 4,
        }
        #endregion

        #region  售价
        /// <summary>
        /// 预设售价
        /// </summary>
        [Flags]
        public enum PresetPrices
        {
            /// <summary>
            /// 无
            /// </summary>
            [Description("无")]
            NonePresetPrices = 0,
            /// <summary>
            /// 预设售价一
            /// </summary>
            [Description("预设售价一")]
            PresetPrices1 = 1,
            /// <summary>
            /// 预设售价二
            /// </summary>
            [Description("预设售价二")]
            PresetPrices2 = 2,
            /// <summary>
            /// 预设售价三
            /// </summary>
            [Description("预设售价三")]
            PresetPrices3 = 3,
            /// <summary>
            /// 预设售价四
            /// </summary>
            [Description("预设售价四")]
            PresetPrices4 = 4,
            /// <summary>
            /// 销售价格
            /// </summary>
            [Description("销售价格")]
            SalesPrice = 5,
        }
        #endregion

        #region 推送类型

        /// <summary>
        /// 推送类型
        /// </summary>
        [Flags]
        public enum PushType
        {
            /// <summary>
            /// 采购订单
            /// </summary>
            [Description("采购订单")]
            PurchaseOrder = 1,
            /// <summary>
            /// 采购入库单
            /// </summary>
            [Description("采购入库单")]
            PurchaseInOrder = 2,
            /// <summary>
            /// 采购退货单
            /// </summary>
            [Description("采购退货单")]
            PurchaseReturnOrder = 3,
            /// <summary>
            /// 销售订单
            /// </summary>
            [Description("销售订单")]
            SalesOrder = 4,
            /// <summary>
            /// 销售出库单
            /// </summary>
            [Description("销售出库单")]
            SalesOutOrder = 5,
            /// <summary>
            /// 销售退货单
            /// </summary>
            [Description("销售退货单")]
            SalesReturnOrder = 6,
            /// <summary>
            /// 销售价格
            /// </summary>
            [Description("销售POS单")]
            SalesOutOrderPos = 7,

            /// <summary>
            /// 促销品
            /// </summary>
            [Description("促销品")]
            PromoteProduct = 8,
            /// <summary>
            /// 新品
            /// </summary>
            [Description("新品")]
            NewProduct = 9,

            /// <summary>
            /// 删除订单
            /// </summary>
            [Description("作废订单")]
            DeleteOrder = 10,

            /// <summary>
            /// 下架推荐品
            /// </summary>
            [Description("下架促销品")]
            DeletePromote = 11,


        }
        #endregion

        #region 字段类型
        /// <summary>
        /// 字段类型
        /// </summary>
        [Flags]
        public enum FieldsType
        {
            /// <summary>
            /// 字段
            /// </summary>
            [Description("字段")]
            Field = 1,
            /// <summary>
            /// 操作
            /// </summary>
            [Description("操作")]
            Operate = 2
        }
        #endregion

        #region  职务等级
        /// <summary>
        /// 职务等级
        /// </summary>
        [Flags]
        public enum PostLevel
        {
            /// <summary>
            /// 字母A
            /// </summary>
            [Description("A")]
            A = 1,
            /// <summary>
            /// 字母B
            /// </summary>
            [Description("B")]
            B = 2,
            /// <summary>
            /// 字母C
            /// </summary>
            [Description("C")]
            C = 3,
            /// <summary>
            /// 字母D
            /// </summary>
            [Description("D")]
            D = 4,
            /// <summary>
            /// 字母E
            /// </summary>
            [Description("E")]
            E = 5,
            /// <summary>
            /// 字母F
            /// </summary>
            [Description("F")]
            F = 6,
            /// <summary>
            /// 字母G
            /// </summary>
            [Description("G")]
            G = 7,
            /// <summary>
            /// 字母H
            /// </summary>
            [Description("H")]
            H = 8,
            /// <summary>
            /// 字母I
            /// </summary>
            [Description("I")]
            I = 9,
            /// <summary>
            /// 字母J
            /// </summary>
            [Description("J")]
            J = 10,
            /// <summary>
            /// 字母K
            /// </summary>
            [Description("K")]
            K = 11,
            /// <summary>
            /// 字母L
            /// </summary>
            [Description("L")]
            L = 12,
            /// <summary>
            /// 字母M
            /// </summary>
            [Description("M")]
            M = 13,
            /// <summary>
            /// 字母B
            /// </summary>
            [Description("N")]
            N = 14,
            /// <summary>
            /// 字母O
            /// </summary>
            [Description("O")]
            O = 15,
            /// <summary>
            /// 字母B
            /// </summary>
            [Description("P")]
            P = 16,
            /// <summary>
            /// 字母Q
            /// </summary>
            [Description("Q")]
            Q = 17,
            /// <summary>
            /// 字母R
            /// </summary>
            [Description("R")]
            R = 18,
            /// <summary>
            /// 字母S
            /// </summary>
            [Description("S")]
            S = 19,
            /// <summary>
            /// 字母T
            /// </summary>
            [Description("T")]
            T = 20,
            /// <summary>
            /// 字母U
            /// </summary>
            [Description("U")]
            U = 21,
            /// <summary>
            /// 字母V
            /// </summary>
            [Description("V")]
            V = 22,
            /// <summary>
            /// 字母W
            /// </summary>
            [Description("W")]
            W = 23,
            /// <summary>
            /// 字母X
            /// </summary>
            [Description("X")]
            X = 24,
            /// <summary>
            /// 字母Y
            /// </summary>
            [Description("Y")]
            Y = 25,
            /// <summary>
            /// 字母Z
            /// </summary>
            [Description("Z")]
            Z = 26
        }

        #endregion

        #region 操作状态
        /// <summary>
        /// 操作状态(1-促销 2-调价)
        /// </summary>
        [Flags]
        public enum OperationStatus
        {
            /// <summary>
            /// 促销
            /// </summary>
            [Description("促销")]
            Promote = 1,
            /// <summary>
            /// 调价
            /// </summary>
            [Description("调价")]
            AdjustPrice = 2
        }
        #endregion

        #region 推动类型
        /// <summary>
        /// 推动类型
        /// </summary>
        [Flags]
        public enum PushUserType
        {
            /// <summary>
            /// 所有人员
            /// </summary>
            [Description("所有人员")]
            All = 0,
            /// <summary>
            /// 部门人员
            /// </summary>
            [Description("指定部门")]
            BuMen = 1,
            /// <summary>
            /// 指定人员
            /// </summary>
            [Description("指定人员")]
            Pop = 2
        }
        #endregion

        #region 存储过程返回类型
        /// <summary>
        /// 存储过程返回类型
        /// </summary>
        [Flags]
        public enum ReturnType
        {
            /// <summary>
            /// 集合
            /// </summary>
            [Description("集合")]
            Collections = 0,
            /// <summary>
            /// 字符串值
            /// </summary>
            [Description("字符串值")]
            SringVal = 1
        }
        #endregion

        #region 权限操作类型
        /// <summary>
        /// 权限操作类型
        /// </summary>
        [Flags]
        public enum AuthorityOperate
        {
            /// <summary>
            ///允许修改采购单价
            /// </summary>
            [Description("允许修改采购单价")]
            M001 = 001,
            /// <summary>
            /// 允许修改销售单价
            /// </summary>
            [Description("允许修改销售单价")]
            M002 = 002,
            /// <summary>
            /// 允许使用折扣率
            /// </summary>
            [Description("允许使用折扣率")]
            M003 = 003,
            /// <summary>
            /// 允许低于成本价销售
            /// </summary>
            [Description("允许低于成本价销售")]
            M004 = 004,
            /// <summary>
            /// 允许开单修改仓位
            /// </summary>
            [Description("允许开单修改仓位")]
            M005 = 005,
            /// <summary>
            /// 允许采购价选择
            /// </summary>
            [Description("允许采购价选择")]
            M006 = 006,
            /// <summary>
            /// 允许销售价选择
            /// </summary>
            [Description("允许销售价选择")]
            M007 = 007,
            /// <summary>
            /// 允许查看部门人员数据
            /// </summary>
            [Description("允许查看部门人员数据")]
            M008 = 008,
            /// <summary>
            /// 允许多次打印单据
            /// </summary>
            [Description("允许多次打印单据")]
            M009 = 009,
            /// <summary>
            /// 打印模板设置
            /// </summary>
            [Description("打印模板设置")]
            M010 = 010,
            /// <summary>
            /// POS单可修改单价
            /// </summary>
            [Description("POS单可修改单价")]
            M011 = 011,
            /// <summary>
            /// POS单可使用折扣
            /// </summary>
            [Description("POS单可使用折扣")]
            M012 = 012,
            /// <summary>
            /// POS单可挂单操作
            /// </summary>
            [Description("POS单可挂单操作")]
            M013 = 013,
            /// <summary>
            /// 计入库存操作
            /// </summary>
            [Description("计入库存操作")]
            M014 = 014
        }
        #endregion

        #region  字段权限
        /// <summary>
        /// 
        /// </summary>
        [Flags]
        public enum AuthorityField
        {
            /// <summary>
            ///成本价格
            /// </summary>
            [Description("是否允许查看产品信息、库存量、报表中的成本价格及库存金额")]
            F001 = 001,
            /// <summary>
            ///进货价格
            /// </summary>
            [Description("是否允许查看进货价")]
            F002 = 002,
            /// <summary>
            ///最新进价
            /// </summary>
            [Description("是否允许查看最新进价(产品预设进价一)")]
            F003 = 003,
            /// <summary>
            ///预设进价二
            /// </summary>
            [Description("是否允许查看预设进价二")]
            F004 = 004,
            /// <summary>
            ///预设进价三
            /// </summary>
            [Description("是否允许查看预设进价三")]
            F005 = 005,
            /// <summary>
            ///销售价格
            /// </summary>
            [Description("是否允许查看销售价格、销售金额")]
            F006 = 006,
            /// <summary>
            ///预设售价一
            /// </summary>
            [Description("是否允许查看预设售价一")]
            F007 = 007,
            /// <summary>
            ///预设售价二
            /// </summary>
            [Description("是否允许查看预设售价二")]
            F008 = 008,
            /// <summary>
            ///预设售价三
            /// </summary>
            [Description("是否允许查看预设售价三")]
            F009 = 009,
            /// <summary>
            ///预设售价四
            /// </summary>
            [Description("是否允许查看预设售价四")]
            F010 = 010,
            /// <summary>
            ///利润
            /// </summary>
            [Description("是否允许查看报表中的利润")]
            F011 = 011,
            /// <summary>
            ///毛利、毛利率
            /// </summary>
            [Description("是否允许查看报表中的毛利、毛利率")]
            F012 = 012,
        }
        #endregion

        #region 员工申请类型
        /// <summary>
        /// 申请类型
        /// </summary>
        [Flags]
        public enum ExamineType
        {
            /// <summary>
            /// 请假
            /// </summary>
            [Description("请假")]
            QJ = 1,
            /// <summary>
            /// 外出
            /// </summary>
            [Description("外出")]
            YC = 2,
            /// <summary>
            /// 费用
            /// </summary>
            [Description("费用")]
            FY = 3,
            /// <summary>
            /// 通用
            /// </summary>
            [Description("通用")]
            TY = 4
        }
        #endregion

        #region 员工申请状态
        /// <summary>
        /// 申请状态
        /// </summary>
        [Flags]
        public enum ExamineState
        {
            /// <summary>
            /// 待审批
            /// </summary>
            [Description("待审批")]
            Await = 1,
            /// <summary>
            /// 同意
            /// </summary>
            [Description("同意")]
            Consent = 2,
            /// <summary>
            /// 驳回
            /// </summary>
            [Description("驳回")]
            Reject = 3

        }
        #endregion

        #region 业务员标识
        /// <summary>
        /// 性别
        /// </summary>
        [Flags]
        public enum EmployeIdent
        {
            /// <summary>
            /// 是业务员
            /// </summary>
            [Description("是业务员")]
            IsEmployee = 1,
            /// <summary>
            /// 不是业务员
            /// </summary>
            [Description("不是业务员")]
            NotEmployee = 0
        }
        #endregion

        #region 微商城价格类型
        /// <summary>
        /// 价格类型//    价格类型(1 销售价格  2 预售价格一 3 预售价格二  3 预售价格三   4预售价格四)
        /// </summary>
        [Flags]
        public enum PlatformPriceType
        {
            /// <summary>
            /// 销售价格
            /// </summary>
            [Description("销售价格")]
            Price = 1,
            /// <summary>
            /// 预售价格一
            /// </summary>
            [Description("预售价格一")]
            Price1 = 2,
            /// <summary>
            /// 预售价格二
            /// </summary>
            [Description("预售价格二")]
            Price2 = 3,

            /// <summary>
            /// 预售价格三
            /// </summary>
            [Description("预售价格三")]
            Price3 = 4,
            /// <summary>
            /// 预售价格四
            /// </summary>
            [Description("预售价格四")]
            Price4 = 5,
        }
        #endregion

        #region 微商城推送类型
        /// <summary>
        /// 推送类型// 常规 新品 促销
        /// </summary>
        [Flags]
        public enum PlatformPushType
        {
            /// <summary>
            /// 常规
            /// </summary>
            [Description("常规")]
            General = 1,
            /// <summary>
            /// 新品
            /// </summary>
            [Description("新品")]
            New = 2,
            /// <summary>
            /// 促销
            /// </summary>
            [Description("促销")]
            Promote = 3,
        }
        #endregion

        #region 微商城是否推送成功
        /// <summary>
        /// 微商城是否推送成功
        /// </summary>
        [Flags]
        public enum PlatformIsPush
        {
            /// <summary>
            /// 失败
            /// </summary>
            [Description("失败")]
            Fail = 0,
            /// <summary>
            ///成功
            /// </summary>
            [Description("成功")]
            Succeed = 2,
        }
        #endregion

        #region 微商城产品上下架状态
        /// <summary>
        ///微商城产品上下架状态
        /// </summary>
        [Flags]
        public enum PlatformProductIsUp
        {
            /// <summary>
            /// 失败
            /// </summary>
            [Description("下架")]
            Down = 0,
            /// <summary>
            ///成功
            /// </summary>
            [Description("上架")]
            Up = 1,
        }
        #endregion

        #region 产品附件文件类型
        /// <summary>
        ///产品附件文件类型
        /// </summary>
        [Flags]
        public enum FileType
        {
            /// <summary>
            /// 型(如:xls,txt,zip,rar
            /// </summary>
            [Description("型(如:xls,txt,zip,rar ")]
            File = 0,
            /// <summary>
            ///图片
            /// </summary>
            [Description("图片")]
            Picture = 1,
        }
        #endregion

        #region 优惠方式
        /// <summary>
        ///优惠方式(0-常规,1-折扣,2-促销,3-特价)
        /// </summary>
        [Flags]
        public enum PreferentialType
        {
            /// <summary>
            /// 常规
            /// </summary>
            [Description("常规")]
            Common = 0,
            /// <summary>
            ///折扣
            /// </summary>
            [Description("折扣")]
            Discount = 1,
            /// <summary>
            ///促销
            /// </summary>
            [Description("促销")]
            Promote = 2,
            /// <summary>
            ///特价
            /// </summary>
            [Description("特价")]
            Special = 3
        }
        #endregion

        #region 产品物料类型
        /// <summary>
        /// 产品物料类型
        /// </summary>
        [Flags]
        public enum ProductMaterielType
        {
            /// <summary>
            /// 产品
            /// </summary>
            [Description("产品")]
            Product = 1,
            /// <summary>
            /// 物料
            /// </summary>
            [Description("物料")]
            Materiel = 0
        }
        #endregion

        #region  销售单默认价
        /// <summary>
        /// 销售单默认价
        /// </summary>
        [Flags]
        public enum SalesPrice
        {
            /// <summary>
            /// 销售价格
            /// </summary>
            [Description("销售价格")]
            Price = 0,
            /// <summary>
            /// 预设售价一
            /// </summary>
            [Description("预设售价一")]
            Price1 = 1,
            /// <summary>
            /// 预设售价二
            /// </summary>
            [Description("预设售价二")]
            Price2 = 2,
            /// <summary>
            /// 预设售价三
            /// </summary>
            [Description("预设售价三")]
            Price3 = 3,
            /// <summary>
            /// 预设售价四
            /// </summary>
            [Description("预设售价四")]
            Price4 = 4,
            /// <summary>
            /// 价格体系
            /// </summary>
            [Description("价格体系")]
            PriceSeries = 5
        }
        #endregion

        #region 物料来源
        /// <summary>
        /// 物料来源
        /// </summary>
        [Flags]
        public enum MaterielSource
        {
            /// <summary>
            /// 生产
            /// </summary>
            [Description("生产")]
            Produce = 1,
            /// <summary>
            /// 采购
            /// </summary>
            [Description("采购")]
            Purchase = 2
        }
        #endregion

        #region 工序来源
        /// <summary>
        /// 工序来源
        /// </summary>
        [Flags]
        public enum ProcedureSource
        {
            /// <summary>
            /// 自制
            /// </summary>
            [Description("自制")]
            Self = 1,
            /// <summary>
            /// 外协
            /// </summary>
            [Description("外协")]
            Outsource = 2,
            ///// <summary>
            ///// 采购
            ///// </summary>
            //[Description("自制/外协")]
            //SelfOutsource = 3
        }
        #endregion

        #region 批量设置
        /// <summary>
        /// 批量设置
        /// </summary>
        [Flags]
        public enum MaterielBatchSetting
        {
            /// <summary>
            /// 性质
            /// </summary>
            [Description("性质")]
            Property = 1,
            /// <summary>
            /// 类别
            /// </summary>
            [Description("类别")]
            Category = 2,
            /// <summary>
            /// 品牌
            /// </summary>
            [Description("品牌")]
            Brand = 3,
            ///// <summary>
            ///// 产品类型
            ///// </summary>
            //[Description("产品类型")]
            //ProductType = 4,
            /// <summary>
            /// 物料仓位
            /// </summary>
            [Description("物料仓位")]
            MaterielPosition = 5
        }
        #endregion

        #region 任务来源
        /// <summary>
        /// 任务来源
        /// </summary>
        [Flags]
        public enum TaskSourceType
        {
            /// <summary>
            /// 销售订单
            /// </summary>
            [Description("销售订单")]
            SalesOrder = 1,
            /// <summary>
            /// 生产任务单
            /// </summary>
            [Description("生产任务单")]
            TaskOrder = 2
        }
        #endregion

        #region 任务执行状态
        /// <summary>
        /// 任务来源
        /// </summary>
        [Flags]
        public enum TaskOrderStatus
        {
            /// <summary>
            /// 待计划
            /// </summary>
            [Description("待计划")]
            Pending = 1,
            /// <summary>
            /// 计划中
            /// </summary>
            [Description("计划中")]
            InPlan = 2,
            /// <summary>
            /// 部分完成
            /// </summary>
            [Description("部分完成")]
            CompletePart = 3,
            /// <summary>
            /// 已完成
            /// </summary>
            [Description("已完成")]
            Completed = 4,
            /// <summary>
            /// 已终止
            /// </summary>
            [Description("已终止")]
            Ended = 5
        }
        #endregion

        #region 工序状态
        /// <summary>
        /// 工序状态
        /// </summary>
        [Flags]
        public enum RepertoireLineStatus
        {
            /// <summary>
            /// 待物料
            /// </summary>
            [Description("待物料")]
            WaitMateriel = 0,
            /// <summary>
            /// 待分配
            /// </summary>
            [Description("待分配")]
            WaitAllot = 1,
            /// <summary>
            /// 分配完成
            /// </summary>
            [Description("分配完成")]
            AllotFinish = 2,
            /// <summary>
            /// 生产完成
            /// </summary>
            [Description("生产完成")]
            ProduceFinish = 3
        }
        #endregion

        #region 物料满足度
        public enum SatisfyRate
        {
            /// <summary>
            /// 待分配
            /// </summary>
            [Description("100%满足")]
            Finish = 100,
            /// <summary>
            /// 未满足
            /// </summary>
            [Description("未满足")]
            UNFinish = 0,
        }
        #endregion

        #region 计划明细执行状态
        /// <summary>
        /// 计划明细执行状态
        /// </summary>
        public enum PlanLineState
        {
            /// <summary>
            /// 待MRP计算
            /// </summary>
            [Description("待MRP计算")]
            WaitMRP = 0 ,

            /// <summary>
            /// 待生产
            /// </summary>
            [Description("待生产")]
            WaitProduce = 1,

            /// <summary>
            /// 生产中
            /// </summary>
            [Description("生产中")]
            Produceing = 2,

            /// <summary>
            /// 已完成
            /// </summary>
            [Description("已完成")]
            Finish = 3,
        }
        #endregion

        #region 物料属性
        /// <summary>
        /// 产品属性
        /// </summary>
        [Flags]
        public enum MaterielProperty
        {
            /// <summary>
            /// 产品
            /// </summary>
            [Description("产品")]
            Product = 5,
            /// <summary>
            /// 成品
            /// </summary>
            [Description("成品")]
            Finished = 1,
            /// <summary>
            /// 半成品
            /// </summary>
            [Description("半成品")]
            SemiFinished = 2,
            /// <summary>
            /// 原料
            /// </summary>
            [Description("原料")]
            Material = 3,
            /// <summary>
            /// 辅料
            /// </summary>
            [Description("辅料")]
            Accessories = 4
        }
        #endregion

        #region 退料类型
        /// <summary>
        /// 产品属性
        /// </summary>
        [Flags]
        public enum MaterielOrAbrasives
        {
            /// <summary>
            /// 物料
            /// </summary>
            [Description("物料")]
            Materiel = 0,
            /// <summary>
            /// 模具
            /// </summary>
            [Description("模具")]
            Abrasives = 1,
        }
        #endregion

        #region 物料清单执行状态出库状态
        /// <summary>
        /// 物料清单执行状态出库状态
        /// </summary>
        [Flags]
        public enum DeliveryState
        {
            /// <summary>
            /// 无状态
            /// </summary>
            [Description(" ")]
            None = 0,
            /// <summary>
            /// 待出库
            /// </summary>
            [Description("待出库")]
            StayOut = 1,
            /// <summary>
            /// 部分出库
            /// </summary>
            [Description("部分出库")]
            PartOut = 2,
            /// <summary>
            /// 已完成
            /// </summary>
            [Description("出库完成")]
            Complete = 3,
        }
        #endregion

        #region 是否配色
        /// <summary>
        /// 是否配颜料
        /// </summary>
        [Flags]
        public enum IsPigment
        {
            /// <summary>
            /// 已配色
            /// </summary>
            [Description("已配色")]
            Pigment = 1,
            /// <summary>
            /// 未配色
            /// </summary>
            [Description("未配色")]
            NotPigment = 0,
        }
        #endregion

        #region 出库物料类型(0子料 ,1上级工序物料)
        /// <summary>
        /// 出库物料类型(0 子料 ,1 上级工序物料  )
        /// </summary>
        [Flags]
        public enum OutMaterielType
        {
            /// <summary>
            /// 子料
            /// </summary>
            [Description("子料")]
            SonMateriel = 0,
            /// <summary>
            /// 上级工序物料
            /// </summary>
            [Description("上级工序物料")]
            UpMateriel = 1,
        }
        #endregion

        #region Bom单来源(0BOM 1包装 )
        /// <summary>
        /// Bom单来源(0BOM 1包装 )
        /// </summary>
        [Flags]
        public enum BomSource
        {
            /// <summary>
            /// Bom单
            /// </summary>
            [Description("Bom单")]
            Bom = 0,
            /// <summary>
            /// 包装
            /// </summary>
            [Description("包装")]
            Packaging = 1,
        }
        #endregion

        #region 良品次品( 0次品  1良品)
        public enum GoodOrSecondType
        {
            /// <summary>
            /// 良品
            /// </summary>
            [Description("次品")]
            Second = 0,
            /// <summary>
            /// 包装
            /// </summary>
            [Description("良品")]
            Good = 1,
        }


        #endregion

        #region 是否报工单
        public enum IsDailyWork
        {
            /// <summary>
            /// 是报工单
            /// </summary>
            [Description("是报工单")]
            IsDailyWork = 1,
            /// <summary>
            /// 不是报工单
            /// </summary>
            [Description("不是报工单")]
            NotDailyWork = 0,
        }


        #endregion
    }
}
