using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public MouseInputComponent mouseInput { get { return (MouseInputComponent)GetComponent(ComponentIds.MouseInput); } }

        public bool hasMouseInput { get { return HasComponent(ComponentIds.MouseInput); } }

        static readonly Stack<MouseInputComponent> _mouseInputComponentPool = new Stack<MouseInputComponent>();

        public static void ClearMouseInputComponentPool() {
            _mouseInputComponentPool.Clear();
        }

        public Entity AddMouseInput(float newX, float newY, bool newIsDown) {
            var component = _mouseInputComponentPool.Count > 0 ? _mouseInputComponentPool.Pop() : new MouseInputComponent();
            component.x = newX;
            component.y = newY;
            component.isDown = newIsDown;
            return AddComponent(ComponentIds.MouseInput, component);
        }

        public Entity ReplaceMouseInput(float newX, float newY, bool newIsDown) {
            var previousComponent = hasMouseInput ? mouseInput : null;
            var component = _mouseInputComponentPool.Count > 0 ? _mouseInputComponentPool.Pop() : new MouseInputComponent();
            component.x = newX;
            component.y = newY;
            component.isDown = newIsDown;
            ReplaceComponent(ComponentIds.MouseInput, component);
            if (previousComponent != null) {
                _mouseInputComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveMouseInput() {
            var component = mouseInput;
            RemoveComponent(ComponentIds.MouseInput);
            _mouseInputComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherMouseInput;

        public static IMatcher MouseInput {
            get {
                if (_matcherMouseInput == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.MouseInput);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherMouseInput = matcher;
                }

                return _matcherMouseInput;
            }
        }
    }
}
