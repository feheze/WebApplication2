using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.ViewModel
{
    public class LoginViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public LoginModel Login_(string login, string pass)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@login", login));
            parameters.Add(new SqlParameter("@pass", pass));

            SqlDataReader dr = DataBase.Get("select * from Login where login=@login and password=@pass and deleted=0 ", parameters);
            

            List<LoginModel> Logins = new List<LoginModel>();
            LoginModel loginItem = new LoginModel();

            while (dr.Read())
            {
                loginItem = new LoginModel();
                loginItem.IdLogin = (int)dr["Id"];
                loginItem.Login = dr["login"].ToString();
                loginItem.Password = dr["password"].ToString();
                loginItem.Deleted = (bool)dr["deleted"];
            }

            DataBase.CloseConnection();

            return loginItem;
        }
    }
}