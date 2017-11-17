using System;
using System.IO;
using System.Collections.Generic;

namespace app0 {
    class ToDo {
        /*
            ToDo List representation
         */
        private List <string> _object;
        private string _name;
        private string _path;
        public List <string> Object { get; set;}
        public ToDo(){
            /*
                Empty constructor, creates new list.
             */
            this._object = new List<string>();
            this._name = null;
            this._path = null;
        }
        public ToDo(string _n){
            /*
                Creates a ToDo list with name.
             */
            this._object = new List<string>();
            this._name = _n;
            this._path = null;
        }
        public ToDo(List <string> param){
            /*
                Creates a ToDo list from a list of string.
             */
            this._object = param;
            this._name = null;
            this._path = null;
        }
        public void Add(string param){
            /*
                Adds new entry to a list.
             */
            try {
                this._object.Add(param);
            } catch (Exception E){
                Console.WriteLine(E);
                Console.ReadKey();
            }
        }
        public string Printable(){
            /*
                Returns ToDo list in a printable form of a string.
             */
            string _string = null;
            try {
                _string = this._name + Environment.NewLine;
                int i = 1;
                foreach(string s in this._object){
                    _string += Convert.ToString(i)+ ". " + s + Environment.NewLine;
                    i++;
                }
            } catch (Exception E){
                Console.WriteLine(E);
                Console.ReadKey();
            }
            return _string;
        }
        public void Pop(int i){
            /*
                Removes an item from a list.
                Counting from 1.
             */
            try {
                this._object.RemoveAt(i-1);
            } catch (Exception E){
                Console.WriteLine(E);
                Console.ReadKey();
            }
        }

        public void SaveToTxt(){
            /*
                Saves a list to text file.
             */
            try {
                string name = this._name;
                string path = this._path;
                string _object = this.Printable();
                string filename = null;

                if (String.IsNullOrEmpty(name)){
                    filename = "todo.txt";
                }
                else if (String.IsNullOrEmpty(path)){
                    filename = String.Format("{0}.txt", name);
                }
                else {
                    filename = String.Format("{0}.txt", path);
                }
                
                using(StreamWriter saver = new StreamWriter(filename)){
                    saver.WriteLine(_object);
                } 
                Console.WriteLine("Saving succeded.");
            } catch (Exception E){
                Console.WriteLine(E);
                Console.ReadKey();
            }
        }
        
        public void LoadFromTxt(string path){
            /*
                Loads data from text file.
             */
            try{
                if(File.Exists(path)){
                    var lines = File.ReadAllLines(path);
                    this._path = path;
                    this._name = lines[0];
                    for(int i=1; i<lines.Length-1; i++){
                        string [] arr = lines[i].Split();
                        string r = "";
                        for(int j=1; j<arr.Length; j++){
                            r+=arr[j];
                        }
                        this._object.Add(r);
                    }
                }
            } catch (Exception E){
                Console.WriteLine(E);
                Console.ReadKey();
            }

        }
    }
    class Program {
        static void ClearLine(){
            /*
                Clear the current console line.
             */
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
        static void PrintMenu(){
            /*
                Print the main menu.
             */
            Console.Clear();
            string s = Environment.NewLine;
            s+="**********************" + Environment.NewLine;
            s+="*******&&ToDo&&*******" + Environment.NewLine;
            s+="**********************" + Environment.NewLine;
            s+= Environment.NewLine;
            s+="1) Create new list." + Environment.NewLine;
            s+="2) Load existing list." + Environment.NewLine;
            s+="3) Exit." + Environment.NewLine;
            Console.WriteLine(s);
        }
        static void PrintOptionOne(){
            /*
                Print the menu for option one.
             */
            string s = Environment.NewLine;
            s+="a) Add to list." + Environment.NewLine;
            s+="b) Remove from list." + Environment.NewLine;
            s+="c) Back to previous menu." + Environment.NewLine;
            Console.WriteLine(s);
        }
        static void EditList(ToDo todo){
            /*
                List editing loop.
             */
            bool done1 = false;
            while (!done1)
            {
                Console.Clear();
                if (todo != null)
                    Console.WriteLine(todo.Printable());
                PrintOptionOne();
                ConsoleKeyInfo _option = Console.ReadKey();
                switch (_option.Key)
                {
                    case ConsoleKey.A:
                        ClearLine();
                        Console.Write("Add: ");
                        string i = Console.ReadLine();
                        todo.Add(i);
                        break;
                    case ConsoleKey.B:
                        ClearLine();
                        Console.Write("What indeks to remove?: ");
                        string s = Console.ReadLine();
                        todo.Pop(Convert.ToInt32(s));
                        break;
                    case ConsoleKey.C:
                        ClearLine();
                        todo.SaveToTxt();
                        done1 = true;
                        break;
                }
            }
        }
        static void Main(string[] args) {
            /*
                Main method.
             */
            ToDo todo = null; 
            bool done = false; 
            while(!done){
                PrintMenu();
                ConsoleKeyInfo input = Console.ReadKey();
                Console.WriteLine(input.Key);
                switch(input.Key){
                    case ConsoleKey.D1:
                        ClearLine();
                        Console.WriteLine("Enter the name of a list:");
                        string _n = Console.ReadLine();
                        todo = new ToDo(_n);
                        EditList(todo);
                        break;
                    case ConsoleKey.D2:
                        ClearLine();
                        Console.Write("Enter the path: ");
                        string path = Console.ReadLine();
                        todo = new ToDo();
                        todo.LoadFromTxt(path);
                        EditList(todo);
                        break;
                    case ConsoleKey.D3:
                        done = true;
                        break;
                }
            }           
        }
    }
}
