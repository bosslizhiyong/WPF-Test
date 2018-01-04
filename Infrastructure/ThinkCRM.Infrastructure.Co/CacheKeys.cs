using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ThinkCRM.Infrastructure.Co
{
    /// <summary>
    /// 缓存键值
    /// </summary>
    [Flags]
    public enum CacheKeys
    {
        /// <summary>
        /// 区域
        /// </summary>
        [Description("区域")]
        Region,


        /// <summary>
        /// 部门
        /// </summary>
        [Description("部门")]
        Department,
        /// <summary>
        /// 职务
        /// </summary>
        [Description("职务")]
        Post,
        /// <summary>
        /// 部门职务
        /// </summary>
        [Description("部门职务")]
        DepartmentPost,
        /// <summary>
        /// 员工
        /// </summary>
        [Description("员工")]
        Employee,



        /// <summary>
        /// 往来单位类别
        /// </summary>
        [Description("往来单位类别")]
        BusinessCategory,
        /// <summary>
        /// 往来单位
        /// </summary>
        [Description("往来单位")]
        Business,



        /// <summary>
        /// 仓库类别
        /// </summary>
        [Description("仓库类别")]
        StorageCategory,
        /// <summary>
        /// 仓库
        /// </summary>
        [Description("仓库")]
        Storage,


        /// <summary>
        /// 产品类别
        /// </summary>
        [Description("产品类别")]
        ProductCategory,
        /// <summary>
        /// 产品单位
        /// </summary>
        [Description("产品单位")]
        ProductUnit,
        /// <summary>
        /// 产品规格
        /// </summary>
        [Description("产品规格")]
        ProductSize,
        /// <summary>
        /// 产品品牌
        /// </summary>
        [Description("产品品牌")]
        ProductBrand,
        /// <summary>
        /// 产品
        /// </summary>
        [Description("产品")]
        Product,

        /// <summary>
        /// 器械类型
        /// </summary>
        [Description("机械类型")]
        MechanicalType,
        /// <summary>
        /// 器械规格表
        /// </summary>
        [Description("机械规格表")]
        MechanicalSize,
        /// <summary>
        /// 班次表
        /// </summary>
        [Description("班次表")]
        Shift,
        /// <summary>
        /// 机器管理表
        /// </summary>
        [Description("机器管理表")]
        MachineManagement,
        
        /// <summary>
        /// 银行
        /// </summary>
        [Description("银行")]
        Bank,
        /// <summary>
        /// 币种
        /// </summary>
        [Description("币种")]
        Currency,
        /// <summary>
        /// 收付款账户
        /// </summary>
        [Description("收付款账户")]
        Account,


        /// <summary>
        /// 车辆
        /// </summary>
        [Description("车辆")]
        Vehicle,
        /// <summary>
        /// 标签
        /// </summary>
        [Description("标签")]
        Tags,
        /// <summary>
        /// 系统参数
        /// </summary>
        [Description("系统参数")]
        Parameter,
        /// <summary>
        /// 服务代理
        /// </summary>
        [Description("服务代理")]
        Proxy,
        /// <summary>
        /// 打印模板
        /// </summary>
        [Description("打印模板")]
        PrintTemplate,
        /// <summary>
        /// 打印模板配置
        /// </summary>
        [Description("打印模板配置")]
        PrintTemplateColumns,



        /// <summary>
        /// 收款条目
        /// </summary>
        [Description("收款条目")]
        RecEntry,
        /// <summary>
        /// 付款条目
        /// </summary>
        [Description("付款条目")]
        PayEntry,


        /// <summary>
        /// 产品库存
        /// </summary>
        [Description("产品库存")]
        ProductStock,

        /// <summary>
        /// 产品库存汇总
        /// </summary>
        [Description("产品库存汇总")]
        ProductStockSummary,

        /// <summary>
        /// 物料库存
        /// </summary>
        [Description("物料库存")]
        MaterielStock,

        /// <summary>
        /// 物料库存
        /// </summary>
        [Description("物料库存汇总")]
        MaterielStockSummary,

        /// <summary>
        /// 产品单位价格
        /// </summary>
        [Description("产品单位价格")]
        ProductUnitPrice,


        ///// <summary>
        ///// 销售订单行(已审核未完成出库的)
        ///// </summary>
        //[Description("销售订单行(已审核未完成出库的)")]
        //SalesOrderLine,
        ///// <summary>
        ///// 采购订单行(已审核未完成入库的)
        ///// </summary>
        //[Description("采购订单行(已审核未完成入库的)")]
        //PurchaseOrderLine,
        ///// <summary>
        /////盘点生成单行(已审核未录入的)
        ///// </summary>
        //[Description("盘点生成单行(已审核未录入的)")]
        //CreateInvOrderLine,


        /// <summary>
        /// 用户组(用户)区域关系表
        /// </summary>
        [Description("用户组(用户)区域关系表")]
        UserGroupRegionRef,
        /// <summary>
        /// 用户组(用户)区域关系表
        /// </summary>
        [Description("用户组(用户)往来单位系表")]
        UserGroupBusinessRef,
         /// <summary>
        /// 获取用户组(用户)仓库关系
        /// </summary>
        [Description("用户组(用户)仓库关系")]
        UserGroupStorageRef,


        /// <summary>
        /// 物料类别
        /// </summary>
        [Description("物料类别")]
        MaterielCategory,
        /// <summary>
        /// 物料单位
        /// </summary>
        [Description("物料单位")]
        MaterielUnit,
        /// <summary>
        /// 物料规格
        /// </summary>
        [Description("物料规格")]
        MaterielSize,
        /// <summary>
        /// 物料品牌
        /// </summary>
        [Description("物料品牌")]
        MaterielBrand,

   
        /// <summary>
        /// 物料信息
        /// </summary>
        [Description("物料信息")]
        Materiel,
        /// <summary>
        /// 物料附件
        /// </summary>
        [Description("物料附件")]
        MaterielFile,


        /// <summary>
        /// 物料单位价格
        /// </summary>
        [Description("物料单位价格")]
        MaterielUnitPrice,


        /// <summary>
        /// 颜料类别
        /// </summary>
        [Description("颜料类别")]
        PigmentCategory,
        /// <summary>
        /// 颜料信息
        /// </summary>
        [Description("颜料信息")]
        Pigment,


        /// <summary>
        /// 工序管理
        /// </summary>
        [Description("工序管理")]
        Procedure,
        /// <summary>
        /// 流程管理
        /// </summary>
        [Description("流程管理")]
        Flow,

        /// <summary>
        /// 包装方案
        /// </summary>
        [Description("包装方案")]
        Packaging,

        /// <summary>
        /// 包装方案
        /// </summary>
        [Description("模具管理")]
        Mould
       

    }

    /// <summary>
    /// 缓存权限键值
    /// </summary>
    [Flags]
    public enum CacheAuthorityKeys
    {
        /// <summary>
        /// 供应商
        /// </summary>
        [Description("供应商")]
        Supplier,
        /// <summary>
        /// 客户
        /// </summary>
        [Description("客户")]
        Customer,
        /// <summary>
        /// 仓库
        /// </summary>
        [Description("仓库")]
        Storage,
        /// <summary>
        /// 字段
        /// </summary>
        [Description("字段")]
        Field,
        /// <summary>
        /// 操作
        /// </summary>
        [Description("操作")]
        Operate,
        /// <summary>
        /// 部门员工
        /// </summary>
        [Description("部门员工")]
        DepartmentEmployee
    }
}
