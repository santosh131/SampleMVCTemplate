CREATE PROCEDURE SP_USER_SESSION_CUD
	 @pin_cud_operation_type varchar(2),
	 @pin_user_id_txt varchar(50),
	 @pin_user_name_txt varchar(30),	 
	 @pin_role_action_menu_txt varchar(max),
	 @pin_session_id_txt varchar(50),
	 @pout_session_id_txt varchar(50) OUTPUT,
	 @pout_msg_cd	varchar(30) OUTPUT,
	 @pout_msg_txt	varchar(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET @pout_msg_cd='ERROR'

	IF @pin_cud_operation_type ='A'
	BEGIN

		SET @pout_msg_txt='Session Insert failed'

		BEGIN TRY
	
		DECLARE @SessionID uniqueidentifier
		DECLARE @SessionIDString as varchar(50)
    	
		SET @SessionID =NEWID()
		SET @SessionIDString =LOWER(@SessionID)
		
		INSERT INTO user_session_tb(session_id_txt,user_id_txt,user_name_txt,role_menu_action_txt)
		VALUES(@SessionIDString,@pin_user_id_txt,@pin_user_name_txt,@pin_role_action_menu_txt)

			SET @pout_msg_cd='SUCCESS'
			SET @pout_msg_txt=''
			SET @pout_session_id_txt =@SessionIDString

		END TRY 
		BEGIN CATCH
			SET @pout_msg_cd='ERROR'
			SET @pout_msg_txt='Exception occured while insert Session'
		END CATCH	 
	END
	ELSE IF @pin_cud_operation_type ='D'
		BEGIN
			SET @pout_msg_txt='Session Delete failed'

		BEGIN TRY

		DELETE FROM user_session_tb
		WHERE session_id_txt =@pin_session_id_txt

			SET @pout_msg_cd='SUCCESS'
			SET @pout_msg_txt=''

		END TRY 
		BEGIN CATCH
			SET @pout_msg_cd='ERROR'
			SET @pout_msg_txt='Exception occured while deleting Session'
		END CATCH	 
	END	
END