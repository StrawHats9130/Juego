using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    //Public is the accessability modifier other classes can access this class
   public class WorkBench 
    {
       //Class-level variables
       public int classLevelVariabelOne = 3; //Visable to other classes
       private int classLevelVariableTwo = 7; //Only visable to this class (WorkBench)
       private int classLevelVariableThree; //Declaring the variable, without setting a value

       public void Function()
       {
           int functionLevelVariableOne; //limited in scope to this function / method only

           if (classLevelVariabelOne < 5)
           {
               functionLevelVariableOne = 1;
               int innerVariable;
           }

           if (classLevelVariableTwo >= 5)
           {
               functionLevelVariableOne = 3;
               int innerVariable;
           }
       }


       public void Function2()
       {

           int functionLevelVariableOne;  //Limited in scope, can have the same name as the variable on line 18 w/o issue

           if (classLevelVariableTwo < 5) 
           {
               functionLevelVariableOne = 3; 
               int innerVariable;
           }
           if (classLevelVariableTwo >= 5)
           {
               functionLevelVariableOne = 4;
               int innerVariableOne;
           }

       }

       //Example of a simple if statment / evaluation
       public bool IsPlayerStillActiveSimple(int currentHitPoints)
       {
           if (currentHitPoints > 0) //Checking parameter passed in
           {
               return true;
           }
            return false;
       }

       //Example of a complex if statement / evaluations
       //Parenthesis denote order of operations (work from innermost set to the outermost)
       public bool IsPlayerStillActiveComplex(int currentHitPoints, bool hasResurrectionRing)
       {
           if ((currentHitPoints > 0) || ((currentHitPoints ==0) && hasResurrectionRing))
           {
               return true;
           }
           return false;
       }

       public int ComputePlayerLevel(int experiencePoints)
       {
           if (experiencePoints < 100)
           {
               return 1; //Player is level 1
           }
           if (experiencePoints < 250)
           {
               return 2; //Player is level 2
           }
           if (experiencePoints < 300)
           {
               return 3; //Player is level 3
           }
           if (experiencePoints < 1000)
           {
               return 4; //Player is level 4
           }
           return 5; //Player is level 5 (ie maximum level)
       }


       public int ComputePlayerLevelSwitch(int experiencePoints)
       {
           switch (experiencePoints)
           {
               case 1:
                   Console.WriteLine("You are currently at level 1");
                   return 1;
               case 2:
                   Console.WriteLine("You are currently at level 2");
                   return 2;
               case 3:
                   Console.WriteLine("You are currently at level 3");
                   return 3;
               case 4:
                   Console.WriteLine("You are currently at level 4");
                   return 4;
               case 5:
                   Console.WriteLine("You are currently at level 5"+
                                     "\nYou have maxed out for this land");
                   return 5;
               default:
                   Console.WriteLine("You are currently at level 0"+
                       "you need to complete objectives in order to raise your level");
                   return 0;
           }
       }

       public void CalculateAverage()
       {
        //Create and populate the list of numbers
           List<double> values = new List<double>();

           values.Add(1);
           values.Add(5);
           values.Add(21);

           double total = 0;  //Holds value that is addded TO in every itteration
           double counter = 0;  //Holds value that represents total itterations of loop (how many numbers in collection)

           foreach (double value in values)
           {
               total += value; 
               counter = (counter + 1);
           }

           double average = (total / counter);  //Manually calculating average

           double linqAverage = values.Average(); //Using Linq to calculate average (has a special Method to do this)
       }

    }
}
