using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using Moamam.Lib;
using System.ComponentModel;

public partial class Site_UserControls_ucStoreFromToTzGroup : System.Web.UI.UserControl
{
    protected UserControlAddText _addText   = UserControlAddText.None;
    protected string _fromStoreEventID      = "";
    protected string _toStoreEventID        = "";


    #region getter setter
    public UserControlAddText AddText
    {
        set
        {
            _addText = value;
        }
    }

    public bool AutoPostBack
    {
        set
        {
            ddlFromStoreList.AutoPostBack = value;
        }
    }

    public string SelectedFromStoreEventID
    {
        set
        {
            _fromStoreEventID = value;
        }
    }
    public string SelectedToStoreEventID
    {
        set
        {
            _toStoreEventID = value;
        }
    }
    
    //ClientID
    public string ddlFromStoreClientID
    {
        get
        {
            return ddlFromStoreList.ClientID.ToString();
        }
    }
    public string ddlToStoreClientID
    {
        get
        {
            return ddlToStoreList.ClientID.ToString();
        }
    }
    
    //Enabled
    public bool ddlFromStoreEnabled
    {
        set
        {
            ddlFromStoreList.Enabled = value;
        }
    }
    public bool ddlToStoreEnabled
    {
        set
        {
            ddlToStoreList.Enabled = value;
        }
    }

    //SelectedValue
    public string ddlFromStoreSelectedValue
    {
        get
        {
            return ddlFromStoreList.SelectedValue.ToString();
        }
        set
        {
            ddlFromStoreList.SelectedValue = value;
        }
    }
    public string ddlToStoreSelectedValue
    {
        get
        {
            return ddlToStoreList.SelectedValue.ToString();
        }
        set
        {
            ddlToStoreList.SelectedValue = value;
        }
    }

    //SelectedIndex
    public int ddlFromStoreSelectedIndex
    {
        get
        {
            return ddlFromStoreList.SelectedIndex;
        }
        set
        {
            ddlFromStoreList.SelectedIndex = value;
        }
    }
    public int ddlToStoreSelectedIndex
    {
        get
        {
            return ddlToStoreList.SelectedIndex;
        }
        set
        {
            ddlToStoreList.SelectedIndex = value;
        }
    }
    #endregion getter setter


    #region Page_Load / OnInit
    protected void Page_Load(object sender, EventArgs e)
    {
        //
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }
    #endregion Page_Load / OnInit


    protected void Application_Start()
    {
        Application["FromStoreList"]    = StoreList.StoreLists();
        Application["ToStoreList"]      = StoreList.StoreLists();
    }


    public void LoadData()
    {
        Application_Start();

        if (Application["FromStoreList"] != null)
        {
            DataSet ds = new DataSet();
            List<Store> li = (List<Store>)Application["FromStoreList"];

            //convert array to datatable
            ds.Tables.Add(ConvertToDataTable(li));

            SetFromComboBox(ds, "STORE", "STORE_NAME");
        }


        if (Application["ToStoreList"] != null)
        {
            DataSet ds = new DataSet();
            List<Store> li = (List<Store>)Application["ToStoreList"];

            //convert array to datatable
            ds.Tables.Add(ConvertToDataTable(li));

            SetToComboBox(ds, "STORE", "STORE_NAME");
        }
    }


    #region FromSetComboBox
    protected void SetFromComboBox(DataSet ds, string textField, string valueField)
    {
        if (_addText == UserControlAddText.All)
            SetFromComboBox(ds, textField, valueField, "전체");
        else if (_addText == UserControlAddText.Select)
            SetFromComboBox(ds, textField, valueField, "선택");
        else
            SetFromComboBox(ds, textField, valueField, null);
    }


    protected void SetFromComboBox(DataSet ds, string valueField, string textField, string addItem)
    {
        if (ds != null)
        {
            ddlFromStoreList.Items.Clear();
            ddlFromStoreList.DataSource = ds;
            ddlFromStoreList.DataTextField = textField;
            ddlFromStoreList.DataValueField = valueField;
            ddlFromStoreList.DataBind();
        }

        if (!string.IsNullOrEmpty(addItem))
            ddlFromStoreList.Items.Insert(0, new ListItem(addItem, ""));
    }


    protected void SetFromComboBox(string addItem)
    {
        SetFromComboBox(null, null, null, addItem);
    }
    #endregion FromSetComboBox

    
    #region ToSetComboBox
    protected void SetToComboBox(DataSet ds, string textField, string valueField)
    {
        if (_addText == UserControlAddText.All)
            SetToComboBox(ds, textField, valueField, "전체");
        else if (_addText == UserControlAddText.Select)
            SetToComboBox(ds, textField, valueField, "선택");
        else
            SetToComboBox(ds, textField, valueField, null);
    }


    protected void SetToComboBox(DataSet ds, string valueField, string textField, string addItem)
    {
        if (ds != null)
        {
            ddlToStoreList.Items.Clear();
            ddlToStoreList.DataSource = ds;
            ddlToStoreList.DataTextField = textField;
            ddlToStoreList.DataValueField = valueField;
            ddlToStoreList.DataBind();
        }

        if (!string.IsNullOrEmpty(addItem))
            ddlToStoreList.Items.Insert(0, new ListItem(addItem, ""));
    }


    protected void SetToComboBox(string addItem)
    {
        SetToComboBox(null, null, null, addItem);
    }
    #endregion ToSetComboBox


    

    protected void ddlFromStoreList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strStore = ddlFromStoreList.SelectedValue;

        Application["ToStoreList"] = StoreList.GetStoreListTZGroup(strStore);

        if (Application["ToStoreList"] != null)
        {
            DataSet ds = new DataSet();
            List<Store> li = (List<Store>)Application["ToStoreList"];

            //convert array to datatable
            ds.Tables.Add(ConvertToDataTable(li));

            SetToComboBox(ds, "STORE", "STORE_NAME");
        }
    }




    /// <summary>
    /// List 형태를 데이타 테이블로 변환
    /// </summary>
    static public DataTable ConvertToDataTable<T>(IList<T> data)
    {
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new DataTable();

        foreach (PropertyDescriptor prop in properties)
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

        foreach (T item in data)
        {
            if (item != null)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
        }

        return table;
    }



}//END CLASS