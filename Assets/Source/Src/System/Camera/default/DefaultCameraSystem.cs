using Entitas;
using UnityEngine;

public class DefaultCameraSystem : IExecuteSystem, ISetPool {
	Group _group;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.DefaultCamera);
	}
	
	public void Execute() {
		foreach (Entity e in _group.GetEntities()) {
			updateCamera(e);
		}
	}
	
	void updateCamera(Entity e) {
		DefaultCameraComponent regularCamera = e.defaultCamera;
		CameraComponent camera = e.camera;
		FollowTargetComponent target = e.followTarget;
		Vector2 targetPosition = target.target.position.pos;

		camera.camera.transform.position = new Vector3(targetPosition.x + regularCamera.offset.x, targetPosition.y + regularCamera.offset.y, regularCamera.offset.z);
	}
}