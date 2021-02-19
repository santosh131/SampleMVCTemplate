CREATE PROCEDURE SP_ROLE_MENU_CUD
	@pin_cud_operation_type varchar(2),
	@pin_role_id    VARCHAR (50)  ,
	@pin_menu_id  VARCHAR (30)  ,
	@pin_role_menu_ut  role_menu_ut READONLY,
	@pin_created_by     VARCHAR (30),
	@pout_msg_cd		VARCHAR(30) 	OUTPUT,
	@pout_msg_txt		VARCHAR(250) 	OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET @pout_msg_cd='ERROR' 

	IF @pin_cud_operation_type ='S'
	BEGIN

		SET @pout_msg_txt='Menu Actions Insert/Remove failed'

		BEGIN TRY
			
			DECLARE @RoleMenuID AS VARCHAR(50)
			
			SELECT @RoleMenuID = role_menu_id
			FROM	role_menu_tb
			WHERE	role_id=@pin_role_id
			AND		menu_id	=@pin_menu_id
			
			IF ISNULL(@RoleMenuID,'')='' 
			BEGIN
					SET @RoleMenuID = LOWER(NEWID())
					INSERT INTO role_menu_tb(role_menu_id,
									role_id,
									menu_id,
									created_by,
									created_dt)
					VALUES(@RoleMenuID,
							@pin_role_id,
							@pin_menu_id,
							@pin_created_by,
							GETDATE())					
			END
			
			DELETE
			FROM role_menu_action_tb 
			WHERE role_menu_id =@RoleMenuID 

			INSERT INTO role_menu_action_tb(role_menu_action_id,
				role_menu_id,
				menu_action_id,
				created_by,
				created_dt)
			SELECT LOWER(NEWID()),
				@RoleMenuID ,
				menu_action_id,
				@pin_created_by,
				GETDATE()
			FROM @pin_role_menu_ut


			SET @pout_msg_cd='SUCCESS'
			SET @pout_msg_txt='Menu Actions are added/removed successfully'

		END TRY 
		BEGIN CATCH
			SET @pout_msg_cd='ERROR'
			SET @pout_msg_txt='Exception occured while inserting/removing Menu Actions: ' + ERROR_MESSAGE()
		END CATCH	 
	END	 
END