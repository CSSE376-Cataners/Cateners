using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatanersShared;

namespace CatenersServer
{
    class Database
    {

        public static Database INSTANCE;
        catanersDataSetTableAdapters.checkUserDataTableAdapter cta;

        public Database()
        {
            INSTANCE = this;
            cta = new catanersDataSetTableAdapters.checkUserDataTableAdapter();
        }

        public int getUserID(Login login)
        {
            catanersDataSet.checkUserDataTableDataTable table = cta.GetData(login.username, login.password);

            if (table.Rows.Count > 0)
                return ((catanersDataSet.checkUserDataTableRow)table.Rows[0]).UID;
            return -1;
        }


        public int registerUser(Login login)
        {

        }
    }
}
