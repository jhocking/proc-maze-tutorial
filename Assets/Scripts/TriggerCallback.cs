using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCallback : MonoBehaviour {
	public Action<Collider> callback;

	void OnTriggerEnter(Collider other) {
		if (callback != null)
			callback(other);
	}
}
