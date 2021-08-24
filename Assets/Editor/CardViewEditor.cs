using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof(CardView))]
public class CardViewEditor : Editor
{
    protected static bool mShowBack = false;
    protected static bool mShowSpades = false;
    protected static bool mShowHearts = false;
    protected static bool mShowClubs = false;
    protected static bool mShowDiamonds = false;

    public override void OnInspectorGUI()
    {
        var cardView = (CardView)target;

        // Active graphic
        cardView.pCardGraphic = (Image)EditorGUILayout.ObjectField("Active", cardView.pCardGraphic, typeof(Image), true);

        // Back
        mShowBack = EditorGUILayout.Foldout(mShowBack, "Back", new GUIStyle(EditorStyles.foldout));
        if(mShowBack)
        {
            cardView.pBack = (Sprite)EditorGUILayout.ObjectField("Back", cardView.pBack, typeof(Sprite), false);
        }

        // Spades
        mShowSpades = EditorGUILayout.Foldout(mShowSpades, "Spades", new GUIStyle(EditorStyles.foldout));
        if (mShowSpades)
        {
            cardView.pSpadesAce = (Sprite)EditorGUILayout.ObjectField("Ace", cardView.pSpadesAce, typeof(Sprite), false);
            cardView.pSpadesTwo = (Sprite)EditorGUILayout.ObjectField("Two", cardView.pSpadesTwo, typeof(Sprite), false);
            cardView.pSpadesThree = (Sprite)EditorGUILayout.ObjectField("Three", cardView.pSpadesThree, typeof(Sprite), false);
            cardView.pSpadesFour = (Sprite)EditorGUILayout.ObjectField("Four", cardView.pSpadesFour, typeof(Sprite), false);
            cardView.pSpadesFive = (Sprite)EditorGUILayout.ObjectField("Five", cardView.pSpadesFive, typeof(Sprite), false);
            cardView.pSpadesSix = (Sprite)EditorGUILayout.ObjectField("Six", cardView.pSpadesSix, typeof(Sprite), false);
            cardView.pSpadesSeven = (Sprite)EditorGUILayout.ObjectField("Seven", cardView.pSpadesSeven, typeof(Sprite), false);
            cardView.pSpadesEight = (Sprite)EditorGUILayout.ObjectField("Eight", cardView.pSpadesEight, typeof(Sprite), false);
            cardView.pSpadesNine = (Sprite)EditorGUILayout.ObjectField("Nine", cardView.pSpadesNine, typeof(Sprite), false);
            cardView.pSpadesTen = (Sprite)EditorGUILayout.ObjectField("Ten", cardView.pSpadesTen, typeof(Sprite), false);
            cardView.pSpadesJack = (Sprite)EditorGUILayout.ObjectField("Jack", cardView.pSpadesJack, typeof(Sprite), false);
            cardView.pSpadesQueen = (Sprite)EditorGUILayout.ObjectField("Queen", cardView.pSpadesQueen, typeof(Sprite), false);
            cardView.pSpadesKing = (Sprite)EditorGUILayout.ObjectField("King", cardView.pSpadesKing, typeof(Sprite), false);
        }

        // Hearts
        mShowHearts = EditorGUILayout.Foldout(mShowHearts, "Hearts", new GUIStyle(EditorStyles.foldout));
        if (mShowHearts)
        {
            cardView.pHeartsAce = (Sprite)EditorGUILayout.ObjectField("Ace", cardView.pHeartsAce, typeof(Sprite), false);
            cardView.pHeartsTwo = (Sprite)EditorGUILayout.ObjectField("Two", cardView.pHeartsTwo, typeof(Sprite), false);
            cardView.pHeartsThree = (Sprite)EditorGUILayout.ObjectField("Three", cardView.pHeartsThree, typeof(Sprite), false);
            cardView.pHeartsFour = (Sprite)EditorGUILayout.ObjectField("Four", cardView.pHeartsFour, typeof(Sprite), false);
            cardView.pHeartsFive = (Sprite)EditorGUILayout.ObjectField("Five", cardView.pHeartsFive, typeof(Sprite), false);
            cardView.pHeartsSix = (Sprite)EditorGUILayout.ObjectField("Six", cardView.pHeartsSix, typeof(Sprite), false);
            cardView.pHeartsSeven = (Sprite)EditorGUILayout.ObjectField("Seven", cardView.pHeartsSeven, typeof(Sprite), false);
            cardView.pHeartsEight = (Sprite)EditorGUILayout.ObjectField("Eight", cardView.pHeartsEight, typeof(Sprite), false);
            cardView.pHeartsNine = (Sprite)EditorGUILayout.ObjectField("Nine", cardView.pHeartsNine, typeof(Sprite), false);
            cardView.pHeartsTen = (Sprite)EditorGUILayout.ObjectField("Ten", cardView.pHeartsTen, typeof(Sprite), false);
            cardView.pHeartsJack = (Sprite)EditorGUILayout.ObjectField("Jack", cardView.pHeartsJack, typeof(Sprite), false);
            cardView.pHeartsQueen = (Sprite)EditorGUILayout.ObjectField("Queen", cardView.pHeartsQueen, typeof(Sprite), false);
            cardView.pHeartsKing = (Sprite)EditorGUILayout.ObjectField("King", cardView.pHeartsKing, typeof(Sprite), false);
        }

        // Clubs
        mShowClubs = EditorGUILayout.Foldout(mShowClubs, "Clubs", new GUIStyle(EditorStyles.foldout));
        if (mShowClubs)
        {
            cardView.pClubsAce = (Sprite)EditorGUILayout.ObjectField("Ace", cardView.pClubsAce, typeof(Sprite), false);
            cardView.pClubsTwo = (Sprite)EditorGUILayout.ObjectField("Two", cardView.pClubsTwo, typeof(Sprite), false);
            cardView.pClubsThree = (Sprite)EditorGUILayout.ObjectField("Three", cardView.pClubsThree, typeof(Sprite), false);
            cardView.pClubsFour = (Sprite)EditorGUILayout.ObjectField("Four", cardView.pClubsFour, typeof(Sprite), false);
            cardView.pClubsFive = (Sprite)EditorGUILayout.ObjectField("Five", cardView.pClubsFive, typeof(Sprite), false);
            cardView.pClubsSix = (Sprite)EditorGUILayout.ObjectField("Six", cardView.pClubsSix, typeof(Sprite), false);
            cardView.pClubsSeven = (Sprite)EditorGUILayout.ObjectField("Seven", cardView.pClubsSeven, typeof(Sprite), false);
            cardView.pClubsEight = (Sprite)EditorGUILayout.ObjectField("Eight", cardView.pClubsEight, typeof(Sprite), false);
            cardView.pClubsNine = (Sprite)EditorGUILayout.ObjectField("Nine", cardView.pClubsNine, typeof(Sprite), false);
            cardView.pClubsTen = (Sprite)EditorGUILayout.ObjectField("Ten", cardView.pClubsTen, typeof(Sprite), false);
            cardView.pClubsJack = (Sprite)EditorGUILayout.ObjectField("Jack", cardView.pClubsJack, typeof(Sprite), false);
            cardView.pClubsQueen = (Sprite)EditorGUILayout.ObjectField("Queen", cardView.pClubsQueen, typeof(Sprite), false);
            cardView.pClubsKing = (Sprite)EditorGUILayout.ObjectField("King", cardView.pClubsKing, typeof(Sprite), false);
        }

        // Diamonds
        mShowDiamonds = EditorGUILayout.Foldout(mShowDiamonds, "Diamonds", new GUIStyle(EditorStyles.foldout));
        if (mShowDiamonds)
        {
            cardView.pDiamondsAce = (Sprite)EditorGUILayout.ObjectField("Ace", cardView.pDiamondsAce, typeof(Sprite), false);
            cardView.pDiamondsTwo = (Sprite)EditorGUILayout.ObjectField("Two", cardView.pDiamondsTwo, typeof(Sprite), false);
            cardView.pDiamondsThree = (Sprite)EditorGUILayout.ObjectField("Three", cardView.pDiamondsThree, typeof(Sprite), false);
            cardView.pDiamondsFour = (Sprite)EditorGUILayout.ObjectField("Four", cardView.pDiamondsFour, typeof(Sprite), false);
            cardView.pDiamondsFive = (Sprite)EditorGUILayout.ObjectField("Five", cardView.pDiamondsFive, typeof(Sprite), false);
            cardView.pDiamondsSix = (Sprite)EditorGUILayout.ObjectField("Six", cardView.pDiamondsSix, typeof(Sprite), false);
            cardView.pDiamondsSeven = (Sprite)EditorGUILayout.ObjectField("Seven", cardView.pDiamondsSeven, typeof(Sprite), false);
            cardView.pDiamondsEight = (Sprite)EditorGUILayout.ObjectField("Eight", cardView.pDiamondsEight, typeof(Sprite), false);
            cardView.pDiamondsNine = (Sprite)EditorGUILayout.ObjectField("Nine", cardView.pDiamondsNine, typeof(Sprite), false);
            cardView.pDiamondsTen = (Sprite)EditorGUILayout.ObjectField("Ten", cardView.pDiamondsTen, typeof(Sprite), false);
            cardView.pDiamondsJack = (Sprite)EditorGUILayout.ObjectField("Jack", cardView.pDiamondsJack, typeof(Sprite), false);
            cardView.pDiamondsQueen = (Sprite)EditorGUILayout.ObjectField("Queen", cardView.pDiamondsQueen, typeof(Sprite), false);
            cardView.pDiamondsKing = (Sprite)EditorGUILayout.ObjectField("King", cardView.pDiamondsKing, typeof(Sprite), false);
        }
    }
}