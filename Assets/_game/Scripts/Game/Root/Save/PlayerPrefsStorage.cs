using UnityEngine;

namespace _game.Scripts.Game.Root.Save
{
    public class PlayerPrefsStorage : IKeyValueStorage
    {
        public bool TryGetString(string key, out string value)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                value = null;
                return false;
            }
            
            value = PlayerPrefs.GetString(key);
            return true;
        }

        public void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public void Save()
        {
            PlayerPrefs.Save();
        }
        
    }
}