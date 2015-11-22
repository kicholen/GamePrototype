using Entitas;
using System.Collections.Generic;
using UnityEngine;

	public class EnemyFactory {
	Pool _pool;
	Group _paths;

	public void SetPool(Pool pool, Group paths) {
		_pool = pool;
		_paths = paths;
	}

	public void CreateEnemyByType(int type, Vector2 position, int health, int missileSpeedBonus, int path, float speed = 5.0f) {
		Entity e;
		switch(type) {
		case 0:
			e = createStandardEnemy(type, position, health, speed);
			e.isFaceDirection = true;
			e.AddMissileSpawner(0.0f, 2.5f, Resource.MissileEnemy, 0.0f, -4.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy);
		break;
		case 1:
			e = createStandardEnemy(type, position, health, speed);
			e.isFaceDirection = true;
			e.AddHomeMissileSpawner(0.0f, 2.0f, Resource.MissileEnemy, 2.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy);
		break;
		case 101:
			e = createFirstBoss(type, position, health, missileSpeedBonus);
			break;
		default:
			e = createStandardEnemy(type, position, health, speed);
		break;
		}
		if (path > 0) {
			e.AddPath(0, position.y, 0.0f, _paths.GetEntities()[path - 1].pathModel);
		}
	}

	Entity createStandardEnemy(int type, Vector2 position, int health, float speed) {
		Entity e = _pool.CreateEntity()
			.AddEnemy(type)
			.AddPosition(position)
			.AddVelocity(new Vector2())
			.AddVelocityLimit(speed)
			.AddHealth(health)
			.AddCollision(CollisionTypes.Enemy)
			.AddBonusOnDeath(BonusTypes.Star | BonusTypes.Speed)
			.AddCameraShakeOnDeath(1)
			.AddExplosionOnDeath(1.0f, Resource.Explosion)
			.AddResource(Resource.Enemy);
		e.isNonRemovable = true;
		e.isActive = true;

		return e;
	}

	Entity createFirstBoss(int type, Vector2 position, int health, int missileSpeedBonus) {
		Entity boss = _pool.CreateEntity()
			.AddPosition(position)
			.AddVelocity(new Vector2())
			.AddVelocityLimit(5.0f)
			.AddCollision(CollisionTypes.Enemy)
			.AddHealth(health)
			.AddResource(Resource.Boss)
			.AddEnemy(type)
			.AddFirstBoss(22.0f, 0.0f, 90.0f);
		boss.isMoveWithCamera = true;
			

		List<Entity> children = new List<Entity>();
		children.Add(_pool.CreateEntity()
		             .AddRelativePosition(0.5f, 0.5f)
		             .AddPosition(new Vector2(0.0f, 0.0f))
		             .AddChild(boss)
		             .AddHomeMissileSpawner(5.0f, 10f, Resource.MissileEnemy, 2.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy)
		             .AddResource(Resource.Weapon));
		children.Add(_pool.CreateEntity()
		             .AddRelativePosition(-0.5f, 0.5f)
		             .AddPosition(new Vector2(0.0f, 0.0f))
		             .AddChild(boss)
		             .AddHomeMissileSpawner(5.0f, 10f, Resource.MissileEnemy, 2.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy)
		             .AddResource(Resource.Weapon));
		addNonRemovable(children);
		boss.AddParent(children);
		return boss;
	}

	void addNonRemovable(List<Entity> entities) {
		foreach (Entity e in entities) {
			e.isNonRemovable = true;
		}
	}
}
