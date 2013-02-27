namespace Nonoe.Tailz.Core.DAL
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Configuration;
    using System.Data;
    using System.Data.SQLite;
    using System.Linq;

    using Nonoe.Tailz.Core.Objects;

    public class PluginDAL
    {
        /// <summary>The dataset.</summary>
        private readonly DataSet ds = new DataSet();

        /// <summary>The SQLite data adapter.</summary>
        private SQLiteDataAdapter db;

        /// <summary>The SQL command</summary>
        private SQLiteCommand sqlCmd;

        /// <summary>The SQL connection.</summary>
        private SQLiteConnection sqlCon;

        public PluginDAL()
        {
            const string TxtSQLQuery = "CREATE TABLE IF NOT EXISTS plugins(" + "PluginName , RubyScript, Active " + ");";
            this.ExecuteQuery(TxtSQLQuery);
        }

        /// <summary>Gets the connection string.</summary>
        private static string ConnectionString
        {
            get
            {
                return "Data Source=Tailz.db;Version=3;New=False;Compress=True;";
            }
        }

        /// <summary>The execute query method.</summary>
        /// <param name="txtQuery">The query.</param>
        private void ExecuteQuery(string txtQuery)
        {
            using (this.sqlCon = new SQLiteConnection(ConnectionString))
            {
                this.sqlCon.Open();
                using (this.sqlCmd = this.sqlCon.CreateCommand())
                {
                    this.sqlCmd.CommandText = txtQuery;
                    this.sqlCmd.ExecuteNonQuery();
                }
            }
        }

        public void Add(string pluginName, string rubyScript)
        {
            string txtSQLQuery = "insert into plugins (PluginName, RubyScript, Active) values ('" + this.ReplaceQuotes(pluginName) + "', '" + this.ReplaceQuotes(rubyScript) + "', + '" + false + "')";
            this.ExecuteQuery(txtSQLQuery);
        }

        private string ReplaceQuotes(string rubyScript)
        {
            return rubyScript.Replace("'", "''");
        }

        public IList<Plugin> GetPluginsByActivity(bool activity)
        {
            using (this.sqlCon = new SQLiteConnection(ConnectionString))
            {
                this.sqlCon.Open();
                using (this.sqlCmd = this.sqlCon.CreateCommand())
                {
                    string CommandText = "select PluginName, RubyScript, Active from plugins where Active = '" + activity.ToString() + "'";
                    this.db = new SQLiteDataAdapter(CommandText, this.sqlCon);
                    this.ds.Reset();
                    this.db.Fill(this.ds);
                }
            }

            return this.ds.Tables[0].AsEnumerable().Select(ConvertToPlugin).ToList();
        }

        private static Plugin ConvertToPlugin(DataRow dataRow)
        {
            return new Plugin
            {
                Active = bool.Parse(dataRow["Active"].ToString()),
                PluginName = dataRow["PluginName"].ToString(),
                RubyScript = dataRow["RubyScript"].ToString(),
            };
        }

        public void ChangePluginActivity(string pluginName, bool active)
        {
            string txtSQLQuery = "update plugins set " + "Active = '" + active + "' where pluginName = '" + pluginName + "';";
            this.ExecuteQuery(txtSQLQuery);
        }

        public void Delete(string pluginName)
        {
            string txtSQLQuery = "delete from plugins where pluginName = '" + pluginName + "';";
            this.ExecuteQuery(txtSQLQuery);
        }
    }
}
