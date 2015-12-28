using System;
using System.Configuration;
using System.IO;
using BLL.ResourceType;
using Entity;


namespace Resource_Management.ResourceTypeManage
{
    /// <summary>
    /// 资源类型
    /// </summary>
    public partial class wfResourceTypeManageEdit : wfmain
    {


        private string _searchname
        {
            get
            {
                if (ViewState["searchname"] == null)
                {
                    return string.Empty;
                }
                return ViewState["searchname"].ToString();
            }
            set
            {
                ViewState["searchname"] = value;
            }
        }
        private string _oldname
        {
            get
            {
                if (ViewState["oldname"] == null)
                {
                    return string.Empty;
                }
                return ViewState["oldname"].ToString();
            }
            set
            {
                ViewState["oldname"] = value;
            }
        }
        private SpareResourceTypeEntity _complaintstype
        {
            get
            {
                if (ViewState["complaintstype"] == null)
                {
                    ViewState["complaintstype"] = new SpareResourceTypeEntity();
                }
                return ViewState["complaintstype"] as SpareResourceTypeEntity;
            }
            set
            {
                ViewState["complaintstype"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                _excutetype = (EnExcuteType)Enum.Parse(typeof(EnExcuteType), GetQueryString("mode"));
                //Rept.Visible = false;
                GetParam();
                GetType();
                FillForm();
                //Setdata();
                GetInfo();
            }

        }


        /// <summary>
        /// 获取参数信息
        /// </summary>
        public override void GetParam()
        {

            _complaintstype.Id = GetQueryStringToInt("id");
            _searchname = GetQueryString("searchname");
        }
        protected override string GetParamInfo()
        {
            return base.GetParamInfo() + "&searchname=" + _searchname;
        }

        protected override void FillForm()
        {

            if (_excutetype == EnExcuteType.Add)
            {

                Clear();

            }
            if (_excutetype == EnExcuteType.Update || _excutetype == EnExcuteType.Search)
            {
                ResourceTypeManager bll = new ResourceTypeManager();

                _complaintstype = bll.GetInfo(_complaintstype.Id);
            }
        }


        private void Clear()
        {
            txtname.Value = string.Empty;

        }

        /// <summary>
        /// 得到信息
        /// </summary>
        private void GetInfo()
        {
            txtname.Value = _complaintstype.Name;
            _oldname = _complaintstype.Name;//把原来的名字保存下 方便下一次调用
            if (_excutetype == EnExcuteType.Search)
            {

                lblname.Text = _complaintstype.Name;

            }
            if (_excutetype == EnExcuteType.Update)
            {
                _oldname = txtname.Value = _complaintstype.Name;
            }

        }

        /// <summary>
        /// 设置信息
        /// </summary>
        private void setInfo()
        {
            _complaintstype.Name = txtname.Value;
            _complaintstype.Directory = txtname.Value;
            //_complaintstype.Id
        }


        /// <summary>
        /// 清除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        /// <summary>
        /// 返回按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfResourceTypeManageSearch.aspx?" + GetParamInfo());
        }
        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                setInfo();
                if (_excutetype == EnExcuteType.Add)
                {
                    insertInfo();

                }
                if (_excutetype == EnExcuteType.Update)
                {
                    updateInfo();

                }
                Response.Redirect("wfResourceTypeManageSearch.aspx?" + GetParamInfo());
            }
            catch (Exception ex)
            {
                UiLabErr.Text = ex.Message;
            }
          
        }

        /// <summary>
        /// 增加信息
        /// </summary>
        private void insertInfo()
        {
            CreateDirectory();
            _complaintstype.insert_user = _user;
            ResourceTypeManager dep = new ResourceTypeManager();
            dep.Add(_complaintstype);
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <returns></returns>
        public bool CreateDirectory()
        {
            string savePath = Server.MapPath(ConfigurationSettings.AppSettings["UploadFilePath"] + _complaintstype.Name);
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        private void updateInfo()
        {
            _complaintstype.update_user = _user;
            ResourceTypeManager dep = new ResourceTypeManager();
            dep.Update(_complaintstype);
        }


    }
}
