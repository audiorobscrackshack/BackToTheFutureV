﻿namespace BackToTheFutureV
{
    internal static class InternalEnums
    {
        public static string[] MeleeAttacks = new string[] { "walking_punch", "running_punch", "long_0_punch", "heavy_punch_a", "heavy_punch_b", "heavy_punch_b_var_1", "short_0_punch" };

        internal enum EmptyType
        {
            Hide,
            Off,
            On
        }

        internal static class BTTFVDecors
        {
            public const string TimeMachine = "BTTFV_TimeMachine";
            public const string DestDate1 = "BTTFV_DestDate1";
            public const string DestDate2 = "BTTFV_DestDate2";
            public const string LastDate1 = "BTTFV_LastDate1";
            public const string LastDate2 = "BTTFV_LastDate2";
            public const string WormholeType = "BTTFV_WormholeType";
            public const string TimeCircuitsOn = "BTTFV_TCOn";
            public const string CutsceneMode = "BTTFV_TimeTravelType";
            public const string TorqueMultiplier = "BTTFV_TorqueMultiplier";

            public const string AllowHoverMode = "BTTFV_AllowHoverMode";
            public const string IsInHoverMode = "BTTFV_IsInHoverMode";
            public const string IsHoverBoosting = "BTTFV_IsHoverBoosting";
            public const string IsVerticalBoosting = "BTTFV_IsVerticalBoosting";
            public const string IsHoverLanding = "BTTFV_IsHoverLanding";
            public const string IsWaitForLanding = "BTTFV_IsWaitForLanding";
            public const string IsAltitudeHolding = "BTTFV_IsAltitudeHolding";
        }

        internal enum WaybackStatus
        {
            Idle,
            Recording,
            Playing
        }

        internal enum WaybackPedEvent
        {
            None,
            Walking,
            Running,
            EnteringVehicle,
            ShufflingVehicle,
            LeavingVehicle,
            DrivingVehicle,
            Jump,
            MeleeAttack,
            Climb
        }

        internal enum WaybackVehicleEvent
        {
            None,
            RcHandbrakeOn,
            RcHandbrakeOff,
            TimeTravel
        }

        internal enum GarageStatus
        {
            Idle,
            Busy,
            Opening
        }

        internal enum SparkType
        {
            WHE,
            Left,
            Right
        }

        internal enum ReactorState
        {
            Closed,
            Opened,
            Refueling
        }

        internal enum TCDBackground
        {
            BTTF1, BTTF3, Transparent
        }

        internal enum DriverTaskType
        {
            Off,
            LeaveVehicle,
            ParkAndLeave,
            DriveAround,
            DriveAroundAndTimeTravel,
            TimeTravel
        }

        internal enum RcModes
        {
            FromCarCamera,
            FromPlayerCamera
        }

        internal enum TimeTravelPhase
        {
            Completed = 0,
            OpeningWormhole = 1,
            InTime = 2,
            Reentering = 3
        }

        internal enum ReenterType
        {
            Normal,
            Spawn
        }

        internal enum TimeTravelType
        {
            Cutscene,
            Instant,
            RC
        }

        internal enum MissionType
        {
            None,
            Escape
        }

        internal enum TimeMachineCamera
        {
            Default = -1,
            DestinationDate,
            DriverSeat,
            DigitalSpeedo,
            AnalogSpeedo,
            FrontPassengerWheelLookAtRear,
            TrainApproaching,
            RightSide,
            FrontToBack,
            FrontOnRail,
            FrontToBackRightSide,
            LicensePlate,
            TimeTravelOnTracks,
            DigitalSpeedoTowardsFront,
            RearCarTowardsFront,
            PlateCustom,
            ReactorCustom,
            RimCustom,
            HoodCustom,
            ExhaustCustom,
            HookCustom,
            SuspensionsCustom,
            HoverUnderbodyCustom,
            SpeedoWithDestZoom,
            BulovaSetup
        }

        internal enum WormholeType
        {
            Unknown = -1,
            DMC12,
            BTTF1,
            BTTF2,
            BTTF3
        }

        internal enum ModState
        {
            Off = -1,
            On = 0
        }

        internal enum HookState
        {
            Off,
            OnDoor,
            On,
            Removed,
            Unknown
        }

        internal enum PlateType
        {
            Empty = -1,
            Outatime = 0,
            BTTF2 = 1,
            NOTIME = 2,
            TIMELESS = 3,
            TIMELESS2 = 4,
            DMCFACTORY = 5,
            DMCFACTORY2 = 6
        }

        internal enum ReactorType
        {
            None = -1,
            MrFusion = 0,
            Nuclear = 1
        }

        internal enum ExhaustType
        {
            Stock = -1,
            BTTF = 0,
            None = 1
        }

        internal enum WheelType
        {
            Stock = -1,
            StockInvisible = 210,
            RailroadInvisible = 211,
            RedInvisible = 212,
            Red = 213,
            DMC = 214,
            DMCInvisible = 215
        }

        internal enum SuspensionsType
        {
            Unknown = -1,
            Stock = 0,
            LiftFrontLowerRear = 1,
            LiftFront = 2,
            LiftRear = 3,
            LiftFrontAndRear = 4,
            LowerFrontLiftRear = 5,
            LowerFront = 6,
            LowerRear = 7,
            LowerFrontAndRear = 8
        }

        internal enum HoodType
        {
            Stock = -1,
            H1983 = 0,
            H1981 = 1
        }
    }
}
