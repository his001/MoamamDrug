using System;
using System.Data;

namespace Moamam.Data.Site.Transfer
{
    public class TransferAllocationData
    {
        public string CMDCRUD { get; set; }
        public string WEEK_NO { get; set; }
        public string FROM_LOC { get; set; }
        public string TO_LOC { get; set; }
        public string ITEM { get; set; }
        public int TRF_QTY_CONFIRM { get; set; }
        public string CONFIRM { get; set; }
        public string TR_TYPE { get; set; }
        public string ROW_CNT { get; set; }
        public string PAGE_NUM { get; set; }
    }


    public class ExecludItemData
    {
        public string ITEM { get; set; }
        public string ACTION_TYPE { get; set; }
        public string CREATE_USER { get; set; }
    }

}