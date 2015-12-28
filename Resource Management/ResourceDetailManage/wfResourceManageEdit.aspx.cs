using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using BLL.ResourceDetail;
using BLL.ResourceType;
using BLL.ResourceUpload;
using DAL;
using Entity;

namespace Resource_Management.ResourceDetailManage
{
    /// <summary>
    /// 备品规格型号字典 -- 编辑
    /// </summary>
    public partial class wfResourceManageEdit : wfmain
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
        public string _oldname
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
        private ResourceDetailEntity _complaintstype
        {
            get
            {
                if (ViewState["complaintstype"] == null)
                {
                    ViewState["complaintstype"] = new ResourceDetailEntity();
                }
                return ViewState["complaintstype"] as ResourceDetailEntity;
                   ;
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
                FillFormDetail();
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
            
            if (_excutetype == EnExcuteType.Update || _excutetype == EnExcuteType.Add)
            {
                ResourceTypeManager resourceTypeManager=new ResourceTypeManager();
                DDL_type.DataSource = resourceTypeManager.GetAllResourceType();
                DDL_type.DataBind();
                DDL_type.Items.Insert(0, "");
            }
            if (_excutetype == EnExcuteType.Update || _excutetype == EnExcuteType.Search)
            {
                ResourceDetailManager bll = new ResourceDetailManager();

                _complaintstype = bll.GetInfo(_complaintstype.Id);
                lb_name.Text = _complaintstype.Name;
                tb_name.Value = _complaintstype.Name;
                lb_memo.Text = _complaintstype.Memo;
                tb_memo.Value = _complaintstype.Memo;
                lb_upload_people.Text = _complaintstype.UploadPeople;
                tb_upload_people.Value = _complaintstype.UploadPeople;
                //lb_downfiles.Text = _complaintstype.Name +
                //                    _complaintstype.Url.Substring(_complaintstype.Url.LastIndexOf('.'),
                //                        _complaintstype.Url.Length - _complaintstype.Url.LastIndexOf('.'));
                if (_complaintstype.Type != null)
                {
                    lb_type.Text = _complaintstype.Type.Name;
                    DDL_type.SelectedValue = _complaintstype.Type.Id.ToString();
                }
            }
        }
        protected void FillFormDetail()
        {
            int TotalCount = Pager.TotalCount;
            string ErrMsg = string.Empty;
            Pager.StrParm = " and resource_detail_id='" + _complaintstype.Id + "'";
            ResourceUploadBussiness dep = new ResourceUploadBussiness();
            DataTable dt = dep.GetList(Pager, out TotalCount, _complaintstype.Id);
            if (TotalCount > 0)
            {
               
                Rept.DataSource = dt;
                Pager.TotalCount = TotalCount;
               

                Rept.DataBind();
            }
            else
            {
                Rept.DataSource = new DataTable();
                Rept.DataBind();
            }
            
        }

        private void Clear()
        {
            tb_name.Value = string.Empty;
           
        }

        /// <summary>
        /// 得到信息
        /// </summary>
        private void GetInfo()
        {
            //txtname.Value = _complaintstype.Model;
            //_oldname = _complaintstype.Model;//把原来的名字保存下 方便下一次调用
            if (_excutetype == EnExcuteType.Search)
            {

                //lblname.Text = _complaintstype.Model;

            }
            if (_excutetype == EnExcuteType.Add)
            {
                tb_upload_people.Value = _user.Name;
            }

        }

        /// <summary>
        /// 设置信息
        /// </summary>
        private void setInfo()
        {
            _complaintstype.Name = tb_name.Value;
            _complaintstype.Memo = tb_memo.Value;
            _complaintstype.UploadPeople = tb_upload_people.Value;
            _complaintstype.Type = new SpareResourceTypeEntity()
            {
                Id =CommonDBCheck.ToInt(DDL_type.SelectedValue),
                Name = DDL_type.SelectedItem.Text
            };

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
            Response.Redirect("wfResourceManageSearch.aspx?" + GetParamInfo());
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
                    if (!uploadfiles())
                    {

                        return;
                    }
                    insertInfo();

                }
                if (_excutetype == EnExcuteType.Update)
                {
                    updateInfo();

                }
                Response.Redirect("wfResourceManageSearch.aspx?" + GetParamInfo());
            }
            catch (Exception ex)
            {
                UiLabErr.Text = ex.Message;
            }
           
        }

        
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <returns></returns>
        public bool uploadfiles()
        {
            //if (FileUpload1.HasFile)
            //{
            //    //判断文件是否小于10Mb
            //    //if (FileUpload1.PostedFile.ContentLength < 10485760)
            //    //{
            //        try
            //        {
            //            //上传文件并指定上传目录的路径
            //            string name = DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload1.FileName;
            //            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Window/ResourceDetailManage/upload/"+DDL_type.SelectedItem.Text+"/")
            //                + name);
                       
                      
            //            _complaintstype.Url = "~/Window/ResourceDetailManage/upload/" + DDL_type.SelectedItem.Text + "/" +
            //                                  name;
            //            return true;
            //        }
            //        catch (Exception ex)
            //        {
            //            Alert("文件上传失败！");
            //            return false;
            //            //lblMessage.Text += ex.Message;
            //        }

            //    //}
            //    //else
            //    //{
            //    //    lblMessage.Text = "上传文件不能大于10MB!";
            //    //}
            //}
            //else
            //{
            //    Alert("尚未选择文件！");
            //}
            return false;
        }

        /// <summary>
        /// 增加信息
        /// </summary>
        private void insertInfo()
        {
       
            _complaintstype.insert_user = _user;
            ResourceDetailManager dep = new ResourceDetailManager();
            dep.Add(_complaintstype);
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        private void updateInfo()
        {
            _complaintstype.update_user = _user;
            ResourceDetailManager dep = new ResourceDetailManager();
            dep.Update(_complaintstype);
        }

        protected void lb_downfiles_Click(object sender, EventArgs e)
        {

            //download_id.Text = tbx.Text;
            // download_Click(null, null);

            string hz = _complaintstype.Url.Substring(_complaintstype.Url.LastIndexOf('.'), _complaintstype.Url.Length - _complaintstype.Url.LastIndexOf('.'));
            string fileName = _complaintstype.Name + hz;//客户端保存的文件名
            string filePath = Server.MapPath(_complaintstype.Url);//路径
            FileSaveAs(fileName, filePath);
        }

        protected void DDL_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            _oldname = DDL_type.SelectedValue;
        }
        protected void Rept_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            TextBox tbx = (TextBox)e.Item.FindControl("id");
            TextBox url = (TextBox)e.Item.FindControl("url");
            LinkButton name = (LinkButton) e.Item.FindControl("txtname");
            if (e.CommandName == "download")
            {

                string fileName = name.Text;//客户端保存的文件名
                string filePath = Server.MapPath(url.Text);//路径
                HttpContext c=Context;
                //DownloadFile(c, filePath, 99999999);
                DownLoadFile(filePath);
            }
            if (e.CommandName == "delete")
            {
                ResourceDetailManager detailManager = new ResourceDetailManager();
                detailManager.DeleteUpload(CommonDBCheck.ToInt(tbx.Text), 0);
                FillFormDetail();
                //RefreshForm();

            }
            
        }
    }
}
