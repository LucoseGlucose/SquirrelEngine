using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquirrelEngine.Graphics;

namespace SquirrelEngine.Core
{
    internal class Scene
    {
        public List<Entity> AllObjects { get; private set; }
        public string name;
        public Action onStart;
        public Action onUpdate;
        public Action onEnd;

        public Scene(string name)
        {
            this.name = name;
            AllObjects = new();
        }
        public Entity CreateObject(string name)
        {
            Entity newObj = new(name);
            AllObjects.Add(newObj);
            return newObj;
        }
        public void DestroyObject(Entity obj)
        {
            obj.Destroy();
            AllObjects.Remove(obj);
        }
        public void Start()
        {
            foreach (Entity obj in AllObjects)
            {
                obj.Start();
            }

            onStart?.Invoke();
        }
        public void Update()
        {
            foreach (Entity obj in AllObjects)
            {
                obj.Update();
            }

            onUpdate?.Invoke();
        }
        public void End()
        {
            foreach (Entity obj in AllObjects)
            {
                obj.End();
            }

            onEnd?.Invoke();
        }
        public T[] FindComponentsOfType<T>() where T : Component
        {
            List<T> result = new();
            for (int i = 0; i < AllObjects.Count; i++)
            {
                if (AllObjects[i].TryGetComponent(out T comp))
                    result.Add(comp);
            }

            return result.ToArray();
        }
        public T FindComponentOfType<T>(string name = "") where T : Component
        {
            for (int i = 0; i < AllObjects.Count; i++)
            {
                if (AllObjects[i].TryGetComponent(out T comp))
                {
                    if (string.IsNullOrEmpty(name) || AllObjects[i].name == name) return comp;
                }
            }

            return null;
        }
        public Entity FindObjectWithName(string name)
        {
            return AllObjects.FirstOrDefault(obj => obj.name == name);
        }
        public Entity[] FindObjectsWithName(string name)
        {
            return AllObjects.Where(obj => obj.name == name).ToArray();
        }
        public static Scene CreateScene(Func<Scene> func)
        {
            return func.Invoke();
        }
    }
}
