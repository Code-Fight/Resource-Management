using System;
using KASXBean.System;

namespace Entity.@base
{
    /// <summary>
    /// 基本实体类
    /// </summary>
    [Serializable]
    public class BaseEntity
    {
        private UserEntity _insert_user;
        private DateTime _insert_datetime = DateTime.MinValue;
        private UserEntity _update_user;
        private DateTime _update_datetime = DateTime.MinValue;
        private int _delete_flag = 0;//删除标记（1：删除，0：未删除）
        private UserEntity _delete_user;//删除人
        private DateTime _delete_datetime = DateTime.MinValue;//删除时间
        protected BaseEntity _base;
        public BaseEntity()
        {

        }

        /// <summary>
        /// 增加者
        /// </summary>
        public UserEntity insert_user
        {
            set { _insert_user = value; }
            get { return _insert_user; }
        }
        /// <summary>
        /// 增加时间
        /// </summary>
        public DateTime insert_datetime
        {
            set { _insert_datetime = value; }
            get { return _insert_datetime; }
        }
        /// <summary>
        /// 更新者
        /// </summary>
        public UserEntity update_user
        {
            set { _update_user = value; }
            get { return _update_user; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime update_datetime
        {
            set { _update_datetime = value; }
            get { return _update_datetime; }
        }

        /// <summary>
        /// 删除标记（1：删除，0：未删除）
        /// </summary>
        public int delete_flag
        {
            get
            {
                return _delete_flag;
            }
            set
            {
                _delete_flag = value;
            }
        }

        /// <summary>
        /// 删除者
        /// </summary>
        public UserEntity delete_user
        {
            get
            {
                return _delete_user;
            }
            set
            {
                _delete_user = value;
            }
        }
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime delete_datetime
        {
            get
            {
                return _delete_datetime;
            }
            set
            {
                _delete_datetime = value;
            }
        }

        public virtual BaseEntity Copy()
        {
            if (_base == null)
            {
                _base = new BaseEntity();
            }
            _base.delete_flag = delete_flag;
            _base.delete_user.Userid = delete_user.Userid;
            _base.delete_datetime = delete_datetime;
            _base.insert_user.Userid = insert_user.Userid;
            _base.insert_datetime = insert_datetime;
            _base.update_user.Userid = update_user.Userid;
            _base.update_datetime = update_datetime;
            return _base;
        }
    }
}
