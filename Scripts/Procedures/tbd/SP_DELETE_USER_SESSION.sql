CREATE PROCEDURE SP_DELETE_USER_SESSION
	 @pin_session_id_txt varchar(50),
	 @pout_msg_cd	varchar(10) OUTPUT,
	 @pout_msg_txt	varchar(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET @pout_msg_cd='ERROR'
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
GO