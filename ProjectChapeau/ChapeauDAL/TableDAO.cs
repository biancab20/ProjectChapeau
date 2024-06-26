﻿using ChapeauModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauDAL
{
    public class TableDAO : BaseDAO
    {
        public Table GetTableById(int tableId)
        {
            try
            {
                string query = @"SELECT tableId, status 
                                FROM [Table]
                                WHERE tableId = @TableId";
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@TableId", tableId);

                DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow dataRow = dataTable.Rows[0];
                    Table table = ReadTable(dataRow);
                    return table;
                }
                throw new Exception($"Table with the {tableId} id was not found!");
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public List<Table> GetAllTables()
        {
            string query = @"SELECT tableId, status
                     FROM [Table]";

            DataTable dataTable = ExecuteSelectQuery(query);
            List<Table> tables = new List<Table>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                Table table = ReadTable(dataRow);
                tables.Add(table);
            }

            return tables;
        }
        public void UpdateTableStatus(Table table)
        {
            string query = "UPDATE [Table] SET status = @status WHERE tableId = @tableId";
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@Status", table.Status.ToString());
            sqlParameters[1] = new SqlParameter("@TableId", table.TableId);

            ExecuteEditQuery(query, sqlParameters);
        }
        private Table ReadTable(DataRow dataRow)
        {
            Table table = new Table
            {
                TableId = (int)dataRow["TableId"],
                Status = (TableStatus)Enum.Parse(typeof(TableStatus), dataRow["Status"].ToString(), ignoreCase: true),
            };
            return table;
        }
    }
}
