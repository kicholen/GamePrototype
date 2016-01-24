using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public InputComponent input { get { return (InputComponent)GetComponent(ComponentIds.Input); } }

        public bool hasInput { get { return HasComponent(ComponentIds.Input); } }

        static readonly Stack<InputComponent> _inputComponentPool = new Stack<InputComponent>();

        public static void ClearInputComponentPool() {
            _inputComponentPool.Clear();
        }

        public Entity AddInput(float newX, float newY, bool newIsDown, bool newIsInputBlocked) {
            var component = _inputComponentPool.Count > 0 ? _inputComponentPool.Pop() : new InputComponent();
            component.x = newX;
            component.y = newY;
            component.isDown = newIsDown;
            component.isInputBlocked = newIsInputBlocked;
            return AddComponent(ComponentIds.Input, component);
        }

        public Entity ReplaceInput(float newX, float newY, bool newIsDown, bool newIsInputBlocked) {
            var previousComponent = hasInput ? input : null;
            var component = _inputComponentPool.Count > 0 ? _inputComponentPool.Pop() : new InputComponent();
            component.x = newX;
            component.y = newY;
            component.isDown = newIsDown;
            component.isInputBlocked = newIsInputBlocked;
            ReplaceComponent(ComponentIds.Input, component);
            if (previousComponent != null) {
                _inputComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveInput() {
            var component = input;
            RemoveComponent(ComponentIds.Input);
            _inputComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherInput;

        public static IMatcher Input {
            get {
                if (_matcherInput == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Input);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherInput = matcher;
                }

                return _matcherInput;
            }
        }
    }
}
