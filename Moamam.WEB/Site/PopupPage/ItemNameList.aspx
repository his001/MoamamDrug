 <%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage_Popup.master" AutoEventWireup="true" CodeFile="ItemNameList.aspx.cs" Inherits="Site_PopupPage_ItemNameList" %>
<%@ Register Src="~/UserControls/ucPaging.ascx" TagName="ucPaging" TagPrefix="uc1" %> 
<asp:Content ID="Content3" ContentPlaceHolderID="head" Runat="Server">
    
<script type="text/javascript">
    ///////////////////////////////////////////////////////////////////////////////////////////////
    //                        아이템 검색                                                        //    
    /////////////////////////////////////////////////////////////////////////////////////////////// 
    function SerchItemNameList() {
        var txt = document.getElementById("txtSerchName");
        var length = String(txt.value).length;
        if (length < 2) {
            alert("조회할 아이템 명칭을 두자리 이상 입력하세요.");
            $("#txtSerchName").focus();
            $("#txtSerchName").select();
            return false;
        }
        //ShowLoading();
        return true;
    }
    
    $(document).ready(function () { 
        $("#txtSerchName").focus();
        ///site/PopupPage/ItemNameList.aspx
    }); 
 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div  style="width:420px;height:330px;" class="modalPopup">
        <asp:HiddenField ID="hdnDataExists" runat="server" />  
        <asp:Button ID="btnShowPopup1" runat="server" Style="display:none;" />  
        <div class="PopupPanel">
            <table class="PopupTabelTitle">
                <tr>
                    <td class="Popup_td">&nbsp;아이템 검색</td>
                    <td class="t_r" style="padding:0;margin:0;">
                        <asp:ImageButton ID="btnTsfCancel1" runat="server" Height="20px" ImageUrl="~/Images/Common/grn_pp_cls_btn_over.png" OnClientClick="javascript:window.close();return false;" />
                    </td>

                </tr>
            </table>
        </div> 
        
        <br />
        <table >
                <tr > 
                    <td colspan="2"><asp:DropDownList runat="server" ID="ddlName" Height="30">
                                        <asp:ListItem Text="아이템명" Value="name"></asp:ListItem>
                                        <asp:ListItem Text="아이템코드" Value="code"></asp:ListItem>
                                    </asp:DropDownList> 
                        <hpf:NTextBox  ID="txtSerchName" runat="server" CssClass="input hangleDefault" ClientIDMode="Static" style="text-align:center;width:200px;height:30px;" TabIndex="1"></hpf:NTextBox>
                        <asp:Button ID="btnSerchItemName" runat="server" CssClass="btn btn_inquiry" OnClick="btnSerchItemName_Click" Text="조회" OnClientClick="return SerchItemNameList();"/>
                        <asp:Button ID="btnCancel1" ClientIDMode="Static" runat="server" Text="닫기" CssClass="btn btn_default" OnClientClick="javascript:window.close();return false;" />
                        <asp:TextBox ID="hdItemName" runat="server" ClientIDMode="Static" style="display:none;"/>
                    </td>
                </tr> 
        </table> 
        <br />             

        
        <div style="width:100%;text-align:center;height:200px;overflow-x:hidden;overflow-y:auto;">

	    <article id="grid">                          
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                <ContentTemplate>
                        <table>
                            <colgroup>  
                                <col style="width:80px" />
                                <col/> 
                            </colgroup>
                    <asp:Repeater ID="rptTransfer1" runat="server">  
                        <ItemTemplate> 
                            <tr> 
                                <td class="t_c"><%#nEval("ITEM")%></td>
                                <td style="color:blue;text-align:left;"><a href="javascript:opener.SerchItemClick('<%#nEval("ITEM")%>','<%#nEval("ITEM_NAME")%>','<%#nEval("SECTION")%>','');window.close();"> 
                                    &nbsp;<asp:Label ID="lbLinkValue" runat="server" ForeColor="Blue"> <%#nEval("ITEM_NAME")%></asp:Label>
                                    </a>
                                </td>
                            </tr> 
                        </ItemTemplate> 
                    </asp:Repeater> 
                            <tr>
                                    <td colspan="2"><%=hdnDataExists.Value %></td> 
                            </tr>
                            </table>  
                </ContentTemplate> 
            </asp:UpdatePanel >          
	    </article>  
        </div> 


        <div class="pager"> 
            <uc1:ucPaging ID="ucPaging" OnSelEvent="ucPaging_SelEvent" runat="server" />
        </div>
        <div style="width:100%;text-align:center;"> 
            <hr /> 
        </div>
                

    </div>

</asp:Content>