//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ComponentsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Entitas {
    public partial class Entity {
        public SecondBossComponent secondBoss { get { return (SecondBossComponent)GetComponent(ComponentIds.SecondBoss); } }

        public bool hasSecondBoss { get { return HasComponent(ComponentIds.SecondBoss); } }

        public Entity AddSecondBoss(float newDamageFactor, float newMissileSpeedFactor, float newAge, int newStage, float newMissileSpawn) {
            var component = CreateComponent<SecondBossComponent>(ComponentIds.SecondBoss);
            component.damageFactor = newDamageFactor;
            component.missileSpeedFactor = newMissileSpeedFactor;
            component.age = newAge;
            component.stage = newStage;
            component.missileSpawn = newMissileSpawn;
            return AddComponent(ComponentIds.SecondBoss, component);
        }

        public Entity ReplaceSecondBoss(float newDamageFactor, float newMissileSpeedFactor, float newAge, int newStage, float newMissileSpawn) {
            var component = CreateComponent<SecondBossComponent>(ComponentIds.SecondBoss);
            component.damageFactor = newDamageFactor;
            component.missileSpeedFactor = newMissileSpeedFactor;
            component.age = newAge;
            component.stage = newStage;
            component.missileSpawn = newMissileSpawn;
            ReplaceComponent(ComponentIds.SecondBoss, component);
            return this;
        }

        public Entity RemoveSecondBoss() {
            return RemoveComponent(ComponentIds.SecondBoss);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherSecondBoss;

        public static IMatcher SecondBoss {
            get {
                if (_matcherSecondBoss == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.SecondBoss);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherSecondBoss = matcher;
                }

                return _matcherSecondBoss;
            }
        }
    }
}
