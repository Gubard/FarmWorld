using Services;
using UnityEngine;

namespace MonoBehaviours
{
    public class MonoBehaviourInject : MonoBehaviour
    {
        public static DependencyInjector Injector { get; set; }

        static MonoBehaviourInject()
        {
            Injector = new DependencyInjector(false);
        }

        public MonoBehaviourInject()
        {
            Injector.Inject(this);
        }
    }
}