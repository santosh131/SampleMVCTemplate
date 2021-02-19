CREATE PROCEDURE SP_GET_COMMON_DDL
	 @pin_field_cd varchar(30),
	 @pin_option_cd varchar(2)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT '-2' as id_txt,
	'-2' as code_txt,
	'--ALL--' as desc_txt	
	WHERE @pin_option_cd = '-2'
	UNION ALL
	SELECT '-1' as id_txt,
	'-1' as code_txt,
	'--SELECT--' as desc_txt	
	WHERE @pin_option_cd = '-1'
	UNION ALL
	SELECT ddl_id as id_txt,
	code_txt,
	desc_txt
	FROM common_ddl_tb
	WHERE filed_cd =@pin_field_cd
	
END