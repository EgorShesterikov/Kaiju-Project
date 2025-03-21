using Game.EnemyBlock.Controllers;
using UnityEngine;
using Zenject;

namespace Kaiju
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Application.targetFrameRate = 60;

            Container.BindInterfacesAndSelfTo<InputController>().AsSingle();
            Container.BindInterfacesAndSelfTo<HintController>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyFactory>().AsSingle();
        }
    }
}