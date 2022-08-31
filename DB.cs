using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace PublicUtility.Extension {
  public class DB: IDisposable {
    private readonly SqlConnection con = null;
    private SqlTransaction tran = null;
    private SqlCommand cmd = null;

    #region PRIVATE METHODS

    private void RollBack() {
      if(cmd.Transaction != null)
        cmd.Transaction.Rollback();
    }

    private void Commit() {
      if(cmd.Transaction != null)
        cmd.Transaction.Commit();
    }

    private SqlConnection Open() {
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

    private DB(string connectionString) {
      con = new SqlConnection(connectionString);
    }

    public static DB GetConn(string connectionString) => new(connectionString);

    public string ExecCmd(SqlCommand command) {
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

    public string ExecQuery(string query) => ExecCmd(new SqlCommand(query));

    public DataTable ReturnData(SqlCommand command) {
      var table = new DataTable();
      cmd = command;
      try {
        var adapter = new SqlDataAdapter();
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

    public DataTable ReturnData(string query) => ReturnData(new SqlCommand(query));

    public T ReturnData<T>(string query) where T : IEnumerable => ReturnData(query).DeserializeTable<T>();

    public T ReturnData<T>(SqlCommand command) where T : IEnumerable => ReturnData(command).DeserializeTable<T>();

    public void Dispose() {
      Close();
      GC.SuppressFinalize(this);
      GC.Collect();
    }
  }
}
