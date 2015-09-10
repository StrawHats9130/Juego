The Game 1st update 9/6/2015 3:31AM

//Summary

Added a winform Solution to VS called SuperAdventure (GUI).
Added a Project for the logic called Engine (Business Logic).
Created a git repository called 'Juego'
Added SuperAdventure Project to git repo
Commited & Added README.txt


The Game 2nd update 9/6/2015 3:40AM

//Summary

Added 8 labels to the Form & updated their properties to display player stats. 
Added a test button (created click event) 
Tested the program thus far
Commited & updated the README.txt 


The Game 3rd update 9/6/2015 3:58AM

//Summary

Added a Player class to the Engine Project
Added basic properites for the Player object
Commited & Updated the README.txt


The Game 4th update 9/6/2015 4:20AM (shibbie, moment of silence d[*^*]b)

//Summary

Instantiated a Player object in the SuperAdventure Project
Added some default values to the properties  & binded the data to the appropriate labels.
**deleted the test button Click Event from the SuperAdventure.cs
**deleted initialization code from the SuperAdventure.Designer.cs
**deleted the blank button from the form
Ran the program & validated the values were displaying correctly
Commited & updated the README.txt


The Game 5th update 9/6/2015 4:33AM (Grinding it out)

//Summary

Added 6 classes to the Engine. These classes will contain the business logic for the SuperAdventure Solution
-HealingPotion
-Item
-Location
-Monster
-Quest
-Weapon
Commited & updated the README.txt


The Game 6th update 9/6/2015 4:51AM

//Summary

Added the properites for the 6 recently added classes in the Engine Project. Added public accesability modifier to all of them 
commited & updated the README.txt


The Game 7th update 9/6/2015  5:11AM

//Summary

Added ILivingCreature interface to the Engine project. Added LivingCreature class which implements the interface. Set LivingCreature as the Base classe for Monster & Player classes. Removed duplicate properties. Set the Item class as the Super class for the Weapon & HealingPotion classes. Removed duplicate properties. Commited & updated the README.txt


The Game 8th update 9/6/2015 5:30AM

//Summary

Instantiated location object in the SuperAdventure.cs added some sample values. Created default constructors for the Location & Quest classes. Ran the solution & checked that it built. Commited & updated README.txt


The Game 9th update 9/6/2015  6:00AM

//Summary

Modified the following classes (added default constructors) That pass paramaters at instantiation to thier parent (super) classes. Modified the SuperAdventure.cs to implement the changes. Commited & updated the README.txt
-HealingPotion (child)
-Weapon (child) 
-Player (child)
-Monster (child)
-LivingCreature (super)
-Item (super)


The Game 10th update 9/6/2015  6:26AM 

//Summary

Added custom data types to the Location & Quest classes. Modified the constructors to accept the new data types and null values. Tested the modifications in the SuperAdventure.cs. Built solution. Commited & updated the README.txt


The Game 11th update 9/6/2015  7:05AM

//Summary

Added 4 new classes (objects) to the Engine project. QuestCompletionItem, PlayerQuest, LootItem, InventoryItem. Properties to the new classes. Added List collections to the Monster and Player classes to take in instances of LootItem, InventoryItem, and PlayerQuest. Modified Player and Monster classes by adding empty Lists to body of constructor. **note it was necessary to specifiy a default value for the list (empty) in the body since a List unlike the primative data types has no default value.Commited & updated README.txt


The Game 12th update 9/7/2015  12:32AM

//Summary

Added static class called 'World' to Engine project. Added 4 const static readonly Lists of Type Item, Monster, Quest, & Location. Added 24 const ints (10 Item Id's, 3 Monster Id's, 2 Quest Id's, 9 Location Id's. Added default constructor to 'World' class which takes no paramaters, & has 4 methods in the body. PopulateItems(), PopulateMonsters(), PopulateQuests(), PopulateLocations(). Completed PopulateItems() which takes in zero parameters & populates our static readonly 'List<Item> Items' with 7 Items, 2 Weapons, & 1 HealingPotion. Commited & updatfed the README.txt


The Game 13th update 9/7/2015   1:27AM

//Summary

Added PopulateMonsters() to World.cs which returns void & initalizes 3 Monster objects ("rat", "snake", "giant spider"). Each monster has a LootTable (List <LootItem>) with 2 items per monster. Added a static ItemByID() which return an Item if the paramater passed in is == to one of the public const item variables (int) declared in the beginning of the 'World.cs' else it returns null. Commited & updated the README.txt


The Game 14th update 9/7/2015  2:00AM

//Summary

Added PopulateQuests() to World.cs which returns void & initalizes 2 Quest objects / data types (clearAlchemistGarden, clearFarmersField). Each quest has a QuestCompletion property which is a List<QuestCompletionItem>. Items are Added to the list by calling the ItemByID method which returns an Item if the item is ==  to one of the item (int) variables declared int the beginning of the 'World.cs' else it returns null. Uncommented the PopulateQuests() in the body of the World constructor. Built the solution. Commited & updated the README.txt


The Game 15th update 9/7/2015 3:35AM

//Summary

Added implemantation details for the 4th method PopulateLocations(). Initalized 9 locations and assigned their default values. Linked loctions together (explicitly defining thier N,S,E,W relationships). Added two static methods 'MonsterById' & 'QuestById' that mirror the ItemById(). Both accept an int id and if the id passed in is == to one of the monster or quest id variables declared in the beginning of the World.cs a MOnster or Quest object is returned (respectivly). Added each location to the Location collection. Uncommented the PopulateLocations() in the body of the World constructor, built project successfully. Commited & updated the README.txt


The Game 16th update 9/7/2015 4:08PM

//Summary

Updated the UI added the following controls 1 Lable, 2 ComboBoxes, 6 Button controls, 2 RichTextBoxes, 2 DataGridViews. Changed some of the control names and locations on the UI. Built the solution. Commited & updated the README.txt


The Game 17th uodate 9/7/2015 9:50PM - 9/8/2015  10:49AM

//Summary

Created button events for each button control in the UI, added a WorkBench class to the project with a couple functions. Learned that typically function refers to Methods that return no value & method refers to actions that return a value (not a hard fast rule just something to be aware of). The WorkBench class serves no real purpose other than to display a working knowledge of scope & variable lifetime. Commited & updated the README.txt



The Game 18th update 9/8/2015 11:37AM

//Summary

Added two if statements in the WorkBench.cs. One simple (single condition to be evaluated) one complex  (multiple conditions to be evaluated) note the order of presidence. Conditions are evaluated in order from the innermost (parentheses) to the outermost. With || either condition can evaluate true in order for the statement as a whole to evaluate true. With && both conditions must evaluate true in order for the statement as a whole to evaluate true. 



The Game 19th update 9/8/ 2015 2:55PM

//Summary

Added a sample method for possible playerHp calculations to WorkBench.cs. ComputePlayerLevel() is only to serve as an example for executing multiple evaluations in a particular scope. To expand on that example added a second ComputePlayerLevel() and added a switch statement to clean up the code. *note the LACK of a break statement in the switch statement for every case. Since this method returns an int we have to return an integer per case. Return returns the flow of control out of the switch statement (same as break, anything after return statement would be unreachable code). Commited & updated the README.txt



The Game 20th update 9/8/2015 3:47PM

//Summary

Added another sample method to WorkBench.cs called Calculate average. Method overview
-generate a list that contains 3 doubles
-declare two double variables (initalized to 0) 
-one variable to hold the number of itterations required to cycle through list
-one variable to hold the combined total of each item in the list
-manually perform average calculations

-Linq statement that performs these tasks in a single line of code. (sexy)



The Game 21st update 9/8/2015 12:24AM

//Summary

 Began adding code to the Adventure.cs for player movement, & all conditional checks. (healing player checking location requirements, inventory items for quest completion items. The code looks messy and is going to be hard to follow) after code is working it will need to be refactored to follow single responsibility principle. (SOLID)


The Game 22nd update 9/10/2015 2:13PM

//Summary

Finished the playerMovement function. Currently the function meets these business requirements:

If the location has an item required to enter it
If the player does not have the item
Display message
Don’t let the player move here (stop processing the move)
Update the player’s current location
Display location name and description
Show/hide the available movement buttons
Completely heal the player (we assume they rested/healed while moving)
Update hit points display in UI
Does the location have a quest?
If so, does the player already have the quest?
If so, is the quest already completed?
If not, does the player have the items to complete the quest?
If so, complete the quest
Display messages
Remove quest completion items from inventory
Give quest rewards
Mark player’s quest as completed
If not, give the player the quest
Display message
Add quest to player quest list
Is there a monster at the location?
If so,
Display message
Spawn new monster to fight
Display combat comboboxes and buttons
Repopulate comboboxes, in case inventory changed
If not
Hide combat comboboxes and buttons
Refresh the player’s inventory in the UI – in case it changed
Refresh the player’s quest list in the UI – in case it changed
Refresh the cboWeapons ComboBox in the UI
Refresh the cboPotions ComboBox in the UI

The code is however very fragile & comprised of a plethora of nested if statements & foreach loops. Succesfully built the project & updated the README.txt


The Game 23 update  9/10/2015  