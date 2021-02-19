CREATE PROCEDURE SP_GET_ROLE
	 @pin_role_id varchar(50),
	 @pout_msg_cd	varchar(30) OUTPUT,
	 @pout_msg_txt	varchar(250) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET @pout_msg_cd='ERROR'
	SET @pout_msg_txt='Role is either Inactive /does not exists'
	
	SELECT *, CASE active_ind
					WHEN 1  THEN 'Active'
					WHEN 0	THEN 'Inactive'
			END as active_desc
	INTO #role_temp_tb	
	FROM role_tb
	WHERE role_id =@pin_role_id 
	
	IF ( SELECT COUNT(role_id) 
	FROM #role_temp_tb)>0
	BEGIN
		SELECT * 
		FROM #role_temp_tb
		
		SET @pout_msg_cd='SUCCESS_GET'
		SET @pout_msg_txt=''
	END	
	
END
GO
