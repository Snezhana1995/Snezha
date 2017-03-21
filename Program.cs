using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wars
{
    class Program
    {
        static void Main(string[] args)
        {
            // Инициализация полномочий по умолчанию
            MiddleEarthCitizen.getPowers();
            
            List<MiddleEarthCitizen> kindArmy = new List<MiddleEarthCitizen>(),
                evilArmy = new List<MiddleEarthCitizen>();

            int i, j = 1, length = 10;   //заполнение армий
            String type = "";
            Random rnd = new Random();

            if (rnd.Next(2) > 0)
                kindArmy.Add(new Wizard("Wizard", new Horse("WizardHorse", rnd.Next(MiddleEarthCitizen.Powers["Horse"].Item1, MiddleEarthCitizen.Powers["Horse"].Item2 + 1))));
            else
                j = 0;

            for (i = j; i < length; i++)
            {
                switch (rnd.Next(4))
                {
                    case 0:
                        type = "Human";
                        kindArmy.Add(new Human($"{type}{i}", rnd.Next(MiddleEarthCitizen.Powers[type].Item1, MiddleEarthCitizen.Powers[type].Item2 + 1)));
                        break;
                    case 1:
                        type = "Rohhirim";
                        kindArmy.Add(new Rohhirim($"{type}{i}", rnd.Next(MiddleEarthCitizen.Powers[type].Item1, MiddleEarthCitizen.Powers[type].Item2 + 1), new Horse($"Horse{i}", rnd.Next(MiddleEarthCitizen.Powers["Horse"].Item1, MiddleEarthCitizen.Powers["Horse"].Item2 + 1))));
                        break;
                    case 2:
                        type = "Elf";
                        kindArmy.Add(new Elf($"{type}{i}", rnd.Next(MiddleEarthCitizen.Powers[type].Item1, MiddleEarthCitizen.Powers[type].Item2 + 1)));
                        break;
                    case 3:
                        type = "WoodenElf";
                        kindArmy.Add(new WoodenElf($"{type}{i}", rnd.Next(MiddleEarthCitizen.Powers[type].Item1, MiddleEarthCitizen.Powers[type].Item2 + 1)));
                        break;
                }
            }

            for (i = 0; i < length; i++)
            {
                switch (rnd.Next(4))
                {
                    case 0:
                        type = "Orc";
                        evilArmy.Add(new Orc($"{type}{i}", rnd.Next(MiddleEarthCitizen.Powers[type].Item1, MiddleEarthCitizen.Powers[type].Item2 + 1), new Orc.Wolf($"Wolf{i}", rnd.Next(MiddleEarthCitizen.Powers["Wolf"].Item1, MiddleEarthCitizen.Powers["Wolf"].Item2 + 1))));
                        break;
                    case 1:
                        type = "UrukHai";
                        evilArmy.Add(new UrukHai($"{type}{i}", rnd.Next(MiddleEarthCitizen.Powers[type].Item1, MiddleEarthCitizen.Powers[type].Item2 + 1)));
                        break;
                    case 2:
                        type = "Troll";
                        evilArmy.Add(new Troll($"{type}{i}", rnd.Next(MiddleEarthCitizen.Powers[type].Item1, MiddleEarthCitizen.Powers[type].Item2 + 1)));
                        break;
                    case 3:
                        type = "Goblin";
                        evilArmy.Add(new Goblin($"{type}{i}", rnd.Next(MiddleEarthCitizen.Powers[type].Item1, MiddleEarthCitizen.Powers[type].Item2 + 1)));
                        break;
                }
            }
            
            GetArmyInfo(kindArmy, evilArmy, "Default situation");
            FirstRound(kindArmy, evilArmy);
            GetArmyInfo(kindArmy, evilArmy, "After 1st Round");
            SecondRound(kindArmy, evilArmy);
            GetArmyInfo(kindArmy, evilArmy, "After 2nd Round");
            ThirdRound(kindArmy, evilArmy);
            GetArmyInfo(kindArmy, evilArmy, "After 3rd Round");
            String winner = kindArmy.Count > 0 ? "Kind" : "Evil";
            Console.WriteLine($"{winner} Army Won!");
            
            Console.ReadKey();
        }
        
        public static void PrintArmy(List<MiddleEarthCitizen> army)
        {
            int i = 1;
            foreach (var item in army)
            {
                Console.WriteLine($"{i}\t{item.ToString()}");
                i++;
            }
        }

        public static void GetArmyInfo(List<MiddleEarthCitizen> kindArmy, List<MiddleEarthCitizen> evilArmy, String message)
        {
            Console.WriteLine(message);
            Console.WriteLine("*** Kind Army ***");
            PrintArmy(kindArmy);
            Console.WriteLine("\n*** Evil Army ***");
            PrintArmy(evilArmy);
            Console.WriteLine("\n");
        }

        public static void FirstRound(List<MiddleEarthCitizen> kindArmy, List<MiddleEarthCitizen> evilArmy)   //первый раунд
        {
            Random rnd = new Random();
            MiddleEarthCitizen kind, evil;

            List<MiddleEarthCitizen> kindCavalry = new List<MiddleEarthCitizen>(),
                evilCavalry = new List<MiddleEarthCitizen>();

            kindCavalry = (from t in kindArmy
                           where (t is Wizard || t is Rohhirim)
                           select t).ToList();
            evilCavalry = (from t in evilArmy
                           where (t is Orc && ((Orc)t).OwnWolf != null)
                           select t).ToList();
            
            while (kindCavalry.Count > 0 && evilCavalry.Count > 0)
            {
                kind = kindCavalry[rnd.Next(kindCavalry.Count)];
                evil = evilCavalry[rnd.Next(evilCavalry.Count)];

                if (rnd.Next(2) < 1) // Добрые бъют
                {
                    FirstRoundKindBeats(ref kind, ref evil);
                    if (evil.Power > 0)
                        FirstRoundEvilBeats(ref kind, ref evil);
                }
                else // Злые бъют
                {
                    FirstRoundEvilBeats(ref kind, ref evil);
                    if (kind.Power > 0)
                        FirstRoundKindBeats(ref kind, ref evil);
                }

                ClearDeath(kindArmy);
                ClearDeath(evilArmy);

                kindCavalry = (from t in kindArmy
                               where (t is Wizard || t is Rohhirim)
                               select t).ToList();
                evilCavalry = (from t in evilArmy
                               where (t is Orc && ((Orc)t).OwnWolf != null)
                               select t).ToList();
            }
        }
        public static void FirstRoundKindBeats(ref MiddleEarthCitizen kind, ref MiddleEarthCitizen evil)
        {
            ((Orc)evil).OwnWolf.Power -= kind.Power;
            if (((Orc)evil).OwnWolf.Power < 0)
            {
                ((Orc)evil).Power += ((Orc)evil).OwnWolf.Power;
                ((Orc)evil).OwnWolf.Power = 0;
            }
        }

        public static void FirstRoundEvilBeats(ref MiddleEarthCitizen kind, ref MiddleEarthCitizen evil) 
        {
            if (kind is Rohhirim)
            {
                ((Rohhirim)kind).OwnHorse.Power -= evil.Power;
                if (((Rohhirim)kind).OwnHorse.Power < 0)
                {
                    ((Rohhirim)kind).Power += ((Rohhirim)kind).OwnHorse.Power;
                    ((Rohhirim)kind).OwnHorse.Power = 0;
                }
            }
            else if (kind is Wizard)
            {
                ((Wizard)kind).OwnHorse.Power -= kind.Power;
                if (((Wizard)kind).OwnHorse.Power < 0)
                {
                    ((Wizard)kind).Power += ((Wizard)kind).OwnHorse.Power;
                    ((Wizard)kind).OwnHorse.Power = 0;
                }
            }
        }

        public static void SecondRound(List<MiddleEarthCitizen> kindArmy, List<MiddleEarthCitizen> evilArmy) //второй раунд
        {
            Random rnd = new Random();
            MiddleEarthCitizen kind, evil;

            List<MiddleEarthCitizen> kindInfantry = new List<MiddleEarthCitizen>(),
                evilInfantry = new List<MiddleEarthCitizen>();

            kindInfantry = (from t in kindArmy
                            where !(t is IFirstStrike)
                            select t).ToList();
            evilInfantry = (from t in evilArmy
                            where (!(t is IFirstStrike) || t is UrukHai)
                            select t).ToList();

            while (kindInfantry.Count > 0 && evilInfantry.Count > 0)
            {
                kind = kindInfantry[rnd.Next(kindInfantry.Count)];
                evil = evilInfantry[rnd.Next(evilInfantry.Count)];

                if (rnd.Next(2) < 1) // Добрые бъют
                {
                    SecondRoundKindBeats(ref kind, ref evil);
                    if (evil.Power > 0)
                        SecondRoundEvilBeats(ref kind, ref evil);
                }
                else // Злые бъют
                {
                    SecondRoundEvilBeats(ref kind, ref evil);
                    if (kind.Power > 0)
                        SecondRoundKindBeats(ref kind, ref evil);
                }

                ClearDeath(kindArmy);
                ClearDeath(evilArmy);

                kindInfantry = (from t in kindArmy
                                where !(t is IFirstStrike)
                                select t).ToList();
                evilInfantry = (from t in evilArmy
                                where (!(t is IFirstStrike) || t is UrukHai)
                                select t).ToList();
            }
        }

        public static void SecondRoundKindBeats(ref MiddleEarthCitizen kind, ref MiddleEarthCitizen evil) 
        {
            evil.Power -= kind.Power;
        }

        public static void SecondRoundEvilBeats(ref MiddleEarthCitizen kind, ref MiddleEarthCitizen evil)
        {
            kind.Power -= evil.Power;
        }

        public static void ThirdRound(List<MiddleEarthCitizen> kindArmy, List<MiddleEarthCitizen> evilArmy) //третий раунд
        {
            Random rnd = new Random();
            MiddleEarthCitizen kind, evil;

            while (kindArmy.Count > 0 && evilArmy.Count > 0)
            {
                kind = kindArmy[rnd.Next(kindArmy.Count)];
                evil = evilArmy[rnd.Next(evilArmy.Count)];

                if (((kind is IFirstStrike && !(evil is Orc))) || rnd.Next(2) < 1) // Добрые бъют
                {
                    ThirdRoundKindBeats(ref kind, ref evil);
                    if (kind.Power > 0)
                        ThirdRoundEvilBeats(ref kind, ref evil);
                }
                else // Злые бъют
                {
                    ThirdRoundEvilBeats(ref kind, ref evil);
                    if (kind.Power > 0)
                        ThirdRoundKindBeats(ref kind, ref evil);
                }

                ClearDeath(kindArmy);  //чистим
                ClearDeath(evilArmy);
            }
        }

        public static void ThirdRoundKindBeats(ref MiddleEarthCitizen kind, ref MiddleEarthCitizen evil) 
        {
            if (evil is Orc && ((Orc)evil).OwnWolf != null)
                FirstRoundKindBeats(ref kind, ref evil);
            else
                evil.Power -= kind.Power;
        }

        public static void ThirdRoundEvilBeats(ref MiddleEarthCitizen kind, ref MiddleEarthCitizen evil)
        {
            if (kind is Wizard || kind is Rohhirim)
                FirstRoundEvilBeats(ref kind, ref evil);
            else
                kind.Power -= evil.Power;
        }
        
        public static void ClearDeath(List<MiddleEarthCitizen> army) // метод чистки убитых
        {
            for (int i = 0; i < army.Count; i++)
            {
                if (army[i].Power <= 0)
                {
                    army.Remove(army[i]);
                    break;
                }
            }
        }
    }
}
