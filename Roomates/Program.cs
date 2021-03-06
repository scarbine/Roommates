using System;
using System.Collections.Generic;
using System.Linq;
using Roomates.Repositories;
using Roomates.Models;

namespace Roomates
{
    class Program
    {
        private const string V1 = @"server=(localdb)\ProjectsV13;database=Roommates;integrated security=true";
        private const string V = V1;

        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = V;

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
            RoomateRepository roommmateRepo = new RoomateRepository(CONNECTION_STRING);
           
            bool runProgram = true;
            while (runProgram)
            {
                string selection = GetMenuSelection();

                switch (selection)
                {
                    case ("Show all rooms"):
                        List<Room> rooms = roomRepo.GetAll();
                        foreach (Room r in rooms)
                        {
                            Console.WriteLine($"{r.Name} has an Id of {r.Id} and a max occupancy of {r.MaxOccupancy}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for room"):
                        Console.Write("Room Id: ");
                        int id = int.Parse(Console.ReadLine());

                        Room room = roomRepo.GetById(id);

                        Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Add a room"):
                        Console.Write("Room name: ");
                        string name = Console.ReadLine();

                        Console.Write("Max occupancy: ");
                        int max = int.Parse(Console.ReadLine());

                        Room roomToAdd = new Room()
                        {
                            Name = name,
                            MaxOccupancy = max
                        };

                        roomRepo.Insert(roomToAdd);

                        Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Add chore"):
                        Console.WriteLine("Chore Name:");
                        string choreName = Console.ReadLine();

                        Chore addChore = new Chore()
                        {
                            Name = choreName,

                        };

                        choreRepo.Insert(addChore);
                        Console.WriteLine("Please press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Show all chores"):
                        List<Chore> chores = choreRepo.GetAll();
                        foreach (Chore c in chores)
                        {
                            Console.WriteLine($"{c.Name} has an Id of {c.Id}");
                        }
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Search for chore"):
                        Console.WriteLine("Chore Id: ");
                        int choreId = int.Parse(Console.ReadLine());

                        Chore chore = choreRepo.GetById(choreId);

                        Console.WriteLine($"{chore.Name} has and Id of {chore.Id}.");
                        Console.WriteLine($"Press any key to continue.");
                        Console.ReadKey();
                        break;

                    case ("Show all roommates"):
                        List<Roomate> roomate = roommmateRepo.GetAll();
                        foreach (Roomate r in roomate)
                        {
                            Console.WriteLine($"{r.FirstName} {r.LastName} is id number {r.Id} and pays {r.RentPortion} of the rent.");

                        }
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Update a Room"):
                        List<Room> roomOptions = roomRepo.GetAll();
                        foreach (Room r in roomOptions)
                        {
                            Console.WriteLine($" {r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                        }

                        Console.WriteLine("Which room woould you like to update?");
                        int selectedRoomId = int.Parse(Console.ReadLine());
                        Room selectedRoom = roomOptions.FirstOrDefault(roomOptions=> roomOptions.Id == selectedRoomId);

                        Console.Write("New Name: ");
                        selectedRoom.Name = Console.ReadLine();

                        Console.Write("New Max Occupancy: ");
                        selectedRoom.MaxOccupancy = int.Parse(Console.ReadLine());

                        roomRepo.Update(selectedRoom);

                        Console.WriteLine("Room has been sucessfully updated");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        
                        break;

                    case ("Delete Room"):
                        List<Room> rOptions = roomRepo.GetAll();
                       foreach ( Room r in rOptions)
                       {
                           Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy : {r.MaxOccupancy}");
                       }
                       Console.Write("Which room would you like to delete?");
                       selectedRoomId = int.Parse(Console.ReadLine());

                       roomRepo.Delete(selectedRoomId);

                       Console.WriteLine("The Room has been sucessfully deleted.");
                       Console.WriteLine("Please press any key to continue...");
                       Console.ReadKey();
                       break;

                    case ("Update a Chore"):
                        List<Chore> cOPtions = choreRepo.GetAll();

                        foreach(Chore c in cOPtions)
                        {
                            Console.WriteLine($"{c.Id} - {c.Name}");
                        }

                        Console.Write("Which chore would you like to update?");
                        int selectedChoreId = int.Parse(Console.ReadLine());
                        Chore selectedChore = cOPtions.FirstOrDefault(c => c.Id == selectedChoreId);

                        Console.Write("New Name: ");
                        selectedChore.Name = Console.ReadLine();

                        choreRepo.Update(selectedChore);

                        Console.WriteLine("Chore has been sucessfully updated");
                        Console.WriteLine("Please press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Delete Chore"):
                        List<Chore> choreOptions = choreRepo.GetAll();

                        foreach (Chore c in choreOptions)
                        {
                            Console.WriteLine($"{c.Id} - {c.Name}");
                        }
                        Console.WriteLine("Which chore would you like to delete?");
                        selectedChoreId = int.Parse(Console.ReadLine());
                        choreRepo.Delete(selectedChoreId);

                        Console.WriteLine("The chore has been deleted.");
                        Console.WriteLine("Please press any ket to continue.");
                        Console.ReadKey();

                        break;

                    case ("Assign a Chore"):
                        List<Chore> allChores = choreRepo.GetAll();
                        List<Roomate> allRoomates = roommmateRepo.GetAll();

                        foreach( Chore c in allChores)
                        {
                            Console.WriteLine($"{c.Id} - {c.Name}");
                        }
                        Console.Write("Which chore would you like to assign?");
                        int choreChoice = int.Parse(Console.ReadLine());

                        foreach(Roomate r in allRoomates)
                        {
                            Console.WriteLine($"{r.Id} - {r.FirstName} {r.LastName}");
                        }
                        Console.Write("Which roomate would you like to assign this chore to?");
                        int chosenRoomate = int.Parse(Console.ReadLine());

                        choreRepo.AssignChore(chosenRoomate, choreChoice);

                        Console.WriteLine("The chore has been assigned!");
                        Console.WriteLine("Please press any key to continue...");
                        Console.ReadKey();

                        break;



                    case ("Exit"):
                        runProgram = false;
                        break;
                }
            }


        }

        static string GetMenuSelection()
        {
            Console.Clear();

            List<string> options = new List<string>()
            {
                "Show all rooms",
                "Search for room",
                "Add a room",
                "Show all chores",
                "Add chore",
                "Search for chore",
                "Show all roommates",
                "Update a Room",
                "Delete Room",
                "Update a Chore",
                "Delete Chore",
                "Assign a Chore",
                "Exit"
            };

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];
                }
                catch (Exception)
                {

                    continue;
                }
            }
        }
    }
}

