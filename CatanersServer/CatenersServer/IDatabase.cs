using CatanersShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatenersServer
{
    public interface IDatabase
    {
        catanersDataSet.checkUserDataTableRow getUser(Login login);


        int registerUser(Login login);
    }
}
