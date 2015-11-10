using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class RestartGameSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.RestartGame.OnEntityAdded(); } }
	
	Pool _pool;
	Group _createLevels;
	Group _players;
	Group _cameras;
	Group _resources;
	Group _enemySpawners;
	Group _bonusSpawners;
	Group _homeMissileSpawners;
	Group _waveSpawners;
	Group _bonuses;
	Group _cameraShakes;
	Group _gameStats;

	public void SetPool(Pool pool) {
		_pool = pool;
		_createLevels = pool.GetGroup(Matcher.CreateLevel);
		_players = pool.GetGroup(Matcher.Player);
		_cameras = pool.GetGroup(Matcher.SmoothCamera);
		_resources = pool.GetGroup(Matcher.Resource);
		_enemySpawners = pool.GetGroup(Matcher.EnemySpawner);
		_homeMissileSpawners = pool.GetGroup(Matcher.HomeMissileSpawner);
		_waveSpawners = pool.GetGroup(Matcher.WaveSpawner);
		_cameraShakes = pool.GetGroup(Matcher.CameraShake);
		_bonuses = pool.GetGroup(Matcher.BonusModel);
		_gameStats = pool.GetGroup(Matcher.GameStats);
	}
	
	public void Execute(List<Entity> entities) {
		foreach (Entity e in entities) {
			_pool.DestroyEntity(e);
		}

		restartCamera();
		restartLevel();
		clearGameObjects();
		clearEnemySpawners();
		clearHomeMissileSpawners();
		clearWaveSpawners();
		clearBonuses();
		clearCameraShakes();
		clearGameStats();

		restartPlayer();
	}

	void restartPlayer() {
		Entity player = _players.GetSingleEntity();
		player.ReplacePosition(new Vector2(0.0f, 0.0f));
		player.ReplaceHealth(50);
		player.isDestroyEntity = false;
		if (player.hasParent) {
			List<Entity> children = player.parent.children;
			for (int i = 0; i < children.Count; i++) {
				children[i].isDestroyEntity = false;
			}
		}
	}

	void restartCamera() {
		foreach (Entity e in _cameras.GetEntities()) {
			e.ReplacePosition(new Vector2(0.0f, 0.0f));
		}
	}

	void restartLevel() {
		foreach (Entity e in _createLevels.GetEntities()) {
			_pool.DestroyEntity(e);
		}

		_pool.CreateEntity()
			.AddCreateLevel(1, "/Resources/Content/level/")
			.isDestroyEntity = true;
	}

	void clearEnemySpawners() {
		foreach (Entity e in _enemySpawners.GetEntities()) {
			_pool.DestroyEntity(e);
		}
	}

	void clearHomeMissileSpawners() {
		foreach (Entity e in _homeMissileSpawners.GetEntities()) {
			if (!e.hasChild) {
				e.isDestroyEntity = true;
			}
		}
	}

	void clearWaveSpawners() {
		foreach (Entity e in _waveSpawners.GetEntities()) {
			e.isDestroyEntity = true;
		}
	}

	void clearGameObjects() {
		foreach (Entity e in _resources.GetEntities()) {
			if (!e.hasChild) {
				e.isDestroyEntity = true;
			}
		}
	}

	void clearBonuses() {
		foreach (Entity e in _bonuses.GetEntities()) {
			if (!e.hasChild) {
				e.isDestroyEntity = true;
			}
		}
	}

	void clearCameraShakes() {
		foreach (Entity e in _cameraShakes.GetEntities()) {
			if (!e.hasChild) {
				e.isDestroyEntity = true;
			}
		}
	}

	void clearGameStats() {
		foreach (Entity e in _gameStats.GetEntities()) {
			e.ReplaceGameStats(0, 0, 0);
		}
	}
}
