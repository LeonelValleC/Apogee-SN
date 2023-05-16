﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace Portal_Apogee
{
    public class User : Conexion
    {
        int id_user;
        string name;
        int nemploy;
        string username;
        string password;
        int level;
        string email;

        public string Name { get => name; set => name = value; }
        public int Nemploy { get => nemploy; set => nemploy = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public int Level { get => level; set => level = value; }

        public User()
        {
        }

        public User(string name, int nem, string username, string password, int id, int levels)
        {
            this.name = name;
            this.nemploy = nem;
            this.username = username;
            this.password = password;
            this.id_user = id;
            this.level = levels;
        }

        public bool Login(string user, string password)
        {
            return (id_user = ReturnID("select id_user from tb_User where users = '" + user + "' and password = '" + password + "'")) > 0;
        }

        public List<User> GetUser(int getid)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select name, nemploy, users, password, id_user, id_level from tb_User where id_user = " + getid);
            //sql.Append("   FROM Customer ");

            DataTable dt = LlenarDG(sql.ToString()).Tables[0];

            return MakeCustomers(dt);
        }

        private List<User> MakeCustomers(DataTable dt)
        {
            List<User> list = new List<User>();
            foreach (DataRow row in dt.Rows)
                list.Add(MakeCustomer(row));

            return list;
        }

        private User MakeCustomer(DataRow row)
        {
            string name = (row["name"].ToString());
            int nemploy = int.Parse(row["nemploy"].ToString());
            string username = row["users"].ToString();
            string password = row["password"].ToString();
            int id = int.Parse(row["id_user"].ToString());
            int level = int.Parse(row["id_level"].ToString());

            return new User(name, nemploy, username, password, id, level);
        }
    }
}