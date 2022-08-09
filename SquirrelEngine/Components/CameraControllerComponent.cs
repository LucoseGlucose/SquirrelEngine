using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquirrelEngine.Core;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace SquirrelEngine.Components
{
    internal class CameraControllerComponent : Component
    {
        public float rotateSpeed = 1f;
        public float panSpeed = .05f;
        public float scrollSpeed = 1f;

        public override void Update()
        {
            MouseState mouseState = App.GameWindow.MouseState;

            if (mouseState.IsButtonDown(MouseButton.Right))
                Transform.rotation += -rotateSpeed * new Vector3(mouseState.Delta.Y, mouseState.Delta.X, 0f);

            if (mouseState.IsButtonDown(MouseButton.Middle))
            {
                Transform.position += panSpeed * mouseState.Delta.Y * Transform.Up;
                Transform.position += panSpeed * mouseState.Delta.X * Transform.Right;
            }

            if (mouseState.ScrollDelta.LengthSquared > 0) 
                Transform.position += mouseState.ScrollDelta.Y * scrollSpeed * Transform.Forward;
        }
    }
}
