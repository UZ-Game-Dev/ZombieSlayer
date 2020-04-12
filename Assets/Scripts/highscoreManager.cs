using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highscoreManager : MonoBehaviour
{
    public Text name1;
    public Text highscore1;
    public Text name2;
    public Text highscore2;
    public Text name3;
    public Text highscore3;
    public Text name4;
    public Text highscore4;
    public Text name5;
    public Text highscore5;
    private void OnEnable()
    {
       

        if (!PlayerPrefs.HasKey("highscore1"))
        {
            name1.text ="------";
            highscore1.text = "------";

        }
        else
        {
            name1.text = PlayerPrefs.GetString("name1");
            highscore1.text = "" + PlayerPrefs.GetInt("highscore1");
        }




        if (!PlayerPrefs.HasKey("highscore2"))
        {
            name2.text = "------";
            highscore2.text = "------";

        }
        else
        {
            name2.text = PlayerPrefs.GetString("name2");
            highscore2.text = "" + PlayerPrefs.GetInt("highscore2");
        }



        if (!PlayerPrefs.HasKey("highscore3"))
        {
            name3.text = "------";
            highscore3.text = "------";

        }
        else
        {
            name3.text = PlayerPrefs.GetString("name3");
            highscore3.text = "" + PlayerPrefs.GetInt("highscore3");
        }



        if (!PlayerPrefs.HasKey("highscore4"))
        {
            name4.text = "------";
            highscore4.text = "------";

        }
        else
        {
            name4.text = PlayerPrefs.GetString("name4");
            highscore4.text = "" + PlayerPrefs.GetInt("highscore4");
        }


          if (!PlayerPrefs.HasKey("highscore5"))
        {
            name5.text ="------";
            highscore5.text = "------";

        }
        else
        {
            name5.text = PlayerPrefs.GetString("name5");
            highscore5.text = "" + PlayerPrefs.GetInt("highscore5");
        }


     
    }

}
