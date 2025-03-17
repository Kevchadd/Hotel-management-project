namespace FinalProject;
using System;
using System.Linq;
using System.Collections.Generic;
 class Program
    {
        static List<Room> room_list = new List<Room>();

        static void Main(string[] args)
        {
            
    List<Room> roomList = new List<Room>();

  

            Room room1 = new Room("101", 2,null,null);
            Room room2 = new Room("102", 2,null,null);
            Room room3 = new Room("103", 2,null,null);
            Room room4 = new Room("104", 2,null,null);
            Room room5 = new Room("105", 2,null,null);
            Room room6 = new Room("106", 3,null,null);
            Room room7 = new Room("107", 3,null,null);
            Room room8 = new Room("108", 3,null,null);
            Room room9 = new Room("109", 3,null,null);
            Room room10 = new Room("110",4,null,null);

            room_list.Add(room1);
            room_list.Add(room2);
            room_list.Add(room3);
            room_list.Add(room4);
            room_list.Add(room5);
            room_list.Add(room6);
            room_list.Add(room7);
            room_list.Add(room8);
            room_list.Add(room9);
            room_list.Add(room10);
            bool isLoggedIn = false;
              while (!isLoggedIn){

            Console.WriteLine("-------CIDM2315 Final Project: Kevin Chadderton--------");
            Console.WriteLine("---------Welcome to Buff Hotel--------");
            Console.WriteLine("Please input username");
            string inputUsername = Console.ReadLine();
            Console.WriteLine("Please input Password");
            string inputPassword = Console.ReadLine(); 
            string username = "Alice";
            string password = "alice123";

             

            if (inputUsername == username && inputPassword == password)
            {
                Console.WriteLine("Login successfully");
                 bool isRunning = true;
                 isLoggedIn=true;
                  while (isRunning){

                
                Console.WriteLine("**Hello User: Alice**");
                Console.WriteLine("*********************");
                Console.WriteLine("-->Please select: ");
                Console.WriteLine("1.Show Available Room");
                Console.WriteLine("2. Check-In");
                Console.WriteLine("3. Show Reserved Room");
                Console.WriteLine("4. Check Out");
                Console.WriteLine("5. Log out");
                Console.WriteLine("*********************");

                 

                string choice = Console.ReadLine();// Choice list
            if(choice=="1"){
                 show_room(room_list);
            }
            else if(choice=="2"){
                check_in (room_list);
            }
            else if(choice=="3"){
            show_reserv(room_list);            
                }
            else if(choice=="4"){
            check_out(room_list);            
                }
            else if(choice=="5"){
         
                Console.WriteLine("Logout successful.");// quit application
                
                 break;            
                }

                  }
            }
            else
            {
                Console.WriteLine("Wrong username/password.");
            }
        }}

public static void show_room(List<Room> room_list)// show room code
        {
          int room_avail = 0;
            foreach (Room room in room_list)
             if (room.custName==null)
            {room.PrintInfo();
            room_avail ++;
            }
            Console.WriteLine($"---------Number of Available Rooms {room_avail}---------");
        }
public static void check_in(List<Room> room_list)// check in code
{

    Console.WriteLine("-->Input Number of People:");
    int inputPeop = Convert.ToInt32(Console.ReadLine());

    var availableRooms = room_list.Where(room => room.Capacity >= inputPeop && room.IsAvailable && room.custName == null).ToList();

    if (availableRooms.Count == 0)
    {
        Console.WriteLine($"No suitable rooms for {inputPeop} people at this time.");
       
    }
    else
    {
        Console.WriteLine($"Available rooms for {inputPeop} people:");          
        foreach (var room in availableRooms)
        {
            Console.WriteLine($"Room Number {room.Name}; Capacity: {room.Capacity}");                
        }
        Console.WriteLine($"---------Number of Available Rooms {availableRooms.Count}---------");
    

    Console.WriteLine("-->Please select a room number from the available rooms above:");
        string roomNumber = (Console.ReadLine());

       
           
        Room selectedRoom = availableRooms.FirstOrDefault(room => room.Name == roomNumber);

        if (selectedRoom != null)
        {
            // check if the selected room has enough capacity
            if (selectedRoom.Capacity >= inputPeop)
            {
                // Assign the customer name and email to the selected room
                 Console.WriteLine("-->Please input Customer name:");
                string custName = Console.ReadLine();
                Console.WriteLine("-->Please input Customer email:");
                string custEmail = Console.ReadLine();
                
                selectedRoom.custName = custName;
                selectedRoom.custEmail = custEmail;
                selectedRoom.IsAvailable = false; // Mark the room as unavailable ?
                Console.WriteLine($"-->Check-In successful! Customer {custName} is assigned to Room {roomNumber}");
            }
            else
            {
                Console.WriteLine($"Selected room {roomNumber} does not have enough capacity.");
            }
        }
        else
        {
            Console.WriteLine($"Room {roomNumber} not found or does not have enough capacity.");
        }
    }
}
public static void show_reserv(List<Room> room_list){

 Console.WriteLine("--------Reserved Rooms:---------");

    foreach (var room in room_list)
    {
        if (room.custName != null && room.custEmail != null)
        {
            Console.WriteLine($"Room Number: {room.Name}");         
            Console.WriteLine($"Customer Name: {room.custName}");
            Console.WriteLine($"Customer Email: {room.custEmail}");
            Console.WriteLine("---------------");
        }
    }

}

public static void check_out(List<Room> room_list)// check out code
{
    Console.WriteLine("--->Please input room number to check out:");
    string roomNumber = Console.ReadLine();

    Room selectedRoom = room_list.FirstOrDefault(room => room.Name == roomNumber);

    if (selectedRoom != null)
    {
        if (selectedRoom.IsAvailable)
        {
            Console.WriteLine($"Room {roomNumber} is already available.");
        }
        else
        {
            Console.WriteLine($"-->Room:{roomNumber}; Customer Name: {selectedRoom.custName}");
            Console.WriteLine($"-->Please confirm the customer name and input 'y' to continue Check Out or input any key to cancel.");

            string confirm = Console.ReadLine().ToLower();// delete reservation info
            if (confirm == "y")
            {
                Console.WriteLine($"-->Check-Out successful! Customer {selectedRoom.custName} from Room {roomNumber} is checked out.");
                selectedRoom.custName = null;
                selectedRoom.custEmail = null;
                selectedRoom.IsAvailable = true;
                show_reserv(room_list);// show reserved room

            }
            else
            {
                Console.WriteLine("-->Check-Out canceled.");
            }
        }
        }
        else
        {
            Console.WriteLine($"--->Could not find customer record of this room {roomNumber}.");
        }
     }

    }









    class Room// room creation class and print info
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public bool IsAvailable { get; set; }
    public string custName {get; set;}
    public string custEmail {get;set;}

    public Room(string name, int capacity, string custName, string custEmail)
    {
        Name = name;
        Capacity = capacity;
        IsAvailable = true; 
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Room Number {Name}; Capacity: {Capacity}");
    }
    }
