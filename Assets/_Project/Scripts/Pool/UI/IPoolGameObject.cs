namespace com.pepipe.Pool.UI
{
    public interface IPoolGameObject<T>
    {
        T Allocate(bool startActive);
        void Release(T poolObject);
    }
}
