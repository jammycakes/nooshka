namespace FactoryFactory
{
    public class Resolve
    {
        public static T From<T>()
        {
            return default(T);
        }
    }
}
