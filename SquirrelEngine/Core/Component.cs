using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquirrelEngine.Core
{
    internal class Component
    {
        public Entity Object { get; set; }
        public Transform Transform { get => Object.Transform; }
        
        public Component()
        {

        }

        public virtual void Update() {  }
        public virtual void Start() { }
        public virtual void OnRemove() {  }
        public virtual void OnDestroy() {  }
    }
}
