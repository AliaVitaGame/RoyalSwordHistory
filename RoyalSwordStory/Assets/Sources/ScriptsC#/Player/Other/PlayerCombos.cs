using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombos : MonoBehaviour
{
    [SerializeField] private int count;
    [SerializeField] private Text textCount;
    [SerializeField] private float timeReset = 3;
    [SerializeField] private DamageCollision damageCollision;
    [Space]
    [SerializeField] private int middleCombo = 15;
    [SerializeField] private Color colorMiddleCombo = Color.yellow;
    [Space]
    [SerializeField] private int bigCombo = 30;
    [SerializeField] private Color colorBigCombo = Color.red;

    private Color _startColor;

    private string _nameDeactive = "Deactive";
    private Animator _textCountAnimator;

    private void OnEnable()
    {
        GetComponent<PlayerAttacking>().DamageCollisionEvent += ShowCombo;
        GetComponent<PlayerStats>().PlayerHitEvent += ResetCombo;

        if (damageCollision)
            damageCollision.DamageCollisionEvent += ShowCombo;
    }

    private void OnDisable()
    {
        GetComponent<PlayerAttacking>().DamageCollisionEvent -= ShowCombo;
        GetComponent<PlayerStats>().PlayerHitEvent -= ResetCombo;

        if (damageCollision)
            damageCollision.DamageCollisionEvent -= ShowCombo;
    }

    private void Start()
    {
        _textCountAnimator = textCount.GetComponent<Animator>();
        _startColor = textCount.color;

        count = 0;
        RefreshUI();
        SetActiveText(false);

    }

    private void ShowCombo()
    {
        count++;

        SetActiveText(false);
        SetActiveText(true);

        RefreshUI();

        StopAllCoroutines();
        StartCoroutine(ResetTimer());
    }

    private IEnumerator ResetTimer()
    {
        yield return new WaitForSeconds(timeReset);
        _textCountAnimator.SetTrigger(_nameDeactive);
        yield return new WaitForSeconds(1);
        ResetCombo();
    }

    public void ResetCombo()
    {
        count = 0;
        _textCountAnimator.SetTrigger(_nameDeactive);
        RefreshUI();
    }

    private void RefreshUI()
    {
        textCount.text = $"COMBO:\n{count}";

        if (count >= bigCombo) SetColorText(colorBigCombo);
        else if (count >= middleCombo) SetColorText(colorMiddleCombo);
        else SetColorText(_startColor);
    }

    private void SetColorText(Color color)
        => textCount.color = color;

    private void SetActiveText(bool active)
        => textCount.gameObject.SetActive(active);

}
