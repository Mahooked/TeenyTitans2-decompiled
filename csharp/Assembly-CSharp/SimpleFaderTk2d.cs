using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(tk2dBaseSprite))]
public class SimpleFaderTk2d : MonoBehaviour
{
	private Tweener fadeTween;

	private Action onComplete;

	[SerializeField]
	private tk2dBaseSprite _sprite;

	[SerializeField]
	private tk2dUIItem _uiItem;

	[SerializeField]
	private Collider2D _collider2D;

	[SerializeField]
	private bool _useIndependentTime;

	public tk2dBaseSprite sprite => _sprite;

	public tk2dUIItem uiItem => _uiItem;

	public Collider2D collider2D => _collider2D;

	public bool useIndependentTime
	{
		get
		{
			return _useIndependentTime;
		}
		set
		{
			if (value != _useIndependentTime)
			{
				_useIndependentTime = value;
				if (fadeTween != null)
				{
					fadeTween.SetUpdate(_useIndependentTime);
				}
			}
		}
	}

	private void Awake()
	{
		if (_sprite == null)
		{
			_sprite = GetComponent<tk2dBaseSprite>();
		}
		if (_uiItem == null)
		{
			_uiItem = GetComponent<tk2dUIItem>();
		}
		if (_collider2D == null)
		{
			_collider2D = GetComponent<Collider2D>();
		}
	}

	private void Reset()
	{
		_sprite = GetComponent<tk2dBaseSprite>();
		_uiItem = GetComponent<tk2dUIItem>();
		_collider2D = GetComponent<Collider2D>();
	}

	public void FadeTo(float alpha, float duration, Action onComplete = null)
	{
		this.onComplete = onComplete;
		if (fadeTween != null)
		{
			fadeTween.Kill();
		}
		if (!gameObject.activeSelf)
		{
			gameObject.SetActive(value: true);
		}
		fadeTween = DOTween.ToAlpha(() => _sprite.color, (Color x) =>
		{
			_sprite.color = x;
		}, alpha, duration).SetEase(Ease.Linear).SetUpdate(_useIndependentTime)
			.OnComplete(OnFadeComplete);
	}

	private void OnFadeComplete()
	{
		fadeTween = null;
		if (_sprite.color.a <= 0f)
		{
			gameObject.SetActive(value: false);
		}
		if (onComplete != null)
		{
			onComplete();
			onComplete = null;
		}
	}
}
