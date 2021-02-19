CREATE PROCEDURE SP_ROLE_CUD
	@pin_cud_operation_type varchar(2),
	@pin_role_id    VARCHAR (50)  ,
	@pin_role_name_txt  VARCHAR (30)  ,
	@pin_active_ind     BIT           ,
	@pin_created_by     VARCHAR (30)  ,
	@pin_updated_by     VARCHAR (30)  ,
	@pout_role_id 	VARCHAR(50) 	OUTPUT,
	@pout_msg_cd		VARCHAR(30) 	OUTPUT,
	@pout_msg_txt		VARCHAR(250) 	OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET @pout_msg_cd='ERROR'
	
	DECLARE @RoleID AS VARCHAR(50)

	IF @pin_cud_operation_type ='A'
	BEGIN

		SET @pout_msg_txt='Role Insert failed'

		BEGIN TRY
		
			IF (SELECT COUNT(role_id) 
				FROM role_tb
				WHERE role_name_txt = @pin_role_name_txt)> 0
			BEGIN
					SET @pout_msg_txt ='Role with name ' + @pin_role_name_txt + ' already exists'
					RETURN
			END
			
			SET @RoleID = LOWER(NEWID())
	
			INSERT INTO role_tb(role_id,
				role_name_txt,
				active_ind,
				created_by,
				created_dt,
				updated_by,
				updated_dt)
			VALUES(@RoleID,
				@pin_role_name_txt,  
				@pin_active_ind,     
				@pin_created_by,   
				GETDATE(),
				NULL,
				NULL)


				DECLARE @role_menu_ut_temp Table(
				menu_action_id varchar(50) NOT NULL,
				menu_id varchar(50) NOT NULL,
				role_id varchar(50) NOT NULL,
				role_menu_id varchar(50)
				)

				INSERT INTO @role_menu_ut_temp 			 
				SELECT menu_action_id,
						menu_id,
						@RoleID,
						null						
				FROM menu_action_tb
				WHERE edit_locked_ind =1

				INSERT INTO role_menu_tb(role_menu_id,
									role_id,
									menu_id,
									created_by,
									created_dt)
				SELECT  LOWER(NEWID()),
						role_id,
						menu_id,
						@pin_created_by,   
						GETDATE()
				FROM	@role_menu_ut_temp

				UPDATE A 
				SET A.role_menu_id =B.role_menu_id
				FROM @role_menu_ut_temp A
				JOIN role_menu_tb B
				ON A.menu_id =B.menu_id		
				AND A.role_id =B.role_id

				INSERT INTO role_menu_action_tb(role_menu_action_id,
							role_menu_id,
							menu_action_id,
							created_by,
							created_dt)
				SELECT LOWER(NEWID()),
					role_menu_id ,
					menu_action_id,
					@pin_created_by,
					GETDATE()
				FROM @role_menu_ut_temp
			
			SET @pout_msg_cd='SUCCESS'
			SET @pout_msg_txt='Role is added successfully'
			SET @pout_role_id =@RoleID

		END TRY 
		BEGIN CATCH
			SET @pout_msg_cd='ERROR'
			SET @pout_msg_txt='Exception occured while inserting Role: ' + ERROR_MESSAGE()
		END CATCH	 
	END
	ELSE IF @pin_cud_operation_type ='E'
	BEGIN
		SET @pout_msg_txt='Role Update failed'

		BEGIN TRY
		
			IF (SELECT COUNT(role_id) 
				FROM role_tb
				WHERE role_name_txt = @pin_role_name_txt
				AND role_id <> @pin_role_id) > 0
			BEGIN
					SET @pout_msg_txt ='Role with name ' + @pin_role_name_txt + ' already exists'
					RETURN
			END

			UPDATE R 
			SET R.role_name_txt =@pin_role_name_txt,
				R.updated_by	=@pin_updated_by,  
				R.updated_dt	=GETDATE()
			FROM role_tb R
			WHERE R.role_id =@pin_role_id
			
			SET @pout_msg_cd='SUCCESS'
			SET @pout_msg_txt='Role is modified successfully'

		END TRY 
		BEGIN CATCH
			SET @pout_msg_cd='ERROR'
			SET @pout_msg_txt='Exception occured while updating Role: ' + ERROR_MESSAGE()
		END CATCH	 

	END
	ELSE IF @pin_cud_operation_type ='AI'
	BEGIN
		SET @pout_msg_txt='Role Activate/Inactivate failed'

		BEGIN TRY

			UPDATE R 
			SET R.active_ind =@pin_active_ind
			FROM role_tb R
			WHERE role_id =@pin_role_id

			SET @pout_msg_cd='SUCCESS'
			
			IF @pin_active_ind = 1 
			BEGIN
				SET @pout_msg_txt='Role is Activated successfully'
			END
			ELSE
			BEGIN
				SET @pout_msg_txt='Role is Inactivated successfully'
			END

		END TRY 
		BEGIN CATCH
			SET @pout_msg_cd='ERROR'
			SET @pout_msg_txt='Exception occured while Activating/Inactivating Role: ' + ERROR_MESSAGE()
		END CATCH	 
	END	
END