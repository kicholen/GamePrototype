using Entitas;
using UnityEngine;

public class RemoveOutOfViewGOSystem : IExecuteSystem, ISetPool {
	Group _cameraGroup;
	Group _group;
	const float offset = 2.0f;

	public void SetPool(Pool pool) {
		_cameraGroup = pool.GetGroup(Matcher.Camera);
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Position, Matcher.GameObject));
	}
	
	public void Execute() {
		Entity cameraEntity = _cameraGroup.GetSingleEntity();
		Camera camera = cameraEntity.camera.camera;
		Vector3 cameraPosition = camera.transform.position;
		float ortographicSize = camera.orthographicSize;
		float radiusPower = Mathf.Pow(camera.aspect > 1.0f ? ortographicSize * camera.aspect + offset : ortographicSize + offset, 2.0f);

		foreach (Entity e in _group.GetEntities()) {
			Vector2 position = e.position.pos;
			if (!e.isNonRemovable && !isPointInCircle(0.0f, cameraPosition.y, radiusPower, position.x, position.y)) {
				e.isDestroyEntity = true;
			}
		}
	}

	bool isPointInCircle(float centerX, float centerY, float radiusPower, float x, float y) {
		float distance = Mathf.Pow(centerX - x, 2.0f) + Mathf.Pow(centerY - y, 2.0f);
		return distance < radiusPower;
	}
}
