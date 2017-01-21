using System;
using System.Web.UI.WebControls;
using System.Text;

public partial class Site_UserControls_ucPaging : System.Web.UI.UserControl
{
    public event EventHandler SelEvent;


    public int PageNo
    {
        get { return Convert.ToInt32(hidPageNo.Value); }
        set { hidPageNo.Value = Convert.ToString(value); }
    }

    public int RowCount
    {
        get { return Convert.ToInt32(hidRowCnt.Value); }
        set { hidRowCnt.Value = Convert.ToString(value); }
    }

    public int TotalCount
    {
        get { return Convert.ToInt32(hidTotal.Value); }
        set { hidTotal.Value = Convert.ToString(value); }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Next/Prev 이미지 출력여부를 위하여 추가.
            int nCurrentPage = PageNo;
            nCurrentPage = (nCurrentPage - 1) / 10 * 10;
            
        }
    }

    protected void Page_PreRender()
    { 
        litPageing.Text = Pageing(); 
    }

    string Pageing()
    {
        lblCount.Text = "Total " + TotalCount.ToString() + " 건";

        if (TotalCount > 0)
        {
            int currentPage = PageNo;
            int recordSize = RowCount;
            int totalRecord = TotalCount;
            int blockSize = 10;

            int screenStartPageIndex = currentPage - ((currentPage - 1) % blockSize); //시작점
            int lastPage = (TotalCount % RowCount) == 0 ? (TotalCount / RowCount) : (TotalCount / RowCount) + 1;
            int screenEndPageIndex = screenStartPageIndex + (blockSize - 1);// 끝

            if (screenEndPageIndex == 0)
                screenEndPageIndex = 1;

            if (lastPage == 0 && totalRecord > 0)
                lastPage = 1;

            if (lastPage < screenEndPageIndex)
                screenEndPageIndex = lastPage;

            StringBuilder sb = new StringBuilder();

                            if (currentPage == 1)
                            {
                                sb.AppendLine("<button type=\"button\" class=\"start\"></button>");
                            }
                            else if (screenStartPageIndex == 1 && screenEndPageIndex == 1)
                                sb.AppendLine("<button type=\"button\" class=\"start\"></button>");
                            else
                                sb.AppendLine("<button type=\"button\" class=\"start\" onclick=\"Page_Url(1);\"></button>");

                            //현재 블록
                            if (screenStartPageIndex > 1)
                            {
                                sb.AppendLine("<button type=\"button\" class=\"prev\" onclick=\"Page_Url(" + (screenStartPageIndex - 1) + ")\"></button>");
                            }
                            else
                            {
                                sb.AppendLine("<button type=\"button\" class=\"prev\"></button>");
                            }



            sb.AppendLine("<p>");
            for (int i = screenStartPageIndex; i <= screenEndPageIndex; i++)
            {
                if (i == screenStartPageIndex)
                {
                    if ((i == currentPage && i != screenEndPageIndex) || i == screenEndPageIndex)
                        sb.AppendLine("<b><a class=\"frst strong\"  >" + (i) + "</a></b>");
                    else
                        sb.AppendLine("<a class=\"frst\" href=\"#\" onclick=\"Page_Url(" + (i) + ");\">" + (i) + "</a>");
                }
                else
                {
                    if (i == currentPage)
                        sb.AppendLine("<b><a  class=\"frst strong\" >" + (i) + "</a></b>");
                    else
                        sb.AppendLine("<a  href=\"#\" onclick=\"Page_Url(" + (i) + ");\">" + (i) + "</a>");
                }
            }
            sb.AppendLine("</p>");


                            if (screenEndPageIndex != lastPage)
                                sb.AppendLine("<button type=\"button\" class=\"next\" onclick=\"Page_Url(" + (screenEndPageIndex + 1) + ")\"></button>");
                            else
                                sb.AppendLine("<button type=\"button\" class=\"next\"></button>");

                            if (currentPage < lastPage)
                                sb.AppendLine("<button type=\"button\" class=\"end\" onclick=\"Page_Url(" + lastPage + ")\"></button>");
                            else
                                sb.AppendLine("<button type=\"button\" class=\"end\"></button>");


            return sb.ToString();
        }
        else
        {
            StringBuilder sb = new StringBuilder();

            return sb.ToString();
        }
    }

    protected void btnPageing_Click(object sender, EventArgs e)
    {
        // Next/Prev 이미지 출력여부를 위하여 추가.
        int nCurrentPage = PageNo;
        nCurrentPage = (nCurrentPage - 1) / 10 * 10;

        SelEvent(this.SelEvent, null);
    }
}
