# rededepetri

PETRI NET CREATOR
I developed this project to replace create, import, export and simulate petri networks.
It's created to help me and my colleagues in the "Modelling and simulation" class.
I've also created a game in Unity where i use this program to create model and export the game flow to use in the engine.

COMMAND LIST - THIS LIST CAN BE ACCESSED IN-APP TOO.
	help -> Print a command list.
	print -> Print the entire petri net.
	create place [label] -> Creates a new place with desired label.
	create transition [label] -> Creates a new transition with desired label.
	add tokens [place id] [tokenAmount] -> Add [tokenAmount] tokens to a place.
	create arc [weight] [origin id] [target id] -> Creates a new arc with desired origin and target.
	create inhibitor [weight] [origin id] [target id] -> Creates a new inhibitor with desired origin and target.
	create reset [origin id] [target id] -> Creates a new reset arc with desired origin and target.
	execute [iterations] -> Execute the petri net for [iterations] times.
