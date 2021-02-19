CREATE PROCEDURE SP_GET_USER_ROLES_MENUS_LIST
	 @pin_user_name_txt varchar(30),
	 @pout_msg_cd	varchar(30) OUTPUT,
	 @pout_msg_txt	varchar(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET @pout_msg_cd='ERROR'
	SET @pout_msg_txt='User does not have any roles/menus'
	
	SELECT R.role_id,
	R.role_name_txt,
	UR.user_role_id
	INTO #role_tmp_tb
	FROM user_role_tb UR
	JOIN user_tb U
	ON UR.user_id_txt =U.user_id_txt
	LEFT JOIN role_tb R
	ON UR.role_id = R.role_id
	WHERE U.user_name_txt =@pin_user_name_txt
	AND U.active_ind =1
	
	SELECT * INTO #menu_tmp_tb
	FROM(
	SELECT RM.role_menu_id,R.role_id, M.*
	FROM role_menu_tb RM
	JOIN #role_tmp_tb R
	ON RM.role_id=R.role_id
	JOIN menu_tb M
	ON RM.menu_id=M.menu_id
	WHERE M.active_ind=1) AS X
	
	SELECT * INTO #menu_action_tmp_tb
	FROM(
	SELECT RMA.role_menu_id,MA.*
	FROM role_menu_action_tb RMA
	JOIN #menu_tmp_tb M
	on RMA.role_menu_id=M.role_menu_id
	LEFT JOIN menu_action_tb MA
	on RMA.menu_action_id=MA.menu_action_id) AS Y
		
	DELETE 
	FROM #menu_action_tmp_tb
	WHERE menu_action_id in
	(SELECT menu_action_id 
	FROM user_role_menu_action_tb  URMA
	JOIN #role_tmp_tb R
	ON R.user_role_id = URMA.user_role_id
	WHERE allowed_ind=0) 

	SELECT * INTO #menu_temp_tb2
	FROM(
	SELECT DISTINCT M.*
	FROM #menu_tmp_tb M	
	JOIN  #menu_action_tmp_tb MA
	ON M.menu_id = MA.menu_id
	AND M.role_menu_id= MA.role_menu_id) AS Z
	
	IF ((SELECT COUNT(role_id) FROM #role_tmp_tb)>0 AND  
	    (SELECT COUNT(role_menu_id) FROM #menu_tmp_tb)>0 AND
	    (SELECT COUNT(menu_action_id)FROM #menu_action_tmp_tb)>0)
	BEGIN
		SELECT * 
		FROM #menu_temp_tb2		
		
		SELECT DISTINCT MC.menu_category_id,menu_category_name_txt
		FROM #menu_temp_tb2 M
		LEFT JOIN  menu_category_tb MC
		ON MC.menu_category_id =M.menu_category_id

		SELECT R.role_id,
		R.role_name_txt,
		M.role_menu_id,
		M.menu_id,
		M.menu_name_txt,
		M.controller_name_txt,
		M.menu_category_id,		
		M.sub_menu_ind,
		M.parent_menu_id,
		MA.menu_action_id,
		MA.menu_action_cd,
		MA.menu_ind,
		MA.menu_action_name_txt,
		MA.action_desc_txt
		FROM #role_tmp_tb R
		LEFT JOIN #menu_temp_tb2 M
		ON R.role_id =M.role_id
		LEFT JOIN  #menu_action_tmp_tb MA
		ON MA.role_menu_id =M.role_menu_id
		
		
		SET @pout_msg_cd='SUCCESS'
		SET @pout_msg_txt=''
	END	
	
END