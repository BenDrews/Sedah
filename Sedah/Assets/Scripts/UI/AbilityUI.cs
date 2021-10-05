using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] List<Image> images;
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
            //TODO: Update sprite images when ability is added.
            //Sprite sprite = player.GetAbility(0).GetSprite();
            //if (sprite != null)
            //{
            //    images[0].sprite = sprite;
            //}
        }
    }
}