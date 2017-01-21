/******************************************************************
*          시스템명	: PHS Simple FrameWork
*          명칭		: 공통코드 SELECTBOX 용(조회)
*          인수		:
*          Output    :
* exec SPM_Web_COMMON_Tbl_Code_R @CodeGroup=''
******************************************************************
*          작성일자              작성자                내용
******************************************************************
*        2016-12-29             박희수              초기 구현
******************************************************************/
CREATE PROCEDURE [dbo].[SPM_Web_COMMON_Tbl_Code_R]
(
	@CodeGroup varchar(50) =''
)
AS
BEGIN

	SELECT CodeGroup, Code, CodeName, images, regdate, regid, regip, moddate, modid, modip
	FROM Tbl_Code WITH(NOLOCK)
	WHERE CodeGroup like '%'+isnull(@CodeGroup ,'')+'%'
	Order by Code

END