using System;
using System.Collections.Generic;
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

    }
}
