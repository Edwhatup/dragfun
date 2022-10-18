using CardEvent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Card;
using Card.Monster;
using Card.Spell;
using Visual;

namespace Core
{
    public class CardManager : MonoSingleton<CardManager>, IManager
    {
        [SerializeField]
        RectTransform deckTrans;
        [SerializeField]
        RectTransform handTrans;
        [SerializeField]
        GameObject cardPrefab;
        public Dictionary<PlayerCard, PlayerCardVisual> cardObjects = new Dictionary<PlayerCard, PlayerCardVisual>();
        public List<PlayerCard> deck = new List<PlayerCard>();

        public void UseSpell(SpellCard spell)
        {
            if (hand.Contains(spell))
            {
                spell.state = PlayerCardState.Intomb;
                hand.Remove(spell);
                tombs.Add(spell);
                Destroy(cardObjects[spell].gameObject);
                cardObjects.Remove(spell);
            }
            Refresh();
        }

        public List<PlayerCard> hand = new List<PlayerCard>();
        public List<PlayerCard> board = new List<PlayerCard>();
        public List<PlayerCard> tombs = new List<PlayerCard>();
        #region IGameTurn实现区域
        public void EnemyAction()
        {

        }

        public void GameStart()
        {
            ReadDeck();
            ShuffletDeck();
            DrawCard(Player.Instance.initDrawCardCnt);
            Refresh();
        }

        public void PlayerAction()
        {

        }
        public void PlayDraw()
        {
            DrawCard(Player.Instance.drawCardCntTurn);
            Refresh();
        }
        public void DrawCard(int cnt)
        {
            for (int i = 0; i < cnt; i++)
            {
                if (deck.Count == 0) break;
                if (hand.Count == Player.Instance.maxHandCnt) break;
                var card = deck[0];
                card.state = PlayerCardState.InHand;
                hand.Add(card);
                deck.RemoveAt(0);
            }
        }
        //后续应该做成对卡牌情况变化的相应事件
        public void Refresh()
        {
            foreach (var card in deck)
            {
                if (!cardObjects.ContainsKey(card))
                {
                    var visual = CreateCardObject(deckTrans, card);
                    cardObjects.Add(card, visual);
                }
                cardObjects[card].UpdateVisual();
            }
            foreach (var card in hand)
            {
                if (!cardObjects.ContainsKey(card))
                {
                    var visual = CreateCardObject(handTrans, card);
                    cardObjects.Add(card, visual);
                }
                cardObjects[card].UpdateVisual();
            }
            foreach (var card in board)
            {
                var monster = card as MonsterCard;
                if (monster.battleState == BattleState.Dead)
                {
                    Destroy(cardObjects[monster].gameObject);
                    cardObjects.Remove(card);
                    board.Remove(monster);
                    tombs.Add(monster);
                    monster.state = PlayerCardState.Intomb;
                }
                else
                    cardObjects[card].UpdateVisual();
            }


            LayoutRebuilder.ForceRebuildLayoutImmediate(handTrans);
            LayoutRebuilder.ForceRebuildLayoutImmediate(deckTrans);
        }
        #endregion
        public void Summon(MonsterCard card)
        {
            if (hand.Contains(card))
            {
                hand.Remove(card);
                board.Add(card);
                card.state = PlayerCardState.OnBoard;
            }
        }

        //根据玩家信息初始化卡组
        private void ReadDeck()
        {
            deck.Clear();
            foreach (var item in Player.Instance.deck)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    var card = PlayerCardStore.Instance.CopyCard(item.Key) as PlayerCard;
                    deck.Add(card);
                    var listeners = EventListenerAttribute.GetListener(card);
                    foreach (var listener in listeners)
                    {
                        BattleManager.cardEventListeners += listener;
                    }
                }
            }
        }
        //牌库洗牌
        private void ShuffletDeck()
        {
            for (int i = 0; i < deck.Count; i++)
            {
                int rad = Random.Range(0, deck.Count);
                var temp = deck[i];
                deck[i] = deck[rad];
                deck[rad] = temp;
            }
        }

        private PlayerCardVisual CreateCardObject(Transform parent, PlayerCard card)
        {
            var go = Instantiate(cardPrefab, parent);
            PlayerCardVisual visual = go.GetComponent<PlayerCardVisual>();
            visual.card = card;
            return visual;
        }
    }

}