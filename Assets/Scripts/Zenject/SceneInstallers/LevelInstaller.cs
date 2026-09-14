using Scripts.Managers;
using Scripts.Player;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerView>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<UIManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        //Container.BindInterfacesAndSelfTo<DayNightService>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}