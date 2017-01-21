/**********************************************************************************************
개요        : 공공DB 의약정보 저장
생성자      : 박희수
생성일      : 2017-01-19
로직        :
최종수정자  :
최종수정일  :
exec SPM_Web_COMMON_Tbl_member_S @ITEM_SEQ=N'8760',@drugCode=N'A11A0030A0285',@drugName=N'신풍아스피린리신주',@drugDanPoomYN=N'Y'
,@drugSungBun=N'Aspirin Lysine 900mg',@drugCompany=N'신풍제약',@drugBunRu=N'114',@drugToYeo=N'주사'
,@drugJeHyong=N'주사제',@drugGubun=N'전문',@drugInsure=N'300원/1병',@drugImage=N'/drug_info/pop_sb.asp?sbcode=A11A0030A028501',@userId=N'system',@regip=N'10.199.21.67'
***********************************************************************************************/
CREATE PROC dbo.SPM_Web_COMMON_Tbl_DrugOrg_Info_S
	@ITEM_SEQ bigint						--품목일련번호
	,@ITEM_NAME nvarchar(100)				--품목명
	,@ENTP_SEQ bigint						--업체일련번호
	,@ENTP_NAME nvarchar(100)				--업체명
	,@CHART nvarchar(100)					--성상
	,@ITEM_IMAGE nvarchar(300)				--큰제품이미지
	,@PRINT_FRONT nvarchar(100)				--표시(앞)
	,@PRINT_BACK nvarchar(100)				--표시(뒤)
	,@DRUG_SHAPE nvarchar(100)				--의약품모양
	,@COLOR_CLASS1 nvarchar(100)			--색깔(앞)
	,@COLOR_CLASS2 nvarchar(100)			--색깔(뒤)
	,@LINE_FRONT nvarchar(100)				--분할선(앞)
	,@LINE_BACK nvarchar(100)				--분할선(뒤)
	,@LENG_LONG nvarchar(100)				--크기(장축)
	,@LENG_SHORT nvarchar(100)				--크기(단축)
	,@THICK nvarchar(100)					--크기(두께)
	,@IMG_REGIST_TS nvarchar(100)			--약학정보원 이미지 생성일
	,@CLASS_NO nvarchar(100)				--분류번호
	,@CLASS_NAME nvarchar(100)				--분류명
	,@ETC_OTC_NAME nvarchar(100)			--전문/일반
	,@ITEM_PERMIT_DATE nvarchar(100)		--품목허가일자
	,@FORM_CODE_NAME nvarchar(100)			--제형코드이름
	,@MARK_CODE_FRONT_ANAL nvarchar(100)	--마크내용(앞)
	,@MARK_CODE_BACK_ANAL nvarchar(100)		--마크내용(뒤)
	,@MARK_CODE_FRONT_IMG nvarchar(100)		--마크이미지(앞)
	,@MARK_CODE_BACK_IMG nvarchar(100)		--마크이미지(뒤)
	,@regip			VARCHAR(20)
	,@userId		VARCHAR(100)
AS
BEGIN

	BEGIN TRAN

	DECLARE @Cnt int
	SELECT @Cnt = COUNT(*) FROM Tbl_DrugOrg_Info WITH(NOLOCK) WHERE ITEM_SEQ = @ITEM_SEQ

	IF @Cnt=0
	BEGIN
		INSERT INTO dbo.Tbl_DrugOrg_Info
		(
			ITEM_SEQ ,ITEM_NAME ,ENTP_SEQ ,ENTP_NAME ,CHART
			,ITEM_IMAGE ,PRINT_FRONT ,PRINT_BACK ,DRUG_SHAPE ,COLOR_CLASS1
			,COLOR_CLASS2 ,LINE_FRONT ,LINE_BACK ,LENG_LONG ,LENG_SHORT
			,THICK ,IMG_REGIST_TS ,CLASS_NO ,CLASS_NAME ,ETC_OTC_NAME
			,ITEM_PERMIT_DATE ,FORM_CODE_NAME ,MARK_CODE_FRONT_ANAL ,MARK_CODE_BACK_ANAL ,MARK_CODE_FRONT_IMG
			,MARK_CODE_BACK_IMG,regid ,regip
		)
		VALUES
		(
			@ITEM_SEQ , @ITEM_NAME , @ENTP_SEQ , @ENTP_NAME , @CHART
			, @ITEM_IMAGE , @PRINT_FRONT , @PRINT_BACK , @DRUG_SHAPE , @COLOR_CLASS1
			, @COLOR_CLASS2 , @LINE_FRONT , @LINE_BACK , @LENG_LONG , @LENG_SHORT
			, @THICK , @IMG_REGIST_TS , @CLASS_NO , @CLASS_NAME , @ETC_OTC_NAME
			, @ITEM_PERMIT_DATE , @FORM_CODE_NAME , @MARK_CODE_FRONT_ANAL , @MARK_CODE_BACK_ANAL , @MARK_CODE_FRONT_IMG
			, @MARK_CODE_BACK_IMG,@userId ,@regip
		)

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
END
