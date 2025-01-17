public interface ISaveStrategy
{
    public void Save<T>(T data, string key);
    public T Load<T>(string key);
    public bool IsFileExists(string key);
}
