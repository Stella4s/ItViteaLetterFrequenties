using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ItViteaLetterFrequenties.Model
{
    public class LetterInfo : INotifyPropertyChanged
    {
        private Char _Letter;
        private int _Count;
        private double _Frequency;
        private bool _IsLetter;
        public double Frequency
        {
            get { return _Frequency; ; }
            set
            {
                _Frequency = value;
                OnPropertyChanged("Frequency");
            }
        }
        public int Count
        {
            get { return _Count; }
            set
            {
                _Count = value;
                OnPropertyChanged("Count");
            }
        }
        public Char Letter
        {
            get { return _Letter; }
            set
            {
                _Letter = value;
                OnPropertyChanged("Letter");
            }
        }
        public bool IsLetter
        {
            get
            {
                if (Char.IsLetter(_Letter))
                    return true;
                else
                    return false;
            }
        }

        #region INotifyPropertyChanged Members  
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
