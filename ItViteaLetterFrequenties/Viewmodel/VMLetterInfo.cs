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
        private LetterInfoList _LetterList;
        public VMLetterInfo()
        {
            LetterList = new LetterInfoList();
        }

        public LetterInfoList LetterList
        {
            get { return _LetterList; }
            set
            {
                _LetterList = value;
                OnPropertyChanged("LetterList");
            }
        }

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
        public ICommand UpdateListItem
        {
            get
            {
                return new RelayCommand(ChangeListItem);
            }
        }

        #region Methods
        /// <summary>
        /// Hard coded mostly for testing if list will update when item is changed.
        /// </summary>
        public void ChangeListItem()
        {
            LetterList[1].Letter = 'p';
        }

        public void GetList()
        {
            if (TextInput != null)
                LetterList.FillLetterList(TextInput);
            else
                TextInput = "Error.";
        }

        /*public void FillLetterList(string str)
        {
            var query = str.ToLower().Replace(" ", "").GroupBy(c => c)
                    .Select(g => new { Letter = g.Key, Count = g.Count() })
                    .OrderBy(c => c.Letter).ToList();

            LetterList = new List<LetterInfo>();
            foreach (var item in query)
            {
                LetterList.Add(new LetterInfo { Letter = item.Letter, Count = item.Count });
            }
        }*/
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
