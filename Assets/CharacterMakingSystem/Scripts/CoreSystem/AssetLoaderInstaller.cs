using UnityEngine;
using Zenject;

namespace CharacterMakingSystem.CoreSystem
{
    public class AssetLoaderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<AssetLoader>().AsTransient();
        }
    }
}