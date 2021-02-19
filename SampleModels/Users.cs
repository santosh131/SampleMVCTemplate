using Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModels
{
    [Serializable]
    public class Users: ModelHelpers
    {
        private string _user_id;

        [DatabaseColumnName(ColumnName = "user_id_txt")]
        public string UserId
        {
            get { return _user_id; }
            set { _user_id = value; }
        }

        private string  _user_name;

        [DatabaseColumnName(ColumnName = "user_name_txt")]
        [Required(ErrorMessage = "UserName is required")]
        public string  UserName
        {
            get { return _user_name; }
            set { _user_name = value; }
        }

        [DatabaseColumnName(ColumnName = "last_name_txt")]
        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string LastName
        {
            get;set;
        }

        [DatabaseColumnName(ColumnName = "first_name_txt")]
        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string FirstName
        {
            get; set;
        }

        [DatabaseColumnName(ColumnName = "email_id_txt")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Invalid Email")]
        public string Email
        {
            get; set;
        }

        [DatabaseColumnName(ColumnName = "mobile_no_txt")]
        public string Mobile
        {
            get; set;
        }

        [DatabaseColumnName(ColumnName = "addr1_txt")]
        public string Address1
        {
            get; set;
        }

        [DatabaseColumnName(ColumnName = "addr2_txt")]
        public string Address2
        {
            get; set;
        }

        private string _state_cd = string.Empty;

        [DatabaseColumnName(ColumnName = "state_cd")]
        public string StateCode
        {
            get { return _state_cd; }
            set { _state_cd = value; }
        }

        [DatabaseColumnName(ColumnName = "city_txt")]
        public string City
        {
            get; set;
        }

        [DatabaseColumnName(ColumnName = "zip_cd_txt")]
        public string ZipCode
        {
            get; set;
        }

        [DatabaseColumnName(ColumnName = "active_ind")]
        public bool ActiveIndicator
        {
            get; set;
        }

        private string _pwd_txt;

        [DatabaseColumnName(ColumnName = "pwd_txt")]
        [Required]
        public string Password
        {
            get { return _pwd_txt; }
            set { _pwd_txt = value; }
        }

        private bool _remember_me;

        public bool RememberMe
        {
            get { return _remember_me; }
            set { _remember_me = value; }
        }

        private string _created_by;

        [DatabaseColumnName(ColumnName = "created_by")]
        public string CreatedBy
        {
            get { return _created_by; }
            set { _created_by = value; }
        }

        private Nullable<DateTime> _created_dt;

        [DatabaseColumnName(ColumnName = "created_dt")]
        public Nullable<DateTime> CreatedDate
        {
            get { return _created_dt; }
            set { _created_dt = value; }
        }

        private string _updated_by;

        [DatabaseColumnName(ColumnName = "updated_by")]
        public string UpdatedBy
        {
            get { return _updated_by; }
            set { _updated_by = value; }
        }

        private Nullable<DateTime> _updated_dt;

        [DatabaseColumnName(ColumnName = "updated_dt")]
        public Nullable<DateTime> UpdatedDate
        {
            get { return _updated_dt; }
            set { _updated_dt = value; }
        } 
         
    }
}
