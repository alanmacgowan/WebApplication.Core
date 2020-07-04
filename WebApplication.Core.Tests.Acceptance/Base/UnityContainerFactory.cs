using Unity;

namespace WebApplication.Core.Tests.Acceptance.Base
{
    public static class UnityContainerFactory
    {
        private static IUnityContainer _unityContainer;

        static UnityContainerFactory()
        {
            _unityContainer = new UnityContainer();
        }

        public static IUnityContainer GetContainer()
        {
            return _unityContainer;
        }
    }
}
