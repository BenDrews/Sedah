using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    [SerializeField] List<Text> texts;
    CharacterObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.localPlayer.GetComponent<CharacterObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            texts[0].text = "AD: " + player.GetStatValue(StatType.AttackDamage).ToString();
            texts[1].text = "AP: " + player.GetStatValue(StatType.AbilityPower).ToString();
            texts[2].text = "AR: " + player.GetStatValue(StatType.Armor).ToString();
            texts[3].text = "MR: " + player.GetStatValue(StatType.MagicRes).ToString();
            texts[4].text = "AS: " + player.GetStatValue(StatType.AttackSpeed).ToString();
            texts[5].text = "CD: " + player.GetStatValue(StatType.CDR).ToString();
            texts[6].text = "PA: " + player.GetStatValue(StatType.ArmPen).ToString();
            texts[7].text = "PM: " + player.GetStatValue(StatType.MagicPen).ToString();
            texts[8].text = "MS: " + player.GetStatValue(StatType.MoveSpeed).ToString();
        }
    }
}
