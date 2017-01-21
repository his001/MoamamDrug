/**********************************************************************************************
개요        : 약정보 조회
생성자      : 박희수
생성일      : 2017-01-18
로직        : 
최종수정자  : 
최종수정일  :  
exec SPM_Web_COMMON_Tbl_Drug_Info_R @idx ='34200', @drugCode='A11AKP08C0025',@drugName='가스핀정',@drugDanPoomYN='Y',@drugSungBun='Mosapride Citrate Hydrate 5mg'
,@drugCompany='위더스제약',@drugBunRu='239',@drugToYeo='경구(내용고형)',@drugJeHyong='정제',@drugGubun='전문',@drugInsure='103원/1정',@drugImage='',@regip='127.0.0.1'
,@userId='his001_ccp'
***********************************************************************************************/ 
CREATE PROC dbo.SPM_Web_COMMON_Tbl_Drug_Info_R
	@idx			VARCHAR(10) = ''
	,@drugCode		VARCHAR(13) = ''
	,@drugName		NVARCHAR(100) = ''
	,@drugDanPoomYN VARCHAR(1) = ''
	,@drugSungBun	NVARCHAR(200) = ''
	,@drugCompany	NVARCHAR(100) = ''
	,@drugBunRu		VARCHAR(6) = ''
	,@drugToYeo		NVARCHAR(1000) = ''
	,@drugJeHyong	NVARCHAR(10) = ''
	,@drugGubun		NVARCHAR(10) = ''
	,@drugInsure	NVARCHAR(100) = ''
AS
BEGIN

	SELECT idx
		,drugName,drugCode,drugDanPoomYN,drugSungBun,drugCompany
		,drugBunRu,drugToYeo,drugJeHyong,drugGubun,drugInsure
		,drugImage,regdate,regid,regip,moddate
		,modid,modip
	FROM Tbl_Drug_Info WITH(NOLOCK)
	WHERE idx like '%'+ isnull(@idx,'') +'%'
		AND drugCode like '%'+ isnull(@drugCode,'') +'%'
		AND drugName like '%'+ isnull(@drugName,'') +'%'
		AND drugDanPoomYN like '%'+ isnull(@drugDanPoomYN,'') +'%'
		AND drugSungBun like '%'+ isnull(@drugSungBun,'') +'%'
		AND drugCompany like '%'+ isnull(@drugCompany,'') +'%'
		AND drugBunRu like '%'+ isnull(@drugBunRu,'') +'%'
		AND drugToYeo like '%'+ isnull(@drugToYeo,'') +'%'
		AND drugGubun like '%'+ isnull(@drugGubun,'') +'%'
		AND drugJeHyong like '%'+ isnull(@drugJeHyong,'') +'%'
		AND drugInsure like '%'+ isnull(@drugInsure,'') +'%'
END

