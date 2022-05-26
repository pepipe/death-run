namespace com.pepipe.Pool
{
    public interface IFactory<out T>
    {
        T Create();
    }
}