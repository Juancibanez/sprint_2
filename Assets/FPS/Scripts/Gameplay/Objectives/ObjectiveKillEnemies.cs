using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public class ObjectiveKillEnemies : Objective
    {
        [Tooltip("Choose Player")]
        public GameObject player;
        
        [Tooltip("Chose whether you need to kill every enemies or only a minimum amount")]
        public bool MustKillAllEnemies = true;

        [Tooltip("If MustKillAllEnemies is false, this is the amount of enemy kills required")]
        public int KillsToCompleteObjective = 5;

        [Tooltip("Start sending notification about remaining enemies when this amount of enemies is left")]
        public int NotificationEnemiesRemainingThreshold = 3;

        public ControladorCombos combos;

        int m_KillTotal;

        int kills;

        protected override void Start()
        {
            combos = player.GetComponent<ControladorCombos>(); 
            base.Start();

            EventManager.AddListener<EnemyKillEvent>(OnEnemyKilled);

            // set a title and description specific for this type of objective, if it hasn't one
            if (string.IsNullOrEmpty(Title))
                Title = "Eliminate " + (MustKillAllEnemies ? "all the" : KillsToCompleteObjective.ToString()) +
                        " enemies";

            if (string.IsNullOrEmpty(Description))
                Description = GetUpdatedCounterAmount();
                
        }

        void Update()
        {
            kills = combos.getKills();
        }

        void OnEnemyKilled(EnemyKillEvent evt)
        {
            //////////////// Elementos 
            PlayerCharacterController playerController = player.GetComponent<PlayerCharacterController>();
            PlayerWeaponsManager weaponController = playerController.GetComponent<PlayerWeaponsManager>();
            /////////////// 

            ////////////// Cambiar de Arma
            combos.kill();   
            if (kills < 1)
            {
                weaponController.SwitchToWeaponIndex(0);
            }
            else if (kills >= 1 && kills < 3)
            {
                weaponController.SwitchToWeaponIndex(1);
            }
            else if (kills >= 3)
            {
                weaponController.SwitchToWeaponIndex(2);
            }
            ///////////// FIN

            if (IsCompleted)
                return;

            m_KillTotal++;

            if (MustKillAllEnemies)
                KillsToCompleteObjective = evt.RemainingEnemyCount + m_KillTotal;

            int targetRemaining = MustKillAllEnemies ? evt.RemainingEnemyCount : KillsToCompleteObjective - m_KillTotal;

            // update the objective text according to how many enemies remain to kill
            if (targetRemaining == 0)
            {
                CompleteObjective(string.Empty, GetUpdatedCounterAmount(), "Objective complete : " + Title);
            }
            else if (targetRemaining == 1)
            {
                string notificationText = NotificationEnemiesRemainingThreshold >= targetRemaining
                    ? "One enemy left"
                    : string.Empty;
                UpdateObjective(string.Empty, GetUpdatedCounterAmount(), notificationText);
            }
            else
            {
                // create a notification text if needed, if it stays empty, the notification will not be created
                string notificationText = NotificationEnemiesRemainingThreshold >= targetRemaining
                    ? targetRemaining + " enemies to kill left"
                    : string.Empty;

                UpdateObjective(string.Empty, GetUpdatedCounterAmount(), notificationText);
            }
        }

        string GetUpdatedCounterAmount()
        {
            return m_KillTotal + " / " + KillsToCompleteObjective;
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<EnemyKillEvent>(OnEnemyKilled);
        }
    }
}