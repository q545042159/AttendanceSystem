<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        //在应用程序启动时运行的代码
        Application.Lock();
        Application["online"] = 0;
        Application.UnLock();
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //在应用程序关闭时运行的代码
    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        //在出现未处理的错误时运行的代码
    }

    void Session_Start(object sender, EventArgs e) 
    {   
        Application.Lock();
        Application["online"] = (Int32)Application["online"] + 1;//Application最经典的作用就是记录在线人数
        Application.UnLock();
        //在新会话启动时运行的代码
    }

    void Session_End(object sender, EventArgs e) 
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。

        /*Session["Role"] = null;
        Session["UserID"] = null;
        Session["UserName"] = null;
        Session["UserPWD"] = null;
        Session["Code"] = null;*/
        
        Application.Lock();
        Application["online"] = (Int32)Application["online"] - 1;
        Application.UnLock();
    }
       
</script>
