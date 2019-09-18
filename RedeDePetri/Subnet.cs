using System;
using System.Collections.Generic;
using System.Text;

namespace RedeDePetri
{
    class Subnet
    {
        public static int nextId;
        public static int GetNextID()
        {
            return nextId++;
        }


        public int id;
        public string label;
        public Dictionary<int, Place> placesTransitions;
        public List<Arc> arcs;

        public Subnet(string label = "")
        {
            this.id = Subnet.GetNextID();
            this.label = label;
            placesTransitions = new Dictionary<int, Place>();
            arcs = new List<Arc>();
        }

        public Place CreatePlace(string label = "")
        {
            Place newPlace = new Place(label, false);
            placesTransitions[newPlace.id] = newPlace;
            return newPlace;
        }

        public Place CreateTransition(string label = "")
        {
            Place newPlace = new Place(label, true);
            placesTransitions[newPlace.id] = newPlace;
            return newPlace;
        }

        public Arc CreateArc(int weight, string origin, string target)
        {
            Arc newArc = null;
            Place originPlace = FindPlace(origin);
            Place targetPlace = FindPlace(target);
            if(originPlace != null && targetPlace != null)
            {
                newArc = new Arc(weight > 0? weight : 1, originPlace, targetPlace, false, false);
                arcs.Add(newArc);
            }
            return newArc;
        }

        public Arc CreateReset(string origin, string target)
        {
            Arc newArc = null;
            Place originPlace = FindPlace(origin);
            Place targetPlace = FindPlace(target);
            if (originPlace != null && targetPlace != null)
            {
                newArc = new Arc(1, originPlace, targetPlace, false, true);
                arcs.Add(newArc);
            }
            return newArc;
        }

        public Arc CreateInhibitor(int weight, string origin, string target)
        {
            Arc newArc = null;
            Place originPlace = FindPlace(origin);
            Place targetPlace = FindPlace(target);
            if (originPlace != null && targetPlace != null)
            {
                newArc = new Arc(weight > 0? weight : 1, originPlace, targetPlace, true, false);
                arcs.Add(newArc);
            }
            return newArc;
        }

        public Place FindPlace(int id)
        {
            Place place = null;
            if (placesTransitions.ContainsKey(id))
            {
                place = placesTransitions[id];
            }
            return place;
        }

        public Place FindPlace(string nameOrId)
        {
            Place place = null;
            int id;
            if(int.TryParse(nameOrId, out id))
            {
                if (placesTransitions.ContainsKey(id))
                {
                    place = placesTransitions[id];
                    return place;
                }
            }
            if(place == null)
            {
                foreach (KeyValuePair<int, Place> place1 in placesTransitions)
                {
                    if(place1.Value.label == nameOrId)
                    {
                        place = place1.Value;
                        return place;
                    }
                }
            }
            return place;
        }

        public string Info()
        {
            string result = $"Subnet {this.label}\n";
            result += "_______________Places / Transitions_______________\n";
            foreach (KeyValuePair<int, Place> pt in placesTransitions)
            {
                result += pt.Value.Info();
            }
            result += "_______________________Arcs_______________________\n";
            foreach (Arc arc in arcs)
            {
                result += arc.Info();
            }
            return result;
        }

        public void Execute(int count)
        {
            for (int j = 0; j < count; j++)
            {
                List<int> enabledTransitions = new List<int>();
                foreach (KeyValuePair<int, Place> pt in placesTransitions)
                {
                    if (pt.Value.isTransition)
                    {
                        enabledTransitions.Add(pt.Value.id);
                        foreach (Arc arc in arcs)
                        {
                            if (arc.target == pt.Value && !arc.CheckEnabled())
                            {
                                enabledTransitions.Remove(pt.Value.id);
                                break;
                            }
                        }
                    }
                }
                foreach (int i in enabledTransitions)
                {
                    foreach (Arc arc in arcs)
                    {
                        if ((arc.target.id == i || arc.origin.id == i) && arc.CheckEnabled())
                        {
                            arc.Execute();
                        }
                    }
                }
            }          
        }
    }
}
