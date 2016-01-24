using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public LevelDimensionsComponent levelDimensions { get { return (LevelDimensionsComponent)GetComponent(ComponentIds.LevelDimensions); } }

        public bool hasLevelDimensions { get { return HasComponent(ComponentIds.LevelDimensions); } }

        static readonly Stack<LevelDimensionsComponent> _levelDimensionsComponentPool = new Stack<LevelDimensionsComponent>();

        public static void ClearLevelDimensionsComponentPool() {
            _levelDimensionsComponentPool.Clear();
        }

        public Entity AddLevelDimensions(float newWidth, float newHeight) {
            var component = _levelDimensionsComponentPool.Count > 0 ? _levelDimensionsComponentPool.Pop() : new LevelDimensionsComponent();
            component.width = newWidth;
            component.height = newHeight;
            return AddComponent(ComponentIds.LevelDimensions, component);
        }

        public Entity ReplaceLevelDimensions(float newWidth, float newHeight) {
            var previousComponent = hasLevelDimensions ? levelDimensions : null;
            var component = _levelDimensionsComponentPool.Count > 0 ? _levelDimensionsComponentPool.Pop() : new LevelDimensionsComponent();
            component.width = newWidth;
            component.height = newHeight;
            ReplaceComponent(ComponentIds.LevelDimensions, component);
            if (previousComponent != null) {
                _levelDimensionsComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveLevelDimensions() {
            var component = levelDimensions;
            RemoveComponent(ComponentIds.LevelDimensions);
            _levelDimensionsComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherLevelDimensions;

        public static IMatcher LevelDimensions {
            get {
                if (_matcherLevelDimensions == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.LevelDimensions);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherLevelDimensions = matcher;
                }

                return _matcherLevelDimensions;
            }
        }
    }
}
