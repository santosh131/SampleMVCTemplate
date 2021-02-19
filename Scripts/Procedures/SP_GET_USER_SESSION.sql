CREATE PROCEDURE SP_GET_USER_SESSION
	 @pin_session_id_txt varchar(50),
	 @pout_msg_cd	varchar(30) OUTPUT,
	 @pout_msg_txt	varchar(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET @pout_msg_cd='ERROR'
	SET @pout_msg_txt='Session is invalid/expired'
	
	SELECT * INTO #user_session_temp_tb	
	FROM user_session_tb
	WHERE session_id_txt =@pin_session_id_txt
	 
	IF ( SELECT COUNT(user_name_txt) 
	FROM #user_session_temp_tb)>0
	BEGIN
		SELECT * 
		FROM #user_session_temp_tb
		
		SET @pout_msg_cd='SUCCESS'
		SET @pout_msg_txt=''
	END	
	
END
GO
