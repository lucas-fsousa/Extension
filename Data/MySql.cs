using MySqlConnector;
using System.Collections;
using System.Data;

namespace PublicUtility.Extension.Data.MySql {
  public class DB: IDisposable {
    private readonly MySqlConnection con = null;
    private MySqlTransaction tran = null;
    private MySqlCommand cmd = null;

    #region PRIVATE METHODS

    private void RollBack() {
      if(cmd.Transaction != null)
        cmd.Transaction.Rollback();
    }

    private void Commit() {
      if(cmd.Transaction != null)
        cmd.Transaction.Commit();
    }

    private MySqlConnection Open() {
      if(con.State == ConnectionState.Closed) {
        con.Open();
        tran = con.BeginTransaction();
      }
      return con;
    }

    private void Close() {
      if(con.State == ConnectionState.Open)
        con.Close();
    }

    #endregion

    private DB(MySqlConnectionStringBuilder builder) {
      con = new MySqlConnection(builder.ConnectionString);
    }


    public static DB GetConn(MySqlConnectionStringBuilder connectionBuilder) => new(connectionBuilder);

    public string ExecCmd(MySqlCommand command) {
      cmd = command;
      try {
        cmd.Connection = Open();
        cmd.Transaction = tran;
        cmd.ExecuteNonQuery();
        Commit();
        return string.Format($" [OK] - {DateTime.UtcNow}");
      } catch(Exception ex) {
        RollBack();

        return string.Format($" [ERRO] - {DateTime.UtcNow} # {ex.Message} ");
      }
    }

    public string ExecQuery(string query) => ExecCmd(new MySqlCommand(query));

    public DataTable ReturnData(MySqlCommand command) {
      var table = new DataTable();
      cmd = command;
      try {
        var adapter = new MySqlDataAdapter();
        cmd.Connection = Open();
        cmd.Transaction = tran;

        adapter.SelectCommand = cmd;
        adapter.Fill(table);
        Commit();
      } catch {
        table = null;
        RollBack();
      }
      return table;
    }

    public DataTable ReturnData(string query) => ReturnData(new MySqlCommand(query));

    public T ReturnData<T>(string query) where T : IEnumerable => ReturnData(query).DeserializeTable<T>();

    public T ReturnData<T>(MySqlCommand command) where T : IEnumerable => ReturnData(command).DeserializeTable<T>();

    public void Dispose() {
      Close();
      GC.SuppressFinalize(this);
      GC.Collect();
    }
  }
}
