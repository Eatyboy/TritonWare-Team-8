using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private Gradient damageGradient;
    [SerializeField] private AnimationCurve alphaCurve;
    [SerializeField] private AnimationCurve sizeCurve;
    [SerializeField] private AnimationCurve motionCurve;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float minDamage;
    [SerializeField] private float maxDamage;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float maxDistance;
    [SerializeField] private float duration;

    private Vector3 startPos;
    private float t = 0;

    public void Init(Vector3 startPos, float damage)
    {
        this.startPos = startPos;
        direction = direction.normalized;
        text.color = damageGradient.Evaluate(damage / (maxDamage - minDamage));
        text.text = ((int)damage).ToString();
    }

    // Update is called once per frame
    private void Update()
    {
        text.alpha = alphaCurve.Evaluate(t);
        transform.localScale = sizeCurve.Evaluate(t) * Vector3.one;
        transform.position = startPos + motionCurve.Evaluate(t) * maxDistance * direction;
        t += Time.deltaTime / duration;

        if (t > 1) Destroy(gameObject);
    }
}
