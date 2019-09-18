using System;

namespace RedeDePetri
{
    class Program
    {
        static void Main(string[] args)
        {
            Subnet mainNet = new Subnet("Main Net");
            bool running = true;
            Console.WriteLine("Enter a valid command. Enter 'help' for more info.\n");
            while (running)
            {
                string[] opt = Console.ReadLine().Split(' ');
                switch (opt[0])
                {
                    case "quit":
                        running = false;
                        break;
                    case "help":
                        Console.WriteLine("Command list:");
                        Console.WriteLine("help -> Print a command list.");
                        Console.WriteLine("print -> Print the entire petri net.");
                        Console.WriteLine("create place [label] -> Creates a new place with desired label.");
                        Console.WriteLine("create transition [label] -> Creates a new transition with desired label.");
                        Console.WriteLine("add tokens [place id] [tokenAmount] -> Add [tokenAmount] tokens to a place.");
                        Console.WriteLine("create arc [weight] [origin id] [target id] -> Creates a new arc with desired origin and target.");
                        Console.WriteLine("create inhibitor [weight] [origin id] [target id] -> Creates a new inhibitor with desired origin and target.");
                        Console.WriteLine("create reset [origin id] [target id] -> Creates a new reset arc with desired origin and target.");
                        Console.WriteLine("execute [iterations] -> Execute the petri net for [iterations] times.");
                        break;
                    case "create":
                        if(opt.Length == 3) //Place or Transition
                        {
                            if(opt[1] == "place")
                            {
                                Console.WriteLine($"Place created! ID: {mainNet.CreatePlace(opt[2]).id}");
                            }
                            else if(opt[1] == "transition")
                            {
                                Console.WriteLine($"Transition created! ID: {mainNet.CreateTransition(opt[2]).id}");
                            }
                            else
                            {
                                Console.WriteLine("Invalid command!");
                            }
                        }
                        else if(opt.Length == 4)
                        {
                            if(opt[1] == "reset")
                            {
                                string origin = opt[2];
                                string target = opt[3];
                                Arc arc = mainNet.CreateReset(origin, target);
                                if (arc != null)
                                {
                                    Console.WriteLine($"Arc Created!\n");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to create arc!\n");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid command!\n");
                            }
                        }
                        else if(opt.Length == 5) //Arc
                        {
                            string origin = opt[3];
                            string target = opt[4];
                            int weight;
                            if(int.TryParse(opt[2], out weight))
                            {
                                Arc arc = opt[1] == "inhibitor"? mainNet.CreateInhibitor(weight, origin, target) : mainNet.CreateArc(weight, origin, target);
                                if (arc != null)
                                {
                                    Console.WriteLine($"Arc Created!\n");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to create arc!\n");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Origin and Target IDs must be an integer.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid command!");
                        }
                        break;
                    case "add":
                        if(opt.Length == 4)
                        {
                            string placeId = opt[2];
                            int amount;
                            if(int.TryParse(opt[3], out amount))
                            {
                                Place placeToAdd = mainNet.FindPlace(placeId);
                                if(placeToAdd != null && !placeToAdd.isTransition)
                                {
                                    placeToAdd.AddTokens(amount);
                                    Console.WriteLine($"Added {amount} to Place {placeToAdd.id}");
                                }
                                else
                                {
                                    Console.WriteLine("Place not found!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid command!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid command!");
                        }
                        break;
                    case "execute":
                        if(opt.Length == 2)
                        {
                            int aux;
                            if(int.TryParse(opt[1], out aux))
                            {
                                mainNet.Execute(aux);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid command!");
                        }
                        break;
                    case "print":
                        Console.WriteLine(mainNet.Info());
                        break;
                    case "cls":
                        Console.Clear();
                        break;
                    default:
                        break;
                }
            }

        }
    }
}
