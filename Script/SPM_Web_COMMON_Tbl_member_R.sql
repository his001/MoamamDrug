/******************************************************************
*          시스템명	: PHS Simple FrameWork
*          명칭		: 로그인 조회
*          인수		:
*          Output    :
* exec SPM_Web_COMMON_Tbl_member_R @UserID='his001'
******************************************************************
*          작성일자              작성자                내용
******************************************************************
*        2016-12-29             박희수              초기 구현
******************************************************************/
ALTER PROCEDURE [dbo].[SPM_Web_COMMON_Tbl_member_R]
(
	@UserID nvarchar(100)
	--,@UserPwd varchar(50)
)
AS
BEGIN

	SELECT A.idx ,A.UserID ,A.UserPwd ,A.UserName ,A.UserGroup , B.CodeName AS UserGroupName
		,A.UserHP ,A.UserRegIP ,A.regdate ,A.regid ,A.regip
		,A.moddate ,A.modid ,A.modip
	FROM Tbl_member A WITH(NOLOCK)
	inner join Tbl_Code B WITH(NOLOCK) on B.CodeGroup='UserGroup' AND A.UserGroup = B.code
	WHERE UserID = @UserID

END