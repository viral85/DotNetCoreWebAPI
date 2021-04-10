using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyTestProjectRepositoryLayer.BaseClasses
{
    public class BaseDBCommand
    {
        public String connectionString { get; set; }
        public SqlCommand customCommand { get; set; }
        public BaseDBCommand()
        {

        }
        public SqlCommand IntializeCommandDefaults()
        {
            customCommand = new SqlCommand();
            customCommand.CommandTimeout = 0;
            customCommand.Parameters.Clear();
            return customCommand;
        }
    }
}
