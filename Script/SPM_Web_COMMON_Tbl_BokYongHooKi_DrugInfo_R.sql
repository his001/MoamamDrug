/**********************************************************************************************
개요        : 복용 후기시 약정보 조회
생성자      : 박희수
생성일      : 2017-02-02
로직        : 
최종수정자  : 
최종수정일  :  
exec SPM_Web_COMMON_Tbl_BokYongHooKi_DrugInfo_R @idx=N'1'
***********************************************************************************************/ 
CREATE PROC SPM_Web_COMMON_Tbl_BokYongHooKi_DrugInfo_R
	@idx	BIGINT
AS
BEGIN

	SELECT * 
	FROM Tbl_BokYongHooKi_DrugInfo WITH(nolock) 
	WHERE idx = @idx

END
