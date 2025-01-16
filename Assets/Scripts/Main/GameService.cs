using UnityEngine;
using ServiceLocator.Utilities;
using ServiceLocator.Events;
using ServiceLocator.Map;
using ServiceLocator.Wave;
using ServiceLocator.Sound;
using ServiceLocator.Player;
using ServiceLocator.UI;

namespace ServiceLocator.Main
{
    public class GameService : MonoBehaviour
    {
        // Services:
        public EventService eventService;
        public MapService mapService;
        public WaveService waveService;
        public SoundService soundService;
        public PlayerService playerService;

        [SerializeField] private UIService uiService;
        public UIService UIService => uiService;


        // Scriptable Objects:
        [SerializeField] private MapScriptableObject mapScriptableObject;
        [SerializeField] private WaveScriptableObject waveScriptableObject;
        [SerializeField] private SoundScriptableObject soundScriptableObject;
        [SerializeField] private PlayerScriptableObject playerScriptableObject;

        // Scene Referneces:
        [SerializeField] private AudioSource SFXSource;
        [SerializeField] private AudioSource BGSource;

        private void Start()
        {
            createServices();
            InjectDependencies();
        }

        private void createServices()
        {
            eventService = new EventService();
            mapService = new MapService(mapScriptableObject);
            waveService = new WaveService(waveScriptableObject);
            soundService = new SoundService(soundScriptableObject, SFXSource, BGSource);
            playerService = new PlayerService(playerScriptableObject);
        }

        private void InjectDependencies()
        {
            UIService.Init(waveService, eventService, playerService);
            playerService.Init(UIService, mapService, soundService);
            waveService.Init(UIService, mapService, soundService, eventService, playerService);
            mapService.Init(eventService);
        }

        private void Update()
        {
            playerService.Update();
        }
    }
}