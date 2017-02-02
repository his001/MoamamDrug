/**********************************************************************************************
개요        : 복용 후기시 약정보 저장
생성자      : 박희수
생성일      : 2017-02-02
로직        : 
최종수정자  : 
최종수정일  :  
exec SPM_Web_COMMON_Tbl_BokYongHooKi_DrugInfo_S @idx=N'8760', @ITEM_SEQ=1000, @ITEM_MEMO='메모2', @userId=N'system',@ regip=N'10.199.21.67'
***********************************************************************************************/ 
CREATE PROC dbo.SPM_Web_COMMON_Tbl_BokYongHooKi_DrugInfo_S
	@idx			BIGINT
	,@ITEM_SEQ		BIGINT
	,@ITEM_MEMO		NVARCHAR(2000)
	,@regip			VARCHAR(20)
	,@userId		VARCHAR(100)
AS
BEGIN

	BEGIN TRAN

	DELETE FROM Tbl_BokYongHooKi_DrugInfo where idx = @idx

	INSERT INTO Tbl_BokYongHooKi_DrugInfo (idx ,ITEM_SEQ ,ITEM_MEMO ,regid ,regip)
	SELECT @idx ,@ITEM_SEQ , @ITEM_MEMO ,@userId ,@regip
	

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
END
