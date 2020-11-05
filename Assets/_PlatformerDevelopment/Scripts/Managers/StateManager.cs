//-----------------------------------------------------------------------
// <copyright file="StateManager.cs" company="PERIGON GAMES">
//     Copyright (c) PERIGON GAMES. 2020 All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace PersonalDevelopment
{
    public enum State
    {
        StartMenu,
        CharacterSelection,
        PreGame,
        Play,
        EndGame
    }

    public interface IStateManager
    {
        event Action<State> OnStateChanged;
        State GetState();

        void SetState(State state);
    }
    
    public class StateManager : IStateManager 
    {
        private static readonly StateManager _instance = new StateManager();
        
        private State _gameState = State.StartMenu;
        public event Action<State> OnStateChanged;

        static StateManager()
        {
        }

        private StateManager()
        {
        }

        public static StateManager Instance => _instance;

        /// <summary>
        /// Sets the state of the application
        /// </summary>
        /// <returns></returns>
        public State GetState()
        {
            return _gameState;
        }

        public void SetState(State state)
        {
            _gameState = state;
            OnStateChanged?.Invoke(state);
        }
    }
}

