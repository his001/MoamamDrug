using System.Data;
using System.Collections;
using System.Collections.Generic;
using Moamam.Lib;
using Moamam.Data.Common;

public class CommonBiz
{
    /// <summary>
    /// 로그인 사용자 권한에 해당하는 대메뉴 목록 조회
    /// </summary>
    /// <param name="userUserGroupCode">UserGroupCode</param>
    /// <returns>DataSet</returns>
    public static DataSet GetUserMenuGroup(string userUserGroupCode)
    {
        return (new SiteMenu()).GetMenuGroup(userUserGroupCode);
    }



    /// <summary>
    /// 로그인 사용자 권한에 해당하는 대메뉴 하위 소메뉴 목록 조회
    /// </summary>
    /// <param name="PmenuGroupCd"></param>
    /// <param name="userUserGroupCode"></param>
    /// <returns></returns>
    public static DataSet GetUserMenuSub(string PmenuGroupCd)
    {
        return (new SiteMenu()).GetSubMenu(PmenuGroupCd);
    }



    /// <summary>
    /// 로그인 사용자 권한에 접근가능 URL목록 전체 조회
    /// </summary>
    /// <param name="userUserGroupCode">UserGroupCode</param>
    /// <returns>DataSet</returns>
    public static DataSet GetUserUseMenuList(string userUserGroupCode)
    {
        return (new SiteUser()).GetUserUseUrl(userUserGroupCode); ;

    }



    /// <summary>
    /// 로그인 사용자 권한에 따라 첫번째 보여줄 URL 조회
    /// </summary>
    /// <param name="dataPac">UserInfo</param>
    /// <returns>DataSet</returns>
    public static DataSet GetUserFirstMenuUrl(UserInfo dataPac)
    {
        return (new SiteUser()).GetUserLoginFirstUrl(dataPac.UserGroupCode);
    }



    public static Hashtable GetGrantData(string menuCd, string userGroup)
    {
        DataSet ds = (new SiteGrant()).SelectGrant(menuCd, userGroup);

        Hashtable security = new Hashtable();

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1];
            foreach (DataColumn dc in ds.Tables[0].Columns)
                security.Add(dc.ColumnName.ToUpper(), dr[dc.ColumnName]);
        }
        return security;
    }

    /// <summary>
    /// 최종업데이트정보 가져오기
    /// </summary>
    /// <param name="userUserGroupCode">UserGroupCode</param>
    /// <returns>DataSet</returns>
    public static DataSet GetUpdateDate()
    {
        return (new SiteMaster()).GetUpdateDate(); ;

    }

   
}//END CLASS