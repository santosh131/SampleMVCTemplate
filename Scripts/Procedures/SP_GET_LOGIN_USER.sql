CREATE PROCEDURE SP_GET_LOGIN_USER
	 @pin_user_name_txt varchar(30),
	 @pin_pwd_txt varchar(200),
	 @pout_msg_cd	varchar(30) OUTPUT,
	 @pout_msg_txt	varchar(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET @pout_msg_cd='ERROR'
	SET @pout_msg_txt='User is either Inactive /does not exists'
	
	SELECT * INTO #user_temp_tb	
	FROM user_tb
	WHERE user_name_txt =@pin_user_name_txt
	AND pwd_txt=@pin_pwd_txt
	AND active_ind =1
	
	IF ( SELECT COUNT(user_name_txt) 
	FROM #user_temp_tb)>0
	BEGIN
		SELECT * 
		FROM #user_temp_tb
		
		SET @pout_msg_cd='SUCCESS'
		SET @pout_msg_txt=''
	END	
	
END
GO
