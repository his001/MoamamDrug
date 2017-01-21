using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moamam.Data.Site.MasterMain
{
    public class TransferData
    {
    }
    /// <summary>
    /// 상품 마스터
    /// </summary>
    public class ProductExecludItemData
    {
        public string ORDER_GROUP { get; set; }
        public string ITEM { get; set; }
        public string ADO_START_DATE { get; set; }
        public string LAYER_SIZE { get; set; }
        public string PALLET_SIZE { get; set; }
        public string ACTION_TYPE { get; set; }
        public string CREATE_USER { get; set; }
    }

    /// <summary>
    /// 상품 마스터 저장
    /// </summary>
    public class ProductInsert
    {
        public string ProductCode { get; set; }
        public string OrderGroup { get; set; }
        public string Wh { get; set; }
        public string ADO_START_DATE { get; set; }
        public string Layersize { get; set; }
        public string Palletsize { get; set; }
        public string CMDCRUD { get; set; }
        public string UserId { get; set; }
    }
    /// <summary>
    /// 협력업체 마스터
    /// </summary>
    public class CooperativeExecludItemData
    {
        public string SUPPLIER { get; set; }
        public string SUP_TERM_ID { get; set; }
        public string ORDER_GROUP { get; set; }
        public string SUP_START_DATE { get; set; }
        public string SUP_END_DATE { get; set; }
        public string W_MON { get; set; }
        public string W_TUE { get; set; }
        public string W_WED { get; set; }
        public string W_THU { get; set; }
        public string W_FRI { get; set; }
        public string W_SAT { get; set; }
        public string W_SUN { get; set; }
        public string L_MON { get; set; }
        public string L_TUE { get; set; }
        public string L_WED { get; set; }
        public string L_THU { get; set; }
        public string L_FRI { get; set; }
        public string L_SAT { get; set; }
        public string L_SUN { get; set; }

        public string CMDCRUD { get; set; }
        public string UserId { get; set; }
    }

    /// <summary>
    /// 협력업체 마스터 저장
    /// </summary>
    public class CooperativeInsert
    {
        public string SUPPLIER { get; set; }
        public string SUP_TERM_ID { get; set; }
        public string ORDER_GROUP { get; set; }
        public string SUP_START_DATE { get; set; }
        public string SUP_END_DATE { get; set; }
        public string W_MON { get; set; }
        public string W_TUE { get; set; }
        public string W_WED { get; set; }
        public string W_THU { get; set; }
        public string W_FRI { get; set; }
        public string W_SAT { get; set; }
        public string W_SUN { get; set; }
        public string L_MON { get; set; }
        public string L_TUE { get; set; }
        public string L_WED { get; set; }
        public string L_THU { get; set; }
        public string L_FRI { get; set; }
        public string L_SAT { get; set; }
        public string L_SUN { get; set; }
        public string WH { get; set; }
        public string CMDCRUD { get; set; }
        public string UserId { get; set; }
    }
    /// <summary>
    ///  Rounding 마스터
    ///  ITEM,RUD_ID,RUD_LEVEL,RUD_TERM_ID,RUD_START_DATE,RUD_END_DATE
    /// </summary>
    public class RoundingExecludItemData
    {
        public string ITEM { get; set; }
        public string RUD_ID { get; set; }
        public string RUD_LEVEL { get; set; }
        public string RUD_TERM_ID { get; set; }
        public string RUD_START_DATE { get; set; }
        public string RUD_END_DATE { get; set; }
        public string ACTION_TYPE { get; set; }
        public string CREATE_USER { get; set; }
    }

    /// <summary>
    /// Rounding 마스터 저장
    /// </summary>
    public class RoundingInsert
    {
        public string ITEM { get; set; }
        public string WH { get; set; }
        public string RUD_ID { get; set; }
        public string RUD_LEVEL { get; set; }
        public string RUD_TERM_ID { get; set; }
        public string RUD_START_DATE { get; set; }
        public string RUD_END_DATE { get; set; }
        public string CMDCRUD { get; set; }
        public string UserId { get; set; }
    }

    /// <summary>
    /// Safdety Stock 
    /// </summary>
    public class SafetyStockExecludItemData
    {
        public string ITEM { get; set; }
        public string RUD_ID { get; set; }
        public string RUD_LEVEL { get; set; }
        public string RUD_TERM_ID { get; set; }
        public string RUD_START_DATE { get; set; }
        public string RUD_END_DATE { get; set; }
        public string ACTION_TYPE { get; set; }
        public string CREATE_USER { get; set; }
    }
    /// <summary>
    /// Safdety Stock  저장
    /// </summary>
    public class SafetyStockInsert
    {
        public string ITEM { get; set; }
        public string WH { get; set; }
        public string SFS_FIXED_VALUE { get; set; }
        public string SFS_RATE { get; set; }
        public string SFS_TERM_ID { get; set; }
        public string SFS_START_DATE { get; set; }
        public string SFS_END_DATE { get; set; }
        public string CMDCRUD { get; set; }
        public string UserId { get; set; }
    }

    /// <summary>
    /// 발주방법
    /// </summary>
    public class OrderMethodExecludItemData
    {
        public string ITEM { get; set; }
        public string ACTION_TYPE { get; set; }
        public string CREATE_USER { get; set; }
    }
    /// <summary>
    /// 발주방법  저장
    /// </summary>
    public class OrderMethodInsert
    {
        public string ITEM { get; set; }
        public string WH { get; set; }
        public string REPL_METHOD { get; set; } 
        public string CMDCRUD { get; set; }
        public string UserId { get; set; }
    }

    /// <summary>
    /// 수동발주량 리뷰 저장
    /// </summary>
    public class MenualOrderInsert
    {
        public string ITEM { get; set; }
        public string WH { get; set; }
        public string AOQ_QTY { get; set; }
        public string STATUS { get; set; }
        public string CMDCRUD { get; set; }
        public string UserId { get; set; }
    }
}
