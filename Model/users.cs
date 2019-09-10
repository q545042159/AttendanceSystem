using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   public  class users
    {
       public class TabTeachers 
       {
           private string Department;
           private string UserID;
           private string UserName;
           private string UserPWD;
           private string Sex;
           private string Role;
           private string _Department
           {
               get { return Department; }
               set { Department = value; }
           }

           public string _UserID
           {
               get { return UserID; }
               set { UserID = value; }
           }

           public string _UserName
           {
               get { return UserName; }
               set { UserName = value; }
           }

           public string _UserPWD
           {
               get { return UserPWD; }
               set { UserPWD = value; }
           }

           public string _Role
           {
               get { return Role; }
               set { Role = value; }
           }

       }
    }
}
