using MVCSimple.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSimple.Controllers
{
    public class DemoController : Controller
    {
        // GET: Demo
        public ActionResult Index(string inputValue)
        {
            ViewBag.myMessage = "Hi from controller!!  This is from " + inputValue;

            List<Publisher> list = new List<Publisher>();
            try
            {
                // runs stored procedure and returns data to main page
                using (SqlConnection con = new SqlConnection())
                {
                    String sql = @"select * from publishers";
                    con.ConnectionString = @"Server=comp1630.database.windows.net;Database=pubs;User Id=readonlylogin;Password=;";


                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(sql, con);

                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        var pub = new Models.Publisher();
                        pub.Name = row["pub_name"].ToString();
                        pub.City = row["city"].ToString();

                        list.Add(pub);
                    }
                }
                return View(list);
            }
            catch
            {
                return View("Error");
            }

            
        }
    }
}