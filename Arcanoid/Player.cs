﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Arcanoid
{
  public   class Player: INotifyPropertyChanged
    {
        private int _score;
        private int _lifeCount;

        /// <summary>
        /// Очки
        /// </summary>
        public int Score
        {
            get { return _score; }
            set { if (_score != value) { _score = value; NotifyPropertyChanged(); } }
        } 

        /// <summary>
        /// Количество жизней
        /// </summary>
        public int LifeCount
        {
            get { return _score; }
            set { if (_lifeCount != value) { _lifeCount = value; NotifyPropertyChanged(); } }
        } 

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
