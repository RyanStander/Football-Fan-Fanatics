using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private NodeController[] _allBases;
    [SerializeField] private GameObject _victoryObject;
    [SerializeField] private GameObject _failureObject;
    [SerializeField] private float _checkDelay = 1f;
    [SerializeField] private float _timeToSceneSwap = 4f;
    [SerializeField] private string _mainMenuSceneName = "MainMenu";
    
    private float _checkTimeStamp;
    private float _gameOverTimeStamp;
    private bool _gameOver;

    private void OnValidate()
    {
        _allBases= FindObjectsOfType<NodeController>();
    }

    private void Update()
    {
        if (_gameOver)
        {
            if (_gameOverTimeStamp > Time.time) return;

            SceneManager.LoadScene(_mainMenuSceneName);
        }
        else
        {
            if (_checkTimeStamp > Time.time) return;

            _checkTimeStamp = Time.time + _checkDelay;

            var enemyBaseCount = 0;
            var playerBaseCount = 0;

            foreach (var VARIABLE in _allBases)
            {
                switch (VARIABLE.GetTeamIndex())
                {
                    //friendly
                    case 0:
                        playerBaseCount++;
                        break;
                    //hostile
                    case 1:
                        enemyBaseCount++;
                        break;
                }
            }

            if (enemyBaseCount == 0)
            {
                //player wins
                _victoryObject.SetActive(true);
                _gameOverTimeStamp = Time.time + _timeToSceneSwap;
                _gameOver = true;
            }
            else if (playerBaseCount == 0)
            {
                //player loses
                _failureObject.SetActive(true);
                _gameOverTimeStamp = Time.time + _timeToSceneSwap;
                _gameOver = true;
            }
        }
    }
}
