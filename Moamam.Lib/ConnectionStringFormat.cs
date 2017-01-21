namespace Moamam.Lib
{
    class ConnectionStringFormat
    {
        static public string connOracleTNS = "user id={0};password={1};data source={2};";
        static public string connOracleDirect = "user id={0};password={1};data source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST={2})(PORT={3}))(CONNECT_DATA=(SERVICE_NAME={4})))";
    }
}
