using System.Collections;
using System.Collections.Generic;
using _Scripts;
using _Scripts.Function;
using _Scripts.Laser;
using UnityEngine;

public class TitleWave : MonoBehaviour {
    [SerializeField] private LaserHead laserHead;
    [SerializeField] private LaserLead laserLead;
    [SerializeField] private LaserHead[] titleHeads;
    [SerializeField] private LaserLead[] titleLeads;
    [SerializeField] private SpriteRenderer[] titles;
    private float[] _alphas;
    private int _timer = 0;
    private float _iniTime;
    void Start() {
        titleHeads = new LaserHead[30];
        titleLeads = new LaserLead[30];
        _timer = 0;
        _iniTime = Time.time;
        _alphas = new[] { 0f, 0f, 0f, 0f };
        titles[0].color = new Color(titles[0].color.r, titles[0].color.g, titles[0].color.b, 0);
        titles[1].color = new Color(titles[1].color.r, titles[1].color.g, titles[1].color.b, 0);
        titles[2].color = new Color(titles[2].color.r, titles[2].color.g, titles[2].color.b, 0);
        titles[3].color = new Color(titles[3].color.r, titles[3].color.g, titles[3].color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer < 30) {
            titleHeads[_timer] = Instantiate(laserHead);
            titleLeads[_timer] = Instantiate(laserLead);
            titleLeads[_timer]._timer = 4500 + _timer * 100;
            titleHeads[_timer].GenerateLaser(20, 0, 0.02f, 96f,titleLeads[_timer].transform);
            _timer++;
        }

        float curTime = Time.time - _iniTime;
        if(curTime >= 0f) {
            var a = Calc.Approach(titles[0].color.a, 1f, 1280f);
            titles[0].color = new Color(titles[0].color.r, titles[0].color.g, titles[0].color.b, a);
        }
        
        if(curTime >= 2f) {
            var a = Calc.Approach(titles[1].color.a, 1f, 1280f);
            titles[1].color = new Color(titles[1].color.r, titles[1].color.g, titles[1].color.b, a);
        }
        
        if(curTime >= 5f) {
            var a = Calc.Approach(titles[2].color.a, 1f, 1280f);
            titles[2].color = new Color(titles[2].color.r, titles[2].color.g, titles[2].color.b, a);
        }
        
        if(curTime >= 5.5f) {
            var a = Calc.Approach(titles[3].color.a, (1 + Mathf.Sin(curTime))/2f, 32f);
            titles[3].color = new Color(titles[3].color.r, titles[3].color.g, titles[3].color.b, a);
        }


    }
}
