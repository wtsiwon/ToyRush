using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SizeControlItem : AbstractItem
{
    public int sizeTime; //커지는 시간
    public int sizeWaitingTime; //기다릴 시간
    public Vector2 playerSize;

    float sizeTimer;
    protected override IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (Player.Instance.IsBoosting == false)
        {
            Player.Instance.IsBig = true;

            collision.transform.DOScale(new Vector2(playerSize.x + 0.2f, playerSize.y + 0.2f), sizeTime);

            yield return new WaitForSeconds(sizeWaitingTime);
            collision.transform.DOScale(playerSize, sizeTime);

            #region 무적시간
            Player.Instance.tag = "Invincibility";
            Player.Instance.GetComponent<SpriteRenderer>().DOFade(0.5f, 0.5f).SetLoops(-1, LoopType.Yoyo);

            yield return new WaitForSeconds(ItemManager.inst.invincibilityTimer);

            Player.Instance.tag = "Player";
            Player.Instance.GetComponent<SpriteRenderer>().DOKill();
            Player.Instance.GetComponent<SpriteRenderer>().DOFade(1, 0);
            #endregion

            Player.Instance.IsBig = false;

            Destroy(this.gameObject);
            sizeTimer = 0;
        }
    }
}
