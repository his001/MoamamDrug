/******************************************************************
*          시스템명	: PHS Simple FrameWork
*          명칭		: 회원가입 저장
*          인수		:
*          Output    :
* exec SPM_Web_COMMON_Tbl_member_S @UserID='his001',@UserPwd='545434',@UserName='박희수'
******************************************************************
*          작성일자              작성자                내용
******************************************************************
*        2017-01-02             박희수              초기 구현
******************************************************************/
CREATE PROCEDURE [dbo].[SPM_Web_COMMON_Tbl_member_S]
(
	@UserID nvarchar(100)
	,@UserPwd varchar(50)
	,@UserName nvarchar(50)
)
AS
BEGIN TRAN

	INSERT INTO Tbl_member (UserID ,UserPwd ,UserName)
	SELECT @UserID, @UserPwd, @UserName

IF(@@ERROR<>0)
	BEGIN
		ROLLBACK TRAN
		SELECT '' AS RESULT
	END
ELSE
	BEGIN
		COMMIT TRAN
		SELECT 'OK' AS RESULT
	END
