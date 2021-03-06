﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatanersShared;
using System.Diagnostics.CodeAnalysis;

namespace CatenersServer
{
    [ExcludeFromCodeCoverage]
    public class Database : IDatabase
    {

        public static IDatabase INSTANCE;
        catanersDataSetTableAdapters.checkUserDataTableAdapter cta;
        catanersDataSetTableAdapters.registerUserDataTableAdapter rta;

        public Database()
        {
            INSTANCE = this;
            /* Actual Connections
            cta = new catanersDataSetTableAdapters.checkUserDataTableAdapter();
            rta = new catanersDataSetTableAdapters.registerUserDataTableAdapter();
             */
        }

        public catanersDataSet.checkUserDataTableRow getUser(Login login)
        {
            catanersDataSet.checkUserDataTableDataTable table = new catanersDataSet.checkUserDataTableDataTable();
            table.AddcheckUserDataTableRow(table.NewcheckUserDataTableRow());

            catanersDataSet.checkUserDataTableRow row = (catanersDataSet.checkUserDataTableRow)table.Rows[0];
            row.UID = 1;
            row.Username = login.username;

            return row;
            /* Actual Authentication
            try
            {
                catanersDataSet.checkUserDataTableDataTable table = cta.GetData(login.username, login.password);
                if (table.Rows.Count > 0)
                    return (catanersDataSet.checkUserDataTableRow)table.Rows[0];

            }
            catch { }
            return null;
            */
        }


        public int registerUser(Login login)
        {
            return 1;
            /* Actual Athentication
            try
            {
                catanersDataSet.registerUserDataTableDataTable table = rta.GetData(login.username, login.password);

                String result = table.Rows[0][0].ToString();
                int toReturn;

                if (Int32.TryParse(result, out toReturn))
                {
                    return toReturn;
                }
                return -1;
            }
            catch { }
            return -1;
             */
        }
    }
}
