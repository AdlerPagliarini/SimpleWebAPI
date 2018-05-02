using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Data;
using SimpleWebApi.Models;

namespace SimpleWebApi.CRUD
{
    public class CrudDAOJsonString
    {
        /*
         Run this code on your database...

            --drop table JsonStrings;

            CREATE TABLE tJsonString(
	            Id int IDENTITY(1,1) NOT NULL,
	            Projeto nvarchar(255) NOT NULL,
	            Texto nvarchar(MAX) NOT NULL
                CONSTRAINT PK_IdS PRIMARY KEY (Id))
       
        */

        private ConnectionDAO dao;
        private List<SqlParameter> sp;
        private string table = "tJsonString";
        private string ID = "Id";
        private string OrderBy = " order by Id";

        private void Inserir(JsonString model)
        {
            var strQuery = "";
            strQuery += " INSERT INTO " + table + " (Projeto, Texto) ";
            strQuery += " VALUES (@Projeto, @Texto) ";

            sp = generateParams(model);

            using (dao = new ConnectionDAO())
            {
                dao.ExecuteCommand(strQuery, sp);
            }
        }
        public string InserirReturnId(JsonString model)
        {
            var strQuery = "";
            string idInserted = "";
            strQuery += " INSERT INTO " + table + " (Projeto, Texto) ";
            strQuery += " OUTPUT Inserted.Id VALUES (@Projeto, @Texto) ";

            sp = generateParams(model);

            using (dao = new ConnectionDAO())
            {
                var returnDataReader = dao.ReturnInsertedExecuteCommand(strQuery, sp);
                idInserted = readerToListObjectIDInserted(returnDataReader);
            }
            return idInserted;
        }

        private void Alterar(JsonString model)
        {
            var strQuery = "";
            strQuery += " UPDATE " + table + " SET ";
            strQuery += " Projeto = @Projeto, ";
            strQuery += " Texto = @Texto ";
            strQuery += " WHERE " + ID + " = @Id ";

            sp = generateParams(model);

            using (dao = new ConnectionDAO())
            {
                dao.ExecuteCommand(strQuery, sp);
            }
        }
        public void Salvar(JsonString model)
        {
            if (model.Id > 0)
                Alterar(model);
            else
                Inserir(model);
        }
        public void Excluir(int Id)
        {
            var strQuery = string.Format(" DELETE FROM " + table + " WHERE " + ID + " = @Id");
            sp = generateParamsID(Id);

            using (dao = new ConnectionDAO())
            {
                dao.ExecuteCommand(strQuery, sp);
            }
        }
        public IEnumerable<JsonString> ListarTodos()
        {
            var strQuery = " SELECT * FROM " + table + OrderBy;

            using (dao = new ConnectionDAO())
            {
                var returnDataReader = dao.ExecuteReaderCommand(strQuery);
                return readerToListObject(returnDataReader);
            }
        }

        public JsonString ListarPorId(int id)
        {
            var strQuery = string.Format(" SELECT * FROM " + table + " WHERE " + ID + " = @Id");
            JsonString model = new JsonString();
            model.Id = Convert.ToInt32(id);
            sp = generateParamsID(id);

            using (dao = new ConnectionDAO())
            {
                var returnDataReader = dao.ExecuteReaderCommand(strQuery, sp);
                return readerToListObject(returnDataReader).FirstOrDefault();
            }
        }
        private string readerToListObjectIDInserted(SqlDataReader reader)
        {
            var modelList = new List<JsonString>();
            string r = "";
            while (reader.Read())
            {
                r = reader[ID].ToString();
            }
            reader.Close();
            return r;
        }
        private List<JsonString> readerToListObject(SqlDataReader reader)
        {
            var modelList = new List<JsonString>();
            while (reader.Read())
            {
                var tempObject = new JsonString()
                {
                    Id = int.Parse(reader[ID].ToString()),
                    Projeto = reader["Projeto"].ToString(),
                    Texto = reader["Texto"].ToString()
                };
                modelList.Add(tempObject);
            }
            reader.Close();
            return modelList;
        }
        private List<SqlParameter> generateParams(JsonString model)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                    new SqlParameter() {ParameterName = "@Projeto", SqlDbType = SqlDbType.NVarChar, Value = model.Projeto},
                    new SqlParameter() {ParameterName = "@Texto", SqlDbType = SqlDbType.NVarChar, Value = model.Texto}
            };
            if (model.Id > 0)
            {
                sp.Add(new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = model.Id });
            }
            return sp;
        }
        private List<SqlParameter> generateParamsID(int Id)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = Id }
            };
            return sp;
        }
    }
}