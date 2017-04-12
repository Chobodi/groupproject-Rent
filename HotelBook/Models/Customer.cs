using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Diagnostics;

namespace HotelBook.Models
{
    public class Customer
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string cPassword { get; set; }
        public string status { get; set; }
        public string imageFile { get; set; }

    }
    public class Package
    {
        public int id { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public int cusId { get; set; }
        public string availableNo { get; set; }
    }
    public class Boarding
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string bPassword { get; set; }
        public string description { get; set; }
    }

    public class HotelDBContext
    {

        public void Insert(Customer customer)
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Customer (Name,Email, Password,State,City,Address) VALUES (@Name,@Email, @Password,@State,@City,@Address)"))
                {
                    cmd.Parameters.AddWithValue("@Name", customer.name);
                    cmd.Parameters.AddWithValue("@Email", customer.email);
                    cmd.Parameters.AddWithValue("@Password", customer.password);
                    cmd.Parameters.AddWithValue("@State", customer.state);
                    cmd.Parameters.AddWithValue("@City", customer.city);
                    cmd.Parameters.AddWithValue("@Address", customer.address);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public List<Customer> Viewcustomer()
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;

            String sql = "SELECT * FROM Customer WHERE Status IS NULL";

            var model = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var customer = new Customer();
                    customer.name = rdr["Name"].ToString();
                    customer.address = rdr["Address"].ToString();
                    customer.email = rdr["Email"].ToString();
                    customer.state = rdr["State"].ToString();
                    customer.city = rdr["City"].ToString();

                    model.Add(customer);
                }
                return model;
            }
        }

        public void set(string email)
        {

            string h = email;
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {

                using (SqlCommand cmd = new SqlCommand("UPDATE Customer SET Status='Accept' WHERE Email=@email"))
                {

                    cmd.Parameters.AddWithValue("@email", h);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

        }
        public void setr(string email)
        {
            string h = email;
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {

                using (SqlCommand cmd = new SqlCommand("DELETE FROM Customer WHERE Email=@email"))
                {

                    cmd.Parameters.AddWithValue("@email", h);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public void sendMail(string email)
        {
            string h = email;
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("cs4halika@gmail.com");
            msg.To.Add("tishanml993@gmail.com");
            msg.Body = "senajks";
            msg.IsBodyHtml = true;
            SmtpClient Smtp = new SmtpClient();
            Smtp.Host = "smtp.gmail.com";
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            NetworkCred.UserName = "cs4halika@gmail.com";
            NetworkCred.Password = "lumia630";
            Smtp.UseDefaultCredentials = true;
            Smtp.Credentials = NetworkCred;
            Smtp.Port = 587;
            Smtp.EnableSsl = true;
            Smtp.Send(msg);
        }
        public List<Customer> searchCustomer(String name, String city)
        {
            String n = name;
            String c = city;
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;

            String sql = "SELECT * FROM Customer WHERE Name=@name or City=@city";

            var model = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", n);
                cmd.Parameters.AddWithValue("@city", c);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var customer = new Customer();
                    customer.name = rdr["Name"].ToString();
                    customer.address = rdr["Address"].ToString();
                    customer.email = rdr["Email"].ToString();
                    customer.state = rdr["State"].ToString();
                    customer.city = rdr["City"].ToString();
                    customer.imageFile = rdr["ImageFile"].ToString();
                    model.Add(customer);
                }
                return model;
            }
        }
        public List<Package> Viewpackage()
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;

            String sql = "SELECT * FROM Package WHERE CusId=1";

            var mod = new List<Package>();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var package = new Package();
                    package.id = Convert.ToInt32(rdr["Id"]);
                    package.name = rdr["Name"].ToString();
                    package.price = rdr["Price"].ToString();
                    package.description = rdr["Description"].ToString();
                    package.type = rdr["Type"].ToString();
                    package.availableNo = rdr["AvailableNo"].ToString();

                    mod.Add(package);
                }
                return mod;
            }
        }
        public void InsertPack(Package pack)
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Package (Name,Price, Description,Type,CusId,AvailableNo) VALUES (@Name,@Price, @Description,@Type,1,@Available)"))
                {
                    cmd.Parameters.AddWithValue("@Name", pack.name);
                    cmd.Parameters.AddWithValue("@Price", pack.price);
                    cmd.Parameters.AddWithValue("@Description", pack.description);
                    cmd.Parameters.AddWithValue("@Type", pack.type);
                    cmd.Parameters.AddWithValue("@Available", pack.availableNo);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public Package PackView(int id)
        {

            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;

            String sql = "SELECT * FROM Package WHERE Id=@x";

            Package package = new Package();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@x", id);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    package.id = id;
                    Debug.WriteLine("tddd" + id);
                    package.name = rdr["Name"].ToString();
                    package.price = rdr["Price"].ToString();
                    package.description = rdr["Description"].ToString();
                    package.type = rdr["Type"].ToString();
                    package.availableNo = rdr["AvailableNo"].ToString();

                }
                return package;
            }
        }
        public void EditPack(Package pack)
        {
            Debug.WriteLine("old" + pack.id);
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Package SET Name=@Name,Price=@Price,Description=@Description,Type=@Type,AvailableNo=@Available WHERE Id=@id"))
                {
                    cmd.Parameters.AddWithValue("@id", pack.id);
                    cmd.Parameters.AddWithValue("@Name", pack.name);
                    cmd.Parameters.AddWithValue("@Price", pack.price);
                    cmd.Parameters.AddWithValue("@Description", pack.description);
                    cmd.Parameters.AddWithValue("@Type", pack.type);
                    cmd.Parameters.AddWithValue("@Available", pack.availableNo);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public void DeletePackage(int id)
        {
            int i = id;
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {

                using (SqlCommand cmd = new SqlCommand("DELETE FROM Package WHERE Id=@id"))
                {

                    cmd.Parameters.AddWithValue("@id", i);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public void Insertboarding(Boarding boarding)
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Boarding (Name,Email, Password,State,City,Address,Description) VALUES (@Name,@Email, @Password,@State,@City,@Address,null)"))
                {
                    cmd.Parameters.AddWithValue("@Name", boarding.name);
                    cmd.Parameters.AddWithValue("@Email", boarding.email);
                    cmd.Parameters.AddWithValue("@Password", boarding.password);
                    cmd.Parameters.AddWithValue("@State", boarding.state);
                    cmd.Parameters.AddWithValue("@City", boarding.city);
                    cmd.Parameters.AddWithValue("@Address", boarding.address);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public List<Boarding> viewBoarding(string state,string city)
        {
            String s = state;
            String c = city;
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;

            String sql = "SELECT * FROM Boarding WHERE State=@state or City=@city";

            var board = new List<Boarding>();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@state", s);
                cmd.Parameters.AddWithValue("@city", c);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var boarding = new Boarding();
                    boarding.name = rdr["Name"].ToString();
                    boarding.address = rdr["Address"].ToString();
                    boarding.email = rdr["Email"].ToString();
                    boarding.state = rdr["State"].ToString();
                    boarding.city = rdr["City"].ToString();
                    boarding.description = rdr["Description"].ToString();
                    board.Add(boarding);
                }
                return board;
            }
        }
    }
}