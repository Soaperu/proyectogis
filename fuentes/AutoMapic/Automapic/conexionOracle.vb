Imports Oracle.ManagedDataAccess.Client

Module conexionOracle
    'Public _strConexionGis As String = "User Id=ugeo1749;Password=ugeo1749;Data Source=bdgeocat"
    Public _strConexionGis As String = "Data Source=(DESCRIPTION=" &
        "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.102.0.66)(PORT=1521)))" &
        "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=BDGEOCAT)));" &
        "User Id=ugeo1749;Password=ugeo1749;"
    Public OracleConn As New OracleConnection(_strConexionGis)
    Public Oracledr As OracleDataReader
    Public Oracleda As OracleDataAdapter

    Public Sub EjecutarSqlcommand(sql_sentence)
        Dim cn = OracleConn
        Dim command As New OracleCommand(sql_sentence)

        command.Connection = cn
        cn.Open()
        command.ExecuteNonQuery()
    End Sub

    Public Function SelectSqlcommand(sql_sentence)
        Dim cn = OracleConn
        Dim command As New OracleCommand(sql_sentence)

        'Modulos asociados al usuario
        Dim da = New OracleDataAdapter(sql_sentence, cn)
        da.SelectCommand.CommandType = CommandType.Text
        'Dim ds = New DataSet()
        'da.Fill(ds, "Tabla")

        Dim dt = New DataTable()
        da.Fill(dt)
        cn.Close()
        Return dt

    End Function

End Module
