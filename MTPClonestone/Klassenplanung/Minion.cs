using System;

namespace Klassenplanung
{
    class Minion : Card, IOnBoard
    {
        //Fields
        bool hasAttacked;

        //Methods
        void Attack(Card target) { }

        //IOnBoard

        public int AttackValue
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int CurrentAttackValue
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int CurrentHealthValue
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int CurrentMaxHealthValue
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int HealthValue
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int Position
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int RoundsOnBoard
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}