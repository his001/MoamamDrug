/**********************************************************************************************
개요        : 복용후기 작성
생성자      : 박희수
생성일      : 2017-01-03
로직        :
최종수정자  :
최종수정일  :
2017-01-06 @idx 추가로 수정 추가
exec SPM_Web_COMMON_Tbl_BokYongHooKi_S 
***********************************************************************************************/ 
CREATE PROC SPM_Web_COMMON_Tbl_BokYongHooKi_S
	@UserID		nvarchar(100)
	,@VisitDate varchar(8)	--txt_Visit_Date
	,@CureDate	varchar(8)	--txt_NoPain_Date
	,@JungSang	varchar(max)	--txt_JngSang
	,@tempC		varchar(4)		--txt_tempC
	,@Feber		varchar(1)		-- rdoFeber G Y R
	,@HaeYeolJe varchar(1)		-- HaeYeolJeOX
	,@ChouBang	varchar(max)	-- txt_ChouBang
	,@Yak_iLbun varchar(3)		-- txt_Yak_iLbun
	,@HangSaengJeBokan varchar(1)	-- rdoHangSaengJeBokan C,S
	,@HangSaengJeEat varchar(1)		-- rdoHangSaengJeEat A,B,C
	,@ChamGoSaHang varchar(max)	-- txt_ChamGoSaHang
	,@BokYongHooKi varchar(max)	-- txt_BokYongHooKi
	,@regip varchar(20)
	,@idx int = 0
AS
BEGIN TRAN 
	
	IF @idx>0
	BEGIN
		UPDATE Tbl_BokYongHooKi set 
			VisitDate = @VisitDate,
			CureDate = @CureDate,
			JungSang = @JungSang,
			tempC = @tempC,
			Feber = @Feber,
			HaeYeolJe = @HaeYeolJe,
			ChouBang = @ChouBang,
			Yak_iLbun = @Yak_iLbun,
			HangSaengJeBokan = @HangSaengJeBokan,
			HangSaengJeEat = @HangSaengJeEat,
			ChamGoSaHang = @ChamGoSaHang,
			BokYongHooKi = @BokYongHooKi,
			moddate = getdate(),
			modid = @UserID,
			modip = @regip
		WHERE UserID = @UserID AND idx=@idx
	END
	ELSE
	BEGIN
		INSERT INTO Tbl_BokYongHooKi
		(
			UserID ,VisitDate ,CureDate ,JungSang ,tempC
			,Feber ,HaeYeolJe ,ChouBang ,Yak_iLbun ,HangSaengJeBokan
			,HangSaengJeEat ,ChamGoSaHang ,BokYongHooKi ,regip 
		)
		SELECT 
			@UserID ,@VisitDate ,@CureDate ,@JungSang ,@tempC
			,@Feber ,@HaeYeolJe ,@ChouBang ,@Yak_iLbun ,@HangSaengJeBokan
			,@HangSaengJeEat ,@ChamGoSaHang ,@BokYongHooKi, @regip
	END

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
