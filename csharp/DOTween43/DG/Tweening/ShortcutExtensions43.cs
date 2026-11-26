using DG.Tweening.Core;
using UnityEngine;

namespace DG.Tweening;

public static class ShortcutExtensions43
{
	public static Tweener DOColor(this SpriteRenderer target, Color endValue, float duration)
	{
		return DOTween.To(() => target.color, (Color x) =>
		{
			target.color = x;
		}, endValue, duration).SetTarget(target);
	}

	public static Tweener DOFade(this SpriteRenderer target, float endValue, float duration)
	{
		return DOTween.ToAlpha(() => target.color, (Color x) =>
		{
			target.color = x;
		}, endValue, duration).SetTarget(target);
	}

	public static Tweener DOMove(this Rigidbody2D target, Vector2 endValue, float duration, bool snapping = false)
	{
		return DOTween.To(() => target.position, target.MovePosition, endValue, duration).SetOptions(snapping).SetTarget(target);
	}

	public static Tweener DOMoveX(this Rigidbody2D target, float endValue, float duration, bool snapping = false)
	{
		return DOTween.To(() => target.position, target.MovePosition, new Vector2(endValue, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetTarget(target);
	}

	public static Tweener DOMoveY(this Rigidbody2D target, float endValue, float duration, bool snapping = false)
	{
		return DOTween.To(() => target.position, target.MovePosition, new Vector2(0f, endValue), duration).SetOptions(AxisConstraint.Y, snapping).SetTarget(target);
	}

	public static Tweener DORotate(this Rigidbody2D target, float endValue, float duration)
	{
		return DOTween.To(() => target.rotation, target.MoveRotation, endValue, duration).SetTarget(target);
	}

	public static Sequence DOJump(this Rigidbody2D target, Vector2 endValue, float jumpPower, int numJumps, float duration, bool snapping = false)
	{
		if (numJumps < 1)
		{
			numJumps = 1;
		}
		float offsetY = endValue.y - target.position.y;
		Sequence s = DOTween.Sequence().Append(DOTween.To(() => target.position, target.MovePosition, new Vector2(endValue.x, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear)).Join(DOTween.To(() => target.position, target.MovePosition, new Vector2(0f, jumpPower), duration / (float)(numJumps * 2)).SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.OutQuad)
			.SetLoops(numJumps * 2, LoopType.Yoyo))
			.SetTarget(target)
			.SetEase(DOTween.defaultEaseType);
		s.OnUpdate(() =>
		{
			Vector2 position = target.position;
			position.y += DOVirtual.EasedValue(0f, offsetY, s.ElapsedDirectionalPercentage(), Ease.OutQuad);
			target.MovePosition(position);
		});
		return s;
	}

	public static Tweener DOBlendableColor(this SpriteRenderer target, Color endValue, float duration)
	{
		endValue -= target.color;
		Color to = default;
		ref Color reference = ref to;
		reference = new Color(0f, 0f, 0f, 0f);
		return DOTween.To(() => to, (Color x) =>
		{
			Color color = x - to;
			to = x;
			target.color += color;
		}, endValue, duration).Blendable().SetTarget(target);
	}
}
