using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMasterUtility{
   public static Entity_CharacterStatus.Param GetCharacterMaster(int _ID) {
        var characterMasterList = MasterDataManager.characterStatus[0];

        for(int i = 0,max = characterMasterList.Count;i < max; i++) {
            if (characterMasterList[i].ID != _ID) continue;
                
            return characterMasterList[_ID];
        }
        return null;
   }
}
