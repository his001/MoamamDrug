/**********************************************************************************************
개요        : 약정보 저장
생성자      : 박희수
생성일      : 2017-01-18
로직        : 
최종수정자  : 
최종수정일  :  
exec SPM_Web_COMMON_Tbl_member_S @idx=N'8760',@drugCode=N'A11A0030A0285',@drugName=N'신풍아스피린리신주',@drugDanPoomYN=N'Y'
,@drugSungBun=N'Aspirin Lysine 900mg',@drugCompany=N'신풍제약',@drugBunRu=N'114',@drugToYeo=N'주사'
,@drugJeHyong=N'주사제',@drugGubun=N'전문',@drugInsure=N'300원/1병',@drugImage=N'/drug_info/pop_sb.asp?sbcode=A11A0030A028501',@userId=N'system',@regip=N'10.199.21.67'
***********************************************************************************************/ 
CREATE PROC dbo.SPM_Web_COMMON_Tbl_Drug_Info_S
	@idx			BIGINT
	,@drugCode		VARCHAR(13)
	,@drugName		NVARCHAR(100)
	,@drugDanPoomYN CHAR(1)
	,@drugSungBun	NVARCHAR(200)
	,@drugCompany	NVARCHAR(100)
	,@drugBunRu		VARCHAR(6)
	,@drugToYeo		NVARCHAR(1000)
	,@drugJeHyong	NVARCHAR(10)
	,@drugGubun		NVARCHAR(10)
	,@drugInsure	NVARCHAR(100)
	,@drugImage		NVARCHAR(150)
	,@regip			VARCHAR(20)
	,@userId		VARCHAR(100)
AS
BEGIN

	BEGIN TRAN

	DECLARE @Cnt int
	SELECT @Cnt = COUNT(*) FROM Tbl_Drug_Info WITH(NOLOCK) WHERE idx = @idx

	IF @Cnt=0
	BEGIN
		INSERT INTO Tbl_Drug_Info (
			idx, drugCode, drugName ,drugDanPoomYN ,drugSungBun ,drugCompany
			,drugBunRu ,drugToYeo ,drugJeHyong ,drugGubun ,drugInsure
			,drugImage ,regid ,regip
		)
		SELECT 
			@idx ,@drugCode, @drugName ,@drugDanPoomYN ,@drugSungBun ,@drugCompany
			,@drugBunRu ,@drugToYeo ,@drugJeHyong ,@drugGubun ,@drugInsure
			,@drugImage ,@userId ,@regip
	END

	--MERGE Tbl_Drug_Info AS tgt
	--USING (SELECT TOP 1 group_cd, menu_cd, user_group FROM Tbl_Drug_Info WITH(NOLOCK) WHERE group_cd=@group_cd AND menu_cd=@menuCd AND user_group=@usergroup) AS src
	--ON (tgt.user_group=src.user_group and tgt.group_cd=src.group_cd and tgt.menu_cd = src.menu_cd)
	--WHEN MATCHED THEN UPDATE SET grant_v=@grant_v, grant_i=@grant_i, grant_s=@grant_s, grant_r=@grant_r, grant_a=@grant_a, update_user=@userId, update_date = getdate()
	--WHEN NOT MATCHED THEN
	--	INSERT ( user_group, group_cd, menu_cd, grant_v, grant_i, grant_s, grant_r, grant_a, create_user, create_date) 
	--	VALUES (@usergroup,@group_cd,@menuCd,@grant_v,@grant_i,@grant_s,@grant_r,@grant_a,@userId,GETDATE()) ;


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
