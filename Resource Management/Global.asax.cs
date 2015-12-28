using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Resource_Management
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            #region 防止删除文件夹时重启AppDomain

            //在应用程序启动时运行的代码
            PropertyInfo p = typeof(HttpRuntime).GetProperty("FileChangesMonitor",
                BindingFlags.NonPublic | BindingFlags.Public |
                BindingFlags.Static);
            object o = p.GetValue(null, null);
            FieldInfo f = o.GetType()
                .GetField("_dirMonSubdirs",
                    BindingFlags.Instance | BindingFlags.NonPublic |
                    BindingFlags.IgnoreCase);
            object monitor = f.GetValue(o);
            MethodInfo m = monitor.GetType()
                .GetMethod("StopMonitoring",
                    BindingFlags.Instance | BindingFlags.NonPublic);
            m.Invoke(monitor, new object[] { });

            #endregion
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}