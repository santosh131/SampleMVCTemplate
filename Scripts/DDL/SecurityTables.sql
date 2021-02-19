create table user_tb
(
 user_id_txt varchar(50) not null,
 user_name_txt varchar(30) not null,
 last_name_txt varchar(30),
 first_name_txt varchar(30),
 email_id_txt varchar(100),
 mobile_no_txt varchar(100),
 addr1_txt varchar(100),
 addr2_txt varchar(100),
 state_cd varchar(10),
 city_txt varchar(100),
 zip_cd_txt varchar(15),
 pwd_txt varchar(200) not null,
 active_ind bit  not null,
 created_by varchar(30) not null,
 created_dt DateTime not null,
 updated_by varchar(30),
 updated_dt DateTime,
 primary key(user_id_txt) 
)

create table role_tb
(
 role_id varchar(50) not null,
 role_name_txt varchar(100) not null,
 created_by varchar(30) not null,
 created_dt DateTime not null,
 updated_by varchar(30),
 updated_dt DateTime,
 active_ind bit  not null,
 primary key(role_id) 
)

create table menu_tb
(
 menu_id varchar(50) not null,
 menu_name_txt varchar(100) not null,
 controller_name_txt varchar(100) not null,
 sub_menu_ind bit,
 parent_menu_id varchar(50),
 menu_category_id varchar(50),
 active_ind bit,
 created_by varchar(30) not null,
 created_dt DateTime not null,
 updated_by varchar(30),
 updated_dt DateTime,
 primary key(menu_id) 
)

create table menu_category_tb
(
 menu_category_id varchar(50) not null,
 menu_category_name_txt varchar(100) not null, 
 active_ind bit,
 created_by varchar(30) not null,
 created_dt DateTime not null,
 updated_by varchar(30),
 updated_dt DateTime,
 primary key(menu_category_id) 
)

create table user_role_tb
(
 user_role_id varchar(50) not null,
 role_id varchar(50) not null,
 user_id_txt varchar(50) not null,
 created_by varchar(30) not null,
 created_dt DateTime not null,
 primary key(user_role_id) 
)

create table role_menu_tb
(
 role_menu_id varchar(50) not null,
 role_id varchar(50) not null,
 menu_id varchar(50) not null,
 created_by varchar(30) not null,
 created_dt DateTime not null,
 primary key(role_menu_id) 
)

create table menu_action_tb
(
 menu_action_id varchar(50) not null,
 menu_id varchar(50) not null,
 menu_action_cd varchar(100) not null,
 menu_action_name_txt varchar(100) not null,
 action_desc_txt varchar(100) not null,
 menu_ind bit,
 edit_locked_ind bit,
 created_by varchar(30) not null,
 created_dt DateTime not null,
 updated_by varchar(30),
 updated_dt DateTime,
 primary key(menu_action_id) 
)

create table role_menu_action_tb
(
 role_menu_action_id  varchar(50) not null,
 role_menu_id varchar(50) not null,
 menu_action_id varchar(50) not null,
 created_by varchar(30) not null,
 created_dt DateTime not null,
 primary key(role_menu_action_id) 
)

create table user_role_menu_action_tb
(
 user_role_menu_action_id  varchar(50) not null,
 user_role_id varchar(50) not null,
 menu_action_id varchar(50) not null,
 allowed_ind bit not null,
 created_by varchar(30) not null,
 created_dt DateTime not null,
 primary key(user_role_menu_action_id) 
)

create table user_session_tb
(
 session_id_txt varchar(50) not null,
 user_id_txt varchar(50) not null,
 user_name_txt varchar(30) not null, 
 role_menu_action_txt varchar(max) null,
 primary key(session_id_txt ) 
)

