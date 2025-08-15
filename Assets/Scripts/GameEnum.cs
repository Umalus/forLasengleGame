using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnum {
    public enum eGamePart {
        Invalid = -1,
        Standby,
        Title,
        MainGame,
        Ending,

        PartMax
    }

    public enum eDirectionFour {
        Invalid = -1,
        Up,
        Right,
        Down,
        Left,

        eDirectionMax
    }

    public enum eGamePhase {
        Invalid = -1,
        Exploration,
        Battle,

        PhaseMax,
    }

    public enum eFloorEndReason {
        Invalid = -1,
        Dead,
        Door,

        ReasonMax,
    }
}
