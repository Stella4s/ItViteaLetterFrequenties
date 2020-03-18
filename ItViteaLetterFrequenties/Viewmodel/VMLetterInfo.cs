using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using ItViteaLetterFrequenties.Model;

namespace ItViteaLetterFrequenties.Viewmodel
{
    public class VMLetterInfo : INotifyPropertyChanged
    {
        private string _TextInput;
        public VMLetterInfo()
        {
            LetterList = new List<LetterInfo>
            {
                new LetterInfo{Letter = 'q', Count = 5},
                new LetterInfo{Letter = 'z', Count = 2},
                new LetterInfo{Letter = 'w', Count = 9}
            };
        }

        public IList<LetterInfo> LetterList { get; set; }

        public string TextInput
        {
            get { return _TextInput; }
            set
            {
                _TextInput = value;
                OnPropertyChanged("TextInput");
            }
        }

        // public ICommand ButtonCommand { get; private set; }

        public ICommand FillListCommand
        {
            get
            {
                return new RelayCommand(GetList);
            }
        }

        #region Methods


        public void GetList()
        {
            if (TextInput != null)
                FillLetterList(TextInput);
            else
                TextInput = "Error.";
        }

        public void FillLetterList(string str)
        {
            var query = str.ToLower().Replace(" ", "").GroupBy(c => c)
                    .Select(g => new { Letter = g.Key, Count = g.Count() })
                    .OrderBy(c => c.Letter).ToList();

            foreach (var item in query)
            {
                LetterList.Add(new LetterInfo { Letter = item.Letter, Count = item.Count });
            }
        }
        #endregion

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
