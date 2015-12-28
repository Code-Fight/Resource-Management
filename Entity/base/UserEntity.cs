using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity.@base;

namespace KASXBean.System
{
    /// <summary>
    /// 员工
    /// </summary>
    [Serializable]
    public class UserEntity : BaseEntity
    {
        #region Model
        private int _user_id = 0;
        private string _user_sign = string.Empty;
        private string _code = string.Empty;
        private string _name = string.Empty;
        private string _password = string.Empty;
        
        private int _remove = 0;//调动
        private EmUserType _type = 0;
        private int _user_sort = 0;
        private int _user_flag = 0;

        public int User_flag
        {
            get { return _user_flag; }
            set { _user_flag = value; }
        }
        public UserEntity()
        {
           
        }

     
        /// <summary>
        /// 员工id
        /// </summary>
        public int Userid
        {
            set { _user_id = value; }
            get { return _user_id; }
        }
        /// <summary>
        /// 员工工号
        /// </summary>
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// 员工符号
        /// </summary>
        public string UserSign
        {
            set { _user_sign = value; }
            get { return _user_sign; }
        }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 员工密码
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }
       
      
        /// <summary>
        /// 调动
        /// </summary>
        public int Remove
        {
            get
            {
                return _remove;
            }
            set
            {
                _remove = value;
            }
        }

       


        /// <summary>
        /// 类型
        /// </summary>
        public EmUserType Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 级别
        /// </summary>
        public int UserSort
        {
            set { _user_sort = value; }
            get { return _user_sort; }
        }
        #endregion Model
    }
    public enum EmUserType
    {
        Work = 1,
        temp = 2
    }
}
