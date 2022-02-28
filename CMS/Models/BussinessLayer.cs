using CSharp;
using System.Data.SqlClient;
using System.Linq;




namespace CMS.Models
{
    public class BLayer
    {
        public BLayer(IConfiguration configuration)
        {
            Ado.connectionString = configuration.GetConnectionString("ConCMS");
           
        }


        public static IEnumerable<tbl_User_Master> GetUser()
        {
           
                var user = Ado.GetDataTable("_spSelect_tbl_user_master").ToList<tbl_User_Master>();
                return user;
           
               
            
            
        }
        public static tbl_User_Master GetUser(int id)
        {

           

            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("id", id) };
            var user = Ado.GetDataTable("_spSelect_tbl_user_master", sqlParameters).ToList<tbl_User_Master>().FirstOrDefault();
            return user;


        }
    }
}
