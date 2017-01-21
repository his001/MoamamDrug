using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using System.ComponentModel;

public partial class UserControls_ucStoreList : System.Web.UI.UserControl
{
    protected UserControlAddText _addText   = UserControlAddText.None;
    protected string _storeEventID          = "";

    public bool Enabled
    {
        set
        {
            ddlStoreList.Enabled = value;
        }
    }


    public int Width
    {
        set
        {
            ddlStoreList.Width = value;
        }
    }


    public UserControlAddText AddText
    {
        set
        {
            _addText = value;
        }
    }

    #region SelectedValue, SelectedIndex, ClientID, EventID
    public string SelectedStoreEventID
    {
        set
        {
            _storeEventID = value;
        }
    }

    public string SelectedValue
    {
        get
        {
            return ddlStoreList.SelectedValue;
        }
        set
        {
            ddlStoreList.SelectedValue = value;
        }
    }

    public int SelectedIndex
    {
        get
        {
            return ddlStoreList.SelectedIndex;
        }
        set
        {
            ddlStoreList.SelectedIndex = value;
        }
    }

    public string StoreListClientID
    {
        get 
        {
            return ddlStoreList.ClientID.ToString();
        }
    }

    override public string ClientID
    {
        get
        {
            return ddlStoreList.ClientID;
        }
    }
    #endregion



    protected void Page_Load(object sender, EventArgs e)
    {
        
    }


    protected void Application_Start()
    {
        Application["StoreList"] = StoreList.StoreLists();
    }


    public void LoadData()
    {
        Application_Start();

        DataSet ds = new DataSet();

        if (Application["StoreList"] != null)
        {
            List<Store> li = (List<Store>)Application["StoreList"];

            //convert array to datatable
            ds.Tables.Add(ConvertToDataTable(li));

            SetComboBox(ds, "STORE", "STORE_NAME");
            
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    public override void Focus()
    {
        ddlStoreList.Focus();
    }

    #region protected SetComboBox
    protected void SetComboBox(DataSet ds, string textField, string valueField)
    {
        if (_addText == UserControlAddText.All)
            SetComboBox(ds, textField, valueField, "전체");
        else if (_addText == UserControlAddText.Select)
            SetComboBox(ds, textField, valueField, "선택");
        else
            SetComboBox(ds, textField, valueField, null);
    }
    
    
    protected void SetComboBox(DataSet ds, string valueField, string textField, string addItem)
    {
        if (ds != null)
        {
            ddlStoreList.Items.Clear();
            ddlStoreList.DataSource     = ds;
            ddlStoreList.DataTextField  = textField;
            ddlStoreList.DataValueField = valueField;
            ddlStoreList.DataBind();
        }

        if (!string.IsNullOrEmpty(addItem))
            ddlStoreList.Items.Insert(0, new ListItem(addItem, ""));
    }


    protected void SetComboBox(string addItem)
    {
        SetComboBox(null, null, null, addItem);
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
    #endregion

}//END CLASS