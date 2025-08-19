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


    public enum eFloorEndReason {
        Invalid = -1,
        Dead,
        Door,

        ReasonMax,
    }

    public enum eGamePhase {
        Invalid = -1,
        Field,
        Battle,

        PhaseMax,
    }
}
