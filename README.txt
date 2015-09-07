The Game 1st commit 9/6/2015 3:31AM

//Summary

Added a winform Solution to VS called SuperAdventure (GUI).
Added a Project for the logic called Engine (Business Logic).
Created a git repository called 'Juego'
Added SuperAdventure Project to git repo
Commited & Added README.txt


The Game 2nd commit 9/6/2015 3:40AM

//Summary

Added 8 labels to the Form & updated their properties to display player stats. 
Added a test button (created click event) 
Tested the program thus far
Commited & updated the README.txt 


The Game 3rd commit 9/6/2015 3:58AM

//Summary

Added a Player class to the Engine Project
Added basic properites for the Player object
Commited & Updated the README.txt


The Game 4th commit 9/6/2015 4:20AM (shibbie, moment of silence d[*^*]b)

//Summary

Instantiated a Player object in the SuperAdventure Project
Added some default values to the properties  & binded the data to the appropriate labels.
**deleted the test button Click Event from the SuperAdventure.cs
**deleted initialization code from the SuperAdventure.Designer.cs
**deleted the blank button from the form
Ran the program & validated the values were displaying correctly
Commited & updated the README.txt


The Game 5th commit 9/6/2015 4:33AM (Grinding it out)

//Summary

Added 6 classes to the Engine. These classes will contain the business logic for the SuperAdventure Solution
-HealingPotion
-Item
-Location
-Monster
-Quest
-Weapon
Commited & updated the README.txt


The Game 6th commit 9/6/2015 4:51AM

//Summary

Added the properites for the 6 recently added classes in the Engine Project. Added public accesability modifier to all of them 
commited & updated the README.txt


The Game 7th commit 9/6/2015  5:11AM

//Summary

Added ILivingCreature interface to the Engine project. Added LivingCreature class which implements the interface. Set LivingCreature as the Base classe for Monster & Player classes. Removed duplicate properties. Set the Item class as the Super class for the Weapon & HealingPotion classes. Removed duplicate properties. Commited & updated the README.txt


The Game 8th commit 9/6/2015 5:30AM

//Summary

Instantiated location object in the SuperAdventure.cs added some sample values. Created default constructors for the Location & Quest classes. Ran the solution & checked that it built. Commited & updated README.txt


The Game 9th commit 9/6/2015  6:00AM

//Summary

Modified the following classes (added default constructors) That pass paramaters at instantiation to thier parent (super) classes. Modified the SuperAdventure.cs to implement the changes. Commited & updated the README.txt
-HealingPotion (child)
-Weapon (child) 
-Player (child)
-Monster (child)
-LivingCreature (super)
-Item (super)


The Game 10th commit 9/6/2015  6:26AM 

//Summary

Added custom data types to the Location & Quest classes. Modified the constructors to accept the new data types and null values. Tested the modifications in the SuperAdventure.cs. Built solution. Commited & updated the README.txt


The Game 11th commit 9/6/2015  7:05AM

//Summary

Added 4 new classes (objects) to the Engine project. QuestCompletionItem, PlayerQuest, LootItem, InventoryItem. Properties to the new classes. Added List collections to the Monster and Player classes to take in instances of LootItem, InventoryItem, and PlayerQuest. Modified Player and Monster classes by adding empty Lists to body of constructor. **note it was necessary to specifiy a default value for the list (empty) in the body since a List unlike the primative data types has no default value.Commited & updated README.txt


The Game 12th commit 9/7/2015  12:32AM

//Summary

Added static class called 'World' to Engine project. Added 4 const static readonly Lists of Type Item, Monster, Quest, & Location. Added 24 const ints (10 Item Id's, 3 Monster Id's, 2 Quest Id's, 9 Location Id's. Added default constructor to 'World' class which takes no paramaters, & has 4 methods in the body. PopulateItems(), PopulateMonsters(), PopulateQuests(), PopulateLocations(). Completed PopulateItems() which takes in zero parameters & populates our static readonly 'List<Item> Items' with 7 Items, 2 Weapons, & 1 HealingPotion. commited & updatfed the README.txt


The Game 13th commit 9/7/2015   1:27AM

//Summary

Added PopulateMonsters() to World.cs which returns void & initalizes 3 Monster objects ("rat", "snake", "giant spider"). Each monster has a LootTable (List <LootItem>) with 2 items per monster. Added a static ItemByID() which return an Item if the paramater passed in is == to one of the public const item variables (int) declared in the beginning of the 'World.cs' else it returns null. Commited & updated the README.txt


The Game 14th commit 9/7/2015  2:00AM

//Summary

Added PopulateQuests() to World.cs which returns void & initalizes 2 Quest objects / data types (clearAlchemistGarden, clearFarmersField). Each quest has a QuestCompletion property which is a List<QuestCompletionItem>. Items are Added to the list by calling the ItemByID method which returns an Item if the item is ==  to one of the item (int) variables declared int the beginning of the 'World.cs' else it returns null. Uncommented the PopulateQuests() in the body of the World constructor. Built the solution. Commited & updated the README.txt


The Game 15th commit 9/7/2015 

//Summary

The Game 15th commit