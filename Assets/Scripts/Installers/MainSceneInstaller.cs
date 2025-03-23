using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    [SerializeField] private CellsGrid _cellsGrid;
    [SerializeField] private CellsGridConfig _cellsGridConfig;
    [SerializeField] private GroundStateMachine _groundStateMachine;

    public override void InstallBindings()
    {
        Container.Bind<EventBus>().AsSingle().NonLazy();
        Container.Bind<CellsGridConfig>().FromInstance(_cellsGridConfig).AsSingle().NonLazy();
        Container.Bind<CellsGrid>().FromInstance(_cellsGrid).AsSingle().NonLazy();
        Container.Bind<CellFactory>().AsSingle().NonLazy();
        Container.Bind<Pointer>().AsSingle().NonLazy(); 
        Container.BindInterfacesAndSelfTo<Highlighter>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<Builder>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GroundStateMachine>().FromInstance(_groundStateMachine).AsSingle().NonLazy();
        Container.Bind<BuildingState>().AsSingle().NonLazy();
        Container.Bind<MiningState>().AsSingle().NonLazy();
    }
}
