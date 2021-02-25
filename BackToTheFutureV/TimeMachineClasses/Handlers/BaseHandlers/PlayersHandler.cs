﻿using BackToTheFutureV.Players;
using System.Windows.Forms;

namespace BackToTheFutureV.TimeMachineClasses.Handlers.BaseHandlers
{
    public class PlayersHandler : Handler
    {
        //Hover Mode
        public WheelAnimationPlayer HoverModeWheels;

        //Fuel
        public Player Refuel;

        //Wormhole
        public WormholeAnimationPlayer Wormhole;

        public PlayersHandler(TimeMachine timeMachine) : base(timeMachine)
        {
            Events.OnWormholeTypeChanged += OnWormholeTypeChanged;
            FusionLibrary.TimeHandler.OnDayNightChange += OnWormholeTypeChanged;

            OnWormholeTypeChanged();
        }

        public void OnWormholeTypeChanged()
        {
            Wormhole?.Dispose();
            Wormhole = new WormholeAnimationPlayer(TimeMachine);
        }

        public override void Dispose()
        {
            //Hover Mode
            HoverModeWheels?.Dispose();

            //Fuel
            Refuel?.Dispose();

            //Wormhole
            Wormhole?.Dispose();
        }

        public override void KeyDown(Keys key)
        {

        }

        public override void Process()
        {

        }

        public override void Stop()
        {

        }
    }
}
