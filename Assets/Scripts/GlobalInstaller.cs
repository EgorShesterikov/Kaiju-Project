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
        }
    }
}