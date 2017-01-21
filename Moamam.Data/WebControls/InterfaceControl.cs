#region Using Directives

using System.Data;


#endregion

namespace Moamam.Data.WebControls
{
    #region Enum

    public enum CommandParameterType
    {
        NotSet,
        Insert,
        Update,
        Delete,
        Select
    }

    public enum SecurityType
    {
        NotSet,
        Inquiry,
        Save,
        Report,//리포트
        Approval,//승인
    }

    #endregion


    /// <summary>
    /// 컨트롤파라미터 Interface
    /// </summary>
    public interface IControlParameter
    {
        bool SetEmptyToNull { get; set; }
        SqlDbType ColumnDbType { get; set; }
        int ColumnLength { get; set; }

        bool IsInsertParameter { get; set; }
        string InsertParameterName { get; set; }
        ParameterDirection InsertDirection { get; set; }

        bool IsUpdateParameter { get; set; }
        string UpdateParameterName { get; set; }
        ParameterDirection UpdateDirection { get; set; }

        bool IsDeleteParameter { get; set; }
        string DeleteParameterName { get; set; }
        ParameterDirection DeleteDirection { get; set; }

        bool IsSelectParameter { get; set; }
        string SelectParameterName { get; set; }
        ParameterDirection SelectDirection { get; set; }

        bool IsBindParameter { get; set; }
        string BindParameterName { get; set; }

        void SetControlData(object o);
        object GetControlData();
        void ClearControlData();
    }

    public interface IButtonExtender
    {
        bool IsConfirm { get; set; }
        string ConfirmMessage { get; set; }
    }

    public interface IControlSecurity
    {
        SecurityType SecurityType { get; set; }
    }
}
