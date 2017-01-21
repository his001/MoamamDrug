/******************************************************************
*          시스템명	: PHS Simple FrameWork
*          명칭		: 회원가입 저장
*          인수		:
*          Output    :
* exec SPM_Web_COMMON_Tbl_member_Pwd_S @UserID='his001',@UserPwd='545434',@UserName='test1234'
******************************************************************
*          작성일자              작성자                내용
******************************************************************
*        2017-01-12             박희수              초기 구현
******************************************************************/
CREATE PROCEDURE [dbo].[SPM_Web_COMMON_Tbl_member_Pwd_S]
(
	@UserID nvarchar(100)
	,@UserPwd varchar(50)
	,@UserPwdNew nvarchar(50)
)
AS
BEGIN TRAN
DECLARE @DBPwd VARCHAR(50)
, @msg VARCHAR(50) = ''

SELECT @DBPwd = UserPwd FROM Tbl_member WITH(NOLOCK) WHERE UserID =@UserID 

IF (@DBPwd = @UserPwd)
BEGIN
	UPDATE Tbl_member set UserPwd = @UserPwdNew WHERE UserID = @UserID
END
ELSE
BEGIN
	SET @msg = 'pwdDiff'
END


IF(@@ERROR<>0)
	BEGIN
		ROLLBACK TRAN
		SELECT @msg AS RESULT
	END
ELSE
	BEGIN
		COMMIT TRAN
		SELECT 'OK' AS RESULT
	END
