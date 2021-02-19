CREATE PROCEDURE SP_GET_ROLE_MENU_ACTION
	 @pin_role_id varchar(50),
	 @pin_menu_id varchar(50),
	 @pout_msg_cd	varchar(30) OUTPUT,
	 @pout_msg_txt	varchar(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET @pout_msg_cd='ERROR'
	SET @pout_msg_txt='Role Menu Action is does not exist'

	SELECT * INTO #ma_temp
	FROM (
	SELECT MA.* FROM menu_action_tb MA
	JOIN menu_tb M
	on m.menu_id =MA.menu_id
	WHERE m.menu_id =@pin_menu_id)  as x	

	DECLARE @RoleMenuId VARCHAR(50)
	 
	SELECT @RoleMenuId =role_menu_id FROM role_menu_tb 
	WHERE role_id=@pin_role_id 
	AND menu_id =@pin_menu_id
	 
	IF ISNULL(@RoleMenuId,'')<>''
	BEGIN
		SELECT * INTO #role_menu_action_temp_tb	
		FROM(
			SELECT DISTINCT MT.*,RMA.role_menu_action_id, 'selected_ind' =CASE WHEN RMA.menu_action_id =MT.menu_action_id THEN cast(1 as bit)
									ELSE cast(0 as bit)
									END
			FROM #ma_temp MT
			LEFT JOIN role_menu_action_tb RMA			
			on MT.menu_action_id =RMA.menu_action_id
			AND RMA.role_menu_id= @RoleMenuId
			) AS Z
			 
			IF ( SELECT COUNT(menu_action_id) 
			FROM #role_menu_action_temp_tb)>0
			BEGIN
				SELECT * 
				FROM #role_menu_action_temp_tb
		
				SET @pout_msg_cd='SUCCESS'
				SET @pout_msg_txt=''
			END	

	END
	ELSE
	BEGIN
		SELECT * INTO #role_menu_action_temp_tb_t	
		FROM(
			SELECT DISTINCT MT.*,null as role_menu_action_id,'selected_ind' = cast(0 as bit)
			FROM #ma_temp MT) AS Z

			
		IF ( SELECT COUNT(menu_action_id) 
		FROM #role_menu_action_temp_tb_t)>0
		BEGIN
			SELECT * 
			FROM #role_menu_action_temp_tb_t
		
			SET @pout_msg_cd='SUCCESS'
			SET @pout_msg_txt=''
		END	
	END
	 
	
END