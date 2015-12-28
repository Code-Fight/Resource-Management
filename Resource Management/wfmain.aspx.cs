using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using Entity.@base;
using KASXBean.System;

namespace Resource_Management
{
    public partial class wfmain : System.Web.UI.Page
    {
        protected const int _allid = 0;
        protected const string _alltext = "全部";
        /// <summary>
        /// 操作类型
        /// </summary>
        public enum EnExcuteType
        {
            /// <summary>
            /// 无
            /// </summary>
            None = 0,
            /// <summary>
            /// 增加
            /// </summary>
            Add = 1,
            /// <summary>
            /// 修改
            /// </summary>
            Update = 2,
            /// <summary>
            /// 删除
            /// </summary>
            Delete = 3,
            /// <summary>
            /// 查询
            /// </summary>
            Search = 4,
            /// <summary>
            /// 对比
            /// </summary>
            Compare = 5,
            /// <summary>
            /// 回复
            /// </summary>
            Answer = 6


        }
        /// <summary>
        /// 分页查询类
        /// </summary>
        protected PagerQueryParam Pager
        {
            get
            {
                if (ViewState["pager"] == null)
                {
                    ViewState["pager"] = new PagerQueryParam();
                    return ViewState["pager"] as PagerQueryParam;
                }
                return ViewState["pager"] as PagerQueryParam;
            }
            set
            {
                ViewState["pager"] = value;
            }
        }

        /// <summary>
        /// 执行类型
        /// </summary>
        protected EnExcuteType _excutetype
        {
            get
            {
                if (ViewState["excutetype"] == null)
                {
                    return EnExcuteType.None;
                }
                return (EnExcuteType)ViewState["excutetype"];
            }
            set
            {
                ViewState["excutetype"] = value;
            }
        }


        /// <summary>
        /// 页面变换
        /// </summary>
        protected bool _pagerflag
        {
            get
            {
                if (ViewState["pagerflag"] == null)
                {
                    return true;
                }
                return (bool)ViewState["pagerflag"];
            }
            set
            {
                ViewState["pagerflag"] = value;
            }
        }

        /// <summary>
        /// 标题
        /// </summary>
        protected string _title
        {
            get
            {
                if (ViewState["title"] == null)
                {
                    return string.Empty;
                }
                return ViewState["title"].ToString();
            }
            set
            {
                ViewState["title"] = value;
            }
        }

      

        /// <summary>
        /// 用户
        /// </summary>
        protected UserEntity _user
        {
            get
            {
                if (Session["user"] == null)
                {
                    Session["user"] = new UserEntity();
                }
                return Session["user"] as UserEntity;
            }
            set
            {
                Session["user"] = value;
            }
        }

        /// <summary>
        /// 记录用户对于客调命令的权限状态
        /// </summary>
        protected int _perid
        {
            get
            {
                if (ViewState["perid"] != null && ViewState["perid"] != DBNull.Value)
                {
                    return (int)ViewState["perid"];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["perid"] = value;
            }
        }


        protected void IsSessionExist()
        {
            if (_user.Userid == 0)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('用户当前登录已过期，请重新登录！');window.top.location.href='../../login.aspx';", true);
            }
        }
        /// <summary>
        /// 设置参数信息
        /// </summary>
        public virtual void SetParam()
        {
        }
        /// <summary>
        /// 得到参数信息
        /// </summary>
        public virtual void GetParam()
        {
            _excutetype = (EnExcuteType)Enum.Parse(typeof(EnExcuteType), GetQueryString("mode"));
        }
        protected virtual string GetParamInfo()
        {
            return "title=" + GetTitle() + "&pageindex=" + GetPageIndex();
        }

        /// <summary>
        /// 是否为管理员 True是，False不是；
        /// </summary>
        /// <returns></returns>
        public virtual bool IsAdmin()
        {
            if (_user.Code != "admin")
            {
                return false;
            }

            return true;
        }

       
       


        protected bool IsRegister()
        {
            if (_user == null)
            {
                this.Response.Write("<script>alert('用户当前登录已过期，请重新登录！');window.top.location.href='../../login.aspx';</script>");
                return false;
            }
            if (_user.Name == null || _user.Name.Trim().Length == 0)
            {
                this.Response.Write("<script>alert('用户当前登录已过期，请重新登录！');window.top.location.href='../../login.aspx';</script>");
                return false;
            }
            return true;
        }
        protected bool IsRegister(UpdatePanel control)
        {
            if (_user == null)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(control, control.GetType(), "", "alert('用户当前登录已过期，请重新登录！');window.top.location.href='../../login.aspx';", true);
                return false;
            }
            if (_user.Name == null || _user.Name.Trim().Length == 0)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(control, control.GetType(), "", "alert('用户当前登录已过期，请重新登录！');window.top.location.href='../../login.aspx';", true);
                return false;
            }
            return true;
        }
        protected string GetOmitStr(string OldStr)
        {
            string OmitStr = string.Empty;
            if (OldStr.Length < 10)
            {
                return OldStr;
            }
            OmitStr = OldStr.Substring(0, 10) + "...";
            return OmitStr;
        }

        protected virtual void AspNetPager_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            Pager.PageIndex = e.NewPageIndex;
            _pagerflag = false;
            FillForm();
            RefreshForm();
        }

        protected virtual void InitPage()
        {

        }

        protected virtual void FillForm()
        {
            RefreshForm();
        }
        /// <summary>
        /// 得到前一个界面传过来的值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected string GetQueryString(string name)
        {
            if (Request.QueryString[name] == null) return string.Empty;
            return Request.QueryString[name].ToString();
        }

        /// <summary>
        /// 得到前一个界面传过来的值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected int GetQueryStringToInt(string name)
        {
            string strmachineid = GetQueryString(name);
            try
            {
                return int.Parse(strmachineid);
            }
            catch
            {
                return 0;
            }

        }

        protected long GetQueryStringTolong(string name)
        {
            string strmachineid = GetQueryString(name);
            try
            {
                return long.Parse(strmachineid);
            }
            catch
            {
                return 0;
            }

        }

        protected DateTime GetQueryStringToDateTime(string name)
        {
            string strdatetime = GetQueryString(name);
            try
            {
                return DateTime.Parse(strdatetime);
            }
            catch
            {
                return DateTime.MinValue;
            }

        }

        /// <summary>
        /// 得到页码
        /// </summary>
        protected int GetPageIndex()
        {
            if (_pagerflag)
            {
                string strpageindex = this.GetQueryString("pageindex");
                if (strpageindex.Length > 0)
                {
                    Pager.PageIndex = int.Parse(strpageindex);
                }
            }
            _pagerflag = true;
            return Pager.PageIndex;
        }

        /// <summary>
        /// 得到标题
        /// </summary>
        protected string GetTitle()
        {
            _title = GetQueryString("title");

            return _title;
        }

        /// <summary>
        /// 得到标题名称
        /// </summary>
        /// <returns></returns>
        protected string GetTitleName()
        {
            GetTitle();
            //地图界面不显示
            if (_title.Length == 0) return string.Empty;
            string[] strtitle = _title.Split(',');
            if (strtitle.Length < 3) return string.Empty;
            return strtitle[2];

        }
        protected static string getPinYin(string text)
        {
            char pinyin;
            byte[] array;
            System.Text.StringBuilder sb = new System.Text.StringBuilder(text.Length);
            foreach (char c in text)
            {
                pinyin = c;
                array = System.Text.Encoding.Default.GetBytes(new char[] { c });
                if (array.Length == 2)
                {
                    int i = array[0] * 0x100 + array[1];
                    if (i < 0xB0A1) pinyin = c;
                    else
                        if (i < 0xB0C5) pinyin = 'a';
                        else
                            if (i < 0xB2C1) pinyin = 'b';
                            else
                                if (i < 0xB4EE) pinyin = 'c';
                                else
                                    if (i < 0xB6EA) pinyin = 'd';
                                    else
                                        if (i < 0xB7A2) pinyin = 'e';
                                        else
                                            if (i < 0xB8C1) pinyin = 'f';
                                            else
                                                if (i < 0xB9FE) pinyin = 'g';
                                                else
                                                    if (i < 0xBBF7) pinyin = 'h';
                                                    else
                                                        if (i < 0xBFA6) pinyin = 'g';
                                                        else
                                                            if (i < 0xC0AC) pinyin = 'k';
                                                            else
                                                                if (i < 0xC2E8) pinyin = 'l';
                                                                else
                                                                    if (i < 0xC4C3) pinyin = 'm';
                                                                    else
                                                                        if (i < 0xC5B6) pinyin = 'n';
                                                                        else
                                                                            if (i < 0xC5BE) pinyin = 'o';
                                                                            else
                                                                                if (i < 0xC6DA) pinyin = 'p';
                                                                                else
                                                                                    if (i < 0xC8BB) pinyin = 'q';
                                                                                    else
                                                                                        if (i < 0xC8F6) pinyin = 'r';
                                                                                        else
                                                                                            if (i < 0xCBFA) pinyin = 's';
                                                                                            else
                                                                                                if (i < 0xCDDA) pinyin = 't';
                                                                                                else
                                                                                                    if (i < 0xCEF4) pinyin = 'w';
                                                                                                    else
                                                                                                        if (i < 0xD1B9) pinyin = 'x';
                                                                                                        else
                                                                                                            if (i < 0xD4D1) pinyin = 'y';
                                                                                                            else
                                                                                                                if (i < 0xD7FA) pinyin = 'z';
                }
                sb.Append(pinyin);
            }
            return sb.ToString();
        }

        protected int GetOrg()
        {
            return 1;
        }

        #region 直接删除指定目录下的所有文件及文件夹(保留目录) by zwliu 201051015
        /// <summary>
        /// 直接删除指定目录下的所有文件及文件夹(保留目录)
        /// </summary>
        /// <param name="strPath">文件夹路径</param>
        /// <returns>执行结果</returns>
        public bool DeleteDir(string strPath)
        {
            try
            {
                // 清除空格
                strPath = @strPath.Trim().ToString();
                // 判断文件夹是否存在
                if (System.IO.Directory.Exists(strPath))
                {
                    // 获得文件夹数组
                    string[] strDirs = System.IO.Directory.GetDirectories(strPath);
                    // 获得文件数组
                    string[] strFiles = System.IO.Directory.GetFiles(strPath);
                    // 遍历所有子文件夹
                    foreach (string strFile in strFiles)
                    {
                        // 删除文件夹
                        System.IO.File.Delete(strFile);
                    }
                    // 遍历所有文件
                    foreach (string strdir in strDirs)
                    {
                        // 删除文件
                        System.IO.Directory.Delete(strdir, true);
                    }
                }
                // 成功
                return true;
            }
            catch (Exception Exp) // 异常处理
            {
                // 异常信息
                System.Diagnostics.Debug.Write(Exp.Message.ToString());
                // 失败
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 刷新窗体
        /// </summary>
        protected void RefreshForm()
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "", "settablestyle();", true);
        }


        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="saveFileName"></param>
        /// <param name="filePath"></param>
        protected virtual void FileSaveAs(string saveFileName, string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                System.Web.HttpContext.Current.Response.Write("<script>window.alert('你所要下载的文件不存在！');</script>");
                return;
            }
            string contentType = "application/unknown";

            string extension = System.IO.Path.GetExtension(filePath);
            Microsoft.Win32.RegistryKey classesRoot = Microsoft.Win32.Registry.ClassesRoot;
            Microsoft.Win32.RegistryKey extensionKey = classesRoot.OpenSubKey(extension);
            contentType = extensionKey.GetValue("Content Type").ToString();

            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Buffer = true;
            System.Web.HttpContext.Current.Response.ContentType = contentType;
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;

            //修正下载乱码问题 即可
            if (HttpContext.Current.Request.UserAgent != null &&
            (System.Web.HttpContext.Current.Request.UserAgent != null &&
                HttpContext.Current.Request.UserAgent.ToLower().IndexOf("msie", StringComparison.Ordinal) > -1))
            {
                //当客户端使用IE时，对其进行编码；We should encode the filename when our visitors use IE  
                //使用 ToHexString 代替传统的 UrlEncode()；We use "ToHexString" replaced "context.Server.UrlEncode(fileName)"  
                saveFileName = ToHexString(saveFileName);
            }
            if (HttpContext.Current.Request.UserAgent != null &&
                (System.Web.HttpContext.Current.Request.UserAgent != null &&
                 HttpContext.Current.Request.UserAgent.ToLower().IndexOf("firefox", StringComparison.Ordinal) >
                 -1))
            {
                //为了向客户端输出空格，需要在当客户端使用 Firefox 时特殊处理  
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition",
                    "attachment;filename=" + saveFileName + "");

            }
            else
            {
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition",
                    "attachment;filename=" + saveFileName);
            }

            //System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlPathEncode(saveFileName));
            System.Web.HttpContext.Current.Response.WriteFile(filePath);
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Close();
            System.Web.HttpContext.Current.Response.End();
        }

       
        /// <summary>
        /// 弹出窗口
        /// </summary>
        /// <param name="str"></param>
        public void Alert(string str)
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "",
                "AletMsg('" + str + "');", true);
        }




        public void DownLoadFile(string filePath,string fileName="")
        {
            System.IO.Stream iStream = null;

            // Buffer to read 10K bytes in chunk:
            byte[] buffer = new Byte[10240];

            // Length of the file:
            int length;

            // Total bytes to read:
            long dataToRead;
            if (!File.Exists(filePath))
            {
              return ;
            }
            // Identify the file to download including its path.
            string filepath = filePath;

            // Identify the file name.
            string filename = System.IO.Path.GetFileName(filepath);

            try
            {
                // Open the file.
                iStream = new System.IO.FileStream(filepath, System.IO.FileMode.Open,
                    System.IO.FileAccess.Read, System.IO.FileShare.Read);
                Response.Clear();

                // Total bytes to read:
                dataToRead = iStream.Length;

                long p = 0;
                if (Request.Headers["Range"] != null)
                {
                    Response.StatusCode = 206;
                    p = long.Parse(Request.Headers["Range"].Replace("bytes=", "").Replace("-", ""));
                }
                if (p != 0)
                {
                    Response.AddHeader("Content-Range", "bytes " + p.ToString() + "-" + ((long)(dataToRead - 1)).ToString() + "/" + dataToRead.ToString());
                }
                Response.AddHeader("Content-Length", ((long)(dataToRead - p)).ToString());
                Response.ContentType = "application/octet-stream";
                //修正下载乱码问题 即可
                if (HttpContext.Current.Request.UserAgent != null &&
                (System.Web.HttpContext.Current.Request.UserAgent != null &&
                    HttpContext.Current.Request.UserAgent.ToLower().IndexOf("msie", StringComparison.Ordinal) > -1))
                {
                    //当客户端使用IE时，对其进行编码；We should encode the filename when our visitors use IE  
                    //使用 ToHexString 代替传统的 UrlEncode()；We use "ToHexString" replaced "context.Server.UrlEncode(fileName)"  
                    filename = ToHexString(filename);
                }
                if (HttpContext.Current.Request.UserAgent != null &&
                    (System.Web.HttpContext.Current.Request.UserAgent != null &&
                     HttpContext.Current.Request.UserAgent.ToLower().IndexOf("firefox", StringComparison.Ordinal) >
                     -1))
                {
                    //为了向客户端输出空格，需要在当客户端使用 Firefox 时特殊处理  
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition",
                        "attachment;filename=" + (fileName.Length == 0 ? filename : fileName) + "");

                }
                else
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition",
                        "attachment;filename=" + filename);
                }
              //  Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(Request.ContentEncoding.GetBytes(filename)));

                iStream.Position = p;
                dataToRead = dataToRead - p;
                // Read the bytes.
                while (dataToRead > 0)
                {
                    // Verify that the client is connected.
                    if (Response.IsClientConnected)
                    {
                        // Read the data in buffer.
                        length = iStream.Read(buffer, 0, 10240);

                        // Write the data to the current output stream.
                        Response.OutputStream.Write(buffer, 0, length);

                        // Flush the data to the HTML output.
                        Response.Flush();

                        buffer = new Byte[10240];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        //prevent infinite loop if user disconnects
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                // Trap the error, if any.
                Response.Write("Error : " + ex.Message);
            }
            finally
            {
                if (iStream != null)
                {
                    //Close the file.
                    iStream.Close();
                }
                Response.End();
            }
        }

        #region 编码
        /// <summary>
        /// 对字符串中的非 ASCII 字符进行编码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToHexString(string s)
        {
            char[] chars = s.ToCharArray();
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < chars.Length; index++)
            {
                bool needToEncode = NeedToEncode(chars[index]);
                if (needToEncode)
                {
                    string encodedString = ToHexString(chars[index]);
                    builder.Append(encodedString);
                }
                else
                {
                    builder.Append(chars[index]);
                }
            }
            return builder.ToString();
        }
        /// <summary>
        /// 判断字符是否需要使用特殊的 ToHexString 的编码方式
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private static bool NeedToEncode(char chr)
        {
            string reservedChars = "$-_.+!*'(),@=&";
            if (chr > 127)
                return true;
            if (char.IsLetterOrDigit(chr) || reservedChars.IndexOf(chr) >= 0)
                return false;
            return true;
        }
        /// <summary>
        /// 为非 ASCII 字符编码
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private static string ToHexString(char chr)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] encodedBytes = utf8.GetBytes(chr.ToString());
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < encodedBytes.Length; index++)
            {
                builder.AppendFormat("%{0}", Convert.ToString(encodedBytes[index], 16));
            }
            return builder.ToString();
        }
        #endregion



    }
}
