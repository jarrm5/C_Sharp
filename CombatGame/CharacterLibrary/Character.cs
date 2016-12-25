using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterLibrary
{
    public abstract class Character
    {
        protected Character(string name, CharacterType charType) 
        { 
            Name = name; 
            CharType = charType; 
        }

        public string Name { get; private set; }
        public CharacterType CharType { get; private set; }

        public void Chat(string speech)
        {
            Console.WriteLine(Name + ": " + speech);
        }
        public override string ToString()
        {
            return string.Format("Name: {0}\nType: {1}", Name, CharType);
        }
    }
}
