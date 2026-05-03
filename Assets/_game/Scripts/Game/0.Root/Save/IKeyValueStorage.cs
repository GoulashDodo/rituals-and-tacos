namespace _game.Scripts.Game._0.Root.Save
{
    public interface IKeyValueStorage
    {
        bool TryGetString(string key, out string value);
        void SetString(string key, string value);
        void Save();
    }
}