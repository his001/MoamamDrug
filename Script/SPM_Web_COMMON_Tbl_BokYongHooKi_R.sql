/**********************************************************************************************
개요        : 복용후기 리스트
생성자      : 박희수
생성일      : 2017-01-05
로직        : 
최종수정자  : 
최종수정일  : 
exec SPM_Web_COMMON_Tbl_BokYongHooKi_R @UserID='his001'
***********************************************************************************************/ 
CREATE PROC [DBO].[SPM_Web_COMMON_Tbl_BokYongHooKi_R]
	@UserID	NVARCHAR(100)  =''
AS
BEGIN
	--SELECT * FROM (
	--	SELECT 
	--		FLOOR((ROW_NUMBER() OVER (ORDER BY A.ITEM) - 1) / @ROWCNT + 1) PAGE
	--		, COUNT(*) OVER() AS TOTAL_COUNT
	--		,A.ORDER_GROUP,
	--	FROM ADO_ITEM AS A WITH(NOLOCK)
	--	WHERE A.SECTION BETWEEN @SECTIONFROM AND @SECTIONTO
	--		--AND B.SUP_TERM_ID LIKE '%'+ISNULL(@RUD_ID,'')+'%'
	--) T
	--WHERE T.PAGE=@PAGENUM

	SELECT 
		idx, UserID, VisitDate, CureDate, JungSang, 
		tempC, Feber, HaeYeolJe, ChouBang, Yak_iLbun, 
		HangSaengJeBokan, HangSaengJeEat, ChamGoSaHang, BokYongHooKi, regdate, 
		regid, regip, moddate, modid, modip
	FROM Tbl_BokYongHooKi with(nolock)
	WHERE @UserID like '%'+ISNULL(@UserID,'')+'%'
	ORDER BY idx DESC
END

