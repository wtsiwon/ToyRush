using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum EAttackPattern
{
    Crocodile1,
    Crocodile2,
    Soldier,
    Drill,
    Gloves,
}

public class BaseAttack
{
    public Base thisBase;
    AttackPattern context;

    public Transform transform;
    public GameObject gameObject;

    public BaseAttack(AttackPattern _context)
    {
        context = _context;

        transform = _context.transform;
        gameObject = _context.gameObject;
    }

    public void BaseAttackType(EAttackPattern type)
    {
        switch (type)
        {
            case EAttackPattern.Crocodile1:
                thisBase = new Crocodile1(context);
                break;

            case EAttackPattern.Crocodile2:
                thisBase = new Crocodile2(context);
                break;

            case EAttackPattern.Soldier:
                thisBase = new Soldier(context);
                break;

            case EAttackPattern.Drill:
                thisBase = new Drill(context);
                break;

            case EAttackPattern.Gloves:
                thisBase = new Gloves(context);
                break;
        }
    }

    public void Attack()
    {
        thisBase.Attack();
    }
}

public abstract class Base
{
    public float waitTime;

    protected AttackPattern context;
    protected GameObject gameObject;
    protected GameObject attackCenter;
    protected Transform transform;
    protected SpriteRenderer warningLine;

    public void Setting(AttackPattern _context, GameObject _attackCenter)
    {
        waitTime = _context.WaitTime;

        context = _context;
        gameObject = _context.gameObject;
        transform = _context.transform;

        attackCenter = _attackCenter;
        warningLine = _context.warningLine;
    }

    public abstract IEnumerator Attack();
}

public class Crocodile1 : Base
{
    public Crocodile1(AttackPattern _context) => Setting(_context, _context.crocodile);

    public override IEnumerator Attack()
    {
        AttackPatternManager.inst.isAttackSummon = true;
        SoundManager.instance.PlaySoundClip("WarningSFX", SoundType.SFX, SoundManager.instance.soundSFX - 0.5f);

        warningLine.transform.DOLocalMoveY(Player.Instance.transform.position.y, 0);

        warningLine.DOFade(0, waitTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        yield return new WaitForSeconds(3);

        warningLine.DOKill();
        warningLine.DOFade(0, 0);

        attackCenter.transform.DOLocalMoveY(warningLine.transform.position.y, waitTime).SetEase(Ease.InOutQuart).OnComplete(() =>
        {
            attackCenter.transform.DORotate(new Vector3(0, 0, 30), waitTime).SetEase(Ease.Linear);
        });

        yield return new WaitForSeconds(waitTime);

        SoundManager.instance.PlaySoundClip("Attack", SoundType.SFX, SoundManager.instance.soundSFX);
        attackCenter.transform.DOLocalMoveX(-13, waitTime * 2).SetEase(Ease.InOutQuart);


        yield return new WaitForSeconds(0.5f);
        Camera.main.transform.DOShakePosition(0.5f, new Vector2(2, 0f)).SetEase(Ease.Linear);

        yield return new WaitForSeconds(1);

        AttackPatternManager.inst.isAttackSummon = false;

        attackCenter.transform.DOKill();
        Destroy(this.gameObject);
    }

    public static void Destroy(Object obj)
    {
        Destroy(obj);
    }
}

public class Crocodile2 : Base
{
    public Crocodile2(AttackPattern _context) => Setting(_context, _context.crocodile);

    public override IEnumerator Attack()
    {
        AttackPatternManager.inst.isAttackSummon = true;
        SoundManager.instance.PlaySoundClip("WarningSFX", SoundType.SFX, SoundManager.instance.soundSFX - 0.5f);

        attackCenter.transform.DOLocalMoveX(Player.Instance.transform.position.x, 0);
        warningLine.transform.DOLocalMove(new Vector2(Player.Instance.transform.position.x, -1.5f), 0);

        warningLine.DOFade(0, waitTime).SetLoops(-1, LoopType.Yoyo);
        yield return new WaitForSeconds(3);

        warningLine.DOKill();
        warningLine.DOFade(0, 0);

        attackCenter.transform.DOLocalMoveY(0.5f, waitTime).SetEase(Ease.InOutQuart);
        yield return new WaitForSeconds(waitTime);

        SoundManager.instance.PlaySoundClip("Attack", SoundType.SFX, SoundManager.instance.soundSFX);

        attackCenter.transform.DOLocalMoveY(-7, waitTime).SetEase(Ease.InOutQuart);
        Camera.main.transform.DOShakePosition(0.4f, new Vector2(0, 1));

        yield return new WaitForSeconds(waitTime);
        AttackPatternManager.inst.isAttackSummon = false;
        attackCenter.transform.DOKill();
        Destroy(this.gameObject);
    }

    public static void Destroy(Object obj)
    {
        Destroy(obj);
    }
}

public class Soldier : Base
{
    public Soldier(AttackPattern _context) => Setting(_context, _context.enemy);

    public override IEnumerator Attack()
    {
        int enemeyMovePos = 7;
        float enemyMoveSpeed = 1.5f;

        context.warningSummon.transform.DOLocalMoveY(enemeyMovePos, enemyMoveSpeed);
        for (int i = 0; i < 4; i++)
        {
            float warninTimeMin = 0.1f;
            float warninTimeMax = 0.4f;

            context.summonPos[i] = Random.Range(warninTimeMin, warninTimeMax);
            context.InstantiateSetParent(context.warningLineRight, new Vector2(0, context.warningSummon.transform.position.y), Quaternion.identity)/*.transform.parent = gameObject.transform*/;

            yield return new WaitForSeconds(context.summonPos[i]);
        }

        yield return new WaitForSeconds(3);

        attackCenter.transform.DOLocalMoveY(enemeyMovePos, enemyMoveSpeed);
        for (int i = 0; i < 4; i++)
        {
            context.InstantiateSetParent(context.bulletPrefab, new Vector2(attackCenter.transform.position.x, attackCenter.transform.position.y), Quaternion.identity).transform.parent = gameObject.transform;
            yield return new WaitForSeconds(context.summonPos[i]);
        }

        yield return new WaitForSeconds(3);
        AttackPatternManager.inst.isAttackSummon = false;
        attackCenter.transform.DOKill();
        context.bulletPrefab.transform.DOKill();
        Destroy(this.gameObject);
    }

    public static void Destroy(Object obj)
    {
        Destroy(obj);
    }


}

public class Drill : Base
{
    public Drill(AttackPattern _context) => Setting(_context, _context.drill);

    public override IEnumerator Attack()
    {
        for (int i = 0; i <= 2; i++)
        {
            AttackPatternManager.inst.isAttackSummon = true;
            SoundManager.instance.PlaySoundClip("WarningSFX", SoundType.SFX, SoundManager.instance.soundSFX - 0.5f);

            warningLine.DOFade(0.5f, 0);
            transform.DORotate(new Vector3(0, 0, Random.Range(-50, 50)), 0);

            warningLine.DOFade(0, waitTime).SetLoops(-1, LoopType.Yoyo);
            yield return new WaitForSeconds(3);

            warningLine.DOKill();
            warningLine.DOFade(0, 0);
            attackCenter.transform.DOLocalMoveX(16, 0);
            attackCenter.transform.DOLocalMoveX(-15f, 1).SetEase(Ease.Unset);
            SoundManager.instance.PlaySoundClip("Attack", SoundType.SFX, SoundManager.instance.soundSFX);
            Camera.main.transform.DOShakePosition(0.4f, new Vector2(1, 1));

            yield return new WaitForSeconds(0.7f);
        }
        yield return new WaitForSeconds(1);
        AttackPatternManager.inst.isAttackSummon = false;
        attackCenter.transform.DOKill();
        Destroy(this.gameObject);
    }

    public static void Destroy(Object obj)
    {
        Destroy(obj);
    }
}

public class Gloves : Base
{
    public Gloves(AttackPattern _context) => Setting(_context, _context.glove);

    public override IEnumerator Attack()
    {
        int random = Random.Range(-18, 18);
        AttackPatternManager.inst.isAttackSummon = true;
        SoundManager.instance.PlaySoundClip("WarningSFX", SoundType.SFX, SoundManager.instance.soundSFX - 0.5f);

        transform.DOMoveY(0, 0);

        for (int i = 0; i <= 2; i++)
        {
            transform.GetChild(i).DOLocalRotate(new Vector3(0, 0, random), waitTime);
            transform.GetChild(i).GetChild(1).GetComponent<SpriteRenderer>().DOFade(0, waitTime).SetLoops(-1, LoopType.Yoyo);
        }

        yield return new WaitForSeconds(3);

        for (int i = 0; i <= 2; i++)
        {
            transform.GetChild(i).GetChild(1).GetComponent<SpriteRenderer>().DOKill();
            transform.GetChild(i).GetChild(1).GetComponent<SpriteRenderer>().DOFade(0, 0);
        }

        yield return new WaitForSeconds(waitTime);

        SoundManager.instance.PlaySoundClip("Attack", SoundType.SFX, SoundManager.instance.soundSFX);
        for (int i = 0; i <= 2; i++)
            transform.GetChild(i).GetChild(0).DOLocalMoveX(-18, waitTime);

        yield return new WaitForSeconds(1);
        AttackPatternManager.inst.isAttackSummon = false;
        attackCenter.transform.DOKill();
        Destroy(this.gameObject);
    }

    public static void Destroy(Object obj)
    {
        Destroy(obj);
    }
}