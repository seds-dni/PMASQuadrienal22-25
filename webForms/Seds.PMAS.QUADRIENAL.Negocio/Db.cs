using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Seds.PMAS.QUADRIENAL.Persistencia;
using System.Data.SqlClient;
using System.Data.EntityClient;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class Db
    {
        public DataTable GetTable(string command)
        {
            var context = ContextManager.GetContext();
            ContextManager.OpenConnection();
            DataSet ds = new DataSet();         
            try
            {
                EntityConnection connection = (EntityConnection)context.Connection;
                SqlCommand cmd = new SqlCommand(command,(SqlConnection)connection.StoreConnection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                using (cmd)
                {                    
                    cmd.CommandType = CommandType.Text;                    
                    da.Fill(ds);
                }
                ContextManager.CloseConnection();                           
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw ex;
            }
            return ds.Tables[0];
        }


        public void Execute(string command)
        {
            var context = ContextManager.GetContext();
            ContextManager.OpenConnection();            
            try
            {
                EntityConnection connection = (EntityConnection)context.Connection;
                SqlCommand cmd = new SqlCommand(command, (SqlConnection)connection.StoreConnection);
                cmd.ExecuteNonQuery();
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                throw ex;
            }            
        }
    }
}
