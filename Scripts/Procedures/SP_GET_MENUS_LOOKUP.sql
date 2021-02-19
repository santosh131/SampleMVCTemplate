CREATE PROCEDURE SP_GET_MENUS_LOOKUP	 
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
	SELECT menu_id  as id_txt,
	menu_id as code_txt,
	menu_name_txt as desc_txt	
	FROM menu_tb
	--WHERE edit_locked_ind =0 or edit_locked_ind  is NULL 
	
END