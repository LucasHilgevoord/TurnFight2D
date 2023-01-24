using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private BattleData _defaultBattleData;
    private BattleData _battleData;

    [SerializeField] private CharacterData[] _testTeam;

    [Header("Managers")]
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private HealthManager _healthManager;
    [SerializeField] private ProgressionManager _progressionManager;

    [Header("Handlers")]
    [SerializeField] private SpecialHandler specialHandler;

    private void Awake()
    {
        SignalBus.Subscribe<EvPlayerAbility>(OnPlayerAbility);
        SignalBus.Subscribe<EvPlayerTurnFinished>(OnPlayerTurnFinished);
    }

    private void Start()
    {
        // Initialize the battle
        Initialize();
    }

    private void Initialize()
    {
        if (_battleData == null) { _battleData = _defaultBattleData; }

        // Initialize the battle
        _enemyManager.Initialize(_battleData.enemies);
        SignalBus.Subscribe<EvEnemyFocusChanged>(OnEnemyFocusChanged);

        // Initialize the player team
        _playerManager.Initialize(_testTeam);
        
        // Initialize healthbars for each enemy
        _healthManager.Initialize(_enemyManager.Characters.ToArray());
        _healthManager.ChangeFocussedBar(_enemyManager.CurrentFoccussed.Character);
    }

    private void OnEnemyFocusChanged(EvEnemyFocusChanged signal)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Method which handles what happens if a player uses an ability
    /// </summary>
    /// <param name="signal"></param>
    private void OnPlayerAbility(EvPlayerAbility signal)
    {
        UseAbility(_playerManager, signal);
    }

    private void UseAbility(TeamManager team, EvPlayerAbility signal)
    {
        AbilityData ability = signal.ability;

        if (ability.isSpecial)
        {
            // This is a special attack, we need to show the special ability panel
            specialHandler.UpdatePanel(signal.ability.abilityName, signal.caster.Data.battleSprite);
            specialHandler.ShowPanel();
        }
        else
        {
            // This is a normal attack
        }

        // Decide the team which this ability will be used on
        TeamManager enemy = team == _playerManager ? _enemyManager : _playerManager;
        TeamManager friendly = team == _playerManager ? _playerManager : _enemyManager;

        // Apply all ability effects
        foreach (AbilityEffect abilityEffect in ability.abilityEffects)
        {
            Character[] target = GetTarget(friendly, enemy, abilityEffect.effectTarget);
            target = target == null ? new Character[] { signal.caster } : target;

            foreach (Character c in target)
            {
                switch (abilityEffect.effectType)
                {
                    case AbilityType.Damage:
                        c.DamageSelf(abilityEffect.amount);
                        break;
                    case AbilityType.ShieldIncrease:
                        c.AddShield(abilityEffect.amount);
                        break;
                    case AbilityType.Heal:
                        c.HealSelf(abilityEffect.amount);
                        break;
                    default:
                        Debug.LogError("BattleManager: Unknown ability effect type: " + abilityEffect.effectType);
                        break;
                }
            }
        }

        // Apply all status effects
        foreach (StatusEffect statusEffect in ability.statusEffects)
        {

        }
    }

    /// <summary>
    /// Return the target of the ability
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private Character[] GetTarget(TeamManager friendly, TeamManager enemy, AbilityTarget target)
    {
        switch (target)
        {
            default:
            case AbilityTarget.Self:
                return null;
            case AbilityTarget.PlayerTeam:
                return friendly.Characters.ToArray();
            case AbilityTarget.PlayerRandom:
                int randomPlayer = UnityEngine.Random.Range(0, friendly.Characters.Count);
                return new Character[] { friendly.Characters[randomPlayer] };
            case AbilityTarget.EnemyTarget:
                return new Character[] { enemy.Characters[enemy._currentFocusIndex] };
            case AbilityTarget.EnemyTeam:
                return enemy.Characters.ToArray();
            case AbilityTarget.EnemyRandom:
                int randomEnemy = UnityEngine.Random.Range(0, enemy.Characters.Count);
                return new Character[] { enemy.Characters[randomEnemy] };
        }
    }

    private void OnPlayerTurnFinished(EvPlayerTurnFinished signal)
    {
        // Start Enemy turn
        NextTurn();
    }

    /// <summary>
    /// Method to start a new turn for the player
    /// </summary>
    private void NextTurn()
    {
        _progressionManager.NextTurn();
        _playerManager.EnablePanels();
        // Reenable all character panels
    }

    private void NextWave()
    {
        _progressionManager.NextWave();
        // Set new enemies
    }
}
