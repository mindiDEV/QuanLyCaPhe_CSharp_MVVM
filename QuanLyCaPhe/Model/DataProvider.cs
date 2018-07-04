namespace QuanLyCaPhe.Model
{
    public class DataProvider
    {
        private static DataProvider _ins;

        public static DataProvider Instance
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new DataProvider();
                }

                return _ins;
            }
            set
            {
                _ins = value;
            }
        }

        public QuanLyQuanCaPheEntities Database { get; set; }

        private DataProvider()
        {
            Database = new QuanLyQuanCaPheEntities();
        }
    }
}