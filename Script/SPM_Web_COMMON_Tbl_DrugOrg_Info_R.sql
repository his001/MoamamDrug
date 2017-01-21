/**********************************************************************************************
개요        : 약정보 조회
생성자      : 박희수
생성일      : 2017-01-18
로직        : 
최종수정자  : 
최종수정일  :  
exec SPM_Web_COMMON_Tbl_DrugOrg_Info_R @idx ='34200', @drugCode='A11AKP08C0025',@drugName='가스핀정',@drugDanPoomYN='Y',@drugSungBun='Mosapride Citrate Hydrate 5mg'
,@drugCompany='위더스제약',@drugBunRu='239',@drugToYeo='경구(내용고형)',@drugJeHyong='정제',@drugGubun='전문',@drugInsure='103원/1정',@drugImage='',@regip='127.0.0.1'
,@userId='his001_ccp'
***********************************************************************************************/ 
CREATE PROC dbo.SPM_Web_COMMON_Tbl_DrugOrg_Info_R
	@PageNo	INT
	,@RowCount	INT
	,@ITEM_SEQ nvarchar(10)					=''--품목일련번호
	,@ITEM_NAME nvarchar(100)				=''--품목명
	,@ENTP_SEQ nvarchar(10)					=''--업체일련번호
	,@ENTP_NAME nvarchar(100)				=''--업체명
	,@CHART nvarchar(100)					=''--성상
	,@CLASS_NO nvarchar(100)				=''--분류번호
	,@CLASS_NAME nvarchar(100)				=''--분류명
	,@ETC_OTC_NAME nvarchar(100)			=''--전문/일반
	,@ITEM_PERMIT_DATE nvarchar(100)		=''--품목허가일자
	,@FORM_CODE_NAME nvarchar(100)			=''--제형코드이름
	,@MARK_CODE_FRONT_ANAL nvarchar(100)	=''--마크내용(앞)
	,@MARK_CODE_BACK_ANAL nvarchar(100)		=''--마크내용(뒤)
	,@MARK_CODE_FRONT_IMG nvarchar(100)		=''--마크이미지(앞)
	,@MARK_CODE_BACK_IMG nvarchar(100)		=''--마크이미지(뒤)
AS
BEGIN

	SELECT * FROM 
	(
		SELECT 
			FLOOR((ROW_NUMBER() OVER (ORDER BY ITEM_NAME) - 1) / @RowCount + 1) PAGE
			, COUNT(*) OVER() AS TOTAL_COUNT
			,ITEM_SEQ ,ITEM_NAME ,ENTP_SEQ ,ENTP_NAME ,CHART
			,ITEM_IMAGE ,PRINT_FRONT ,PRINT_BACK ,DRUG_SHAPE ,COLOR_CLASS1
			,COLOR_CLASS2 ,LINE_FRONT ,LINE_BACK ,LENG_LONG ,LENG_SHORT
			,THICK ,IMG_REGIST_TS ,CLASS_NO ,CLASS_NAME ,ETC_OTC_NAME
			,ITEM_PERMIT_DATE ,FORM_CODE_NAME ,MARK_CODE_FRONT_ANAL ,MARK_CODE_BACK_ANAL ,MARK_CODE_FRONT_IMG
			,MARK_CODE_BACK_IMG ,regdate ,regid ,regip ,moddate
			,modid ,modip
		FROM Tbl_DrugOrg_Info WITH(NOLOCK)
		WHERE ITEM_SEQ like '%'+ isnull(@ITEM_SEQ,'') +'%'
			AND ITEM_NAME like '%'+ isnull(@ITEM_NAME,'') +'%'
			AND ENTP_SEQ like '%'+ isnull(@ENTP_SEQ,'') +'%'
			AND ENTP_NAME like '%'+ isnull(@ENTP_NAME,'') +'%'
			AND CHART like '%'+ isnull(@CHART,'') +'%'
			AND CLASS_NO like '%'+ isnull(@CLASS_NO,'') +'%'
			AND CLASS_NAME like '%'+ isnull(@CLASS_NAME,'') +'%'
	) T
	WHERE T.PAGE=@PageNo


END

