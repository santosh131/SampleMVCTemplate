create table common_ddl_tb
(
 ddl_id int not null,
 filed_cd varchar(30) not null,
 code_txt varchar(30) not null,	
 desc_txt varchar(100) not null,
 primary key(ddl_id) 
)