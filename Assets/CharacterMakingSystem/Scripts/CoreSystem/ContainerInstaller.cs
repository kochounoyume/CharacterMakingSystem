using Zenject;

namespace CharacterMakingSystem.CoreSystem
{
    public class ContainerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<AssetLoader>().AsCached();
            Container.BindInterfacesTo<SceneController>().AsCached();
        }
    }
}