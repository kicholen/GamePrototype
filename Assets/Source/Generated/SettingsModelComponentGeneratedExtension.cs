using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public SettingsModelComponent settingsModel { get { return (SettingsModelComponent)GetComponent(ComponentIds.SettingsModel); } }

        public bool hasSettingsModel { get { return HasComponent(ComponentIds.SettingsModel); } }

        static readonly Stack<SettingsModelComponent> _settingsModelComponentPool = new Stack<SettingsModelComponent>();

        public static void ClearSettingsModelComponentPool() {
            _settingsModelComponentPool.Clear();
        }

        public Entity AddSettingsModel(int newDifficulty, bool newMusic, bool newSound, string newLanguage) {
            var component = _settingsModelComponentPool.Count > 0 ? _settingsModelComponentPool.Pop() : new SettingsModelComponent();
            component.difficulty = newDifficulty;
            component.music = newMusic;
            component.sound = newSound;
            component.language = newLanguage;
            return AddComponent(ComponentIds.SettingsModel, component);
        }

        public Entity ReplaceSettingsModel(int newDifficulty, bool newMusic, bool newSound, string newLanguage) {
            var previousComponent = hasSettingsModel ? settingsModel : null;
            var component = _settingsModelComponentPool.Count > 0 ? _settingsModelComponentPool.Pop() : new SettingsModelComponent();
            component.difficulty = newDifficulty;
            component.music = newMusic;
            component.sound = newSound;
            component.language = newLanguage;
            ReplaceComponent(ComponentIds.SettingsModel, component);
            if (previousComponent != null) {
                _settingsModelComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveSettingsModel() {
            var component = settingsModel;
            RemoveComponent(ComponentIds.SettingsModel);
            _settingsModelComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherSettingsModel;

        public static IMatcher SettingsModel {
            get {
                if (_matcherSettingsModel == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.SettingsModel);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherSettingsModel = matcher;
                }

                return _matcherSettingsModel;
            }
        }
    }
}
