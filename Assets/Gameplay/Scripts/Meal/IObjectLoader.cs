
public interface IObjectLoader<T>
{
    void LoadObject(T obj);

    void SwapObject(T obj);
}