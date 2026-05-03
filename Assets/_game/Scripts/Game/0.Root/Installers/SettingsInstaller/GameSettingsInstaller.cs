using _game.Scripts.Game._0.Root.Settings;
using _game.Scripts.Game._2.Gameplay._root.Settings;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game._0.Root.Installers.SettingsInstaller
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [field: SerializeField] public GameSettings GameSettings{ get; private set; }
        
        public override void InstallBindings()
        {
            Container.Bind<GameSettings>().FromInstance(GameSettings).AsSingle();
            Container.Bind<GameplaySettings>().FromInstance(GameSettings.GameplaySettings).AsSingle();
        }
    }
}