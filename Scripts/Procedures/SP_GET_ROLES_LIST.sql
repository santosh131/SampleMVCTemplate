CREATE PROCEDURE SP_GET_ROLES_LIST
	@pin_role_name_txt varchar(100),
	@pin_status_cd varchar(10),
	@pin_page_index int,	
	@pin_sort_col_name_txt varchar(50),	
	@pin_sort_asc_desc varchar(5),
	@pout_tot_pages	int OUTPUT,
	@pout_msg_cd	varchar(30) OUTPUT,
	@pout_msg_txt	varchar(250) OUTPUT	
AS
BEGIN
 
	SET NOCOUNT ON;
	SET @pout_msg_cd='ERROR'
	SET @pout_msg_txt='No Records Found'
	
	BEGIN TRY
		
		DECLARE @page_size as int
		DECLARE @page_offset as int
		DECLARE @status_cd as bit
		DECLARE @dynQuery as  nvarchar(max)
		
		SET @pout_tot_pages =1
		SET @status_cd =0
		SET @page_size =10
		SET @page_offset =@page_size *( @pin_page_index -1)
	 

		IF @pin_status_cd =1
		BEGIN
			SET @status_cd =1
		END
		
		IF ISNULL(@pin_sort_col_name_txt,'') =''
		BEGIN
			SET @pin_sort_col_name_txt ='role_name_txt'
		END 
		
		SELECT * INTO #temp_role_tb
		FROM (
			SELECT *,CASE R.active_ind
					WHEN 1  THEN 'Active'
					WHEN 0	THEN 'Inactive'
			END as active_desc
			FROM role_tb R
			WHERE 
			ISNULL(R.role_name_txt,'') like '%' + ISNULL(@pin_role_name_txt,'') + '%'
			AND (R.active_ind =@status_cd or  @pin_status_cd  =-2)
		) As X
		
		SELECT @pout_tot_pages= COUNT(role_id) 
		FROM #temp_role_tb

		IF @pout_tot_pages % @page_size =0
		BEGIN
			SET @pout_tot_pages =@pout_tot_pages/@page_size
		END
		ELSE
		BEGIN
			SET @pout_tot_pages =@pout_tot_pages/@page_size + 1
		END
		
		SET @dynQuery = 'SELECT * FROM #temp_role_tb order by ' +
						@pin_sort_col_name_txt +' '+ @pin_sort_asc_desc +
						' offset ' + CAST( @page_offset as varchar(10))  +' ROWS' + 
						' FETCH NEXT ' + CAST( @page_size  as varchar(10)) + ' ROWS ONLY OPTION (RECOMPILE)'
						
		EXEC  sp_executesql @dynQuery
		
		
		SET @pout_msg_cd='SUCCESS_LIST'
		SET @pout_msg_txt=''
		
	END TRY 
	BEGIN CATCH
		SET @pout_msg_cd='ERROR'
		SET @pout_msg_txt='Exception occured while retrieving data' + ERROR_MESSAGE()
	END CATCH	
	
END
GO
