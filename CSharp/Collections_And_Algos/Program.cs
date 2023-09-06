using System.Diagnostics;
using System.Net;
using System;
/*using UnityEngine;
using Random = UnityEngine.Random;*/
namespace Collections_And_Algos
{
    public interface IHp
    {
        float hp { get; }
        float hpMax { get; }
        float hpMin { get; }
        //public delegate void OnHpChangedHandler(float value);
        //event OnHpChangedHandler OnHpChanged;

        event Action<float> OnHpChanged;// event 한정자 : +=, -= 기능만 외부에서 접근가능하게 한정함.
        event Action<object, float> OnHpRecovered;
        event Action<object, float> OnHpDepleted;
        event Action OnHpMin;
        event Action OnHpMax;
        //event Func<int> func1;
        //event Predicate<double> pred1;

        void RecoverHP(object subject, float amount);
        void DepleteHP(object subject, float amount);
    }

    public class Character : IHp
    {
        //public int a
        //{
        //    get => _a;
        //    set => _a = value;
        //}
        //private int _a;

        public float hp
        {
            get
            {
                return _hp;
            }
            private set
            {
                if (_hp == value)
                    return;

                if (value > hpMax)
                    value = hpMax;
                else if (value < hpMin)
                    value = hpMin;

                _hp = value;
                OnHpChanged(value);
                if (value == hpMax)
                    OnHpMax();
                else if (value == hpMin)
                    OnHpMin();
            }
        }

        public float hpMax => _hpMax;

        public float hpMin => _hpMin;

        public event Action<float> OnHpChanged;
        public event Action<object, float> OnHpRecovered;
        public event Action<object, float> OnHpDepleted;
        public event Action OnHpMin;
        public event Action OnHpMax;

        private float _hp;
        private float _hpMax;
        private float _hpMin = 0;


        public void DepleteHP(object subject, float amount)
        {
            hp -= amount;
            OnHpDepleted(subject, amount);
        }

        public void RecoverHP(object subject, float amount)
        {
            hp += amount;
            OnHpRecovered(subject, amount);
        }
    }

    public class HPBar
    {
        public string text;

        public void Refresh(float value)
        {
            text = value.ToString();
        }
    }


    class Dummy
    {
        public int D
        {
            get
            {
                return d;
            }
            private set
            {
                d = value;
            }
        }

        public int a = 1;
        public int b;
        public int c;
        int d;

        public int GetD()
        {
            return d;
        }

        public int CalcSum() 
        {
            int result = this.a + this.b + this.c + this.d;
            return result;
        }
    }

    struct Coord 
    {
        public float x;
        public float y;
        public float z;
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Character player = new Character();
            HPBar hpBar = new HPBar();
            HPBar hpBar1 = new HPBar();
            HPBar hpBar2 = new HPBar();
            HPBar hpBar3 = new HPBar();
            //player.OnHpChanged = hpBar.Refresh;
            player.OnHpChanged += hpBar1.Refresh;
            player.OnHpChanged += hpBar2.Refresh;
            player.OnHpChanged += hpBar3.Refresh;
            player.OnHpChanged -= hpBar3.Refresh;
            // GameLogic
            while (true)
            {
                player.hp -= 0.1f;
            }

            List<int> da = new List<int>();
            foreach (var item in da)
            {
                Console.WriteLine(item);
            }

            using (List<int>.Enumerator daEnum = da.GetEnumerator())
            {
                while (daEnum.MoveNext())
                {
                    Console.WriteLine(daEnum.Current);
                }
            }
                
            


            /*
            Dummy dummy1 = new Dummy();
            Dummy dummy2 = new Dummy();
            dummy1.a = 3;
            dummy2.a = 5;

            Console.WriteLine(dummy1.CalcSum());
            Console.WriteLine(dummy2.CalcSum());


            Coord coord = new Coord();
            */
        }
    }
}