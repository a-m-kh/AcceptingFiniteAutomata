using System;

namespace AcceptFinitAutomata
{
    using System.Collections.Generic;
    using System;

    namespace node
    {


        class State
        {
            public string name;
            public bool final = false;
            public List<Yal> yals = new List<Yal>();
            public State(string Name)
            {
                name = Name;
            }
        }

        class Yal
        {
            public State now;
            public string language;
            public State next;
        }
        class Program
        {
            static bool find(State now, string line)
            {
                List<bool> final = new List<bool>();
                string status = "";
                List<Yal> F = now.yals.FindAll(a => a.language == "$");
                if (line.Length > 0)
                {
                    status = line[0].ToString();
                }
                List<Yal> yal = new List<Yal>();
                if (now.yals != null)
                {
                    yal = now.yals; ;
                }
                if (now.yals.Count == 0 && line.Length > 0)
                {
                    return false;
                }
                if (line.Length == 0 && now.final)
                {
                    return true;
                }
                if (line.Length == 0 && !now.final)
                {
                    if (F.Count == 0)
                    {
                        return false;
                    }
                }
                List<Yal> fin = now.yals.FindAll(a => (a.language == status || a.language == "$"));
                if (line.Length > 0 && fin.Count == 0)
                {
                    return false;
                }
                int d = 0;
                foreach (Yal y in yal)
                {
                    if (y.language == "$")
                    {
                        final.Add(find(y.next, line));
                    }
                    if (y.language == status)
                    {
                        d++;
                        string NewLine = "";
                        for (int i = 0; i < line.Length; i++)
                        {
                            if (i != 0)
                            {
                                NewLine += line[i];
                            }
                        }
                        final.Add(find(y.next, NewLine));
                    }
                }
                /*if(d == 0 && now.final)
                {                
                    return true;
                }*/
                if (d == 0 && !now.final)
                {
                    if (now.yals.FindAll(a => a.language == "$").Count == 0)
                    {
                        return false;
                    }
                }
                foreach (bool s in final)
                {
                    if (s)
                    {
                        return true;
                    }
                }
                return false;
            }
            static void Main(string[] args)
            {
                string[] states = Console.ReadLine().Split('{', '}')[1].Split(',');
                string[] language = Console.ReadLine().Split('{', '}')[1].Split(',');
                string[] FinalState = Console.ReadLine().Split('{', '}')[1].Split(',');
                int number = int.Parse(Console.ReadLine());
                string[,] yals = new string[number, 3];
                List<State> States = new List<State>();

                foreach (string state in states)
                {
                    State s = new State(state);
                    States.Add(s);
                }
                foreach (string state in FinalState)
                {
                    States.Find(a => a.name == state).final = true;
                }
                for (int i = 0; i < number; i++)
                {
                    string[] yal = Console.ReadLine().Split(',');
                    Yal y = new Yal();
                    y.now = States.Find(a => a.name == yal[0]);
                    y.language = yal[1];
                    y.next = States.Find(a => a.name == yal[2]);
                    States.Find(a => a.name == yal[0]).yals.Add(y);
                }
                string line = Console.ReadLine();
                State x = States.Find(a => a.name == states[0]);
                bool status = find(x, line);
                if (status)
                {
                    Console.WriteLine("Accepted");
                }
                else
                {
                    Console.WriteLine("Rejected");
                }
            }
        }
    }

}
