using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.PauseGame.OnEntityAddedOrRemoved(); } }
	
	Pool _pool;
	Group _time;

	const float EPSILON = 0.005f;

	public void SetPool(Pool pool) {
		_pool = pool;
		_time = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute(List<Entity> entities) {
		Entity time = _time.GetSingleEntity();
		TimeComponent component = time.time;
		if (Mathf.Abs(component.modificator - 1.0f) < EPSILON) {
			component.modificator = 0.0f;
			component.isPaused = false;
		}
		else {
			component.isPaused = true;
			component.modificator =  1.0f;
		}
	}
}
