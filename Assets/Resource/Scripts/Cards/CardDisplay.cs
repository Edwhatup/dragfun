using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardDisplay : MonoBehaviour
{
    public Text nameText;
    public Text attackText;
    public Text healthText;
    public Text effectText;

    public Image backgroundImage;

    public Card card;

    // Start is called before the first frame update
    void Start()
    {
        ShowCard();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void  ShowCard()
    {
        nameText.text = card.name;
        if(card is BattleCard)
        {
            var monster = card as MonsterCard;
            attackText.text = monster.atk.ToString();
            healthText.text = monster.healthPoint.ToString();

            
            //if (monster.effect == null)
            //{
            //    effectText.gameObject.SetActive(false);
            //}
            //else
            //{
            //    effectText.text = monster.effect;
            //}



        }

        else if(card is SpellCard)
        {
            var spell = card as SpellCard;
            //effectText.text = spell.effect;
            attackText.gameObject.SetActive(false);
            healthText.gameObject.SetActive(false);
        }
    }
}
