CREATE PROCEDURE SP_GET_USER
	 @pin_user_id_txt varchar(50),
	 @pout_msg_cd	varchar(30) OUTPUT,
	 @pout_msg_txt	varchar(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET @pout_msg_cd='ERROR'
	SET @pout_msg_txt='User does not exists'
	
	SELECT *, CASE active_ind
					WHEN 1  THEN 'Active'
					WHEN 0	THEN 'Inactive'
			END as active_desc
	INTO #user_temp_tb	
	FROM user_tb
	WHERE user_id_txt =@pin_user_id_txt
	
	IF ( SELECT COUNT(user_id_txt) 
	FROM #user_temp_tb)>0
	BEGIN
		SELECT U.*,UR.role_id 
		FROM #user_temp_tb U
		LEFT JOIN user_role_tb UR
		ON U.user_id_txt = UR.user_id_txt
		
		SET @pout_msg_cd='SUCCESS_GET'
		SET @pout_msg_txt=''
	END	
	
END