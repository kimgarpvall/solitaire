using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    public Image pCardGraphic;

    // Back
    public Sprite pBack;

    // Spades
    public Sprite pSpadesAce;
    public Sprite pSpadesTwo;
    public Sprite pSpadesThree;
    public Sprite pSpadesFour;
    public Sprite pSpadesFive;
    public Sprite pSpadesSix;
    public Sprite pSpadesSeven;
    public Sprite pSpadesEight;
    public Sprite pSpadesNine;
    public Sprite pSpadesTen;
    public Sprite pSpadesJack;
    public Sprite pSpadesQueen;
    public Sprite pSpadesKing;

    // Hearts
    public Sprite pHeartsAce;
    public Sprite pHeartsTwo;
    public Sprite pHeartsThree;
    public Sprite pHeartsFour;
    public Sprite pHeartsFive;
    public Sprite pHeartsSix;
    public Sprite pHeartsSeven;
    public Sprite pHeartsEight;
    public Sprite pHeartsNine;
    public Sprite pHeartsTen;
    public Sprite pHeartsJack;
    public Sprite pHeartsQueen;
    public Sprite pHeartsKing;

    // Clubs
    public Sprite pClubsAce;
    public Sprite pClubsTwo;
    public Sprite pClubsThree;
    public Sprite pClubsFour;
    public Sprite pClubsFive;
    public Sprite pClubsSix;
    public Sprite pClubsSeven;
    public Sprite pClubsEight;
    public Sprite pClubsNine;
    public Sprite pClubsTen;
    public Sprite pClubsJack;
    public Sprite pClubsQueen;
    public Sprite pClubsKing;

    // Diamonds
    public Sprite pDiamondsAce;
    public Sprite pDiamondsTwo;
    public Sprite pDiamondsThree;
    public Sprite pDiamondsFour;
    public Sprite pDiamondsFive;
    public Sprite pDiamondsSix;
    public Sprite pDiamondsSeven;
    public Sprite pDiamondsEight;
    public Sprite pDiamondsNine;
    public Sprite pDiamondsTen;
    public Sprite pDiamondsJack;
    public Sprite pDiamondsQueen;
    public Sprite pDiamondsKing;

    private int mId = -1;
    private Sprite mCardFace;

    public void SetId(int cardId)
    {
        mId = cardId;
        mCardFace = GetCardFace();
    }
    public int GetId()
    {
        return mId;
    }

    public void SetIsShowing(bool isShowing)
    {
        pCardGraphic.sprite = isShowing ? mCardFace : pBack;
    }

    private Sprite GetCardFace()
    {
        switch(mId)
        {
            // Spades
            case 0: return pSpadesAce;
            case 1: return pSpadesTwo;
            case 2: return pSpadesThree;
            case 3: return pSpadesFour;
            case 4: return pSpadesFive;
            case 5: return pSpadesSix;
            case 6: return pSpadesSeven;
            case 7: return pSpadesEight;
            case 8: return pSpadesNine;
            case 9: return pSpadesTen;
            case 10: return pSpadesJack;
            case 11: return pSpadesQueen;
            case 12: return pSpadesKing;

            // Hearts
            case 13: return pHeartsAce;
            case 14: return pHeartsTwo;
            case 15: return pHeartsThree;
            case 16: return pHeartsFour;
            case 17: return pHeartsFive;
            case 18: return pHeartsSix;
            case 19: return pHeartsSeven;
            case 20: return pHeartsEight;
            case 21: return pHeartsNine;
            case 22: return pHeartsTen;
            case 23: return pHeartsJack;
            case 24: return pHeartsQueen;
            case 25: return pHeartsKing;

            // Clubs
            case 26: return pClubsAce;
            case 27: return pClubsTwo;
            case 28: return pClubsThree;
            case 29: return pClubsFour;
            case 30: return pClubsFive;
            case 31: return pClubsSix;
            case 32: return pClubsSeven;
            case 33: return pClubsEight;
            case 34: return pClubsNine;
            case 35: return pClubsTen;
            case 36: return pClubsJack;
            case 37: return pClubsQueen;
            case 38: return pClubsKing;

            // Diamonds
            case 39: return pDiamondsAce;
            case 40: return pDiamondsTwo;
            case 41: return pDiamondsThree;
            case 42: return pDiamondsFour;
            case 43: return pDiamondsFive;
            case 44: return pDiamondsSix;
            case 45: return pDiamondsSeven;
            case 46: return pDiamondsEight;
            case 47: return pDiamondsNine;
            case 48: return pDiamondsTen;
            case 49: return pDiamondsJack;
            case 50: return pDiamondsQueen;
            case 51: return pDiamondsKing;

            default:
                Debug.LogAssertion("Could not find card face for id " + mId.ToString());
                return pBack;
        }
    }

    // OnUpdate touch check? How do I do it in penguin drop? Do I have a handler I can reuse?
}
