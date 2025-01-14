using ServiceLocator.Events;
using ServiceLocator.Map;
using ServiceLocator.Player;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using ServiceLocator.Utilities;
using ServiceLocator.Wave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    [SerializeField] private PlayerScriptableObject playerScriptableObject;
    [SerializeField] private SoundScriptableObject soundScriptableObject;
    [SerializeField] private MapScriptableObject mapScriptableObject;
    [SerializeField] private WaveScriptableObject waveScriptableObject;

    [SerializeField] private AudioSource audioEffects;
    [SerializeField] private AudioSource backgroundMusic;

    [SerializeField] private UIService uiService;
    public UIService UIService => uiService;
    public PlayerService PlayerService {  get; private set; }
    public SoundService SoundService { get; private set; }
    public EventService EventService { get; private set; }
    public MapService MapService { get; private set; }
    public WaveService WaveService { get; private set; }

    private void Start()
    {
        EventService = new EventService();
        uiService.SubscribeToEvents();
        PlayerService = new PlayerService(playerScriptableObject);
        SoundService = new SoundService(soundScriptableObject, audioEffects, backgroundMusic);
        MapService = new MapService(mapScriptableObject);
        WaveService = new WaveService(waveScriptableObject);
    }

    private void Update()
    {
        PlayerService.Update();
    }
}
