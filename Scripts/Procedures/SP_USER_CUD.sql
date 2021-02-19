CREATE PROCEDURE SP_USER_CUD
	@pin_cud_operation_type varchar(2),
	@pin_user_id_txt    VARCHAR (50)  ,
	@pin_user_name_txt  VARCHAR (30)  ,
	@pin_last_name_txt  VARCHAR (30)  ,
	@pin_first_name_txt VARCHAR (30)  ,
	@pin_email_id_txt   VARCHAR (100) ,
	@pin_mobile_no_txt  VARCHAR (100) ,
	@pin_addr1_txt      VARCHAR (100) ,
	@pin_addr2_txt      VARCHAR (100) ,
	@pin_state_cd       VARCHAR (10)  ,
	@pin_city_txt       VARCHAR (100) ,
	@pin_zip_cd_txt     VARCHAR (15)  ,
	@pin_pwd_txt        VARCHAR (200) ,
	@pin_role_id		VARCHAR (50) ,
	@pin_active_ind     BIT           ,
	@pin_created_by     VARCHAR (30)  ,
	@pin_updated_by     VARCHAR (30)  ,
	@pout_user_id_txt 	VARCHAR(50) 	OUTPUT,
	@pout_msg_cd		VARCHAR(30) 	OUTPUT,
	@pout_msg_txt		VARCHAR(250) 	OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET @pout_msg_cd='ERROR'

	DECLARE @UserRoleID uniqueidentifier
	DECLARE @UserRoleIDString as varchar(50)

	SET @UserRoleID =NEWID()
	SET @UserRoleIDString =LOWER(@UserRoleID)

	IF @pin_cud_operation_type ='A'
	BEGIN

		SET @pout_msg_txt='User Insert failed'

		BEGIN TRY
	
			DECLARE @UserID uniqueidentifier
			DECLARE @UserIDString as varchar(50)
			
			SET @UserID =NEWID()
			SET @UserIDString =LOWER(@UserID)
			
			IF (SELECT COUNT(user_id_txt) 
						FROM user_tb
						WHERE user_name_txt = @pin_user_name_txt) >0
			BEGIN
					SET @pout_msg_txt ='User with UserName ' + @pin_user_name_txt + ' already exists'
					RETURN
			END
		
			INSERT INTO user_tb(user_id_txt,
				user_name_txt,
				last_name_txt,
				first_name_txt,
				email_id_txt,
				mobile_no_txt,
				addr1_txt,
				addr2_txt,
				state_cd,
				city_txt,
				zip_cd_txt,
				pwd_txt,
				active_ind,
				created_by,
				created_dt,
				updated_by,
				updated_dt)
			VALUES(@UserIDString,
				@pin_user_name_txt,  
				@pin_last_name_txt , 
				@pin_first_name_txt, 
				@pin_email_id_txt,   
				@pin_mobile_no_txt,  
				@pin_addr1_txt,      
				@pin_addr2_txt,      
				@pin_state_cd,       
				@pin_city_txt,       
				@pin_zip_cd_txt,     
				@pin_pwd_txt,        
				@pin_active_ind,     
				@pin_created_by,   
				GETDATE(),
				NULL,
				NULL)

			DELETE
			FROM user_role_tb 
			WHERE user_id_txt =@pin_user_id_txt

			INSERT INTO user_role_tb(user_role_id,
				role_id,
				user_id_txt,
				created_by,
				created_dt)
			VALUES(@UserRoleIDString,
				@pin_role_id,
				@UserIDString,
				@pin_created_by,
				GETDATE())


			SET @pout_msg_cd='SUCCESS'
			SET @pout_msg_txt='User is added successfully'
			SET @pout_user_id_txt =@UserIDString

		END TRY 
		BEGIN CATCH
			SET @pout_msg_cd='ERROR'
			SET @pout_msg_txt='Exception occured while inserting User: ' + ERROR_MESSAGE()
		END CATCH	 
	END
	ELSE IF @pin_cud_operation_type ='E'
	BEGIN
		SET @pout_msg_txt='User Update failed'

		BEGIN TRY
		
			IF (SELECT COUNT(user_id_txt) 
						FROM user_tb
						WHERE user_name_txt = @pin_user_name_txt
						AND user_id_txt <> @pin_user_id_txt) > 0
			BEGIN
					SET @pout_msg_txt ='User with UserName ' + @pin_user_name_txt + ' already exists'
					RETURN
			END

			UPDATE U 
			SET U.user_name_txt =@pin_user_name_txt,
				U.last_name_txt  =@pin_last_name_txt ,
				U.first_name_txt =@pin_first_name_txt,
				U.email_id_txt   =@pin_email_id_txt  ,
				U.mobile_no_txt  =@pin_mobile_no_txt ,
				U.addr1_txt      =@pin_addr1_txt     ,
				U.addr2_txt      =@pin_addr2_txt     ,
				U.state_cd       =@pin_state_cd      ,
				U.city_txt       =@pin_city_txt      ,
				U.zip_cd_txt     =@pin_zip_cd_txt    ,
				U.pwd_txt        =@pin_pwd_txt       ,
				U.updated_by	=@pin_updated_by     ,  
				U.updated_dt	=GETDATE()
			FROM user_tb U
			WHERE U.user_id_txt =@pin_user_id_txt

			DELETE
			FROM user_role_tb 
			WHERE user_id_txt =@pin_user_id_txt

			INSERT INTO user_role_tb(user_role_id,
				role_id,
				user_id_txt,
				created_by,
				created_dt)
			VALUES(@UserRoleIDString,
				@pin_role_id,
				@pin_user_id_txt,
				@pin_updated_by,
				GETDATE())

			SET @pout_msg_cd='SUCCESS'
			SET @pout_msg_txt='User is modified successfully'

		END TRY 
		BEGIN CATCH
			SET @pout_msg_cd='ERROR'
			SET @pout_msg_txt='Exception occured while updating User: ' + ERROR_MESSAGE()
		END CATCH	 

	END
	ELSE IF @pin_cud_operation_type ='AI'
	BEGIN
		SET @pout_msg_txt='User Activate/Inactivate failed'

		BEGIN TRY

			UPDATE U 
			SET U.active_ind =@pin_active_ind
			FROM user_tb U
			WHERE user_id_txt =@pin_user_id_txt

			SET @pout_msg_cd='SUCCESS'
			
			IF @pin_active_ind = 1 
			BEGIN
				SET @pout_msg_txt='User is Activated successfully'
			END
			ELSE
			BEGIN
				SET @pout_msg_txt='User is Inactivated successfully'
			END

		END TRY 
		BEGIN CATCH
			SET @pout_msg_cd='ERROR'
			SET @pout_msg_txt='Exception occured while Activating/Inactivating User: ' + ERROR_MESSAGE()
		END CATCH	 
	END	
END