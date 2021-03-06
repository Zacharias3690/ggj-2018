﻿using UnityEngine;

[DisallowMultipleComponent]
public class ActionControlsUI : MonoBehaviour
{
	public Camera m_MainCamera;
	public CanvasGroup controlsCanvas;
	public Vector3 labelOffset = new Vector3(0f, 40f, 0f);
	public PlatformerCharacter2D playerWithUI;

	private bool m_IsShowing;

	public bool IsShowing
	{
		get { return m_IsShowing; }
		private set
		{
			m_IsShowing = value;
		}
	}

	// Use this for initialization
	void Start()
	{
		var lockedColliders = FindObjectsOfType<OnLockedCollider>();
		foreach (var l in lockedColliders) {
			l.ShowLockedEvent += OnLockedBoxCollide;
		}

		MovementControlsUI.ToggleCanvasGroup(controlsCanvas, false);
	}

	void OnLockedBoxCollide(OnLockedCollider collider, PlatformerCharacter2D player, bool enter)
	{
		if (playerWithUI != player)
			return;

		MovementControlsUI.ToggleCanvasGroup(controlsCanvas, enter);

		m_IsShowing = enter;

		transform.position = m_MainCamera.WorldToScreenPoint(collider.block.transform.position) + labelOffset;

		if (enter) {
			collider.actionHandler.ActionUIEvent += ActionHandler_ActionUIEvent;
		} else {
			collider.actionHandler.ActionUIEvent -= ActionHandler_ActionUIEvent;
		}
		Debug.Log("OnLockedBoxCollide");
	}

	void ActionHandler_ActionUIEvent(ActionHandler.ActionType type)
	{
		var shouldHide = type == ActionHandler.ActionType.Start || type == ActionHandler.ActionType.Pass;
		MovementControlsUI.ToggleCanvasGroup(controlsCanvas, !shouldHide);
		Debug.LogFormat("ActionHandler_ActionUIEvent shouldHide:{0} type:{1}", shouldHide, type);
	}
}
