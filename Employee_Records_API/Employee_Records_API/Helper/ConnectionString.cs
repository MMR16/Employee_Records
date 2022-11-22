namespace Employee_Records_API.Helper
{
    public static class ConnectionString
    {

        private static string cName = "Data Source=.;Initial Catalog=Employee_Records;Trusted_Connection=True;";
        public static string CName { get => cName; }
    }
}
