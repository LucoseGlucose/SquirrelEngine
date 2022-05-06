using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace SquirrelEngine.Core
{
    internal sealed class Entity
    {
        public string name;
        public List<Component> Components { get; private set; }
        public Transform Transform { get; private set; }

        public Entity(string name)
        {
            this.name = name;
            Components = new();
            Transform = new(Vector3.Zero, Quaternion.Identity, Vector3.One);
        }
        public void Update()
        {
            Transform.Update();

            foreach (Component component in Components)
            {
                component.Update();
            }
        }
        public void Start()
        {
            Transform.Start();

            foreach (Component component in Components)
            {
                component.Start();
            }
        }
        public void Destroy()
        {
            foreach (Component component in Components)
            {
                component.OnDestroy();
            }
        }
        public T GetComponent<T>() where T : Component
        {
            for (int i = 0; i < Components.Count; i++)
            {
                if (Components[i].GetType() == typeof(T)) return (T)Components[i];
            }
            return null;
        }
        public bool TryGetComponent<T>(out T component) where T : Component
        {
            Component result = GetComponent<T>();
            component = (T)result ?? null;
            return result != null;
        }
        public T AddComponent<T>() where T : Component, new()
        {
            T newComponent = new();
            newComponent.Object = this;
            Components.Add(newComponent);
            return newComponent;
        }
        public void RemoveComponent(Component component)
        {
            if (!Components.Contains(component)) throw new ArgumentException("Object does not have the specified component");

            component.OnRemove();
            Components.Remove(component);
        }
    }
}
