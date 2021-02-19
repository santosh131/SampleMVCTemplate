CREATE PROCEDURE SP_INSERT_USER_SESSION
	 @pin_user_id_txt varchar(20),
	 @pin_user_name_txt varchar(30),	 
	 @pin_role_action_menu_txt varchar(max),
	 @pout_session_id_txt varchar(50) OUTPUT,
	 @pout_msg_cd	varchar(10) OUTPUT,
	 @pout_msg_txt	varchar(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET @pout_msg_cd='ERROR'
	SET @pout_msg_txt='Session Insert failed'

	BEGIN TRY

	DECLARE @SessionID uniqueidentifier
    SET @SessionID = NEWID()

	INSERT INTO user_session_tb(session_id_txt,user_id_txt,user_name_txt,role_menu_action_txt)
	VALUES(@SessionID,@pin_user_id_txt,@pin_user_name_txt,@pin_role_action_menu_txt)

		SET @pout_msg_cd='SUCCESS'
		SET @pout_msg_txt=''
		SET @pout_session_id_txt =@SessionID

	END TRY 
	BEGIN CATCH
		SET @pout_msg_cd='ERROR'
		SET @pout_msg_txt='Exception occured while insert Session'
	END CATCH	 
	
END
GO