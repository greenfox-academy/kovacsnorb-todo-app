﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ToDoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] content;
            var toDos = new List<ToDo>(); 

            if (args.Length == 0)
            {
                PrintUsage();
            }

            if (args.Contains("-l"))
            {
                try
                {
                    content = ReadFile();
                    toDos = FillToDoList(content);
                    WriteToConsole(toDos);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Could not read the file!");
                }
            }
            if (args.Contains("-a"))
            {
                AddNewTask(args[1]);
            }
            if (args.Contains("-c"))
            {
                CheckTask(args[1]);
            }
        }

        public static void PrintUsage()
        {
            Console.WriteLine("\nCommand Line Todo application" +
            "\n=============================" +
            "\n" +
            "\nCommand line arguments:" +
            "\n-l   Lists all the tasks" +
            "\n-a   Adds a new task" +
            "\n-r   Removes a task" +
            "\n-c   Completes a task");
        }

        public static string[] ReadFile(string path = "./todolist.txt")
        {
            return File.ReadAllLines(path);
        }

        public static List<ToDo> FillToDoList(string[] inputArray)
        {
            var toDos = new List<ToDo>();
            for (int i = 0; i < inputArray.Count(); i++)
            {
                string[] taskToArray = inputArray[i].Split(';');
                ToDo todo = new ToDo(taskToArray[0], Convert.ToBoolean(taskToArray[1]), taskToArray[2]);
                toDos.Add(todo);
            }
            return toDos;
        }

        public static void WriteToConsole(List<ToDo> toDoList)
        {
            if (toDoList.Count == 0)
            {
                Console.WriteLine("No todos for today! :) ");
            }
            else
            {
                for (int i = 0; i < toDoList.Count(); i++)
                {
                    Console.WriteLine((i + 1) + toDoList[i].ToString());
                }
            }
        }

        public static void AddNewTask(string newTask, string path = "./todolist.txt")
        {
            using (StreamWriter writer = File.AppendText(path))
            {
                writer.WriteLine(newTask + ";false;" + newTask);
            }
        }

        public static void CheckTask(string finishedTask, string path = "./todolist.txt")
        {
            var list = FillToDoList(ReadFile());
            using (StreamWriter writer = new StreamWriter(path))
            {
                list[int.Parse(finishedTask) - 1].SetStatus(true);

                for (int i = 0; i < list.Count(); i++)
                {
                    writer.WriteLine(list[i].Name + ";" + list[i].IsDone + ";" + list[i].Description);
                }
            }
        }
    }
}

//if (args.Contains("-r"))
//{
//    path = @"./todolist.txt";
//    content = File.ReadAllLines(path);
//    using (StreamWriter writer = new StreamWriter(path))
//    {
//        for (int i = 0; i < content.Count(); i++)
//        {
//            contentDetailed.Add(content[i].Split(';'));
//        }

//        contentDetailed.RemoveAt(int.Parse(args[1]) - 1);

//        for (int i = 0; i < contentDetailed.Count(); i++)
//        {
//            writer.WriteLine(contentDetailed[i][0] + ";" + contentDetailed[i][1] + ";" + contentDetailed[i][2]);
//        }
//    }
//}