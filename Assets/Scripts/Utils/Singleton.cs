namespace NotFound.Utils
{
    public class Singleton<T>
    {

        public static Singleton<T> Instance
        {
            get
            {
                _instance ??= new Singleton<T>();
                return _instance;
            }
        }

        private static Singleton<T> _instance;

    }
}