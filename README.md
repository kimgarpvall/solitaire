##Klondike solitaire

  
##Overview
Basic Klondike Solitaire game. You can use this for anything you want, as help for a school project or to build on - I don't care.
  

The main design pattern used is MVC. The project also utilises the command pattern to easily accomplish undo/redo functionality.


##Controls:  
-  **Mouse** to drag n drop cards.  
-  **Left/right arrows** to undo/redo.  
-  **R** to restart.  
-  **Escape** to close


##High level architecture overview
######GameOrchestrator   
- Initialises dependencies  
- Maps commands  
- Contains the update loop  

######TableModel : IReadTableState, IWriteTableState
- Stores the state of the game (each card's position and "is showing" state)
- Interacted with through the two interfaces *IReadTableState* and *IWriteTableState*

######CommandQueue
- Executes *commands*
- Handles *undos* and *redos*

######TableView   
- Creates the card graphics
- Positions the card graphics

######CardView
- Handles the individual card display (which graphic + front/back side)
