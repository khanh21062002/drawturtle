using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Utils.Common
{

    public class RandomGenerator
    {
        private Random _rnd = new Random();
        private List<string> _names = new List<string>();

        public RandomGenerator()
        {
            _names.Add("Smith");
            _names.Add("Johnson");
            _names.Add("Williams");
            _names.Add("Jones");
            _names.Add("Brown");
            _names.Add("Davis");
            _names.Add("Miller");
            _names.Add("Wilson");
            _names.Add("Moore");
            _names.Add("Taylor");
            _names.Add("Anderson");
            _names.Add("Thomas");
            _names.Add("Jackson");
            _names.Add("White");
            _names.Add("Harris");
            _names.Add("Martin");
            _names.Add("Thompson");
            _names.Add("Garcia");
            _names.Add("Martinez");
            _names.Add("Robinson");
            _names.Add("Clark");
            _names.Add("Rodriguez");
            _names.Add("Lewis");
            _names.Add("Lee");
            _names.Add("Walker");
            _names.Add("Hall");
            _names.Add("Allen");
            _names.Add("Young");
            _names.Add("Hernandez");
            _names.Add("King");
            _names.Add("Wright");
            _names.Add("Lopez");
            _names.Add("Hill");
            _names.Add("Scott");
            _names.Add("Green");
            _names.Add("Adams");
            _names.Add("Baker");
            _names.Add("Gonzalez");
            _names.Add("Nelson");
            _names.Add("Carter");
            _names.Add("Mitchell");
            _names.Add("Perez");
            _names.Add("Roberts");
            _names.Add("Turner");
            _names.Add("Phillips");
            _names.Add("Campbell");
            _names.Add("Parker");
            _names.Add("Evans");
            _names.Add("Edwards");
            _names.Add("Collins");
            _names.Add("Stewart");
            _names.Add("Sanchez");
            _names.Add("Morris");
            _names.Add("Rogers");
            _names.Add("Reed");
            _names.Add("Cook");
            _names.Add("Morgan");
            _names.Add("Bell");
            _names.Add("Murphy");
            _names.Add("Bailey");
            _names.Add("Rivera");
            _names.Add("Cooper");
            _names.Add("Richardson");
            _names.Add("Cox");
            _names.Add("Howard");
            _names.Add("Ward");
            _names.Add("Torres");
            _names.Add("Peterson");
            _names.Add("Gray");
            _names.Add("Ramirez");
            _names.Add("James");
            _names.Add("Watson");
            _names.Add("Brooks");
            _names.Add("Kelly");
            _names.Add("Sanders");
            _names.Add("Price");
            _names.Add("Bennett");
            _names.Add("Wood");
            _names.Add("Barnes");
            _names.Add("Ross");
            _names.Add("Henderson");
            _names.Add("Coleman");
            _names.Add("Jenkins");
            _names.Add("Perry");
            _names.Add("Powell");
            _names.Add("Long");
            _names.Add("Patterson");
            _names.Add("Hughes");
            _names.Add("Flores");
            _names.Add("Washington");
            _names.Add("Butler");
            _names.Add("Simmons");
            _names.Add("Foster");
            _names.Add("Gonzales");
            _names.Add("Bryant");
            _names.Add("Alexander");
            _names.Add("Russell");
            _names.Add("Griffin");
            _names.Add("Diaz");
            _names.Add("Hayes");

            _names.Add("Aiden");
            _names.Add("Jackson");
            _names.Add("Mason");
            _names.Add("Liam");
            _names.Add("Jacob");
            _names.Add("Jayden");
            _names.Add("Ethan");
            _names.Add("Noah");
            _names.Add("Lucas");
            _names.Add("Logan");
            _names.Add("Caleb");
            _names.Add("Caden");
            _names.Add("Jack");
            _names.Add("Ryan");
            _names.Add("Connor");
            _names.Add("Michael");
            _names.Add("Elijah");
            _names.Add("Brayden");
            _names.Add("Benjamin");
            _names.Add("Nicholas");
            _names.Add("Alexander");
            _names.Add("William");
            _names.Add("Matthew");
            _names.Add("James");
            _names.Add("Landon");
            _names.Add("Nathan");
            _names.Add("Dylan");
            _names.Add("Evan");
            _names.Add("Luke");
            _names.Add("Andrew");
            _names.Add("Gabriel");
            _names.Add("Gavin");
            _names.Add("Joshua");
            _names.Add("Owen");
            _names.Add("Daniel");
            _names.Add("Carter");
            _names.Add("Tyler");
            _names.Add("Cameron");
            _names.Add("Christian");
            _names.Add("Wyatt");
            _names.Add("Henry");
            _names.Add("Eli");
            _names.Add("Joseph");
            _names.Add("Max");
            _names.Add("Isaac");
            _names.Add("Samuel");
            _names.Add("Anthony");
            _names.Add("Grayson");
            _names.Add("Zachary");
            _names.Add("David");
            _names.Add("Christopher");
            _names.Add("John");
            _names.Add("Isaiah");
            _names.Add("Levi");
            _names.Add("Jonathan");
            _names.Add("Oliver");
            _names.Add("Chase");
            _names.Add("Cooper");
            _names.Add("Tristan");
            _names.Add("Colton");
            _names.Add("Austin");
            _names.Add("Colin");
            _names.Add("Charlie");
            _names.Add("Dominic");
            _names.Add("Parker");
            _names.Add("Hunter");
            _names.Add("Thomas");
            _names.Add("Alex");
            _names.Add("Ian");
            _names.Add("Jordan");
            _names.Add("Cole");
            _names.Add("Julian");
            _names.Add("Aaron");
            _names.Add("Carson");
            _names.Add("Miles");
            _names.Add("Blake");
            _names.Add("Brody");
            _names.Add("Adam");
            _names.Add("Sebastian");
            _names.Add("Adrian");
            _names.Add("Nolan");
            _names.Add("Sean");
            _names.Add("Riley");
            _names.Add("Bentley");
            _names.Add("Xavier");
            _names.Add("Hayden");
            _names.Add("Jeremiah");
            _names.Add("Jason");
            _names.Add("Jake");
            _names.Add("Asher");
            _names.Add("Micah");
            _names.Add("Jace");
            _names.Add("Brandon");
            _names.Add("Josiah");
            _names.Add("Hudson");
            _names.Add("Nathaniel");
            _names.Add("Bryson");
            _names.Add("Ryder");
            _names.Add("Justin");
            _names.Add("Bryce");

            //—————female

            _names.Add("Sophia");
            _names.Add("Emma");
            _names.Add("Isabella");
            _names.Add("Olivia");
            _names.Add("Ava");
            _names.Add("Lily");
            _names.Add("Chloe");
            _names.Add("Madison");
            _names.Add("Emily");
            _names.Add("Abigail");
            _names.Add("Addison");
            _names.Add("Mia");
            _names.Add("Madelyn");
            _names.Add("Ella");
            _names.Add("Hailey");
            _names.Add("Kaylee");
            _names.Add("Avery");
            _names.Add("Kaitlyn");
            _names.Add("Riley");
            _names.Add("Aubrey");
            _names.Add("Brooklyn");
            _names.Add("Peyton");
            _names.Add("Layla");
            _names.Add("Hannah");
            _names.Add("Charlotte");
            _names.Add("Bella");
            _names.Add("Natalie");
            _names.Add("Sarah");
            _names.Add("Grace");
            _names.Add("Amelia");
            _names.Add("Kylie");
            _names.Add("Arianna");
            _names.Add("Anna");
            _names.Add("Elizabeth");
            _names.Add("Sophie");
            _names.Add("Claire");
            _names.Add("Lila");
            _names.Add("Aaliyah");
            _names.Add("Gabriella");
            _names.Add("Elise");
            _names.Add("Lillian");
            _names.Add("Samantha");
            _names.Add("Makayla");
            _names.Add("Audrey");
            _names.Add("Alyssa");
            _names.Add("Ellie");
            _names.Add("Alexis");
            _names.Add("Isabelle");
            _names.Add("Savannah");
            _names.Add("Evelyn");
            _names.Add("Leah");
            _names.Add("Keira");
            _names.Add("Allison");
            _names.Add("Maya");
            _names.Add("Lucy");
            _names.Add("Sydney");
            _names.Add("Taylor");
            _names.Add("Molly");
            _names.Add("Lauren");
            _names.Add("Harper");
            _names.Add("Scarlett");
            _names.Add("Brianna");
            _names.Add("Victoria");
            _names.Add("Liliana");
            _names.Add("Aria");
            _names.Add("Kayla");
            _names.Add("Annabelle");
            _names.Add("Gianna");
            _names.Add("Kennedy");
            _names.Add("Stella");
            _names.Add("Reagan");
            _names.Add("Julia");
            _names.Add("Bailey");
            _names.Add("Alexandra");
            _names.Add("Jordyn");
            _names.Add("Nora");
            _names.Add("Carolin");
            _names.Add("Mackenzie");
            _names.Add("Jasmine");
            _names.Add("Jocelyn");
            _names.Add("Kendall");
            _names.Add("Morgan");
            _names.Add("Nevaeh");
            _names.Add("Maria");
            _names.Add("Eva");
            _names.Add("Juliana");
            _names.Add("Abby");
            _names.Add("Alexa");
            _names.Add("Summer");
            _names.Add("Brooke");
            _names.Add("Penelope");
            _names.Add("Violet");
            _names.Add("Kate");
            _names.Add("Hadley");
            _names.Add("Ashlyn");
            _names.Add("Sadie");
            _names.Add("Paige");
            _names.Add("Katherine");
            _names.Add("Sienna");
            _names.Add("Piper");
        }

        public string RandomName(int numberOfWords = 2)
        {
            if (numberOfWords < 1)
            {
                numberOfWords = 1;
            }

            string name = string.Empty;
            for (int i = 0; i < numberOfWords; i++)
            {
                if (name != string.Empty)
                {
                    name += " ";
                }
                name += _names.OrderBy(xx => _rnd.Next()).First();
            }
            return name;
        }

        public DateTime RandomDate(int fromYear, int toYear)
        {
            return new DateTime(_rnd.Next(fromYear, toYear), _rnd.Next(1, 12), _rnd.Next(1, 28));
        }

        public int RandomNumber(int from, int to)
        {
            return _rnd.Next(from, to);
        }

        public IEnumerable<T> TakeRandoms<T>(IEnumerable<T> list, int count)
        {
            return list.OrderBy(x => _rnd.Next()).Take(Math.Min(count, list.Count()));
        }
    }

}
