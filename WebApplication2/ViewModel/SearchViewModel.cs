using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication2.Services;

namespace WebApplication2.ViewModel
{
    public class SearchViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Deleted { get; set; }
        public string SearchBy { get; set; }
        public bool EnableEdit { get; set; }


        public void Save()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@login", Login));
            parameters.Add(new SqlParameter("@pass", Password));
            parameters.Add(new SqlParameter("@del", Deleted));

            if(Id <= 1)
            {
                var insert = DataBase.Execute("insert into Login Values (@login, @pass, @del)", parameters);
            }
            else
            {
                parameters.Add(new SqlParameter("@id", Id));
                var update = DataBase.Execute("update Login set login=@login, password=@pass, deleted=@del where id=@id", parameters);
            }
            
        }

        public SearchViewModel Search()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@searchby", SearchBy));

            SqlDataReader dr = DataBase.Get("select * from Login where login like @searchby and deleted=0 ", parameters);

            SearchViewModel searchResult = new SearchViewModel();

            while (dr.Read())
            {
                searchResult = new SearchViewModel();
                searchResult.Id = (int)dr["Id"];
                searchResult.Login = dr["login"].ToString();
                searchResult.Password = dr["password"].ToString();
                searchResult.Deleted = (bool)dr["deleted"];
            }

            DataBase.CloseConnection();

            return searchResult;
        }
    }
}